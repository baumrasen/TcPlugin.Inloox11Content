namespace TcPlugins.ContentInloox11
{
    partial class InlooxLogin
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel = new System.Windows.Forms.Panel();
            this.cb_regexp_saved = new System.Windows.Forms.CheckBox();
            this.cb_regexp_checked = new System.Windows.Forms.CheckBox();
            this.bResetRegExpr = new System.Windows.Forms.Button();
            this.cbKeepItOpen = new System.Windows.Forms.CheckBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.cbRegExprCheckResult = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbDivision = new System.Windows.Forms.TextBox();
            this.lDivision = new System.Windows.Forms.Label();
            this.tbProjectNumber = new System.Windows.Forms.TextBox();
            this.tbUnc = new System.Windows.Forms.TextBox();
            this.tbDrive = new System.Windows.Forms.TextBox();
            this.lProjectNumber = new System.Windows.Forms.Label();
            this.lUnc = new System.Windows.Forms.Label();
            this.lDrive = new System.Windows.Forms.Label();
            this.lTestResult = new System.Windows.Forms.Label();
            this.bSaveRegExpr = new System.Windows.Forms.Button();
            this.bCheckRegExpr = new System.Windows.Forms.Button();
            this.tbPathContent = new System.Windows.Forms.TextBox();
            this.lTestPathContent = new System.Windows.Forms.Label();
            this.tbDefaultRegExpr = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.cbRemoveTokenFromRegistry = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lPrjNrRegExpr = new System.Windows.Forms.Label();
            this.tbPrjNrRegExpr = new System.Windows.Forms.TextBox();
            this.tbUrl = new System.Windows.Forms.TextBox();
            this.tbOdataPathString = new System.Windows.Forms.TextBox();
            this.tbTokenPath = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.bGetAuthToken = new System.Windows.Forms.Button();
            this.cb_saved = new System.Windows.Forms.CheckBox();
            this.bCheck = new System.Windows.Forms.Button();
            this.cb_checked = new System.Windows.Forms.CheckBox();
            this.bSaveAuthToken = new System.Windows.Forms.Button();
            this.tbError = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbAuthToken = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbEndPointString = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbUserPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lHintToOkay = new System.Windows.Forms.Label();
            this.panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(1185, 15);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(112, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Enabled = false;
            this.btnOK.Location = new System.Drawing.Point(1050, 15);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(112, 35);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel
            // 
            this.panel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel.Controls.Add(this.cb_regexp_saved);
            this.panel.Controls.Add(this.cb_regexp_checked);
            this.panel.Controls.Add(this.bResetRegExpr);
            this.panel.Controls.Add(this.cbKeepItOpen);
            this.panel.Controls.Add(this.textBox3);
            this.panel.Controls.Add(this.cbRegExprCheckResult);
            this.panel.Controls.Add(this.label7);
            this.panel.Controls.Add(this.tbDivision);
            this.panel.Controls.Add(this.lDivision);
            this.panel.Controls.Add(this.tbProjectNumber);
            this.panel.Controls.Add(this.tbUnc);
            this.panel.Controls.Add(this.tbDrive);
            this.panel.Controls.Add(this.lProjectNumber);
            this.panel.Controls.Add(this.lUnc);
            this.panel.Controls.Add(this.lDrive);
            this.panel.Controls.Add(this.lTestResult);
            this.panel.Controls.Add(this.bSaveRegExpr);
            this.panel.Controls.Add(this.bCheckRegExpr);
            this.panel.Controls.Add(this.tbPathContent);
            this.panel.Controls.Add(this.lTestPathContent);
            this.panel.Controls.Add(this.tbDefaultRegExpr);
            this.panel.Controls.Add(this.textBox2);
            this.panel.Controls.Add(this.cbRemoveTokenFromRegistry);
            this.panel.Controls.Add(this.textBox1);
            this.panel.Controls.Add(this.lPrjNrRegExpr);
            this.panel.Controls.Add(this.tbPrjNrRegExpr);
            this.panel.Controls.Add(this.tbUrl);
            this.panel.Controls.Add(this.tbOdataPathString);
            this.panel.Controls.Add(this.tbTokenPath);
            this.panel.Controls.Add(this.label6);
            this.panel.Controls.Add(this.cb_saved);
            this.panel.Controls.Add(this.bCheck);
            this.panel.Controls.Add(this.cb_checked);
            this.panel.Controls.Add(this.bSaveAuthToken);
            this.panel.Controls.Add(this.tbError);
            this.panel.Controls.Add(this.label5);
            this.panel.Controls.Add(this.tbAuthToken);
            this.panel.Controls.Add(this.label4);
            this.panel.Controls.Add(this.tbEndPointString);
            this.panel.Controls.Add(this.label3);
            this.panel.Controls.Add(this.tbUserPassword);
            this.panel.Controls.Add(this.label2);
            this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel.Location = new System.Drawing.Point(0, 0);
            this.panel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel.Name = "panel";
            this.panel.Size = new System.Drawing.Size(1313, 1094);
            this.panel.TabIndex = 10;
            this.panel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_Paint);
            // 
            // cb_regexp_saved
            // 
            this.cb_regexp_saved.AutoSize = true;
            this.cb_regexp_saved.Enabled = false;
            this.cb_regexp_saved.Location = new System.Drawing.Point(429, 695);
            this.cb_regexp_saved.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_regexp_saved.Name = "cb_regexp_saved";
            this.cb_regexp_saved.Size = new System.Drawing.Size(77, 24);
            this.cb_regexp_saved.TabIndex = 42;
            this.cb_regexp_saved.Text = "saved";
            this.cb_regexp_saved.UseVisualStyleBackColor = true;
            // 
            // cb_regexp_checked
            // 
            this.cb_regexp_checked.AutoSize = true;
            this.cb_regexp_checked.Enabled = false;
            this.cb_regexp_checked.Location = new System.Drawing.Point(427, 650);
            this.cb_regexp_checked.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_regexp_checked.Name = "cb_regexp_checked";
            this.cb_regexp_checked.Size = new System.Drawing.Size(66, 24);
            this.cb_regexp_checked.TabIndex = 41;
            this.cb_regexp_checked.Text = "valid";
            this.cb_regexp_checked.UseVisualStyleBackColor = true;
            // 
            // bResetRegExpr
            // 
            this.bResetRegExpr.Location = new System.Drawing.Point(23, 472);
            this.bResetRegExpr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bResetRegExpr.Name = "bResetRegExpr";
            this.bResetRegExpr.Size = new System.Drawing.Size(170, 35);
            this.bResetRegExpr.TabIndex = 40;
            this.bResetRegExpr.Text = "reset to default";
            this.bResetRegExpr.UseVisualStyleBackColor = true;
            this.bResetRegExpr.Click += new System.EventHandler(this.bResetRegExpr_Click);
            // 
            // cbKeepItOpen
            // 
            this.cbKeepItOpen.AutoSize = true;
            this.cbKeepItOpen.Location = new System.Drawing.Point(212, 869);
            this.cbKeepItOpen.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbKeepItOpen.Name = "cbKeepItOpen";
            this.cbKeepItOpen.Size = new System.Drawing.Size(689, 24);
            this.cbKeepItOpen.TabIndex = 21;
            this.cbKeepItOpen.Text = "open this dialog on every start (used to change the regular expression for the pr" +
    "oject number)";
            this.cbKeepItOpen.UseVisualStyleBackColor = true;
            this.cbKeepItOpen.CheckedChanged += new System.EventHandler(this.cbKeepItOpen_CheckedChanged);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox3.Location = new System.Drawing.Point(24, 886);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(1268, 19);
            this.textBox3.TabIndex = 39;
            this.textBox3.Text = "_________________________________________________________________________________" +
    "___________________________________________________________________________";
            this.textBox3.WordWrap = false;
            // 
            // cbRegExprCheckResult
            // 
            this.cbRegExprCheckResult.AutoSize = true;
            this.cbRegExprCheckResult.Enabled = false;
            this.cbRegExprCheckResult.Location = new System.Drawing.Point(671, 659);
            this.cbRegExprCheckResult.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbRegExprCheckResult.Name = "cbRegExprCheckResult";
            this.cbRegExprCheckResult.Size = new System.Drawing.Size(223, 24);
            this.cbRegExprCheckResult.TabIndex = 38;
            this.cbRegExprCheckResult.Text = "bool result for reg expr test";
            this.cbRegExprCheckResult.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(18, 517);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 20);
            this.label7.TabIndex = 36;
            this.label7.Text = "default PrjNr reg expr";
            this.label7.Click += new System.EventHandler(this.label7_Click_3);
            // 
            // tbDivision
            // 
            this.tbDivision.Location = new System.Drawing.Point(672, 778);
            this.tbDivision.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDivision.Name = "tbDivision";
            this.tbDivision.ReadOnly = true;
            this.tbDivision.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDivision.Size = new System.Drawing.Size(621, 26);
            this.tbDivision.TabIndex = 35;
            // 
            // lDivision
            // 
            this.lDivision.AutoSize = true;
            this.lDivision.Location = new System.Drawing.Point(507, 784);
            this.lDivision.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lDivision.Name = "lDivision";
            this.lDivision.Size = new System.Drawing.Size(60, 20);
            this.lDivision.TabIndex = 34;
            this.lDivision.Text = "division";
            this.lDivision.Click += new System.EventHandler(this.label7_Click_2);
            // 
            // tbProjectNumber
            // 
            this.tbProjectNumber.Location = new System.Drawing.Point(672, 819);
            this.tbProjectNumber.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbProjectNumber.Name = "tbProjectNumber";
            this.tbProjectNumber.ReadOnly = true;
            this.tbProjectNumber.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbProjectNumber.Size = new System.Drawing.Size(621, 26);
            this.tbProjectNumber.TabIndex = 33;
            // 
            // tbUnc
            // 
            this.tbUnc.Location = new System.Drawing.Point(671, 734);
            this.tbUnc.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbUnc.Name = "tbUnc";
            this.tbUnc.ReadOnly = true;
            this.tbUnc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbUnc.Size = new System.Drawing.Size(621, 26);
            this.tbUnc.TabIndex = 32;
            // 
            // tbDrive
            // 
            this.tbDrive.Location = new System.Drawing.Point(671, 693);
            this.tbDrive.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDrive.Name = "tbDrive";
            this.tbDrive.ReadOnly = true;
            this.tbDrive.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbDrive.Size = new System.Drawing.Size(621, 26);
            this.tbDrive.TabIndex = 31;
            // 
            // lProjectNumber
            // 
            this.lProjectNumber.AutoSize = true;
            this.lProjectNumber.Location = new System.Drawing.Point(507, 825);
            this.lProjectNumber.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lProjectNumber.Name = "lProjectNumber";
            this.lProjectNumber.Size = new System.Drawing.Size(111, 20);
            this.lProjectNumber.TabIndex = 30;
            this.lProjectNumber.Text = "projectnumber";
            // 
            // lUnc
            // 
            this.lUnc.AutoSize = true;
            this.lUnc.Location = new System.Drawing.Point(507, 740);
            this.lUnc.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lUnc.Name = "lUnc";
            this.lUnc.Size = new System.Drawing.Size(35, 20);
            this.lUnc.TabIndex = 29;
            this.lUnc.Text = "unc";
            // 
            // lDrive
            // 
            this.lDrive.AutoSize = true;
            this.lDrive.Location = new System.Drawing.Point(507, 699);
            this.lDrive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lDrive.Name = "lDrive";
            this.lDrive.Size = new System.Drawing.Size(42, 20);
            this.lDrive.TabIndex = 28;
            this.lDrive.Text = "drive";
            this.lDrive.Click += new System.EventHandler(this.label9_Click);
            // 
            // lTestResult
            // 
            this.lTestResult.AutoSize = true;
            this.lTestResult.Location = new System.Drawing.Point(506, 659);
            this.lTestResult.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTestResult.Name = "lTestResult";
            this.lTestResult.Size = new System.Drawing.Size(79, 20);
            this.lTestResult.TabIndex = 27;
            this.lTestResult.Text = "test result";
            this.lTestResult.Click += new System.EventHandler(this.label8_Click);
            // 
            // bSaveRegExpr
            // 
            this.bSaveRegExpr.Enabled = false;
            this.bSaveRegExpr.Location = new System.Drawing.Point(211, 689);
            this.bSaveRegExpr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSaveRegExpr.Name = "bSaveRegExpr";
            this.bSaveRegExpr.Size = new System.Drawing.Size(208, 35);
            this.bSaveRegExpr.TabIndex = 26;
            this.bSaveRegExpr.Text = "save reg expr";
            this.bSaveRegExpr.UseVisualStyleBackColor = true;
            this.bSaveRegExpr.Click += new System.EventHandler(this.bSaveRegExpr_Click);
            // 
            // bCheckRegExpr
            // 
            this.bCheckRegExpr.Location = new System.Drawing.Point(211, 644);
            this.bCheckRegExpr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bCheckRegExpr.Name = "bCheckRegExpr";
            this.bCheckRegExpr.Size = new System.Drawing.Size(208, 35);
            this.bCheckRegExpr.TabIndex = 25;
            this.bCheckRegExpr.Text = "check reg expr";
            this.bCheckRegExpr.UseVisualStyleBackColor = true;
            this.bCheckRegExpr.Click += new System.EventHandler(this.bCheckRegExpr_Click);
            // 
            // tbPathContent
            // 
            this.tbPathContent.Location = new System.Drawing.Point(212, 608);
            this.tbPathContent.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPathContent.Name = "tbPathContent";
            this.tbPathContent.Size = new System.Drawing.Size(1080, 26);
            this.tbPathContent.TabIndex = 24;
            this.tbPathContent.WordWrap = false;
            this.tbPathContent.TextChanged += new System.EventHandler(this.tbPathContent_TextChanged);
            // 
            // lTestPathContent
            // 
            this.lTestPathContent.AutoSize = true;
            this.lTestPathContent.Location = new System.Drawing.Point(20, 614);
            this.lTestPathContent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lTestPathContent.Name = "lTestPathContent";
            this.lTestPathContent.Size = new System.Drawing.Size(134, 20);
            this.lTestPathContent.TabIndex = 23;
            this.lTestPathContent.Text = "Test path content";
            this.lTestPathContent.Click += new System.EventHandler(this.label7_Click_1);
            // 
            // tbDefaultRegExpr
            // 
            this.tbDefaultRegExpr.Enabled = false;
            this.tbDefaultRegExpr.Location = new System.Drawing.Point(211, 517);
            this.tbDefaultRegExpr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbDefaultRegExpr.Multiline = true;
            this.tbDefaultRegExpr.Name = "tbDefaultRegExpr";
            this.tbDefaultRegExpr.ReadOnly = true;
            this.tbDefaultRegExpr.Size = new System.Drawing.Size(1080, 81);
            this.tbDefaultRegExpr.TabIndex = 22;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Control;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox2.Location = new System.Drawing.Point(24, 840);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(1268, 19);
            this.textBox2.TabIndex = 20;
            this.textBox2.Text = "_________________________________________________________________________________" +
    "___________________________________________________________________________";
            this.textBox2.WordWrap = false;
            // 
            // cbRemoveTokenFromRegistry
            // 
            this.cbRemoveTokenFromRegistry.Location = new System.Drawing.Point(601, 328);
            this.cbRemoveTokenFromRegistry.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cbRemoveTokenFromRegistry.Name = "cbRemoveTokenFromRegistry";
            this.cbRemoveTokenFromRegistry.Size = new System.Drawing.Size(396, 35);
            this.cbRemoveTokenFromRegistry.TabIndex = 15;
            this.cbRemoveTokenFromRegistry.Text = "delete saved authtoken";
            this.cbRemoveTokenFromRegistry.UseVisualStyleBackColor = true;
            this.cbRemoveTokenFromRegistry.Click += new System.EventHandler(this.cbRemoveTokenFromRegistry_Click);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.textBox1.Location = new System.Drawing.Point(24, 373);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1268, 19);
            this.textBox1.TabIndex = 19;
            this.textBox1.Text = "_________________________________________________________________________________" +
    "___________________________________________________________________________";
            this.textBox1.WordWrap = false;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // lPrjNrRegExpr
            // 
            this.lPrjNrRegExpr.AutoSize = true;
            this.lPrjNrRegExpr.Location = new System.Drawing.Point(20, 422);
            this.lPrjNrRegExpr.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lPrjNrRegExpr.Name = "lPrjNrRegExpr";
            this.lPrjNrRegExpr.Size = new System.Drawing.Size(173, 20);
            this.lPrjNrRegExpr.TabIndex = 18;
            this.lPrjNrRegExpr.Text = "Projectnumber reg expr";
            this.lPrjNrRegExpr.Click += new System.EventHandler(this.label7_Click);
            // 
            // tbPrjNrRegExpr
            // 
            this.tbPrjNrRegExpr.Location = new System.Drawing.Point(212, 419);
            this.tbPrjNrRegExpr.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbPrjNrRegExpr.Multiline = true;
            this.tbPrjNrRegExpr.Name = "tbPrjNrRegExpr";
            this.tbPrjNrRegExpr.Size = new System.Drawing.Size(1080, 88);
            this.tbPrjNrRegExpr.TabIndex = 17;
            this.tbPrjNrRegExpr.TextChanged += new System.EventHandler(this.tbPrjNrRegExpr_TextChanged);
            // 
            // tbUrl
            // 
            this.tbUrl.Location = new System.Drawing.Point(211, 55);
            this.tbUrl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbUrl.Name = "tbUrl";
            this.tbUrl.ReadOnly = true;
            this.tbUrl.Size = new System.Drawing.Size(1080, 26);
            this.tbUrl.TabIndex = 16;
            this.tbUrl.WordWrap = false;
            this.tbUrl.TextChanged += new System.EventHandler(this.tbUrl_TextChanged);
            // 
            // tbOdataPathString
            // 
            this.tbOdataPathString.Location = new System.Drawing.Point(610, 19);
            this.tbOdataPathString.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbOdataPathString.Name = "tbOdataPathString";
            this.tbOdataPathString.Size = new System.Drawing.Size(681, 26);
            this.tbOdataPathString.TabIndex = 15;
            this.tbOdataPathString.WordWrap = false;
            this.tbOdataPathString.TextChanged += new System.EventHandler(this.tbOdataPathString_TextChanged);
            // 
            // tbTokenPath
            // 
            this.tbTokenPath.Location = new System.Drawing.Point(211, 138);
            this.tbTokenPath.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbTokenPath.Name = "tbTokenPath";
            this.tbTokenPath.ReadOnly = true;
            this.tbTokenPath.Size = new System.Drawing.Size(1080, 26);
            this.tbTokenPath.TabIndex = 13;
            this.tbTokenPath.WordWrap = false;
            this.tbTokenPath.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(22, 104);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(498, 20);
            this.label6.TabIndex = 14;
            this.label6.Text = "Goto this URL and create an authentication token for this application:";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // bGetAuthToken
            // 
            this.bGetAuthToken.Location = new System.Drawing.Point(13, 15);
            this.bGetAuthToken.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bGetAuthToken.Name = "bGetAuthToken";
            this.bGetAuthToken.Size = new System.Drawing.Size(224, 32);
            this.bGetAuthToken.TabIndex = 3;
            this.bGetAuthToken.Text = "get token from server";
            this.bGetAuthToken.UseVisualStyleBackColor = true;
            this.bGetAuthToken.Visible = false;
            this.bGetAuthToken.Click += new System.EventHandler(this.bGetAuthToken_Click);
            // 
            // cb_saved
            // 
            this.cb_saved.AutoSize = true;
            this.cb_saved.Enabled = false;
            this.cb_saved.Location = new System.Drawing.Point(429, 334);
            this.cb_saved.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_saved.Name = "cb_saved";
            this.cb_saved.Size = new System.Drawing.Size(77, 24);
            this.cb_saved.TabIndex = 12;
            this.cb_saved.Text = "saved";
            this.cb_saved.UseVisualStyleBackColor = true;
            // 
            // bCheck
            // 
            this.bCheck.Location = new System.Drawing.Point(212, 284);
            this.bCheck.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bCheck.Name = "bCheck";
            this.bCheck.Size = new System.Drawing.Size(208, 35);
            this.bCheck.TabIndex = 10;
            this.bCheck.Text = "check token";
            this.bCheck.UseVisualStyleBackColor = true;
            this.bCheck.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // cb_checked
            // 
            this.cb_checked.AutoSize = true;
            this.cb_checked.Enabled = false;
            this.cb_checked.Location = new System.Drawing.Point(429, 290);
            this.cb_checked.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.cb_checked.Name = "cb_checked";
            this.cb_checked.Size = new System.Drawing.Size(66, 24);
            this.cb_checked.TabIndex = 9;
            this.cb_checked.Text = "valid";
            this.cb_checked.UseVisualStyleBackColor = true;
            this.cb_checked.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // bSaveAuthToken
            // 
            this.bSaveAuthToken.Enabled = false;
            this.bSaveAuthToken.Location = new System.Drawing.Point(212, 328);
            this.bSaveAuthToken.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.bSaveAuthToken.Name = "bSaveAuthToken";
            this.bSaveAuthToken.Size = new System.Drawing.Size(208, 35);
            this.bSaveAuthToken.TabIndex = 11;
            this.bSaveAuthToken.Text = "save authtoken";
            this.bSaveAuthToken.UseVisualStyleBackColor = true;
            this.bSaveAuthToken.Click += new System.EventHandler(this.bSaveAuthToken_Click);
            // 
            // tbError
            // 
            this.tbError.Location = new System.Drawing.Point(211, 915);
            this.tbError.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbError.Multiline = true;
            this.tbError.Name = "tbError";
            this.tbError.ReadOnly = true;
            this.tbError.Size = new System.Drawing.Size(1080, 95);
            this.tbError.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 918);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "last error message";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // tbAuthToken
            // 
            this.tbAuthToken.Location = new System.Drawing.Point(211, 224);
            this.tbAuthToken.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbAuthToken.Name = "tbAuthToken";
            this.tbAuthToken.ReadOnly = true;
            this.tbAuthToken.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbAuthToken.Size = new System.Drawing.Size(1080, 26);
            this.tbAuthToken.TabIndex = 6;
            this.tbAuthToken.TextChanged += new System.EventHandler(this.tbAuthToken_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 227);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(158, 20);
            this.label4.TabIndex = 5;
            this.label4.Text = "Saved Access Token";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // tbEndPointString
            // 
            this.tbEndPointString.Location = new System.Drawing.Point(211, 19);
            this.tbEndPointString.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbEndPointString.Name = "tbEndPointString";
            this.tbEndPointString.Size = new System.Drawing.Size(378, 26);
            this.tbEndPointString.TabIndex = 2;
            this.tbEndPointString.WordWrap = false;
            this.tbEndPointString.TextChanged += new System.EventHandler(this.tbEndPointString_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 22);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Inloox-Odata-URL";
            // 
            // tbUserPassword
            // 
            this.tbUserPassword.Location = new System.Drawing.Point(211, 185);
            this.tbUserPassword.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbUserPassword.Name = "tbUserPassword";
            this.tbUserPassword.PasswordChar = '*';
            this.tbUserPassword.Size = new System.Drawing.Size(1080, 26);
            this.tbUserPassword.TabIndex = 1;
            this.tbUserPassword.UseSystemPasswordChar = true;
            this.tbUserPassword.WordWrap = false;
            this.tbUserPassword.TextChanged += new System.EventHandler(this.tbUserPassword_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 190);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(109, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Access Token";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(256, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Email address";
            this.label1.Visible = false;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(397, 15);
            this.tbUserName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(378, 26);
            this.tbUserName.TabIndex = 0;
            this.tbUserName.Visible = false;
            this.tbUserName.WordWrap = false;
            this.tbUserName.TextChanged += new System.EventHandler(this.tbUserName_TextChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lHintToOkay);
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Controls.Add(this.bGetAuthToken);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tbUserName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 1031);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1313, 63);
            this.panel1.TabIndex = 11;
            // 
            // lHintToOkay
            // 
            this.lHintToOkay.AutoSize = true;
            this.lHintToOkay.Location = new System.Drawing.Point(397, 22);
            this.lHintToOkay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lHintToOkay.Name = "lHintToOkay";
            this.lHintToOkay.Size = new System.Drawing.Size(618, 24);
            this.lHintToOkay.TabIndex = 8;
            this.lHintToOkay.Text = "You have to do a successfull check for authtoken and regexpr before you can hit O" +
    "K";
            this.lHintToOkay.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lHintToOkay.UseCompatibleTextRendering = true;
            // 
            // InlooxLogin
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(1313, 1094);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InlooxLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Check the authentification token and set the project regexp";
            this.Load += new System.EventHandler(this.form_Load);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel panel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tbEndPointString;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbUserPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Button bCheck;
        private System.Windows.Forms.TextBox tbAuthToken;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbError;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox cb_checked;
        private System.Windows.Forms.Button bSaveAuthToken;
        private System.Windows.Forms.CheckBox cb_saved;
        private System.Windows.Forms.Button bGetAuthToken;
        private System.Windows.Forms.TextBox tbTokenPath;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button cbRemoveTokenFromRegistry;
        private System.Windows.Forms.TextBox tbOdataPathString;
        private System.Windows.Forms.TextBox tbUrl;
        private System.Windows.Forms.Label lPrjNrRegExpr;
        private System.Windows.Forms.TextBox tbPrjNrRegExpr;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox cbKeepItOpen;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label lTestPathContent;
        private System.Windows.Forms.TextBox tbDefaultRegExpr;
        private System.Windows.Forms.TextBox tbPathContent;
        private System.Windows.Forms.Label lTestResult;
        private System.Windows.Forms.Button bSaveRegExpr;
        private System.Windows.Forms.Button bCheckRegExpr;
        private System.Windows.Forms.Label lDrive;
        private System.Windows.Forms.TextBox tbProjectNumber;
        private System.Windows.Forms.TextBox tbUnc;
        private System.Windows.Forms.TextBox tbDrive;
        private System.Windows.Forms.Label lProjectNumber;
        private System.Windows.Forms.Label lUnc;
        private System.Windows.Forms.Label lDivision;
        private System.Windows.Forms.TextBox tbDivision;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox cbRegExprCheckResult;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button bResetRegExpr;
        private System.Windows.Forms.CheckBox cb_regexp_saved;
        private System.Windows.Forms.CheckBox cb_regexp_checked;
        private System.Windows.Forms.Label lHintToOkay;
    }
}