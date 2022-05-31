namespace ImportTrialAccount.Forms
{
    partial class frmMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnImport = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.txtSheetName = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnChangePass = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetPassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setTrainingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateEmailToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.insertTeacherTemplate2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAutoImport = new System.Windows.Forms.Button();
            this.cbbSheetName = new System.Windows.Forms.ComboBox();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.lblError = new System.Windows.Forms.Label();
            this.insertSchoolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.Location = new System.Drawing.Point(23, 115);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(799, 405);
            this.dataGridView1.TabIndex = 12;
            // 
            // btnImport
            // 
            this.btnImport.Location = new System.Drawing.Point(828, 86);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(75, 23);
            this.btnImport.TabIndex = 8;
            this.btnImport.Text = "IMPORT";
            this.btnImport.UseVisualStyleBackColor = true;
            this.btnImport.Click += new System.EventHandler(this.btnImport_ClickAsync);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(909, 56);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(75, 23);
            this.btnRead.TabIndex = 9;
            this.btnRead.Text = "READ FILE";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Location = new System.Drawing.Point(828, 56);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.TabIndex = 10;
            this.btnOpen.Text = "OPEN";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // txtSheetName
            // 
            this.txtSheetName.Location = new System.Drawing.Point(828, 27);
            this.txtSheetName.Name = "txtSheetName";
            this.txtSheetName.PlaceholderText = "Sheet Name";
            this.txtSheetName.Size = new System.Drawing.Size(133, 23);
            this.txtSheetName.TabIndex = 6;
            this.txtSheetName.Text = "Sheet1";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(23, 42);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(600, 23);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "D:\\Projects\\HanhTrangSo\\Import Template\\Template SGK 7.xlsx";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "Đường dẫn tới File Excel";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(909, 86);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "EXPORT";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnChangePass
            // 
            this.btnChangePass.Location = new System.Drawing.Point(828, 115);
            this.btnChangePass.Name = "btnChangePass";
            this.btnChangePass.Size = new System.Drawing.Size(156, 23);
            this.btnChangePass.TabIndex = 13;
            this.btnChangePass.Text = "CHANGE PASS";
            this.btnChangePass.UseVisualStyleBackColor = true;
            this.btnChangePass.Click += new System.EventHandler(this.btnChangePass_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(828, 144);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 23);
            this.button1.TabIndex = 13;
            this.button1.Text = "RESET PASS";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuToolStripMenuItem,
            this.settingToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(990, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuToolStripMenuItem
            // 
            this.menuToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changePassToolStripMenuItem,
            this.resetPassToolStripMenuItem,
            this.setTrainingToolStripMenuItem,
            this.updateEmailToolStripMenuItem,
            this.insertTeacherTemplate2ToolStripMenuItem,
            this.insertSchoolToolStripMenuItem});
            this.menuToolStripMenuItem.Name = "menuToolStripMenuItem";
            this.menuToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.menuToolStripMenuItem.Text = "Menu";
            // 
            // changePassToolStripMenuItem
            // 
            this.changePassToolStripMenuItem.Name = "changePassToolStripMenuItem";
            this.changePassToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.changePassToolStripMenuItem.Text = "Change Pass";
            this.changePassToolStripMenuItem.Click += new System.EventHandler(this.changePassToolStripMenuItem_Click);
            // 
            // resetPassToolStripMenuItem
            // 
            this.resetPassToolStripMenuItem.Name = "resetPassToolStripMenuItem";
            this.resetPassToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.resetPassToolStripMenuItem.Text = "Reset Pass";
            this.resetPassToolStripMenuItem.Click += new System.EventHandler(this.resetPassToolStripMenuItem_Click);
            // 
            // setTrainingToolStripMenuItem
            // 
            this.setTrainingToolStripMenuItem.Name = "setTrainingToolStripMenuItem";
            this.setTrainingToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.setTrainingToolStripMenuItem.Text = "Set Training";
            this.setTrainingToolStripMenuItem.Click += new System.EventHandler(this.setTrainingToolStripMenuItem_Click);
            // 
            // updateEmailToolStripMenuItem
            // 
            this.updateEmailToolStripMenuItem.Name = "updateEmailToolStripMenuItem";
            this.updateEmailToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.updateEmailToolStripMenuItem.Text = "Update Email";
            this.updateEmailToolStripMenuItem.Click += new System.EventHandler(this.updateEmailToolStripMenuItem_Click);
            // 
            // insertTeacherTemplate2ToolStripMenuItem
            // 
            this.insertTeacherTemplate2ToolStripMenuItem.Name = "insertTeacherTemplate2ToolStripMenuItem";
            this.insertTeacherTemplate2ToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.insertTeacherTemplate2ToolStripMenuItem.Text = "Insert Teacher template 2";
            this.insertTeacherTemplate2ToolStripMenuItem.Click += new System.EventHandler(this.insertTeacherTemplate2ToolStripMenuItem_Click);
            // 
            // settingToolStripMenuItem
            // 
            this.settingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setURLToolStripMenuItem});
            this.settingToolStripMenuItem.Name = "settingToolStripMenuItem";
            this.settingToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
            this.settingToolStripMenuItem.Text = "Setting";
            // 
            // setURLToolStripMenuItem
            // 
            this.setURLToolStripMenuItem.Name = "setURLToolStripMenuItem";
            this.setURLToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.setURLToolStripMenuItem.Text = "Set URL";
            this.setURLToolStripMenuItem.Click += new System.EventHandler(this.setURLToolStripMenuItem_Click);
            // 
            // btnAutoImport
            // 
            this.btnAutoImport.Location = new System.Drawing.Point(828, 240);
            this.btnAutoImport.Name = "btnAutoImport";
            this.btnAutoImport.Size = new System.Drawing.Size(156, 23);
            this.btnAutoImport.TabIndex = 13;
            this.btnAutoImport.Text = "AUTO MODE";
            this.btnAutoImport.UseVisualStyleBackColor = true;
            this.btnAutoImport.Click += new System.EventHandler(this.btnAutoImport_Click);
            // 
            // cbbSheetName
            // 
            this.cbbSheetName.FormattingEnabled = true;
            this.cbbSheetName.Location = new System.Drawing.Point(629, 42);
            this.cbbSheetName.Name = "cbbSheetName";
            this.cbbSheetName.Size = new System.Drawing.Size(193, 23);
            this.cbbSheetName.TabIndex = 32;
            // 
            // txtOutput
            // 
            this.txtOutput.Location = new System.Drawing.Point(23, 86);
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.Size = new System.Drawing.Size(317, 23);
            this.txtOutput.TabIndex = 7;
            this.txtOutput.Text = "D:\\Projects\\HanhTrangSo\\Export\\import acc 2012";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(178, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Đường dẫn lưu File Excel Output";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(347, 85);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(475, 23);
            this.progressBar1.TabIndex = 33;
            // 
            // lblProgress
            // 
            this.lblProgress.AutoSize = true;
            this.lblProgress.Location = new System.Drawing.Point(828, 266);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(0, 15);
            this.lblProgress.TabIndex = 5;
            // 
            // lblError
            // 
            this.lblError.AutoSize = true;
            this.lblError.Location = new System.Drawing.Point(828, 308);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 15);
            this.lblError.TabIndex = 5;
            // 
            // insertSchoolToolStripMenuItem
            // 
            this.insertSchoolToolStripMenuItem.Name = "insertSchoolToolStripMenuItem";
            this.insertSchoolToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.insertSchoolToolStripMenuItem.Text = "Insert School";
            this.insertSchoolToolStripMenuItem.Click += new System.EventHandler(this.insertSchoolToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 531);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.cbbSheetName);
            this.Controls.Add(this.btnAutoImport);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnChangePass);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnImport);
            this.Controls.Add(this.btnRead);
            this.Controls.Add(this.btnOpen);
            this.Controls.Add(this.txtSheetName);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnImport;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.TextBox txtSheetName;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnChangePass;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem changePassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetPassToolStripMenuItem;
        private System.Windows.Forms.Button btnAutoImport;
        private System.Windows.Forms.ToolStripMenuItem setTrainingToolStripMenuItem;
        private System.Windows.Forms.ComboBox cbbSheetName;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblProgress;
        private System.Windows.Forms.ToolStripMenuItem updateEmailToolStripMenuItem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblError;
        private System.Windows.Forms.ToolStripMenuItem settingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertTeacherTemplate2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insertSchoolToolStripMenuItem;
    }
}
