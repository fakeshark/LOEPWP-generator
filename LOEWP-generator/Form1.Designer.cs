namespace LOEWP_generator
{
    partial class frmLOEWP
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
            this.btnSelectTM = new System.Windows.Forms.Button();
            this.lblSelectedXmlTM = new System.Windows.Forms.Label();
            this.rtbOutputPreview = new System.Windows.Forms.RichTextBox();
            this.btnExportCode = new System.Windows.Forms.Button();
            this.lblOutputPreview = new System.Windows.Forms.Label();
            this.btnCopyToClipboard = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.btnSelectPDF = new System.Windows.Forms.Button();
            this.lblSelectedPdfTM = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.dtpDateOfIssue = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblJulianDate = new System.Windows.Forms.Label();
            this.ckbAddWpComment = new System.Windows.Forms.CheckBox();
            this.btnResetForm = new System.Windows.Forms.Button();
            this.btnGenerateCode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSelectTM
            // 
            this.btnSelectTM.Location = new System.Drawing.Point(28, 33);
            this.btnSelectTM.Name = "btnSelectTM";
            this.btnSelectTM.Size = new System.Drawing.Size(239, 40);
            this.btnSelectTM.TabIndex = 0;
            this.btnSelectTM.Text = "Import TM source code (ExampleTM.xml)";
            this.btnSelectTM.UseVisualStyleBackColor = true;
            this.btnSelectTM.Click += new System.EventHandler(this.btnSelectTM_Click);
            // 
            // lblSelectedXmlTM
            // 
            this.lblSelectedXmlTM.AutoSize = true;
            this.lblSelectedXmlTM.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblSelectedXmlTM.Location = new System.Drawing.Point(32, 17);
            this.lblSelectedXmlTM.Name = "lblSelectedXmlTM";
            this.lblSelectedXmlTM.Size = new System.Drawing.Size(127, 13);
            this.lblSelectedXmlTM.TabIndex = 1;
            this.lblSelectedXmlTM.Text = "XML TM:   none selected";
            // 
            // rtbOutputPreview
            // 
            this.rtbOutputPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOutputPreview.Location = new System.Drawing.Point(32, 164);
            this.rtbOutputPreview.Name = "rtbOutputPreview";
            this.rtbOutputPreview.Size = new System.Drawing.Size(833, 401);
            this.rtbOutputPreview.TabIndex = 2;
            this.rtbOutputPreview.Text = "";
            // 
            // btnExportCode
            // 
            this.btnExportCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportCode.Enabled = false;
            this.btnExportCode.Location = new System.Drawing.Point(744, 571);
            this.btnExportCode.Name = "btnExportCode";
            this.btnExportCode.Size = new System.Drawing.Size(121, 40);
            this.btnExportCode.TabIndex = 3;
            this.btnExportCode.Text = "&Export Code";
            this.btnExportCode.UseVisualStyleBackColor = true;
            this.btnExportCode.Click += new System.EventHandler(this.btnExportCode_Click);
            // 
            // lblOutputPreview
            // 
            this.lblOutputPreview.AutoSize = true;
            this.lblOutputPreview.Location = new System.Drawing.Point(32, 148);
            this.lblOutputPreview.Name = "lblOutputPreview";
            this.lblOutputPreview.Size = new System.Drawing.Size(157, 13);
            this.lblOutputPreview.TabIndex = 4;
            this.lblOutputPreview.Text = "LOEPWP Code output preview:";
            // 
            // btnCopyToClipboard
            // 
            this.btnCopyToClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCopyToClipboard.Location = new System.Drawing.Point(617, 571);
            this.btnCopyToClipboard.Name = "btnCopyToClipboard";
            this.btnCopyToClipboard.Size = new System.Drawing.Size(121, 40);
            this.btnCopyToClipboard.TabIndex = 5;
            this.btnCopyToClipboard.Text = "&Copy to Clipboard";
            this.btnCopyToClipboard.UseVisualStyleBackColor = true;
            this.btnCopyToClipboard.Click += new System.EventHandler(this.btnCopyToClipboard_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(622, 618);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 13);
            this.lblStatus.TabIndex = 6;
            // 
            // btnSelectPDF
            // 
            this.btnSelectPDF.Enabled = false;
            this.btnSelectPDF.Location = new System.Drawing.Point(273, 33);
            this.btnSelectPDF.Name = "btnSelectPDF";
            this.btnSelectPDF.Size = new System.Drawing.Size(239, 40);
            this.btnSelectPDF.TabIndex = 7;
            this.btnSelectPDF.Text = "Import published TM (ExampleTM.pdf)";
            this.btnSelectPDF.UseVisualStyleBackColor = true;
            this.btnSelectPDF.Click += new System.EventHandler(this.btnSelectPDF_Click);
            // 
            // lblSelectedPdfTM
            // 
            this.lblSelectedPdfTM.AutoSize = true;
            this.lblSelectedPdfTM.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblSelectedPdfTM.Location = new System.Drawing.Point(277, 17);
            this.lblSelectedPdfTM.Name = "lblSelectedPdfTM";
            this.lblSelectedPdfTM.Size = new System.Drawing.Size(126, 13);
            this.lblSelectedPdfTM.TabIndex = 8;
            this.lblSelectedPdfTM.Text = "PDF TM:   none selected";
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button2.Location = new System.Drawing.Point(32, 571);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 40);
            this.button2.TabIndex = 9;
            this.button2.Text = "&Quit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.QuitApplication);
            // 
            // dtpDateOfIssue
            // 
            this.dtpDateOfIssue.Location = new System.Drawing.Point(32, 101);
            this.dtpDateOfIssue.Name = "dtpDateOfIssue";
            this.dtpDateOfIssue.Size = new System.Drawing.Size(200, 20);
            this.dtpDateOfIssue.TabIndex = 10;
            this.dtpDateOfIssue.ValueChanged += new System.EventHandler(this.dtpDateOfIssue_ValueChanged);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(32, 85);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(72, 13);
            this.lblDate.TabIndex = 11;
            this.lblDate.Text = "Date of issue:";
            // 
            // lblJulianDate
            // 
            this.lblJulianDate.AutoSize = true;
            this.lblJulianDate.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblJulianDate.Location = new System.Drawing.Point(238, 107);
            this.lblJulianDate.Name = "lblJulianDate";
            this.lblJulianDate.Size = new System.Drawing.Size(75, 13);
            this.lblJulianDate.TabIndex = 12;
            this.lblJulianDate.Text = "Julian Date: ...";
            // 
            // ckbAddWpComment
            // 
            this.ckbAddWpComment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbAddWpComment.AutoSize = true;
            this.ckbAddWpComment.Location = new System.Drawing.Point(713, 141);
            this.ckbAddWpComment.Name = "ckbAddWpComment";
            this.ckbAddWpComment.Size = new System.Drawing.Size(152, 17);
            this.ckbAddWpComment.TabIndex = 13;
            this.ckbAddWpComment.Text = "Insert WP # comment tags";
            this.ckbAddWpComment.UseVisualStyleBackColor = true;
            // 
            // btnResetForm
            // 
            this.btnResetForm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnResetForm.Location = new System.Drawing.Point(159, 571);
            this.btnResetForm.Name = "btnResetForm";
            this.btnResetForm.Size = new System.Drawing.Size(121, 40);
            this.btnResetForm.TabIndex = 14;
            this.btnResetForm.Text = "&Reset Form";
            this.btnResetForm.UseVisualStyleBackColor = true;
            this.btnResetForm.Click += new System.EventHandler(this.btnResetForm_Click);
            // 
            // btnGenerateCode
            // 
            this.btnGenerateCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGenerateCode.Enabled = false;
            this.btnGenerateCode.Location = new System.Drawing.Point(685, 33);
            this.btnGenerateCode.Name = "btnGenerateCode";
            this.btnGenerateCode.Size = new System.Drawing.Size(180, 40);
            this.btnGenerateCode.TabIndex = 15;
            this.btnGenerateCode.Text = "Generate LOEPWP code";
            this.btnGenerateCode.UseVisualStyleBackColor = true;
            this.btnGenerateCode.Click += new System.EventHandler(this.btnGenerateCode_Click);
            // 
            // frmLOEWP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(898, 643);
            this.Controls.Add(this.btnGenerateCode);
            this.Controls.Add(this.btnResetForm);
            this.Controls.Add(this.ckbAddWpComment);
            this.Controls.Add(this.lblJulianDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDateOfIssue);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.lblSelectedPdfTM);
            this.Controls.Add(this.btnSelectPDF);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCopyToClipboard);
            this.Controls.Add(this.lblOutputPreview);
            this.Controls.Add(this.btnExportCode);
            this.Controls.Add(this.rtbOutputPreview);
            this.Controls.Add(this.lblSelectedXmlTM);
            this.Controls.Add(this.btnSelectTM);
            this.Name = "frmLOEWP";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TRG - List of Effective Pages Work Package Code Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectTM;
        private System.Windows.Forms.Label lblSelectedXmlTM;
        private System.Windows.Forms.RichTextBox rtbOutputPreview;
        private System.Windows.Forms.Button btnExportCode;
        private System.Windows.Forms.Label lblOutputPreview;
        private System.Windows.Forms.Button btnCopyToClipboard;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Button btnSelectPDF;
        private System.Windows.Forms.Label lblSelectedPdfTM;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DateTimePicker dtpDateOfIssue;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblJulianDate;
        private System.Windows.Forms.CheckBox ckbAddWpComment;
        private System.Windows.Forms.Button btnResetForm;
        private System.Windows.Forms.Button btnGenerateCode;
    }
}

