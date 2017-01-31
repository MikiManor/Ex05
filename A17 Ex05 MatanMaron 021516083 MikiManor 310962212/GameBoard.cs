using System.Collections.Generic;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    internal class GameBoard : Form
    {
        public GameBoard()
        {
            this.InitializeComponent();
            this.ShowDialog();
        }

        private void InitializeComponent()
        {
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            List<PictureBox> PictureList = new List<PictureBox>();
            for (int i = 0; i < OtheloBoard.BoardSize; i++)
            {
                for (int j = 0; j < OtheloBoard.BoardSize; j++)
                {
                    PictureBox picture = new PictureBox();
                    picture.Size = new System.Drawing.Size(20, 20);
                    picture.Top = (i * 20 + 2);
                    picture.Left = (j * 20 + 2);
                    picture.BackColor = System.Drawing.Color.Green;
                    this.Controls.Add(picture);
                    PictureList.Add(picture);
                }
            }
        }
    }
}