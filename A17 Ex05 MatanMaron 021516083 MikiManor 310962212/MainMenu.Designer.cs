namespace A17_Ex05_MatanMaron_021516083_MikiManor_310962212
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.buttonChangeBoardSize = new System.Windows.Forms.Button();
            this.buttonPlayVsPc = new System.Windows.Forms.Button();
            this.buttonPlayVsHuman = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonChangeBoardSize
            // 
            this.buttonChangeBoardSize.Location = new System.Drawing.Point(12, 12);
            this.buttonChangeBoardSize.Name = "buttonChangeBoardSize";
            this.buttonChangeBoardSize.Size = new System.Drawing.Size(337, 55);
            this.buttonChangeBoardSize.TabIndex = 0;
            this.buttonChangeBoardSize.Text = "Board &Size: 6x6 (Click to increase)";
            this.buttonChangeBoardSize.UseVisualStyleBackColor = true;
            this.buttonChangeBoardSize.Click += new System.EventHandler(this.buttonChangeBoardSize_Click);
            // 
            // buttonPlayVsPc
            // 
            this.buttonPlayVsPc.Location = new System.Drawing.Point(12, 85);
            this.buttonPlayVsPc.Name = "buttonPlayVsPc";
            this.buttonPlayVsPc.Size = new System.Drawing.Size(153, 55);
            this.buttonPlayVsPc.TabIndex = 1;
            this.buttonPlayVsPc.Text = "Play against the &Computer";
            this.buttonPlayVsPc.UseVisualStyleBackColor = true;
            this.buttonPlayVsPc.Click += new System.EventHandler(this.buttonPlayVsPc_Click);
            // 
            // buttonPlayVsHuman
            // 
            this.buttonPlayVsHuman.Location = new System.Drawing.Point(196, 85);
            this.buttonPlayVsHuman.Name = "buttonPlayVsHuman";
            this.buttonPlayVsHuman.Size = new System.Drawing.Size(153, 55);
            this.buttonPlayVsHuman.TabIndex = 2;
            this.buttonPlayVsHuman.Text = "Play against a &Friend";
            this.buttonPlayVsHuman.UseVisualStyleBackColor = true;
            this.buttonPlayVsHuman.Click += new System.EventHandler(this.buttonPlayVsHuman_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 152);
            this.Controls.Add(this.buttonPlayVsHuman);
            this.Controls.Add(this.buttonPlayVsPc);
            this.Controls.Add(this.buttonChangeBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "MainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othello - Game Settings";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonChangeBoardSize;
        private System.Windows.Forms.Button buttonPlayVsPc;
        private System.Windows.Forms.Button buttonPlayVsHuman;
    }
}