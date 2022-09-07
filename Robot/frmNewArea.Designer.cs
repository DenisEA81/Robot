namespace Robot
{
    partial class frmNewArea
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNewArea));
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlSettings = new System.Windows.Forms.Panel();
            this.nudLevelFieldSizeY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.nudLevelFieldSizeX = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.nudLevelNumber = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.pnlSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelFieldSizeY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelFieldSizeX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(272, 212);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 38);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "  Отмена";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlSettings
            // 
            this.pnlSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSettings.Controls.Add(this.nudLevelNumber);
            this.pnlSettings.Controls.Add(this.label4);
            this.pnlSettings.Controls.Add(this.nudLevelFieldSizeY);
            this.pnlSettings.Controls.Add(this.label3);
            this.pnlSettings.Controls.Add(this.label2);
            this.pnlSettings.Controls.Add(this.nudLevelFieldSizeX);
            this.pnlSettings.Controls.Add(this.label1);
            this.pnlSettings.Location = new System.Drawing.Point(12, 12);
            this.pnlSettings.Name = "pnlSettings";
            this.pnlSettings.Size = new System.Drawing.Size(526, 179);
            this.pnlSettings.TabIndex = 4;
            // 
            // nudLevelFieldSizeY
            // 
            this.nudLevelFieldSizeY.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.nudLevelFieldSizeY.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudLevelFieldSizeY.Increment = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudLevelFieldSizeY.Location = new System.Drawing.Point(320, 29);
            this.nudLevelFieldSizeY.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.nudLevelFieldSizeY.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudLevelFieldSizeY.Name = "nudLevelFieldSizeY";
            this.nudLevelFieldSizeY.Size = new System.Drawing.Size(86, 34);
            this.nudLevelFieldSizeY.TabIndex = 5;
            this.nudLevelFieldSizeY.Value = new decimal(new int[] {
            768,
            0,
            0,
            0});
            this.nudLevelFieldSizeY.Leave += new System.EventHandler(this.nudLevelFieldSizeX_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(417, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 27);
            this.label3.TabIndex = 4;
            this.label3.Text = "клеток";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(288, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 27);
            this.label2.TabIndex = 3;
            this.label2.Text = "X";
            // 
            // nudLevelFieldSizeX
            // 
            this.nudLevelFieldSizeX.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.nudLevelFieldSizeX.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudLevelFieldSizeX.Increment = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudLevelFieldSizeX.Location = new System.Drawing.Point(196, 29);
            this.nudLevelFieldSizeX.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.nudLevelFieldSizeX.Minimum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.nudLevelFieldSizeX.Name = "nudLevelFieldSizeX";
            this.nudLevelFieldSizeX.Size = new System.Drawing.Size(86, 34);
            this.nudLevelFieldSizeX.TabIndex = 1;
            this.nudLevelFieldSizeX.Value = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.nudLevelFieldSizeX.Leave += new System.EventHandler(this.nudLevelFieldSizeX_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(28, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 27);
            this.label1.TabIndex = 0;
            this.label1.Text = "Размеры уровня";
            // 
            // btnOK
            // 
            this.btnOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(176, 212);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(90, 38);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "  OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // nudLevelNumber
            // 
            this.nudLevelNumber.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.nudLevelNumber.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.nudLevelNumber.Location = new System.Drawing.Point(196, 96);
            this.nudLevelNumber.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLevelNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudLevelNumber.Name = "nudLevelNumber";
            this.nudLevelNumber.Size = new System.Drawing.Size(86, 34);
            this.nudLevelNumber.TabIndex = 7;
            this.nudLevelNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(28, 96);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 27);
            this.label4.TabIndex = 6;
            this.label4.Text = "№ уровня";
            // 
            // frmNewArea
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ClientSize = new System.Drawing.Size(550, 274);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.pnlSettings);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNewArea";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmNewArea";
            this.Load += new System.EventHandler(this.frmNewArea_Load);
            this.pnlSettings.ResumeLayout(false);
            this.pnlSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelFieldSizeY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelFieldSizeX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLevelNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlSettings;
        public System.Windows.Forms.NumericUpDown nudLevelFieldSizeY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.NumericUpDown nudLevelFieldSizeX;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.NumericUpDown nudLevelNumber;
        private System.Windows.Forms.Label label4;
    }
}