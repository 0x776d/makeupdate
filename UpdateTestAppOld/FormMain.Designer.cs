namespace UpdateTestAppOld
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
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.checkBoxStartAfterUpdate = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonUpdate
            // 
            this.buttonUpdate.Location = new System.Drawing.Point(12, 12);
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.Size = new System.Drawing.Size(173, 52);
            this.buttonUpdate.TabIndex = 0;
            this.buttonUpdate.Text = "Update";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.buttonUpdate_Click);
            // 
            // checkBoxStartAfterUpdate
            // 
            this.checkBoxStartAfterUpdate.AutoSize = true;
            this.checkBoxStartAfterUpdate.Location = new System.Drawing.Point(12, 70);
            this.checkBoxStartAfterUpdate.Name = "checkBoxStartAfterUpdate";
            this.checkBoxStartAfterUpdate.Size = new System.Drawing.Size(173, 17);
            this.checkBoxStartAfterUpdate.TabIndex = 1;
            this.checkBoxStartAfterUpdate.Text = "Programm nach Update starten";
            this.checkBoxStartAfterUpdate.UseVisualStyleBackColor = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(199, 105);
            this.Controls.Add(this.checkBoxStartAfterUpdate);
            this.Controls.Add(this.buttonUpdate);
            this.Name = "FormMain";
            this.Text = "UpdateTest";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.CheckBox checkBoxStartAfterUpdate;
    }
}

