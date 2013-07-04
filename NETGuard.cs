using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

using System.Net.NetworkInformation;    // PingReply
using System.Diagnostics;               // ProcessStartInfo
using System.Net;                       // HttpWebRequest
using System.ServiceProcess;            // Für Diensteüberprüfung
using System.Net.Mail;                  // Mail senden

using NETGuard.Properties;             // Usersettings verwalten

using System.Management;                // WMI Abfragen (RAMCheck, HDDCheck)


// Damit die POP-UPs funktionieren muss Remotedesktop enabled und AllowRemoteRPC auf 1 gesetzt werden
// http://technet.microsoft.com/de-de/library/ff710472(v=ws.10).aspx


namespace XMLConfigSaver
{
    public partial class IPChecker : Form
    {
        public IPChecker()
        {
            InitializeComponent();

            this.Text = "NETGuard by Wolfgang Ruthner, Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        String XMLHostKonfig = "hosts.xml";

        BindingList<CheckPoint> checkpoint = new BindingList<CheckPoint>();
        int TroubleCounter;
        Boolean TotalAusfall = false;
        Boolean SentToTray = false;
        string Computername = System.Windows.Forms.SystemInformation.ComputerName.ToString();   // Rechnernamen ermitteln

        Settings setting = new Settings();

        private void Form1_Load(object sender, EventArgs e)
        {
            var checkpoint_in = new BindingList<CheckPoint>();
            checkpoint_in = DeserializeToList<CheckPoint>(XMLHostKonfig);

            // Linq Sortierung http://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b
            var sortedCheckpoints =
                from cp in checkpoint_in
                orderby cp.IPAdresse
                select cp;

            foreach (var r in sortedCheckpoints)
                checkpoint.Add(r);

            daten_config.DataSource = checkpoint;
            daten_config.BackgroundColor = Color.LightGreen;
            // Letzten 3 Spalten auf "Read-Only" setzen
            daten_config.Columns[4].ReadOnly = true;
            daten_config.Columns[5].Visible = false; // Spalte ausblenden
            daten_config.Columns[5].ReadOnly = true;
            daten_config.Columns[8].Visible = false;

            // <userSettings> (PopUpClients, ...) einlesen
            string PopUpClients = setting.PopUpClients;
            textBox_PopUpClients.Text = PopUpClients;
            textBox_EMailClients.Text = setting.EMailClients;
            textBox_SMTPServer.Text = setting.SMTPServer;
            textBox_SMTPPort.Text = setting.SMTPPort;
            textBox_SMTPFrom.Text = setting.EMailFrom;
            textBox_SMSNummern.Text = setting.SMSNummern;
            textBox_SMSGateway.Text = setting.SMSGateway;
            textBox_SMSBenutzerID.Text = setting.SMSID;
            textBox_SMSPasswort.Text = setting.SMSPasswort;
            
            int Intervall = Convert.ToInt32(setting.Intervall);
            textBox_Intervall.Text = setting.Intervall;
            timer1.Interval = Intervall * 1000;

            checkBox_StartMinimized.Checked = setting.StartMinimized;
            if (setting.StartMinimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
                this.notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(6000);
            }

            // Bei Programmstart gleich mal Testen
            CheckLoop();
        }


        //
        // http://www.mycsharp.de/wbb2/thread.php?postid=3608336
        // http://www.codeproject.com/Articles/18688/INotifyPropertyChanged-and-Beyond-Part-I
        //
        public class CheckPoint : INotifyPropertyChanged
        {
            
            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged( String propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            private string _Bezeichnung;
            private string _IPAdresse;
            private string _LastResult;
            private string _PrevResult1;
            private string _PrevResult2;
            private string _PrevResult3;
            private int _CheckAusfall;
            private string _LastWarn;
            private string _Art;

            public bool Inaktiv { get; set; }

            [DisplayName("Rechnerbezeichnung:")]
            [XmlElement("Bezeichnung")]
            public string Bezeichnung
            {
                get { return _Bezeichnung; }
                set 
                { 
                    _Bezeichnung = value;
                    this.NotifyPropertyChanged("Bezeichnung");
                }
            }

            [DisplayName("IP-Adresse:")]
            [XmlElement("IPAdresse")]
            public string IPAdresse
            {
                get { return _IPAdresse; }
                set 
                { 
                    _IPAdresse = value;
                    this.NotifyPropertyChanged("IPAdresse");
                }
            }

            [DisplayName("Art des Checks:")]
            public String Art
            {
                get
                {
                    if (_Art == "") _Art = "IP";
                    return _Art;
                }
                set
                {
                    _Art = value;
                    this.NotifyPropertyChanged("Art");
                }
            }

            [DisplayName("Letztes Ergebnis:")]
            public string LastResult    // Letztes Ergebnis reinschreiben, alle anderen verschieben
            {
                get { return _LastResult; }
                set
                {
                    _PrevResult3 = _PrevResult2;
                    _PrevResult2 = _PrevResult1;
                    _PrevResult1 = _LastResult;
                    _LastResult = value;
                }
            }

            [DisplayName("Ausfallscheck / Mittelwert:")]
            public int CheckAusfall
            {
                get {
                    if (_LastResult == "Warnung" && _PrevResult1 == "Warnung" && _PrevResult2 == "Warnung" && _PrevResult3 == "Warnung") return 99999; // Knoten länger ausgefallen
                    else if (_LastResult == "Warnung" || _PrevResult1 == "Warnung" || _PrevResult2 == "Warnung" || _PrevResult3 == "Warnung") return 88888; // Zumindest ein Warnung
                    else
                    {   // Durchschnittswert
                        return Convert.ToInt32((Convert.ToInt32(_LastResult) + Convert.ToInt32(_PrevResult1) + Convert.ToInt32(_PrevResult2) + Convert.ToInt32(_PrevResult3)) / 4.0);
                    }
                }
                set
                {
                    _CheckAusfall = value;
                }
            }


            [DisplayName("E-Mail:")]
            public bool EMail { get; set; }

            [DisplayName("SMS:")]
            public bool SMS { get; set; }

            public int LetzterStatus  { get; set; }

            [DisplayName("Ausfall:")]
            public DateTime LetzterAusfall { get; set; }

            [DisplayName("Minuten:")]
            public double AusfallMinuten { get; set; }
             
/*
            public CheckPoint(string Bezeichnung, string IPAdresse)
            {
                _Bezeichnung = Bezeichnung;
                _IPAdresse = IPAdresse;
            }

 */
            public CheckPoint() { }
        }

        private class CheckResult
        {
            public int Wert { get; set; }
            public string Text { get; set; }
        }



        public void CheckLoop()
        {
            int ZeilenCounter = 0;
            TroubleCounter = 0;
            TotalAusfall = false;
            string _Art = "IP";
            string Servicename = "";

            CheckResult checkResult = new CheckResult();


            foreach (CheckPoint cp in checkpoint)
            {

                try
                {

                    if (cp.IPAdresse.Length > 3 && !cp.Inaktiv) 
                    {
                        string result = "";
                        int ausfall = 0;
                        string substring;

                        try
                        {
                            substring = cp.Art.Substring(0, 4);
                        }
                        catch
                        {
                            substring = "";
                        }
                                               
                        
                        if ( substring.ToUpper() == "HTML")
                        {
                            string[] options = cp.Art.Split(';');
                            result = htmlcheck(cp.IPAdresse, options[1]);   // Retourniert "Warnung" falls Text nicht gefunden wird, andernfalls "0"
                            _Art = "HTML";
                        }
                        else if (substring.ToUpper() == "SRVC")
                        {
                            string[] options = cp.Art.Split(';');
                            Servicename = options[1];
                            bool running = DoesServiceRun(Servicename, cp.Bezeichnung);
                            if (!running) result = "Warnung"; // Dienst läuft nicht
                            else result = "0";
                            _Art = "DIENST";
                        }
                        else if (substring.ToUpper() == "FRAM") //FreeRAM
                        {
                            string[] options = cp.Art.Split(';');
                            string free_RAM = options[1];
                            checkResult = RAMCheck(cp.Bezeichnung);
                            if (checkResult.Wert < Convert.ToInt32(free_RAM) ) result = "Warnung"; // RAM < free_RAM Vorgabe
                            else result = checkResult.Wert.ToString();
                            checkResult.Text = ", freier RAM kleiner " + free_RAM + "MB (" + checkResult.Wert.ToString() + "MB)";
                            _Art = "FRAM";
                        }
                        else if (substring.ToUpper() == "FHDD") // Free Harddisk
                        {
                            string[] options = cp.Art.Split(';');
                            string HDDstring = options[1];
                            string[] HDDOptions = HDDstring.Split(':');
                            string HDDName = HDDOptions[0];
                            string HDDLimit = HDDOptions[1];
                            checkResult = HDDCheck(cp.Bezeichnung, HDDName);
                            if (checkResult.Wert < Convert.ToInt32(HDDLimit)) result = "Warnung"; // RAM < free_RAM Vorgabe
                            else result = checkResult.Wert.ToString();
                            checkResult.Text = ", freier Platz auf " + HDDName + ": kleiner " + HDDLimit + "GB (" + checkResult.Wert.ToString() + "GB)";
                            _Art = "FHDD";
                        }

                        else
                        {
                            // IPCheck durchführen (Anpingen)
                            result = pingcheck(cp.IPAdresse);
                            _Art = "IP";
                        }


                        cp.LastResult = result;
                        //if (result == "Warnung") cp.LastResult += " ( " + checkResult.Wert + " ) ";
                        ausfall = cp.CheckAusfall;
                        cp.CheckAusfall = ausfall;
                        if (_Art == "IP") TroubleCounter += ausfall;


                        // Zeilen Hintergrund einfärben
                        if (ausfall < 88888 && (_Art == "HTML" || _Art == "DIENST" || _Art == "FRAM" || _Art == "FHDD" || _Art == "SRVC")) daten_config.Rows[ZeilenCounter].DefaultCellStyle.BackColor = Color.LightBlue;
                        else if (ausfall < 5) daten_config.Rows[ZeilenCounter].DefaultCellStyle.BackColor = Color.LightGreen;
                        else if (ausfall < 100) daten_config.Rows[ZeilenCounter].DefaultCellStyle.BackColor = Color.GreenYellow;
                        else if (ausfall < 200) daten_config.Rows[ZeilenCounter].DefaultCellStyle.BackColor = Color.Khaki;
                        else if (ausfall < 88888) daten_config.Rows[ZeilenCounter].DefaultCellStyle.BackColor = Color.Yellow;
                        else if (ausfall == 88888) daten_config.Rows[ZeilenCounter].DefaultCellStyle.BackColor = Color.Orange;
                        else if (ausfall == 99999) daten_config.Rows[ZeilenCounter].DefaultCellStyle.BackColor = Color.OrangeRed;
                       
                        //ZeilenCounter++;

                        if ((ausfall == 99999) && (cp.LastResult == "Warnung") && cp.LetzterStatus < 99999) // neuer Totalausfall 
                        {

                            // Programm in den Vordergrund holen
                            this.Show();
                            this.WindowState = FormWindowState.Normal;
                            this.ShowInTaskbar = true;
                            notifyIcon1.Visible = false;

                            TotalAusfall = true;
                            //string aktuelleWarnung = cp.LastWarn;   // Wann wurde zuletzt gewarnt?
                            //cp.LastWarn = cp.LastWarn;  // Letzte Warnungszeit auslesen und überprüfen, ob neu gewarnt werden soll
                            //string letzteWarnung = cp.LastWarn; // Hat die Zuweisung ergeben, dass erneut gewarnt werden soll? -> Neue Zeit

                            cp.LetzterAusfall = DateTime.Now;

                                // Warnung aussenden ...
                                string Art = "IP";
                                tabControl_Rechner.SelectedIndex = 0;   // Tabpage mit den Rechnern in den Vordergrund holen (falls eventuell andere aktiv)
                                try
                                {
                                    if (cp.Art.Substring(0, 4).ToUpper() == "HTML") Art = "HTML";
                                    else if (cp.Art.Substring(0, 4).ToUpper() == "SRVC") Art = "Service";
                                    else if (cp.Art.Substring(0, 4).ToUpper() == "FRAM") Art = "Free RAM";
                                    else if (cp.Art.Substring(0, 4).ToUpper() == "FHDD") Art = "HD Speicher";
                                }
                                catch { }
                                if (Art == "Free RAM" || Art == "HD Speicher")
                                {
                                    WarnLoop("DOWN", Computername, checkResult.Text, cp.IPAdresse, Art);
                                    if (cp.EMail) Infomail("DOWN", Computername, checkResult.Text, cp.IPAdresse, Art, 0); 
                                }
                                else if (Art != "Service")
                                {
                                    WarnLoop("DOWN", Computername, cp.Bezeichnung, cp.IPAdresse, Art);
                                    if (cp.EMail) Infomail("DOWN", Computername, cp.Bezeichnung, cp.IPAdresse, Art, 0);
                                }
                                else
                                {
                                    WarnLoop("DOWN", Computername, Servicename, cp.IPAdresse, Art);
                                    if (cp.EMail) Infomail("DOWN", Computername, Servicename, cp.IPAdresse, Art, 0);
                                }

                        }

                        if (ausfall == 99999) cp.AusfallMinuten = Math.Round(DateTime.Now.Subtract(cp.LetzterAusfall).TotalMinutes,2);
                        //else cp.AusfallMinuten = 0;


                        // Es bestand zuvor ein Alarm, der behoben wurde => Informieren dass Device/Service wieder OK
                        if ((cp.LetzterStatus != ausfall) && (ausfall < 99999) && (cp.LetzterStatus == 99999))
                        {
                            
                            string Art = "IP";
                            try
                            {
                                if (cp.Art.Substring(0, 4).ToUpper() == "HTML") Art = "HTML";
                                else if (cp.Art.Substring(0, 4).ToUpper() == "SRVC") Art = "Service";
                                else if (cp.Art.Substring(0, 4).ToUpper() == "FRAM") Art = "Free RAM";
                                else if (cp.Art.Substring(0, 4).ToUpper() == "FHDD") Art = "HD Speicher";
                            }
                            catch { }
                            if (Art == "Free RAM" || Art == "HD Speicher")
                            {
                                WarnLoop("UP", Computername, checkResult.Text, cp.IPAdresse, Art);
                                if (cp.EMail) Infomail("UP", Computername, checkResult.Text, cp.IPAdresse, Art, cp.AusfallMinuten);
                            }
                            else if (Art != "Service")
                            {
                                WarnLoop("UP", Computername, cp.Bezeichnung, cp.IPAdresse, Art);
                                if (cp.EMail) Infomail("UP", Computername, cp.Bezeichnung, cp.IPAdresse, Art, cp.AusfallMinuten);
                            }
                            else
                            {
                                WarnLoop("UP", Computername, Servicename, cp.IPAdresse, Art);
                                if (cp.EMail) Infomail("UP", Computername, Servicename, cp.IPAdresse, Art, cp.AusfallMinuten);
                            }
                        }


                        // DataGridViewer refreshen
                        daten_config.EndEdit();
                        daten_config.AutoResizeColumns(); // Spaltenbreiten anpassen
                        daten_config.Refresh();

                        cp.LetzterStatus = ausfall; // aktuellen Status wegschreiben 99999 = Ausfall, < 99999 Warnung oder OK                        
                    }
                }
                catch (Exception e)   // Fehler der zBsp. bei Neueingabe entstehen kann ignorieren
                {
                  //  Log("Exception: " + e);
                }
                ZeilenCounter++;
            }
        }


        public void WarnLoop(string status, string computername, string Bezeichnung, string IPAdresse, string Art)
        {
            string[] PopUpClient = textBox_PopUpClients.Text.Split(';');
            foreach (string user in PopUpClient)
                MSG(user, status + " - NETGuard @" + computername + " >> " + Art + " Triggered " + Bezeichnung + " ( IP: " + IPAdresse + " )");
        }

        public void SMSWarn()
        {
            /*
                $header = 'Content-type: text/plain; charset=iso-8859-1' . "\r\n";
                $header .= "Content-Transfer-Encoding: 8bit\n\n";
                $mail_sent = mail ("xxxx@gateway.any-sms.biz","xxxx|xxxxx|gw=10",$sms_text,$header);
                echo $mail_sent ? "Mail versendet" : "Mailversand fehlgeschlagen";
                if ($mail_sent) SetValueBoolean("SMS_Warnung_".$bereich,true);
                mail ("wolfgang@ruthner.at","SMS Warnung versendet $sms_text",$sms_text,$header);
             */ 
        }



        public string pingcheck(string host)
        {
            Ping pingSender = new Ping();
            PingOptions options = new PingOptions();

            options.DontFragment = true;

            string data = "PingerPingerPingerPingerPingerPi";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int Warnung = 1500;

            PingReply reply = pingSender.Send(host, Warnung, buffer, options);
            if (reply.Status == IPStatus.Success)
            {
                return reply.RoundtripTime.ToString();
            }
            else
                return ("Warnung");           
        }

        public string htmlcheck(string host, string checkfor)
        {
            string returnvalue="0";
            string response = "";

            //Create a Web-Request to an URL
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(@"Http://" + host);

            //Defined poperties for the Web-Request
            httpWebRequest.Method = "POST";
            httpWebRequest.MediaType = "HTTP/1.1";
            httpWebRequest.ContentType = "text/xml";
            httpWebRequest.UserAgent = "IPChecker (wolfgang@ruthner.at)";

            //Defined data for the Web-Request
            byte[] byteArrayData = Encoding.ASCII.GetBytes("");
            httpWebRequest.ContentLength = byteArrayData.Length;



            //Translate data from the Web-Response to a string
            try
            {            
                //Attach data to the Web-Request
                Stream dataStream = httpWebRequest.GetRequestStream();
                dataStream.Write(byteArrayData, 0, byteArrayData.Length);
                dataStream.Close();

                //Send Web-Request and receive a Web-Response
                HttpWebResponse httpWebesponse = (HttpWebResponse)httpWebRequest.GetResponse();
                dataStream = httpWebesponse.GetResponseStream();
                StreamReader streamreader = new StreamReader(dataStream, Encoding.UTF8);
                response = streamreader.ReadToEnd();
                streamreader.Close();
            }
            catch
            {
                // Keine Verbindung 
                response = "Keine Verbindung";
            }

            if (response.IndexOf(checkfor) != -1)   // Text gefunden
                returnvalue = "1";
            else returnvalue = "Warnung";           // Text nicht gefunden
            
            return returnvalue;
        }

        private CheckResult RAMCheck(string host) // ramstring in MB
        {
            CheckResult result = new CheckResult();
            ManagementObjectSearcher query;
            ManagementObjectCollection ObjectCollection;
            ObjectQuery oq;
            ManagementScope ms = new ManagementScope("\\\\" + host + "\\root\\cimv2");
            // Freien RAM ermitteln
            // http://www.vbarchiv.net/tipps/tipp_1738-ram-speicher-via-wmi-ermitteln.html
            oq = new ObjectQuery("SELECT FreePhysicalMemory from Win32_OperatingSystem");
            query = new ManagementObjectSearcher(ms, oq);
            ObjectCollection = query.Get();

            int memfree = 0;
            foreach (ManagementObject memoryFree in ObjectCollection)
            {
                memfree += Convert.ToInt32(memoryFree["FreePhysicalMemory"]);
            }

            memfree /= 1024; // in MB zurückgeben
            result.Wert = memfree;
            result.Text = "MB";
            return result;
        }

        private CheckResult HDDCheck (string host, string hddname) // freier HDU Speicher in GB
        {
            CheckResult result = new CheckResult();
            ManagementObjectSearcher query;
            ManagementObjectCollection ObjectCollection;
            ObjectQuery oq;
            ManagementScope ms = new ManagementScope("\\\\" + host + "\\root\\cimv2");

            oq = new ObjectQuery("SELECT FreeSpace from Win32_LogicalDisk where DriveType=3 AND Name='" + hddname + ":'");
            query = new ManagementObjectSearcher(ms, oq);
            ObjectCollection = query.Get();

            double FreeSpace = 0;
            foreach (ManagementObject disksItem in ObjectCollection)
            {
                FreeSpace += Convert.ToDouble(disksItem["FreeSpace"]);
            }

            result.Wert = Convert.ToInt32( FreeSpace / 1024 / 1024 / 1024) ;   // GB
            result.Text = "OK";
            return result;
        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckLoop();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            SerializeToXML(checkpoint, XMLHostKonfig);
        }

        // Konfiguration in XML schreiben
        public static void SerializeToXML<T>(BindingList<T> dataList, string path)
        {
            XmlSerializer ser = new XmlSerializer(typeof(BindingList<T>));
            using (FileStream str = new FileStream(path, FileMode.Create))
            {
                ser.Serialize(str, dataList);
            }
        }

        // Konfiguration aus XML einlesen
        public BindingList<T> DeserializeToList<T>(string path)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(BindingList<T>));

            BindingList<T> list;
            /*
             * using (FileStream str = new FileStream(path, FileMode.Open))
            {
                list = (BindingList<T>)xmlSerializer.Deserialize(str);
            }
             */
            FileStream file = new FileStream(path, FileMode.Open);
            //list = xmlSerializer.Deserialize(file) as BindingList<T>;
            list = (BindingList<T>)xmlSerializer.Deserialize(file);
            file.Close();
            return list; 
        }

        // Checklauf deaktivieren/reaktivieren (zur weiteren Datenerfassung)
        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                button1.Text = "Aktivieren";
                daten_config.BackgroundColor = Color.Red;
            }
            else
            {
                timer1.Enabled = true;
                button1.Text = "Überprüfung Stoppen";
                daten_config.BackgroundColor = Color.LightGreen;
            }
        }


        // Mithilfe des Systemprogramms msg.exe die Nachricht raussenden
        private void MSG(string name, string message)
        {

            ProcessStartInfo psi = new ProcessStartInfo("msg.exe", " * /TIME:1190 /server:" + name + " \"" + message + "\"");
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            psi.UseShellExecute = false;

            Process p = Process.Start(psi);
            Log(message + " -> " + name);

            int delaycounter = 0;

            do
            {
                if (!p.HasExited)
                {
                    p.Refresh();
                    Delay(500);
                    delaycounter++;
                }
            }
            while (!p.WaitForExit(1000));

            p.WaitForExit();
            int result = p.ExitCode;
            Log("Exitcode: " + Convert.ToString(result) + ", Delays: " + Convert.ToString(delaycounter) );
        }


        private void Delay(int ms)
        {
            int time = Environment.TickCount;

            while (true)
            {
                if (Environment.TickCount - time >= ms) return;
            }
        }


        // Send to Tray
        private void IPChecker_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.ShowInTaskbar = false;
                this.WindowState = FormWindowState.Minimized;
                this.notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(6000);
                SentToTray = true;
            }
        }   

        // Wiederherstellen aus Tray
        private void notifyIcon1_Click(object sender, EventArgs e)
        {
            this.Show();
            this.ShowInTaskbar = true;
            notifyIcon1.Visible = false;
            SentToTray = false;
            this.WindowState = FormWindowState.Normal;
        }

        // Logtext schreiben
        private void Log(string logtext)
        {
            FileStream fs = new FileStream("NetGuard.log", FileMode.Append);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss.fff") + "> " + logtext);
            sw.Flush();
            fs.Close();
        }

        bool DoesServiceRun(string serviceName, string machineName)
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices(machineName);
                foreach (ServiceController service in services)
                {
                    if (service.ServiceName == serviceName || service.DisplayName == serviceName)
                    {
                        if (service.Status == ServiceControllerStatus.Running) return true;
                        else return false;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Log("Fehler beim Abrufen des Status des Services " + serviceName + " für " + machineName);
                Log(ex.Message);
                return false;
            }
        }

        private void textBox_PopUpClients_TextChanged(object sender, EventArgs e)
        {
            setting.PopUpClients = textBox_PopUpClients.Text;
            setting.Save();
        }

        private void textBox_Intervall_TextChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(textBox_Intervall.Text) < 5) return;    // Vermeiden dass das Intervall auf "0" gesetzt wird.
            timer1.Interval = Convert.ToInt32(textBox_Intervall.Text) * 1000;
            setting.Intervall = textBox_Intervall.Text;
            setting.Save();
        }

        private void programmBeendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
           System.Windows.Forms.Application.Exit();
        }

        private void programminfoHilfeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void rechnerSpeichernUndBeendenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SerializeToXML(checkpoint, XMLHostKonfig);
            System.Windows.Forms.Application.Exit();
        }

        private void checkBox_StartMinimized_CheckedChanged(object sender, EventArgs e)
        {
            setting.StartMinimized = checkBox_StartMinimized.Checked;
            setting.Save();
        }

        // Mail versenden
        // http://code.msdn.microsoft.com/windowsdesktop/E-Maisl-mit-C-versenden-310c300e#content

        //Infomail(textBox_EMailClients, checkResult.Text, cp.IPAdresse, Art); 
        private void Infomail(string status, string Computername, string ResultText, string IPAddresse, string Art, double AusfallMinuten)
        {

            string SMTP_Host = textBox_SMTPServer.Text;
            int SMTP_Port = Convert.ToInt32(textBox_SMTPPort.Text);
            string FROM = textBox_SMTPFrom.Text;

            string[] MailClients = textBox_EMailClients.Text.Split(';');
            foreach (string emailaddy in MailClients)
            {

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(FROM);
                mail.To.Add(emailaddy);
                mail.Subject = status + ": NetGuard@" + Computername + ": " + Art + " auf " + IPAddresse + " : " + ResultText;
                mail.Body = status + ": NetGuard@" + Computername + ": " + Art + " auf " + IPAddresse + " : " + ResultText;
                mail.Body += "\r\nUhrzeit: " + DateTime.Now.ToString("yyyy-MM-dd, HH:mm:ss");
                if (status == "UP") mail.Body += "\r\nDauer in Minuten: " + AusfallMinuten;

                SmtpClient client;

                client = new SmtpClient();
                client.Host = SMTP_Host;
                client.Port = 25;

                Log("Mail an " + emailaddy);
                try
                {
                    client.Send(mail);
                }
                catch (Exception e)   // Fehler beim Mailversand
                {
                    Log("Exception: " + e as string);
                }
            }

        }


        
    }
}
