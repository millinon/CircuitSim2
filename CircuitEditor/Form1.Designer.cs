namespace CircuitEditor
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
            this.circuitCanvasControl1 = new CircuitEditor.CircuitCanvasControl();
            this.SuspendLayout();
            // 
            // circuitCanvasControl1
            // 
            this.circuitCanvasControl1.Location = new System.Drawing.Point(108, 12);
            this.circuitCanvasControl1.Name = "circuitCanvasControl1";
            this.circuitCanvasControl1.Size = new System.Drawing.Size(782, 595);
            this.circuitCanvasControl1.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 619);
            this.Controls.Add(this.circuitCanvasControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private CircuitCanvasControl circuitCanvasControl1;
    }
}

