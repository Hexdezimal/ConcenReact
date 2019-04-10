namespace ConcenReact
{
    partial class ConcenReact
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConcenReact));
            this.timerGameTick = new System.Windows.Forms.Timer(this.components);
            this.pbMainGame = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainGame)).BeginInit();
            this.SuspendLayout();
            // 
            // timerGameTick
            // 
            this.timerGameTick.Interval = 35;
            this.timerGameTick.Tick += new System.EventHandler(this.timerGameTick_Tick);
            // 
            // pbMainGame
            // 
            this.pbMainGame.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pbMainGame.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbMainGame.Location = new System.Drawing.Point(86, 30);
            this.pbMainGame.Name = "pbMainGame";
            this.pbMainGame.Size = new System.Drawing.Size(672, 416);
            this.pbMainGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pbMainGame.TabIndex = 0;
            this.pbMainGame.TabStop = false;
            this.pbMainGame.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbMainGame_MouseClick);
            // 
            // ConcenReact
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 511);
            this.Controls.Add(this.pbMainGame);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "ConcenReact";
            this.Text = "ConcenReact";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ConcenReact_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbMainGame)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMainGame;
        private System.Windows.Forms.Timer timerGameTick;
    }
}

