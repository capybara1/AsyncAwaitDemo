namespace AsyncAwait.WindowsFormsApplication
{
    partial class MainWindow
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
            this.btnSimpleExample = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSimpleExample
            // 
            this.btnSimpleExample.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSimpleExample.Location = new System.Drawing.Point(12, 12);
            this.btnSimpleExample.Name = "btnSimpleExample";
            this.btnSimpleExample.Size = new System.Drawing.Size(257, 42);
            this.btnSimpleExample.TabIndex = 0;
            this.btnSimpleExample.Text = "Simple Example";
            this.btnSimpleExample.UseVisualStyleBackColor = true;
            this.btnSimpleExample.Click += new System.EventHandler(this.OnSimpleExampleButtonClick);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(281, 66);
            this.Controls.Add(this.btnSimpleExample);
            this.Name = "MainWindow";
            this.Text = "Async/Await (WinForms)";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSimpleExample;
    }
}

