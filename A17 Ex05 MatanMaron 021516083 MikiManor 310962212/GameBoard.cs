using System.Collections.Generic;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    internal class GameBoard : Form
    {
        const int k_Size = 60;
        const int k_Space = k_Size + 10;

        List<Button> buttonOnBoardList = new List<Button>();
        public GameBoard()
        {
            this.InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void InitializeComponent()
        {
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = System.Drawing.Color.Green;
            this.Size = new System.Drawing.Size(k_Space * OtheloBoard.BoardSize + 40, k_Space * OtheloBoard.BoardSize + 60);
        }

        public void FirstDrawBoard(Piece[,] Matrix)
        {
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
                    buttonOnBoard.Location = new System.Drawing.Point(i * k_Size + 40, j * k_Size + 40);
                    buttonOnBoard.BackColor = System.Drawing.Color.ForestGreen;
                    this.Controls.Add(buttonOnBoard);
                    buttonOnBoardList.Add(buttonOnBoard);
                }
            }
            this.BackColor = System.Drawing.Color.Green;
            this.Size = new System.Drawing.Size(k_Space * OtheloBoard.BoardSize + 40, k_Space * OtheloBoard.BoardSize + 60);
        }
        public void DrawBoard(Piece[,] Matrix)
        {
            for (int rowsCounter = 0; rowsCounter < OtheloBoard.BoardSize; rowsCounter++)
            {
                for (int columnsCounter = 0; columnsCounter < OtheloBoard.BoardSize; columnsCounter++)
                {
                    Piece cellValue = Matrix[rowsCounter, columnsCounter];
                    if (cellValue == Piece.Black)
                    {
                        buttonOnBoardList[rowsCounter * OtheloBoard.BoardSize + columnsCounter].BackColor = System.Drawing.Color.Black;
                    }
                    else if (cellValue == Piece.White)
                    {
                        buttonOnBoardList[rowsCounter * OtheloBoard.BoardSize + columnsCounter].BackColor = System.Drawing.Color.White;
                    }
                }
            }
            this.Refresh();
        }

        public void DrawMoves(Piece[,] Matrix,bool IsPlayer1)
        {
            this.Refresh();
        }

    }
}