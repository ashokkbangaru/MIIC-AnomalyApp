namespace AnomalyDetector
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button BtnPredict;
        private System.Windows.Forms.Panel pluginPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.BtnPredict = new System.Windows.Forms.Button();
            this.pluginPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();

            // BtnPredict
            this.BtnPredict.Location = new System.Drawing.Point(12, 12);
            this.BtnPredict.Name = "BtnPredict";
            this.BtnPredict.Size = new System.Drawing.Size(100, 30);
            this.BtnPredict.TabIndex = 0;
            this.BtnPredict.Text = "Predict";
            this.BtnPredict.UseVisualStyleBackColor = true;
            this.BtnPredict.Click += new System.EventHandler(this.BtnPredict_Click);

            // pluginPanel
            this.pluginPanel.AutoScroll = true;
            this.pluginPanel.Location = new System.Drawing.Point(12, 60);
            this.pluginPanel.Name = "pluginPanel";
            this.pluginPanel.Size = new System.Drawing.Size(360, 380);
            this.pluginPanel.TabIndex = 1;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 461);
            this.Controls.Add(this.pluginPanel);
            this.Controls.Add(this.BtnPredict);
            this.Name = "Form1";
            this.Text = "Anomaly Detector";
            this.ResumeLayout(false);
        }
    }
}
