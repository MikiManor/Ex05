using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    internal class GameBoard : Form
    {
        const int k_Size = 60;
        const int k_Space = k_Size + 10;
        TextBox playerName;
        TextBox playerNameHere;
        Point playerPoint;
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

        public string GetName()
        {
            string name = playerNameHere.Text;
            this.Controls.Remove(playerName);
            this.Controls.Remove(playerNameHere);
            return name;
        }

        public void FirstDrawBoard(Piece[,] Matrix)
        {
            for (int i = 0; i < OtheloBoard.BoardSize; i++)
            {
                for (int j = 0; j < OtheloBoard.BoardSize; j++)
                {
                    Button buttonOnBoard = new Button();
                    buttonOnBoard.Click += OnClick;
                    buttonOnBoard.Size = new System.Drawing.Size(k_Size, k_Size);
                    if (i == 0 || j == 0)
                    {
                        buttonOnBoard.Location = new System.Drawing.Point(40, 40);
                    }
                    buttonOnBoard.Location = new System.Drawing.Point(i * k_Size + 40, j * k_Size + 40);
                    this.Controls.Add(buttonOnBoard);
                    buttonOnBoardList.Add(buttonOnBoard);
                }
            }
            
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
                        buttonOnBoardList[rowsCounter * OtheloBoard.BoardSize + columnsCounter].Enabled = false;
                    }
                    else if (cellValue == Piece.White)
                    {
                        buttonOnBoardList[rowsCounter * OtheloBoard.BoardSize + columnsCounter].BackColor = System.Drawing.Color.White;
                        buttonOnBoardList[rowsCounter * OtheloBoard.BoardSize + columnsCounter].Enabled = false;
                    }
                    else
                    {
                        buttonOnBoardList[rowsCounter * OtheloBoard.BoardSize + columnsCounter].BackColor = System.Drawing.Color.Gray;
                        buttonOnBoardList[rowsCounter * OtheloBoard.BoardSize + columnsCounter].Enabled = false;
                    }
                }
            }
            this.Refresh();
        }

        public void DrawMoves(Piece[,] Matrix, Point[] validpointlist)
        {
            foreach (Point item in validpointlist)
            {
                buttonOnBoardList[item.Y * OtheloBoard.BoardSize + item.X].BackColor = System.Drawing.Color.Green;
                buttonOnBoardList[item.Y * OtheloBoard.BoardSize + item.X].Enabled = true;
            }
        }

        public void GetPlayerName(string msg)
        {
            playerName = new TextBox();
            playerNameHere = new TextBox();
            playerNameHere.KeyPress += PlayerNameHere_KeyPress;
            playerName.Width = 200;
            playerNameHere.Width = 200;
            playerName.Left = 100;
            playerName.Top = 100;
            playerNameHere.Left = 100;
            playerNameHere.Top = 130;
            playerName.ReadOnly = true;
            playerName.Text = msg;
            playerNameHere.Text = "Here, and press \"Enter\"";
            this.Controls.Add(playerName);
            this.Controls.Add(playerNameHere);
            this.ActiveControl = playerNameHere;
            this.ShowDialog();
        }

        private void PlayerNameHere_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                this.Hide();
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int i = buttonOnBoardList.IndexOf(button);
            int n = OtheloBoard.BoardSize;
            playerPoint.X = i / n;
            playerPoint.Y = i % n;
            OtheloUI.gameMoves(playerPoint);
            buttonOnBoardList[i].Enabled = false;
            if (OtheloUI.isPlayerOne)
            {
                buttonOnBoardList[i].BackColor = System.Drawing.Color.Black;
            }
            else
            {
                buttonOnBoardList[i].BackColor = System.Drawing.Color.White;
            }
            OtheloUI.isPlayerOne = !OtheloUI.isPlayerOne;
            this.Refresh();
            OtheloUI.gameNextMoves();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Exit();
        }
    }
}