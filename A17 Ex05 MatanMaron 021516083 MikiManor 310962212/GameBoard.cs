using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    internal class GameBoard : Form
    {
        private const int k_Size = 60;
        private const int k_Space = k_Size + 10;
        private TextBox playerNameText;
        private TextBox playerNameHereText;
        private Point playerPoint;
        private PictureBox pcMove;
        private List<Button> buttonOnBoardList = new List<Button>();
        private Image blackPiecePicture = A17_Ex05_MatanMaron_021516083_MikiManor_310962212.Properties.Resources.black;
        private Image whitePiecePicture = A17_Ex05_MatanMaron_021516083_MikiManor_310962212.Properties.Resources.white;
        private Image pcMovePicture = A17_Ex05_MatanMaron_021516083_MikiManor_310962212.Properties.Resources.pc;

        public GameBoard()
        {
            this.initializeComponent();
            this.pcMove = new PictureBox();
            this.pcMove.Height = 100;
            this.pcMove.Width = 100;
            this.pcMove.Image = this.pcMovePicture;
            this.pcMove.BackColor = Color.White;
            this.pcMove.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pcMove.Hide();
            this.Controls.Add(this.pcMove);
            this.pcMove.Left = (this.ClientSize.Width - this.pcMove.Width) / 2;
            this.pcMove.Top = 5;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public string GetName()
        {
            string name = this.playerNameHereText.Text;
            this.Controls.Remove(this.playerNameText);
            this.Controls.Remove(this.playerNameHereText);
            return name;
        }

        public void FirstDrawBoard(Piece[,] matrix)
        {
            for (int i = 0; i < OtheloBoard.BoardSize; i++)
            {
                for (int j = 0; j < OtheloBoard.BoardSize; j++)
                {
                    Button buttonOnBoard = new Button();
                    buttonOnBoard.Click += this.OnClick;
                    buttonOnBoard.Size = new System.Drawing.Size(k_Size, k_Size);
                    if (i == 0 || j == 0)
                    {
                        buttonOnBoard.Location = new System.Drawing.Point(40, 40);
                    }

                    buttonOnBoard.Location = new System.Drawing.Point((i * k_Size) + 40, (j * k_Size) + 40);
                    this.Controls.Add(buttonOnBoard);
                    this.buttonOnBoardList.Add(buttonOnBoard);
                }
            }

            this.Text = "Othello - Black's Turn (" + OtheloUI.m_GameEngine.Player1.PlayerName + ")";
            this.Size = new System.Drawing.Size((k_Space * OtheloBoard.BoardSize) + 40, (k_Space * OtheloBoard.BoardSize) + 60);
        }

        public void DrawBoard(Piece[,] matrix)
        {
            for (int rowsCounter = 0; rowsCounter < OtheloBoard.BoardSize; rowsCounter++)
            {
                for (int columnsCounter = 0; columnsCounter < OtheloBoard.BoardSize; columnsCounter++)
                {
                    Piece cellValue = matrix[rowsCounter, columnsCounter];
                    if (cellValue == Piece.Black)
                    {
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].BackgroundImage = this.blackPiecePicture;
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].BackgroundImageLayout = ImageLayout.Stretch;
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].BackColor = Color.Green;
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].Enabled = false;
                    }
                    else if (cellValue == Piece.White)
                    {
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].BackgroundImage = this.whitePiecePicture;
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].BackgroundImageLayout = ImageLayout.Stretch;
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].BackColor = Color.Green;
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].Enabled = false;
                    }
                    else
                    {
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].BackColor = System.Drawing.Color.Green;
                        this.buttonOnBoardList[(columnsCounter * OtheloBoard.BoardSize) + rowsCounter].Enabled = false;
                    }
                }
            }

            this.Refresh();
        }

        public void DrawMoves(Piece[,] matrix, Point[] validpointlist)
        {
            foreach (Point item in validpointlist)
            {
                this.buttonOnBoardList[(item.X * OtheloBoard.BoardSize) + item.Y].BackColor = System.Drawing.Color.GreenYellow;
                this.buttonOnBoardList[(item.X * OtheloBoard.BoardSize) + item.Y].Enabled = true;
            }
        }

        public void GetPlayerName(string msg)
        {
            this.playerNameText = new TextBox();
            this.playerNameHereText = new TextBox();
            this.playerNameHereText.KeyPress += this.playerNameHere_KeyPress;
            this.playerNameText.Width = 200;
            this.playerNameHereText.Width = 200;
            this.playerNameText.Left = 100;
            this.playerNameText.Top = 100;
            this.playerNameHereText.Left = 100;
            this.playerNameHereText.Top = 130;
            this.playerNameText.ReadOnly = true;
            this.playerNameText.Text = msg;
            this.playerNameHereText.Text = "Here, and press \"Enter\"";
            this.Controls.Add(this.playerNameText);
            this.Controls.Add(this.playerNameHereText);
            this.ActiveControl = this.playerNameHereText;
            this.ShowDialog();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Exit();
        }

        private void initializeComponent()
        {
            this.MaximizeBox = false;
            this.ShowIcon = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = System.Drawing.Color.Green;
            this.Size = new System.Drawing.Size((k_Space * OtheloBoard.BoardSize) + 40, (k_Space * OtheloBoard.BoardSize) + 60);
        }

        private void playerNameHere_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                this.Hide();
            }
        }

        private void OnClick(object sender, EventArgs e)
        {
            this.moveOnClick(sender, e);
            this.DrawBoard(OtheloUI.m_GameEngine.Board);
            if (OtheloUI.menuSelection == 2)
            {
                this.moveOnClick(sender, e);
            }

            OtheloUI.gameNextMoves();
            this.Refresh();
        }

        private void moveOnClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            int i = this.buttonOnBoardList.IndexOf(button);
            int n = OtheloBoard.BoardSize;
            this.playerPoint.X = i / n;
            this.playerPoint.Y = i % n;
            OtheloUI.gameMoves(this.playerPoint);
            this.buttonOnBoardList[i].Enabled = false;
            if (OtheloUI.isPlayerOne)
            {
                this.buttonOnBoardList[i].BackgroundImage = this.blackPiecePicture;
                this.buttonOnBoardList[i].BackgroundImageLayout = ImageLayout.Stretch;
                this.Text = "Othello - Whites's Turn (" + OtheloUI.m_GameEngine.Player2.PlayerName + ")";
            }
            else
            {
                if (OtheloUI.menuSelection == 1)
                {
                    this.buttonOnBoardList[i].BackgroundImage = this.whitePiecePicture;
                    this.buttonOnBoardList[i].BackgroundImageLayout = ImageLayout.Stretch;
                }
                else if (OtheloUI.menuSelection == 2)
                {
                    this.pcMove.Show();
                    this.Refresh();
                    System.Threading.Thread.Sleep(1000);
                    this.pcMove.Hide();
                }

                this.Text = "Othello - Black's Turn (" + OtheloUI.m_GameEngine.Player1.PlayerName + ")";
            }

            OtheloUI.isPlayerOne = !OtheloUI.isPlayerOne;
            this.Refresh();
        }
    }
}