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
            const int k_Size = 60;
            const int k_Space = k_Size+10;
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            List<Button> buttonOnBoardList = new List<Button>();
            for (int i = 0; i < OtheloBoard.BoardSize; i++)
            {
                for (int j = 0; j < OtheloBoard.BoardSize; j++)
                {
                    Button buttonOnBoard = new Button();
                    buttonOnBoard.Size = new System.Drawing.Size(k_Size, k_Size);
                    if (i == 0 || j == 0)
                    {
                        buttonOnBoard.Location = new System.Drawing.Point(40, 40);
                    }
                    buttonOnBoard.Location = new System.Drawing.Point(i*k_Size+40, j * k_Size+40);
                    buttonOnBoard.BackColor = System.Drawing.Color.ForestGreen;
                    this.Controls.Add(buttonOnBoard);
                    buttonOnBoardList.Add(buttonOnBoard);
                }
            }
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = System.Drawing.Color.Green;
            this.Size = new System.Drawing.Size(k_Space * OtheloBoard.BoardSize + 40, k_Space * OtheloBoard.BoardSize + 60);
        }
    }
}