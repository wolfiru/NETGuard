namespace XMLConfigSaver
{
    partial class IPChecker
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(IPChecker));
            this.daten_config = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.label_PopUpClients = new System.Windows.Forms.Label();
            this.textBox_PopUpClients = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_Intervall = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_SMSPasswort = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_SMSNummern = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_SMSGateway = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_SMSBenutzerID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_SMTPServer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_SMTPPort = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_SMTPFrom = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_EMailClients = new System.Windows.Forms.TextBox();
            this.checkBox_StartMinimized = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rechnerSpeichernUndBeendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programmBeendenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.programminfoHilfeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_Rechner = new System.Windows.Forms.TabControl();
            this.tabPage_Rechner = new System.Windows.Forms.TabPage();
            this.tabPage_Settings = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.daten_config)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.tabControl_Rechner.SuspendLayout();
            this.tabPage_Rechner.SuspendLayout();
            this.tabPage_Settings.SuspendLayout();
            this.SuspendLayout();
            // 
            // daten_config
            // 
            this.daten_config.AllowUserToOrderColumns = true;
            this.daten_config.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.daten_config.Location = new System.Drawing.Point(6, 6);
            this.daten_config.Name = "daten_config";
            this.daten_config.Size = new System.Drawing.Size(923, 393);
            this.daten_config.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(822, 405);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(107, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Rechner speichern";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 405);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Prüflauf Pausieren";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "IPChecker ist nun im System-Tray";
            this.notifyIcon1.BalloonTipTitle = "Hinweis";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "IPChecker";
            this.notifyIcon1.Click += new System.EventHandler(this.notifyIcon1_Click);
            // 
            // label_PopUpClients
            // 
            this.label_PopUpClients.AutoSize = true;
            this.label_PopUpClients.ForeColor = System.Drawing.Color.White;
            this.label_PopUpClients.Location = new System.Drawing.Point(10, 55);
            this.label_PopUpClients.Name = "label_PopUpClients";
            this.label_PopUpClients.Size = new System.Drawing.Size(201, 13);
            this.label_PopUpClients.TabIndex = 10;
            this.label_PopUpClients.Text = "Welche Clients über PopUp informieren? ";
            this.label_PopUpClients.TextChanged += new System.EventHandler(this.textBox_PopUpClients_TextChanged);
            // 
            // textBox_PopUpClients
            // 
            this.textBox_PopUpClients.Location = new System.Drawing.Point(218, 49);
            this.textBox_PopUpClients.Name = "textBox_PopUpClients";
            this.textBox_PopUpClients.Size = new System.Drawing.Size(628, 20);
            this.textBox_PopUpClients.TabIndex = 11;
            this.textBox_PopUpClients.TextChanged += new System.EventHandler(this.textBox_PopUpClients_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(38, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "Überprüfungsintervall in Sekunden:";
            // 
            // textBox_Intervall
            // 
            this.textBox_Intervall.Location = new System.Drawing.Point(219, 22);
            this.textBox_Intervall.Name = "textBox_Intervall";
            this.textBox_Intervall.Size = new System.Drawing.Size(25, 20);
            this.textBox_Intervall.TabIndex = 13;
            this.textBox_Intervall.Text = "30";
            this.textBox_Intervall.TextChanged += new System.EventHandler(this.textBox_Intervall_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SeaGreen;
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.textBox_SMSPasswort);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox_SMSNummern);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.textBox_SMSGateway);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBox_SMSBenutzerID);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.textBox_SMTPServer);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBox_SMTPPort);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_SMTPFrom);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.textBox_EMailClients);
            this.groupBox1.Controls.Add(this.checkBox_StartMinimized);
            this.groupBox1.Controls.Add(this.label_PopUpClients);
            this.groupBox1.Controls.Add(this.textBox_Intervall);
            this.groupBox1.Controls.Add(this.textBox_PopUpClients);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(6, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(923, 414);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Einstellungen:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(145, 318);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Passwort:";
            // 
            // textBox_SMSPasswort
            // 
            this.textBox_SMSPasswort.Location = new System.Drawing.Point(218, 315);
            this.textBox_SMSPasswort.Name = "textBox_SMSPasswort";
            this.textBox_SMSPasswort.Size = new System.Drawing.Size(82, 20);
            this.textBox_SMSPasswort.TabIndex = 30;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(117, 240);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "SMS-Nummern:";
            // 
            // textBox_SMSNummern
            // 
            this.textBox_SMSNummern.Location = new System.Drawing.Point(218, 237);
            this.textBox_SMSNummern.Name = "textBox_SMSNummern";
            this.textBox_SMSNummern.Size = new System.Drawing.Size(628, 20);
            this.textBox_SMSNummern.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(90, 266);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "any-sms.biz Gateway:";
            // 
            // textBox_SMSGateway
            // 
            this.textBox_SMSGateway.Location = new System.Drawing.Point(218, 263);
            this.textBox_SMSGateway.Name = "textBox_SMSGateway";
            this.textBox_SMSGateway.Size = new System.Drawing.Size(26, 20);
            this.textBox_SMSGateway.TabIndex = 26;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(133, 292);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Benutzer-ID:";
            // 
            // textBox_SMSBenutzerID
            // 
            this.textBox_SMSBenutzerID.Location = new System.Drawing.Point(218, 289);
            this.textBox_SMSBenutzerID.Name = "textBox_SMSBenutzerID";
            this.textBox_SMSBenutzerID.Size = new System.Drawing.Size(82, 20);
            this.textBox_SMSBenutzerID.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(124, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "SMTP-Server:";
            // 
            // textBox_SMTPServer
            // 
            this.textBox_SMTPServer.Location = new System.Drawing.Point(218, 123);
            this.textBox_SMTPServer.Name = "textBox_SMTPServer";
            this.textBox_SMTPServer.Size = new System.Drawing.Size(120, 20);
            this.textBox_SMTPServer.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(168, 152);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Port:";
            // 
            // textBox_SMTPPort
            // 
            this.textBox_SMTPPort.Location = new System.Drawing.Point(218, 149);
            this.textBox_SMTPPort.Name = "textBox_SMTPPort";
            this.textBox_SMTPPort.Size = new System.Drawing.Size(26, 20);
            this.textBox_SMTPPort.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(110, 178);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Versandadresse:";
            // 
            // textBox_SMTPFrom
            // 
            this.textBox_SMTPFrom.Location = new System.Drawing.Point(218, 175);
            this.textBox_SMTPFrom.Name = "textBox_SMTPFrom";
            this.textBox_SMTPFrom.Size = new System.Drawing.Size(241, 20);
            this.textBox_SMTPFrom.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 103);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = "Welche Clients über Email informieren? ";
            // 
            // textBox_EMailClients
            // 
            this.textBox_EMailClients.Location = new System.Drawing.Point(218, 97);
            this.textBox_EMailClients.Name = "textBox_EMailClients";
            this.textBox_EMailClients.Size = new System.Drawing.Size(628, 20);
            this.textBox_EMailClients.TabIndex = 16;
            // 
            // checkBox_StartMinimized
            // 
            this.checkBox_StartMinimized.AutoSize = true;
            this.checkBox_StartMinimized.Location = new System.Drawing.Point(634, 373);
            this.checkBox_StartMinimized.Name = "checkBox_StartMinimized";
            this.checkBox_StartMinimized.Size = new System.Drawing.Size(201, 17);
            this.checkBox_StartMinimized.TabIndex = 14;
            this.checkBox_StartMinimized.Text = "Programm in den System-Tray starten";
            this.checkBox_StartMinimized.UseVisualStyleBackColor = true;
            this.checkBox_StartMinimized.CheckedChanged += new System.EventHandler(this.checkBox_StartMinimized_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.hilfeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(971, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.rechnerSpeichernUndBeendenToolStripMenuItem,
            this.programmBeendenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.dateiToolStripMenuItem.Text = "Datei";
            // 
            // rechnerSpeichernUndBeendenToolStripMenuItem
            // 
            this.rechnerSpeichernUndBeendenToolStripMenuItem.Name = "rechnerSpeichernUndBeendenToolStripMenuItem";
            this.rechnerSpeichernUndBeendenToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.rechnerSpeichernUndBeendenToolStripMenuItem.Text = "Rechner speichern und beenden";
            this.rechnerSpeichernUndBeendenToolStripMenuItem.Click += new System.EventHandler(this.rechnerSpeichernUndBeendenToolStripMenuItem_Click);
            // 
            // programmBeendenToolStripMenuItem
            // 
            this.programmBeendenToolStripMenuItem.Name = "programmBeendenToolStripMenuItem";
            this.programmBeendenToolStripMenuItem.Size = new System.Drawing.Size(321, 22);
            this.programmBeendenToolStripMenuItem.Text = "Programm ohne Rechnerspeicherung beenden";
            this.programmBeendenToolStripMenuItem.Click += new System.EventHandler(this.programmBeendenToolStripMenuItem_Click);
            // 
            // hilfeToolStripMenuItem
            // 
            this.hilfeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programminfoHilfeToolStripMenuItem});
            this.hilfeToolStripMenuItem.Name = "hilfeToolStripMenuItem";
            this.hilfeToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.hilfeToolStripMenuItem.Text = "Hilfe";
            // 
            // programminfoHilfeToolStripMenuItem
            // 
            this.programminfoHilfeToolStripMenuItem.Name = "programminfoHilfeToolStripMenuItem";
            this.programminfoHilfeToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
            this.programminfoHilfeToolStripMenuItem.Text = "Programminfo und Hilfe";
            this.programminfoHilfeToolStripMenuItem.Click += new System.EventHandler(this.programminfoHilfeToolStripMenuItem_Click);
            // 
            // tabControl_Rechner
            // 
            this.tabControl_Rechner.Controls.Add(this.tabPage_Rechner);
            this.tabControl_Rechner.Controls.Add(this.tabPage_Settings);
            this.tabControl_Rechner.Location = new System.Drawing.Point(16, 50);
            this.tabControl_Rechner.Name = "tabControl_Rechner";
            this.tabControl_Rechner.SelectedIndex = 0;
            this.tabControl_Rechner.Size = new System.Drawing.Size(943, 461);
            this.tabControl_Rechner.TabIndex = 16;
            // 
            // tabPage_Rechner
            // 
            this.tabPage_Rechner.AutoScroll = true;
            this.tabPage_Rechner.BackColor = System.Drawing.Color.PowderBlue;
            this.tabPage_Rechner.Controls.Add(this.button1);
            this.tabPage_Rechner.Controls.Add(this.daten_config);
            this.tabPage_Rechner.Controls.Add(this.button2);
            this.tabPage_Rechner.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Rechner.Name = "tabPage_Rechner";
            this.tabPage_Rechner.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Rechner.Size = new System.Drawing.Size(935, 435);
            this.tabPage_Rechner.TabIndex = 0;
            this.tabPage_Rechner.Text = "zu überprüfende Rechner";
            // 
            // tabPage_Settings
            // 
            this.tabPage_Settings.BackColor = System.Drawing.Color.PowderBlue;
            this.tabPage_Settings.Controls.Add(this.groupBox1);
            this.tabPage_Settings.Location = new System.Drawing.Point(4, 22);
            this.tabPage_Settings.Name = "tabPage_Settings";
            this.tabPage_Settings.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_Settings.Size = new System.Drawing.Size(935, 435);
            this.tabPage_Settings.TabIndex = 1;
            this.tabPage_Settings.Text = "Einstellungen";
            // 
            // IPChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(971, 525);
            this.Controls.Add(this.tabControl_Rechner);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "IPChecker";
            this.Text = "NETGuard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.IPChecker_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.daten_config)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl_Rechner.ResumeLayout(false);
            this.tabPage_Rechner.ResumeLayout(false);
            this.tabPage_Settings.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView daten_config;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label label_PopUpClients;
        private System.Windows.Forms.TextBox textBox_PopUpClients;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_Intervall;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programmBeendenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem programminfoHilfeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem rechnerSpeichernUndBeendenToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox_StartMinimized;
        private System.Windows.Forms.TabControl tabControl_Rechner;
        private System.Windows.Forms.TabPage tabPage_Rechner;
        private System.Windows.Forms.TabPage tabPage_Settings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_EMailClients;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_SMTPServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_SMTPPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_SMTPFrom;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_SMSPasswort;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_SMSNummern;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_SMSGateway;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_SMSBenutzerID;
    }
}

