namespace LSWFTPConnect {
    partial class Form1 {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.btnFiles = new System.Windows.Forms.Button();
            this.lbFiles = new System.Windows.Forms.ListBox();
            this.lblOutput = new System.Windows.Forms.Label();
            this.btnOutputDir = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFiles
            // 
            this.btnFiles.Location = new System.Drawing.Point(15, 50);
            this.btnFiles.Name = "btnFiles";
            this.btnFiles.Size = new System.Drawing.Size(75, 23);
            this.btnFiles.TabIndex = 1;
            this.btnFiles.Text = "Load Files";
            this.btnFiles.UseVisualStyleBackColor = true;
            this.btnFiles.Click += new System.EventHandler(this.btnFiles_Click);
            // 
            // lbFiles
            // 
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.Location = new System.Drawing.Point(13, 129);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(375, 446);
            this.lbFiles.TabIndex = 2;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(139, 29);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(16, 13);
            this.lblOutput.TabIndex = 3;
            this.lblOutput.Text = "...";
            // 
            // btnOutputDir
            // 
            this.btnOutputDir.Location = new System.Drawing.Point(15, 24);
            this.btnOutputDir.Name = "btnOutputDir";
            this.btnOutputDir.Size = new System.Drawing.Size(118, 23);
            this.btnOutputDir.TabIndex = 4;
            this.btnOutputDir.Text = "Output Dir";
            this.btnOutputDir.UseVisualStyleBackColor = true;
            this.btnOutputDir.Click += new System.EventHandler(this.btnOutputDir_Click);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(15, 76);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(118, 23);
            this.btnGo.TabIndex = 5;
            this.btnGo.Text = "Process Selected File";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(15, 110);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "...";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 595);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.btnOutputDir);
            this.Controls.Add(this.lblOutput);
            this.Controls.Add(this.lbFiles);
            this.Controls.Add(this.btnFiles);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnFiles;
        private System.Windows.Forms.ListBox lbFiles;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Button btnOutputDir;
        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.Label lblStatus;
    }
}

