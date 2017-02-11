using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Ex05_Othelo
{
    internal class GameBoard : Form
    {
        const int k_Size = 60;
        const int k_Space = k_Size + 10;
        TextBox playerName;
        TextBox playerNameHere;
        Point playerPoint;
        PictureBox pcMove;
        List<Button> buttonOnBoardList = new List<Button>();
        Image BlackPiece = A17_Ex05_MatanMaron_021516083_MikiManor_310962212.Properties.Resources.black;
        Image WhitePiece = A17_Ex05_MatanMaron_021516083_MikiManor_310962212.Properties.Resources.white;
        Image PcMoveImage = A17_Ex05_MatanMaron_021516083_MikiManor_310962212.Properties.Resources.pc;

        public GameBoard()
        {
            this.InitializeComponent();
            pcMove = new PictureBox();
            pcMove.Height = 150;
            pcMove.Width = 150;
            pcMove.Image = PcMoveImage;
            pcMove.BackColor = Color.White;
            this.pcMove.SizeMode = PictureBoxSizeMode.StretchImage;
            pcMove.Hide();
            this.Controls.Add(pcMove);
            pcMove.Left = (this.ClientSize.Width - pcMove.Width) / 2;
            pcMove.Top = (20);
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
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].BackgroundImage = BlackPiece;
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].BackgroundImageLayout = ImageLayout.Stretch;
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].BackColor = Color.Green;
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].Enabled = false;
                    }
                    else if (cellValue == Piece.White)
                    {
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].BackgroundImage = WhitePiece;
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].BackgroundImageLayout = ImageLayout.Stretch;
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].BackColor = Color.Green;
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].Enabled = false;
                    }
                    else
                    {
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].BackColor = System.Drawing.Color.Green;
                        buttonOnBoardList[columnsCounter * OtheloBoard.BoardSize + rowsCounter].Enabled = false;
                    }
                }
            }
            this.Refresh();
        }

        public void DrawMoves(Piece[,] Matrix, Point[] validpointlist)
        {
            foreach (Point item in validpointlist)
            {
                buttonOnBoardList[item.X * OtheloBoard.BoardSize + item.Y].BackColor = System.Drawing.Color.GreenYellow;
                buttonOnBoardList[item.X * OtheloBoard.BoardSize + item.Y].Enabled = true;
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
            MoveOnClick(sender, e);
            if (OtheloUI.menuSelection == 2)
            {
                MoveOnClick(sender, e);
            }
        }

        private void MoveOnClick(object sender, EventArgs e)
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
                buttonOnBoardList[i].BackgroundImage = BlackPiece;
                buttonOnBoardList[i].BackgroundImageLayout = ImageLayout.Stretch;
            }
            else
            {
                if (OtheloUI.menuSelection == 1)
                {
                    buttonOnBoardList[i].BackgroundImage = WhitePiece;
                    buttonOnBoardList[i].BackgroundImageLayout = ImageLayout.Stretch;
                }

                else if (OtheloUI.menuSelection == 2)
                {
                    if (OtheloUI.isPlayerOne)
                    {
                        buttonOnBoardList[i].BackgroundImage = BlackPiece;
                        buttonOnBoardList[i].BackgroundImageLayout = ImageLayout.Stretch;
                    }
                    else
                    {
                        pcMove.Show();
                        this.Refresh();
                        System.Threading.Thread.Sleep(2000);
                        pcMove.Hide();
                    }
                }
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