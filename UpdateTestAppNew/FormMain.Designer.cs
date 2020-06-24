namespace UpdateTestAppNew
{
    partial class FormMain
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
            this.buttonHelloWorld = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonHelloWorld
            // 
            this.buttonHelloWorld.Location = new System.Drawing.Point(12, 12);
            this.buttonHelloWorld.Name = "buttonHelloWorld";
            this.buttonHelloWorld.Size = new System.Drawing.Size(165, 67);
            this.buttonHelloWorld.TabIndex = 1;
            this.buttonHelloWorld.Text = "Klick mich!";
            this.buttonHelloWorld.UseVisualStyleBackColor = true;
            this.buttonHelloWorld.Click += new System.EventHandler(this.buttonHelloWorld_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(189, 91);
            this.Controls.Add(this.buttonHelloWorld);
            this.Name = "FormMain";
            this.Text = "UpdateTest";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonHelloWorld;
    }
}

