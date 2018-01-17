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
            this.tbServer = new System.Windows.Forms.TextBox();
            this.lblServer = new System.Windows.Forms.Label();
            this.lblUserName = new System.Windows.Forms.Label();
            this.tbUserName = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnStore = new System.Windows.Forms.Button();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.btnLocalFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFiles
            // 
            this.btnFiles.Location = new System.Drawing.Point(13, 149);
            this.btnFiles.Name = "btnFiles";
            this.btnFiles.Size = new System.Drawing.Size(118, 23);
            this.btnFiles.TabIndex = 1;
            this.btnFiles.Text = "Load Remote Files";
            this.btnFiles.UseVisualStyleBackColor = true;
            this.btnFiles.Click += new System.EventHandler(this.btnFiles_Click);
            // 
            // lbFiles
            // 
            this.lbFiles.FormattingEnabled = true;
            this.lbFiles.Location = new System.Drawing.Point(13, 180);
            this.lbFiles.Name = "lbFiles";
            this.lbFiles.Size = new System.Drawing.Size(375, 485);
            this.lbFiles.TabIndex = 2;
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(97, 99);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(63, 13);
            this.lblOutput.TabIndex = 3;
            this.lblOutput.Text = "H:\\Desktop";
            // 
            // btnOutputDir
            // 
            this.btnOutputDir.Location = new System.Drawing.Point(13, 94);
            this.btnOutputDir.Name = "btnOutputDir";
            this.btnOutputDir.Size = new System.Drawing.Size(78, 23);
            this.btnOutputDir.TabIndex = 4;
            this.btnOutputDir.Text = "Output Dir";
            this.btnOutputDir.UseVisualStyleBackColor = true;
            this.btnOutputDir.Click += new System.EventHandler(this.btnOutputDir_Click);
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(137, 150);
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
            this.lblStatus.Location = new System.Drawing.Point(12, 676);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 13);
            this.lblStatus.TabIndex = 6;
            this.lblStatus.Text = "...";
            // 
            // tbServer
            // 
            this.tbServer.Location = new System.Drawing.Point(97, 12);
            this.tbServer.Name = "tbServer";
            this.tbServer.Size = new System.Drawing.Size(291, 20);
            this.tbServer.TabIndex = 7;
            this.tbServer.Text = "FTPS.NATIONALLIFE.COM";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Location = new System.Drawing.Point(12, 15);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(79, 13);
            this.lblServer.TabIndex = 8;
            this.lblServer.Text = "Server Address";
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Location = new System.Drawing.Point(12, 41);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(60, 13);
            this.lblUserName.TabIndex = 10;
            this.lblUserName.Text = "User Name";
            // 
            // tbUserName
            // 
            this.tbUserName.Location = new System.Drawing.Point(97, 38);
            this.tbUserName.Name = "tbUserName";
            this.tbUserName.Size = new System.Drawing.Size(291, 20);
            this.tbUserName.TabIndex = 9;
            this.tbUserName.Text = "RALotterAgencyBuilders";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new System.Drawing.Point(12, 67);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(53, 13);
            this.lblPassword.TabIndex = 12;
            this.lblPassword.Text = "Password";
            // 
            // btnStore
            // 
            this.btnStore.Location = new System.Drawing.Point(346, 62);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(42, 23);
            this.btnStore.TabIndex = 13;
            this.btnStore.Text = "Store";
            this.btnStore.UseVisualStyleBackColor = true;
            this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(97, 64);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(243, 20);
            this.tbPassword.TabIndex = 11;
            // 
            // btnLocalFile
            // 
            this.btnLocalFile.Location = new System.Drawing.Point(13, 121);
            this.btnLocalFile.Name = "btnLocalFile";
            this.btnLocalFile.Size = new System.Drawing.Size(375, 23);
            this.btnLocalFile.TabIndex = 14;
            this.btnLocalFile.Text = "Load and Process Local File";
            this.btnLocalFile.UseVisualStyleBackColor = true;
            this.btnLocalFile.Click += new System.EventHandler(this.btnLocalFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(401, 696);
            this.Controls.Add(this.btnLocalFile);
            this.Controls.Add(this.btnStore);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.tbPassword);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.tbUserName);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.tbServer);
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
        private System.Windows.Forms.TextBox tbServer;
        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox tbUserName;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Button btnLocalFile;
    }
}

