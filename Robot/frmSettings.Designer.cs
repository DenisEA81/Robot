namespace Robot
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.btnOK = new System.Windows.Forms.Button();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.trbVolumeMusic = new System.Windows.Forms.TrackBar();
            this.lblVolumeMusic = new System.Windows.Forms.Label();
            this.lblVolumeBackSound = new System.Windows.Forms.Label();
            this.trbVolumeBackSound = new System.Windows.Forms.TrackBar();
            this.label6 = new System.Windows.Forms.Label();
            this.lblVolumeCommand = new System.Windows.Forms.Label();
            this.trbVolumeCommand = new System.Windows.Forms.TrackBar();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolumeMusic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolumeBackSound)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolumeCommand)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(223, 294);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 38);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "  OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // pnlSettings
            // 
            this.pnlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSettings.Controls.Add(this.lblVolumeCommand);
            this.pnlSettings.Controls.Add(this.trbVolumeCommand);
            this.pnlSettings.Controls.Add(this.label8);
            this.pnlSettings.Controls.Add(this.lblVolumeBackSound);
            this.pnlSettings.Controls.Add(this.trbVolumeBackSound);
            this.pnlSettings.Controls.Add(this.label6);
            this.pnlSettings.Controls.Add(this.lblVolumeMusic);
            this.pnlSettings.Controls.Add(this.trbVolumeMusic);
            this.pnlSettings.Controls.Add(this.label4);
            this.pnlSettings.Location = new System.Drawing.Point(13, 13);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(606, 259);
            this.pnlSettings.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(319, 294);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 38);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "  Отмена";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(28, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(182, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "Громкость музыки";
            // 
            // trbVolumeMusic
            // 
            this.trbVolumeMusic.Location = new System.Drawing.Point(298, 25);
            this.trbVolumeMusic.Maximum = 100;
            this.trbVolumeMusic.Name = "trbVolumeMusic";
            this.trbVolumeMusic.Size = new System.Drawing.Size(181, 45);
            this.trbVolumeMusic.TabIndex = 7;
            this.trbVolumeMusic.TickFrequency = 10;
            this.trbVolumeMusic.Value = 100;
            this.trbVolumeMusic.ValueChanged += new System.EventHandler(this.trbVolumeMusic_ValueChanged);
            // 
            // lblVolumeMusic
            // 
            this.lblVolumeMusic.AutoSize = true;
            this.lblVolumeMusic.BackColor = System.Drawing.Color.Transparent;
            this.lblVolumeMusic.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVolumeMusic.Location = new System.Drawing.Point(490, 25);
            this.lblVolumeMusic.Name = "lblVolumeMusic";
            this.lblVolumeMusic.Size = new System.Drawing.Size(64, 27);
            this.lblVolumeMusic.TabIndex = 8;
            this.lblVolumeMusic.Text = "100%";
            // 
            // lblVolumeBackSound
            // 
            this.lblVolumeBackSound.AutoSize = true;
            this.lblVolumeBackSound.BackColor = System.Drawing.Color.Transparent;
            this.lblVolumeBackSound.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVolumeBackSound.Location = new System.Drawing.Point(490, 102);
            this.lblVolumeBackSound.Name = "lblVolumeBackSound";
            this.lblVolumeBackSound.Size = new System.Drawing.Size(64, 27);
            this.lblVolumeBackSound.TabIndex = 11;
            this.lblVolumeBackSound.Text = "100%";
            // 
            // trbVolumeBackSound
            // 
            this.trbVolumeBackSound.Location = new System.Drawing.Point(298, 102);
            this.trbVolumeBackSound.Maximum = 100;
            this.trbVolumeBackSound.Name = "trbVolumeBackSound";
            this.trbVolumeBackSound.Size = new System.Drawing.Size(181, 45);
            this.trbVolumeBackSound.TabIndex = 10;
            this.trbVolumeBackSound.TickFrequency = 10;
            this.trbVolumeBackSound.Value = 100;
            this.trbVolumeBackSound.ValueChanged += new System.EventHandler(this.trbVolumeBackSound_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(28, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(264, 27);
            this.label6.TabIndex = 9;
            this.label6.Text = "Громкость фоновых звуков";
            // 
            // lblVolumeCommand
            // 
            this.lblVolumeCommand.AutoSize = true;
            this.lblVolumeCommand.BackColor = System.Drawing.Color.Transparent;
            this.lblVolumeCommand.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblVolumeCommand.Location = new System.Drawing.Point(490, 177);
            this.lblVolumeCommand.Name = "lblVolumeCommand";
            this.lblVolumeCommand.Size = new System.Drawing.Size(64, 27);
            this.lblVolumeCommand.TabIndex = 14;
            this.lblVolumeCommand.Text = "100%";
            // 
            // trbVolumeCommand
            // 
            this.trbVolumeCommand.Location = new System.Drawing.Point(298, 177);
            this.trbVolumeCommand.Maximum = 100;
            this.trbVolumeCommand.Name = "trbVolumeCommand";
            this.trbVolumeCommand.Size = new System.Drawing.Size(181, 45);
            this.trbVolumeCommand.TabIndex = 13;
            this.trbVolumeCommand.TickFrequency = 10;
            this.trbVolumeCommand.Value = 100;
            this.trbVolumeCommand.ValueChanged += new System.EventHandler(this.trbVolumeCommand_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(28, 177);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(251, 27);
            this.label8.TabIndex = 12;
            this.label8.Text = "Громкость речи и команд";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(631, 353);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmSettings";
            this.Opacity = 0.8D;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmSettings";
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolumeMusic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolumeBackSound)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbVolumeCommand)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Panel pnlSettings;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblVolumeCommand;
        public System.Windows.Forms.TrackBar trbVolumeCommand;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblVolumeBackSound;
        public System.Windows.Forms.TrackBar trbVolumeBackSound;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblVolumeMusic;
        public System.Windows.Forms.TrackBar trbVolumeMusic;
        private System.Windows.Forms.Label label4;
    }
}