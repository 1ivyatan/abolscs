namespace abolscs
{
    partial class Form1
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
            this.molberts = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // molberts
            // 
            this.molberts.BackColor = System.Drawing.Color.Transparent;
            this.molberts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.molberts.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.molberts.Location = new System.Drawing.Point(0, 0);
            this.molberts.Margin = new System.Windows.Forms.Padding(0);
            this.molberts.Name = "molberts";
            this.molberts.Size = new System.Drawing.Size(446, 325);
            this.molberts.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(446, 325);
            this.ControlBox = false;
            this.Controls.Add(this.molberts);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ābols";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Red;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel molberts;


    }
}

