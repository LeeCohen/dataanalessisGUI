namespace dataanalessisGUI
{
    partial class simultionDays
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
            System.Windows.Forms.Label label4;
            this.buttonRunBydive = new System.Windows.Forms.Button();
            this.inputPanel = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.CB_CalcByPrecentian = new System.Windows.Forms.CheckBox();
            this.TB_PrecenteForPrecentian = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.TB_DaysForMarkov = new System.Windows.Forms.TextBox();
            this.TB_daysForStdForTheMarkov = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.zeroBuyTypeComboBox = new System.Windows.Forms.ComboBox();
            this.b_SelectOutPath = new System.Windows.Forms.Button();
            this.b_selectBid = new System.Windows.Forms.Button();
            this.b_selectAsk = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.t_lenthZerobuyStd = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.t_minStd = new System.Windows.Forms.TextBox();
            this.t_maxStd = new System.Windows.Forms.TextBox();
            this.t_zerobuyForReg = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.t_MinProfitPerMill = new System.Windows.Forms.TextBox();
            this.ch_isLearnFromFile = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.t_checkTestMode = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.comboBoxPaperType = new System.Windows.Forms.ComboBox();
            this.t_MaxProfitPerMill = new System.Windows.Forms.TextBox();
            this.t_textStartDate = new System.Windows.Forms.TextBox();
            this.t_textOrderValue = new System.Windows.Forms.TextBox();
            this.t_textdivideTime = new System.Windows.Forms.TextBox();
            this.t_textDaysBack = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.t_textMaxLos = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.t_textEndDate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.t_textAverageShift = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.t_textNumberOfSTD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.setUpB = new System.Windows.Forms.Button();
            this.t_textOutFile = new System.Windows.Forms.TextBox();
            this.t_textFileBid = new System.Windows.Forms.TextBox();
            this.t_textFIleAsk = new System.Windows.Forms.TextBox();
            this.labelstatus = new System.Windows.Forms.Label();
            this.panelOutput = new System.Windows.Forms.Panel();
            this.t_inPutArray = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmb_TypeOfArray = new System.Windows.Forms.ComboBox();
            this.t_checkBoxRoll = new System.Windows.Forms.CheckBox();
            this.b_runOnVectors = new System.Windows.Forms.Button();
            this.p_progressBar = new System.Windows.Forms.ProgressBar();
            this.label15 = new System.Windows.Forms.Label();
            this.textProgress = new System.Windows.Forms.TextBox();
            this.b_Stop = new System.Windows.Forms.Button();
            this.d_openLeanFronFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.t_dateViwe = new System.Windows.Forms.TextBox();
            this.b_runOnAllTypes = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            label4 = new System.Windows.Forms.Label();
            this.inputPanel.SuspendLayout();
            this.panelOutput.SuspendLayout();
            this.SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(48, 94);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(68, 13);
            label4.TabIndex = 12;
            label4.Text = "Out FIle path";
            // 
            // buttonRunBydive
            // 
            this.buttonRunBydive.Location = new System.Drawing.Point(12, 507);
            this.buttonRunBydive.Name = "buttonRunBydive";
            this.buttonRunBydive.Size = new System.Drawing.Size(342, 34);
            this.buttonRunBydive.TabIndex = 1;
            this.buttonRunBydive.Text = "Run";
            this.buttonRunBydive.UseVisualStyleBackColor = true;
            this.buttonRunBydive.Click += new System.EventHandler(this.buttonRunBydive_Click);
            // 
            // inputPanel
            // 
            this.inputPanel.Controls.Add(this.label20);
            this.inputPanel.Controls.Add(this.CB_CalcByPrecentian);
            this.inputPanel.Controls.Add(this.TB_PrecenteForPrecentian);
            this.inputPanel.Controls.Add(this.label19);
            this.inputPanel.Controls.Add(this.label18);
            this.inputPanel.Controls.Add(this.TB_DaysForMarkov);
            this.inputPanel.Controls.Add(this.TB_daysForStdForTheMarkov);
            this.inputPanel.Controls.Add(this.label17);
            this.inputPanel.Controls.Add(this.label14);
            this.inputPanel.Controls.Add(this.zeroBuyTypeComboBox);
            this.inputPanel.Controls.Add(this.b_SelectOutPath);
            this.inputPanel.Controls.Add(this.b_selectBid);
            this.inputPanel.Controls.Add(this.b_selectAsk);
            this.inputPanel.Controls.Add(this.button2);
            this.inputPanel.Controls.Add(this.label29);
            this.inputPanel.Controls.Add(this.t_lenthZerobuyStd);
            this.inputPanel.Controls.Add(this.label28);
            this.inputPanel.Controls.Add(this.label27);
            this.inputPanel.Controls.Add(this.label26);
            this.inputPanel.Controls.Add(this.label25);
            this.inputPanel.Controls.Add(this.t_minStd);
            this.inputPanel.Controls.Add(this.t_maxStd);
            this.inputPanel.Controls.Add(this.t_zerobuyForReg);
            this.inputPanel.Controls.Add(this.label24);
            this.inputPanel.Controls.Add(this.label23);
            this.inputPanel.Controls.Add(this.label22);
            this.inputPanel.Controls.Add(this.label21);
            this.inputPanel.Controls.Add(this.t_MinProfitPerMill);
            this.inputPanel.Controls.Add(this.ch_isLearnFromFile);
            this.inputPanel.Controls.Add(this.textBox1);
            this.inputPanel.Controls.Add(this.t_checkTestMode);
            this.inputPanel.Controls.Add(this.label16);
            this.inputPanel.Controls.Add(this.comboBoxPaperType);
            this.inputPanel.Controls.Add(this.t_MaxProfitPerMill);
            this.inputPanel.Controls.Add(this.t_textStartDate);
            this.inputPanel.Controls.Add(this.t_textOrderValue);
            this.inputPanel.Controls.Add(this.t_textdivideTime);
            this.inputPanel.Controls.Add(this.t_textDaysBack);
            this.inputPanel.Controls.Add(this.label13);
            this.inputPanel.Controls.Add(this.t_textMaxLos);
            this.inputPanel.Controls.Add(this.label10);
            this.inputPanel.Controls.Add(this.label12);
            this.inputPanel.Controls.Add(this.label7);
            this.inputPanel.Controls.Add(this.label9);
            this.inputPanel.Controls.Add(this.label11);
            this.inputPanel.Controls.Add(this.label6);
            this.inputPanel.Controls.Add(this.t_textEndDate);
            this.inputPanel.Controls.Add(this.label8);
            this.inputPanel.Controls.Add(this.t_textAverageShift);
            this.inputPanel.Controls.Add(this.label5);
            this.inputPanel.Controls.Add(this.t_textNumberOfSTD);
            this.inputPanel.Controls.Add(label4);
            this.inputPanel.Controls.Add(this.label2);
            this.inputPanel.Controls.Add(this.label1);
            this.inputPanel.Controls.Add(this.setUpB);
            this.inputPanel.Controls.Add(this.t_textOutFile);
            this.inputPanel.Controls.Add(this.t_textFileBid);
            this.inputPanel.Controls.Add(this.t_textFIleAsk);
            this.inputPanel.Location = new System.Drawing.Point(12, -1);
            this.inputPanel.Name = "inputPanel";
            this.inputPanel.Size = new System.Drawing.Size(714, 494);
            this.inputPanel.TabIndex = 2;
            this.inputPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.inputPanel_Paint);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label20.Location = new System.Drawing.Point(48, 386);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(61, 13);
            this.label20.TabIndex = 57;
            this.label20.Text = "Precentian:";
            // 
            // CB_CalcByPrecentian
            // 
            this.CB_CalcByPrecentian.AutoSize = true;
            this.CB_CalcByPrecentian.Checked = true;
            this.CB_CalcByPrecentian.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_CalcByPrecentian.Location = new System.Drawing.Point(329, 382);
            this.CB_CalcByPrecentian.Name = "CB_CalcByPrecentian";
            this.CB_CalcByPrecentian.Size = new System.Drawing.Size(300, 17);
            this.CB_CalcByPrecentian.TabIndex = 56;
            this.CB_CalcByPrecentian.Text = "Calculate by precentian of differnce between high and low";
            this.CB_CalcByPrecentian.UseVisualStyleBackColor = true;
            // 
            // TB_PrecenteForPrecentian
            // 
            this.TB_PrecenteForPrecentian.Location = new System.Drawing.Point(220, 383);
            this.TB_PrecenteForPrecentian.Name = "TB_PrecenteForPrecentian";
            this.TB_PrecenteForPrecentian.Size = new System.Drawing.Size(79, 20);
            this.TB_PrecenteForPrecentian.TabIndex = 55;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(132, 386);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 13);
            this.label19.TabIndex = 54;
            this.label19.Text = "Precente:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.ForeColor = System.Drawing.SystemColors.Highlight;
            this.label18.Location = new System.Drawing.Point(562, 266);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(99, 13);
            this.label18.TabIndex = 53;
            this.label18.Text = "Markov Parameters";
            // 
            // TB_DaysForMarkov
            // 
            this.TB_DaysForMarkov.Location = new System.Drawing.Point(609, 336);
            this.TB_DaysForMarkov.Name = "TB_DaysForMarkov";
            this.TB_DaysForMarkov.Size = new System.Drawing.Size(79, 20);
            this.TB_DaysForMarkov.TabIndex = 52;
            // 
            // TB_daysForStdForTheMarkov
            // 
            this.TB_daysForStdForTheMarkov.Location = new System.Drawing.Point(609, 288);
            this.TB_daysForStdForTheMarkov.Name = "TB_daysForStdForTheMarkov";
            this.TB_daysForStdForTheMarkov.Size = new System.Drawing.Size(79, 20);
            this.TB_daysForStdForTheMarkov.TabIndex = 51;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(518, 339);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(85, 13);
            this.label17.TabIndex = 50;
            this.label17.Text = "Days for Markov";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(518, 295);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(63, 13);
            this.label14.TabIndex = 49;
            this.label14.Text = "Days for std";
            // 
            // zeroBuyTypeComboBox
            // 
            this.zeroBuyTypeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zeroBuyTypeComboBox.FormattingEnabled = true;
            this.zeroBuyTypeComboBox.Location = new System.Drawing.Point(527, 430);
            this.zeroBuyTypeComboBox.Name = "zeroBuyTypeComboBox";
            this.zeroBuyTypeComboBox.Size = new System.Drawing.Size(121, 21);
            this.zeroBuyTypeComboBox.TabIndex = 48;
            // 
            // b_SelectOutPath
            // 
            this.b_SelectOutPath.Location = new System.Drawing.Point(51, 130);
            this.b_SelectOutPath.Name = "b_SelectOutPath";
            this.b_SelectOutPath.Size = new System.Drawing.Size(75, 23);
            this.b_SelectOutPath.TabIndex = 47;
            this.b_SelectOutPath.Text = "select";
            this.b_SelectOutPath.UseVisualStyleBackColor = true;
            this.b_SelectOutPath.Click += new System.EventHandler(this.b_SelectOutPath_Click);
            // 
            // b_selectBid
            // 
            this.b_selectBid.Location = new System.Drawing.Point(268, 59);
            this.b_selectBid.Name = "b_selectBid";
            this.b_selectBid.Size = new System.Drawing.Size(75, 23);
            this.b_selectBid.TabIndex = 46;
            this.b_selectBid.Text = "select";
            this.b_selectBid.UseVisualStyleBackColor = true;
            this.b_selectBid.Click += new System.EventHandler(this.b_selectBid_Click);
            // 
            // b_selectAsk
            // 
            this.b_selectAsk.Location = new System.Drawing.Point(51, 59);
            this.b_selectAsk.Name = "b_selectAsk";
            this.b_selectAsk.Size = new System.Drawing.Size(75, 23);
            this.b_selectAsk.TabIndex = 45;
            this.b_selectAsk.Text = "select";
            this.b_selectAsk.UseVisualStyleBackColor = true;
            this.b_selectAsk.Click += new System.EventHandler(this.b_selectAsk_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(357, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(148, 23);
            this.button2.TabIndex = 44;
            this.button2.Text = "set as diffalte directories";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(289, 266);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(75, 26);
            this.label29.TabIndex = 43;
            this.label29.Text = "lenth form  \r\nzero buy in std";
            this.label29.Click += new System.EventHandler(this.label29_Click);
            // 
            // t_lenthZerobuyStd
            // 
            this.t_lenthZerobuyStd.Location = new System.Drawing.Point(292, 296);
            this.t_lenthZerobuyStd.Name = "t_lenthZerobuyStd";
            this.t_lenthZerobuyStd.Size = new System.Drawing.Size(72, 20);
            this.t_lenthZerobuyStd.TabIndex = 40;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(98, 348);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(47, 13);
            this.label28.TabIndex = 39;
            this.label28.Text = "zero buy";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(101, 295);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(47, 13);
            this.label27.TabIndex = 38;
            this.label27.Text = "zero buy";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(48, 322);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(79, 13);
            this.label26.TabIndex = 37;
            this.label26.Text = "Min present std";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(48, 266);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(82, 13);
            this.label25.TabIndex = 36;
            this.label25.Text = "Max present std";
            // 
            // t_minStd
            // 
            this.t_minStd.Location = new System.Drawing.Point(48, 345);
            this.t_minStd.Name = "t_minStd";
            this.t_minStd.Size = new System.Drawing.Size(44, 20);
            this.t_minStd.TabIndex = 35;
            // 
            // t_maxStd
            // 
            this.t_maxStd.Location = new System.Drawing.Point(48, 292);
            this.t_maxStd.Name = "t_maxStd";
            this.t_maxStd.Size = new System.Drawing.Size(46, 20);
            this.t_maxStd.TabIndex = 34;
            // 
            // t_zerobuyForReg
            // 
            this.t_zerobuyForReg.Location = new System.Drawing.Point(287, 345);
            this.t_zerobuyForReg.Name = "t_zerobuyForReg";
            this.t_zerobuyForReg.Size = new System.Drawing.Size(100, 20);
            this.t_zerobuyForReg.TabIndex = 33;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(273, 322);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(132, 13);
            this.label24.TabIndex = 32;
            this.label24.Text = "prodiction zerobuy by days";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(213, 299);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(54, 13);
            this.label23.TabIndex = 31;
            this.label23.Text = "/1000000";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(213, 348);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(54, 13);
            this.label22.TabIndex = 30;
            this.label22.Text = "/1000000";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(169, 322);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(98, 13);
            this.label21.TabIndex = 29;
            this.label21.Text = "min profit per million";
            // 
            // t_MinProfitPerMill
            // 
            this.t_MinProfitPerMill.Location = new System.Drawing.Point(164, 345);
            this.t_MinProfitPerMill.Name = "t_MinProfitPerMill";
            this.t_MinProfitPerMill.Size = new System.Drawing.Size(43, 20);
            this.t_MinProfitPerMill.TabIndex = 28;
            // 
            // ch_isLearnFromFile
            // 
            this.ch_isLearnFromFile.AutoSize = true;
            this.ch_isLearnFromFile.Checked = true;
            this.ch_isLearnFromFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ch_isLearnFromFile.Enabled = false;
            this.ch_isLearnFromFile.Location = new System.Drawing.Point(417, 348);
            this.ch_isLearnFromFile.Name = "ch_isLearnFromFile";
            this.ch_isLearnFromFile.Size = new System.Drawing.Size(88, 17);
            this.ch_isLearnFromFile.TabIndex = 24;
            this.ch_isLearnFromFile.Text = "learn from file";
            this.ch_isLearnFromFile.UseVisualStyleBackColor = true;
            this.ch_isLearnFromFile.CheckedChanged += new System.EventHandler(this.ch_isLearnFromFile_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(575, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 20);
            this.textBox1.TabIndex = 23;
            // 
            // t_checkTestMode
            // 
            this.t_checkTestMode.AutoSize = true;
            this.t_checkTestMode.Location = new System.Drawing.Point(502, 32);
            this.t_checkTestMode.Name = "t_checkTestMode";
            this.t_checkTestMode.Size = new System.Drawing.Size(70, 17);
            this.t_checkTestMode.TabIndex = 22;
            this.t_checkTestMode.Text = "testMode";
            this.t_checkTestMode.UseVisualStyleBackColor = true;
            this.t_checkTestMode.CheckedChanged += new System.EventHandler(this.t_checkTestMode_CheckedChanged);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(551, 68);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(61, 13);
            this.label16.TabIndex = 18;
            this.label16.Text = "pepar Type";
            // 
            // comboBoxPaperType
            // 
            this.comboBoxPaperType.FormattingEnabled = true;
            this.comboBoxPaperType.Location = new System.Drawing.Point(554, 84);
            this.comboBoxPaperType.Name = "comboBoxPaperType";
            this.comboBoxPaperType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPaperType.TabIndex = 17;
            this.comboBoxPaperType.SelectedIndexChanged += new System.EventHandler(this.comboBoxPaperType_SelectedIndexChanged);
            this.comboBoxPaperType.SelectedValueChanged += new System.EventHandler(this.comboBoxPaperType_SelectedValueChanged);
            // 
            // t_MaxProfitPerMill
            // 
            this.t_MaxProfitPerMill.Location = new System.Drawing.Point(164, 296);
            this.t_MaxProfitPerMill.Name = "t_MaxProfitPerMill";
            this.t_MaxProfitPerMill.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.t_MaxProfitPerMill.Size = new System.Drawing.Size(43, 20);
            this.t_MaxProfitPerMill.TabIndex = 16;
            // 
            // t_textStartDate
            // 
            this.t_textStartDate.Location = new System.Drawing.Point(337, 226);
            this.t_textStartDate.Name = "t_textStartDate";
            this.t_textStartDate.Size = new System.Drawing.Size(100, 20);
            this.t_textStartDate.TabIndex = 16;
            // 
            // t_textOrderValue
            // 
            this.t_textOrderValue.Location = new System.Drawing.Point(337, 174);
            this.t_textOrderValue.Name = "t_textOrderValue";
            this.t_textOrderValue.Size = new System.Drawing.Size(100, 20);
            this.t_textOrderValue.TabIndex = 16;
            // 
            // t_textdivideTime
            // 
            this.t_textdivideTime.BackColor = System.Drawing.SystemColors.Window;
            this.t_textdivideTime.Cursor = System.Windows.Forms.Cursors.Cross;
            this.t_textdivideTime.Location = new System.Drawing.Point(575, 226);
            this.t_textdivideTime.Name = "t_textdivideTime";
            this.t_textdivideTime.Size = new System.Drawing.Size(100, 20);
            this.t_textdivideTime.TabIndex = 16;
            // 
            // t_textDaysBack
            // 
            this.t_textDaysBack.Location = new System.Drawing.Point(199, 226);
            this.t_textDaysBack.Name = "t_textDaysBack";
            this.t_textDaysBack.Size = new System.Drawing.Size(100, 20);
            this.t_textDaysBack.TabIndex = 16;
            this.t_textDaysBack.TextChanged += new System.EventHandler(this.t_textDaysBack_TextChanged);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(161, 266);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(101, 13);
            this.label13.TabIndex = 15;
            this.label13.Text = "max profit per million";
            // 
            // t_textMaxLos
            // 
            this.t_textMaxLos.Location = new System.Drawing.Point(201, 174);
            this.t_textMaxLos.Name = "t_textMaxLos";
            this.t_textMaxLos.Size = new System.Drawing.Size(100, 20);
            this.t_textMaxLos.TabIndex = 16;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(334, 210);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Start date";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(572, 210);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(57, 13);
            this.label12.TabIndex = 15;
            this.label12.Text = "divide time";
            this.label12.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(334, 156);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "order value";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(196, 210);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(81, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Days backward";
            this.label9.Click += new System.EventHandler(this.label6_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(453, 210);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 13);
            this.label11.TabIndex = 14;
            this.label11.Text = "End Date";
            this.label11.Click += new System.EventHandler(this.label8_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 156);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Max lost";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // t_textEndDate
            // 
            this.t_textEndDate.Location = new System.Drawing.Point(456, 226);
            this.t_textEndDate.Name = "t_textEndDate";
            this.t_textEndDate.Size = new System.Drawing.Size(100, 20);
            this.t_textEndDate.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(48, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Avreag shift";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // t_textAverageShift
            // 
            this.t_textAverageShift.Location = new System.Drawing.Point(48, 226);
            this.t_textAverageShift.Name = "t_textAverageShift";
            this.t_textAverageShift.Size = new System.Drawing.Size(100, 20);
            this.t_textAverageShift.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 156);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 14;
            this.label5.Text = "number of STD";
            // 
            // t_textNumberOfSTD
            // 
            this.t_textNumberOfSTD.Location = new System.Drawing.Point(51, 175);
            this.t_textNumberOfSTD.Name = "t_textNumberOfSTD";
            this.t_textNumberOfSTD.Size = new System.Drawing.Size(100, 20);
            this.t_textNumberOfSTD.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(265, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Bid directory";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Ask directory";
            // 
            // setUpB
            // 
            this.setUpB.Location = new System.Drawing.Point(31, 405);
            this.setUpB.Name = "setUpB";
            this.setUpB.Size = new System.Drawing.Size(451, 69);
            this.setUpB.TabIndex = 7;
            this.setUpB.Text = "data in";
            this.setUpB.UseVisualStyleBackColor = true;
            this.setUpB.Click += new System.EventHandler(this.setUpB_Click);
            // 
            // t_textOutFile
            // 
            this.t_textOutFile.Location = new System.Drawing.Point(48, 110);
            this.t_textOutFile.Name = "t_textOutFile";
            this.t_textOutFile.Size = new System.Drawing.Size(185, 20);
            this.t_textOutFile.TabIndex = 2;
            // 
            // t_textFileBid
            // 
            this.t_textFileBid.Location = new System.Drawing.Point(268, 32);
            this.t_textFileBid.Name = "t_textFileBid";
            this.t_textFileBid.Size = new System.Drawing.Size(214, 20);
            this.t_textFileBid.TabIndex = 1;
            // 
            // t_textFIleAsk
            // 
            this.t_textFIleAsk.Location = new System.Drawing.Point(48, 32);
            this.t_textFIleAsk.Name = "t_textFIleAsk";
            this.t_textFIleAsk.Size = new System.Drawing.Size(185, 20);
            this.t_textFIleAsk.TabIndex = 0;
            // 
            // labelstatus
            // 
            this.labelstatus.AutoSize = true;
            this.labelstatus.Location = new System.Drawing.Point(1084, 41);
            this.labelstatus.Name = "labelstatus";
            this.labelstatus.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.labelstatus.Size = new System.Drawing.Size(87, 13);
            this.labelstatus.TabIndex = 3;
            this.labelstatus.Text = "runing not ended";
            // 
            // panelOutput
            // 
            this.panelOutput.Controls.Add(this.t_inPutArray);
            this.panelOutput.Controls.Add(this.label3);
            this.panelOutput.Controls.Add(this.cmb_TypeOfArray);
            this.panelOutput.Controls.Add(this.t_checkBoxRoll);
            this.panelOutput.Controls.Add(this.b_runOnVectors);
            this.panelOutput.Location = new System.Drawing.Point(732, 322);
            this.panelOutput.Name = "panelOutput";
            this.panelOutput.Size = new System.Drawing.Size(534, 172);
            this.panelOutput.TabIndex = 1;
            // 
            // t_inPutArray
            // 
            this.t_inPutArray.Location = new System.Drawing.Point(23, 82);
            this.t_inPutArray.Name = "t_inPutArray";
            this.t_inPutArray.Size = new System.Drawing.Size(217, 20);
            this.t_inPutArray.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "label3";
            // 
            // cmb_TypeOfArray
            // 
            this.cmb_TypeOfArray.FormattingEnabled = true;
            this.cmb_TypeOfArray.Location = new System.Drawing.Point(20, 35);
            this.cmb_TypeOfArray.Name = "cmb_TypeOfArray";
            this.cmb_TypeOfArray.Size = new System.Drawing.Size(121, 21);
            this.cmb_TypeOfArray.TabIndex = 2;
            // 
            // t_checkBoxRoll
            // 
            this.t_checkBoxRoll.AutoSize = true;
            this.t_checkBoxRoll.Location = new System.Drawing.Point(271, 89);
            this.t_checkBoxRoll.Name = "t_checkBoxRoll";
            this.t_checkBoxRoll.Size = new System.Drawing.Size(123, 17);
            this.t_checkBoxRoll.TabIndex = 1;
            this.t_checkBoxRoll.Text = "make all permutation";
            this.t_checkBoxRoll.UseVisualStyleBackColor = true;
            // 
            // b_runOnVectors
            // 
            this.b_runOnVectors.Location = new System.Drawing.Point(271, 112);
            this.b_runOnVectors.Name = "b_runOnVectors";
            this.b_runOnVectors.Size = new System.Drawing.Size(240, 39);
            this.b_runOnVectors.TabIndex = 0;
            this.b_runOnVectors.Text = "run on vectors";
            this.b_runOnVectors.UseVisualStyleBackColor = true;
            this.b_runOnVectors.Click += new System.EventHandler(this.b_runOnVectors_Click);
            // 
            // p_progressBar
            // 
            this.p_progressBar.Location = new System.Drawing.Point(749, 31);
            this.p_progressBar.Name = "p_progressBar";
            this.p_progressBar.Size = new System.Drawing.Size(291, 23);
            this.p_progressBar.TabIndex = 6;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(749, 12);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(116, 13);
            this.label15.TabIndex = 7;
            this.label15.Text = "files red from data base";
            // 
            // textProgress
            // 
            this.textProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textProgress.Cursor = System.Windows.Forms.Cursors.Default;
            this.textProgress.Font = new System.Drawing.Font("Miriam", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.textProgress.Location = new System.Drawing.Point(871, 9);
            this.textProgress.Name = "textProgress";
            this.textProgress.ReadOnly = true;
            this.textProgress.Size = new System.Drawing.Size(127, 21);
            this.textProgress.TabIndex = 8;
            // 
            // b_Stop
            // 
            this.b_Stop.Location = new System.Drawing.Point(732, 507);
            this.b_Stop.Name = "b_Stop";
            this.b_Stop.Size = new System.Drawing.Size(323, 34);
            this.b_Stop.TabIndex = 9;
            this.b_Stop.Text = "Stop";
            this.b_Stop.UseVisualStyleBackColor = true;
            this.b_Stop.Click += new System.EventHandler(this.b_Stop_Click);
            // 
            // t_dateViwe
            // 
            this.t_dateViwe.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.t_dateViwe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.t_dateViwe.Location = new System.Drawing.Point(732, 244);
            this.t_dateViwe.Multiline = true;
            this.t_dateViwe.Name = "t_dateViwe";
            this.t_dateViwe.ReadOnly = true;
            this.t_dateViwe.Size = new System.Drawing.Size(253, 71);
            this.t_dateViwe.TabIndex = 10;
            // 
            // b_runOnAllTypes
            // 
            this.b_runOnAllTypes.Location = new System.Drawing.Point(369, 507);
            this.b_runOnAllTypes.Name = "b_runOnAllTypes";
            this.b_runOnAllTypes.Size = new System.Drawing.Size(318, 34);
            this.b_runOnAllTypes.TabIndex = 11;
            this.b_runOnAllTypes.Text = "run on all types";
            this.b_runOnAllTypes.UseVisualStyleBackColor = true;
            this.b_runOnAllTypes.Click += new System.EventHandler(this.b_runOnAllTypes_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(749, 129);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 80);
            this.textBox2.TabIndex = 12;
            this.textBox2.Text = "This is the Day Reggtion Based  \r\n";
            // 
            // simultionDays
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1278, 553);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.b_runOnAllTypes);
            this.Controls.Add(this.t_dateViwe);
            this.Controls.Add(this.b_Stop);
            this.Controls.Add(this.textProgress);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.p_progressBar);
            this.Controls.Add(this.panelOutput);
            this.Controls.Add(this.labelstatus);
            this.Controls.Add(this.inputPanel);
            this.Controls.Add(this.buttonRunBydive);
            this.Name = "simultionDays";
            this.Text = "Simulation days";
            this.Load += new System.EventHandler(this.simultionDays_Load);
            this.inputPanel.ResumeLayout(false);
            this.inputPanel.PerformLayout();
            this.panelOutput.ResumeLayout(false);
            this.panelOutput.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRunBydive;
        private System.Windows.Forms.Panel inputPanel;
        private System.Windows.Forms.Button setUpB;
        private System.Windows.Forms.TextBox t_textOutFile;
        private System.Windows.Forms.TextBox t_textFileBid;
        private System.Windows.Forms.TextBox t_textFIleAsk;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelstatus;
        private System.Windows.Forms.Panel panelOutput;
        private System.Windows.Forms.TextBox t_textOrderValue;
        private System.Windows.Forms.TextBox t_textMaxLos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox t_textNumberOfSTD;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox t_textStartDate;
        private System.Windows.Forms.TextBox t_textDaysBack;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox t_textAverageShift;
        private System.Windows.Forms.TextBox t_MaxProfitPerMill;
        private System.Windows.Forms.TextBox t_textdivideTime;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox t_textEndDate;
        private System.Windows.Forms.ProgressBar p_progressBar;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textProgress;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox comboBoxPaperType;
        private System.Windows.Forms.CheckBox t_checkBoxRoll;
        private System.Windows.Forms.Button b_runOnVectors;
        private System.Windows.Forms.Button b_Stop;
        private System.Windows.Forms.CheckBox t_checkTestMode;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox ch_isLearnFromFile;
        private System.Windows.Forms.OpenFileDialog d_openLeanFronFileDialog1;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox t_MinProfitPerMill;
        private System.Windows.Forms.TextBox t_zerobuyForReg;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox t_minStd;
        private System.Windows.Forms.TextBox t_maxStd;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox t_lenthZerobuyStd;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button b_selectBid;
        private System.Windows.Forms.Button b_selectAsk;
        private System.Windows.Forms.TextBox t_dateViwe;
        private System.Windows.Forms.Button b_SelectOutPath;
        private System.Windows.Forms.Button b_runOnAllTypes;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox t_inPutArray;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmb_TypeOfArray;
        private System.Windows.Forms.ComboBox zeroBuyTypeComboBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox TB_DaysForMarkov;
        private System.Windows.Forms.TextBox TB_daysForStdForTheMarkov;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox TB_PrecenteForPrecentian;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.CheckBox CB_CalcByPrecentian;
    }
}

