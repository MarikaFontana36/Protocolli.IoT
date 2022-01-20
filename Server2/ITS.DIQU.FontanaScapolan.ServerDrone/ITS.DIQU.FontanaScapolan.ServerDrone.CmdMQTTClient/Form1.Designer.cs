namespace ITS.DIQU.FontanaScapolan.ServerDrone.CmdMQTTClient
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ckbDrone = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ckbLED = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBase = new System.Windows.Forms.Button();
            this.cmbDrone = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ckbDrone);
            this.groupBox1.Location = new System.Drawing.Point(12, 166);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 125);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Accensione";
            // 
            // ckbDrone
            // 
            this.ckbDrone.AutoSize = true;
            this.ckbDrone.Location = new System.Drawing.Point(30, 54);
            this.ckbDrone.Name = "ckbDrone";
            this.ckbDrone.Size = new System.Drawing.Size(121, 24);
            this.ckbDrone.TabIndex = 0;
            this.ckbDrone.Text = "Drone acceso";
            this.ckbDrone.UseVisualStyleBackColor = true;
            this.ckbDrone.CheckStateChanged += new System.EventHandler(this.ckbDrone_CheckChangedAsync);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckbLED);
            this.groupBox2.Location = new System.Drawing.Point(268, 166);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(250, 125);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "LED Posizione";
            // 
            // ckbLED
            // 
            this.ckbLED.AutoSize = true;
            this.ckbLED.Location = new System.Drawing.Point(66, 54);
            this.ckbLED.Name = "ckbLED";
            this.ckbLED.Size = new System.Drawing.Size(108, 24);
            this.ckbLED.TabIndex = 0;
            this.ckbLED.Text = "LED Acceso";
            this.ckbLED.UseVisualStyleBackColor = true;
            this.ckbLED.CheckStateChanged += new System.EventHandler(this.ckbLED_CheckChangedAsync);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnBase);
            this.groupBox3.Location = new System.Drawing.Point(524, 166);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(250, 125);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ritorno alla base";
            // 
            // btnBase
            // 
            this.btnBase.Location = new System.Drawing.Point(48, 51);
            this.btnBase.Name = "btnBase";
            this.btnBase.Size = new System.Drawing.Size(167, 29);
            this.btnBase.TabIndex = 0;
            this.btnBase.Text = "Torna alla base";
            this.btnBase.UseVisualStyleBackColor = true;
            this.btnBase.Click += new System.EventHandler(this.btnBase_Click);
            // 
            // cmbDrone
            // 
            this.cmbDrone.FormattingEnabled = true;
            this.cmbDrone.Location = new System.Drawing.Point(157, 49);
            this.cmbDrone.Name = "cmbDrone";
            this.cmbDrone.Size = new System.Drawing.Size(151, 28);
            this.cmbDrone.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.cmbDrone);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox ckbDrone;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckbLED;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBase;
        private System.Windows.Forms.ComboBox cmbDrone;
    }
}
