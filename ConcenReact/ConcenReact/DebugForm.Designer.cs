﻿namespace ConcenReact
{
    partial class DebugForm
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
            this.richTextBoxDebug = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBoxDebug
            // 
            this.richTextBoxDebug.DetectUrls = false;
            this.richTextBoxDebug.Location = new System.Drawing.Point(12, 12);
            this.richTextBoxDebug.Name = "richTextBoxDebug";
            this.richTextBoxDebug.ReadOnly = true;
            this.richTextBoxDebug.Size = new System.Drawing.Size(744, 426);
            this.richTextBoxDebug.TabIndex = 0;
            this.richTextBoxDebug.Text = "";
            // 
            // DebugForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 450);
            this.Controls.Add(this.richTextBoxDebug);
            this.Name = "DebugForm";
            this.Text = "DebugForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBoxDebug;
    }
}