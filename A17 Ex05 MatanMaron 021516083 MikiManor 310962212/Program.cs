using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Ex05_Othelo
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            OtheloUI othello = new OtheloUI();
            othello.RunGame();
        }
    }

    public class OtheloUI
    {
        private static MainMenu m_MyMenu;
        internal static GameEngine m_GameEngine;
        private static GameBoard m_GameBoard;
        internal static bool gameOver = false;
        internal static bool isPlayerOne = true;
        internal static int menuSelection;

        public OtheloUI()
        {
            m_MyMenu = new MainMenu();
        }

        internal void RunGame()
        {
            Application.Run(m_MyMenu);
        }

        internal static void StartPlay(int i_boardSize, int i_menuSelection)
        {
            m_GameEngine = new GameEngine();
            m_GameBoard = new GameBoard();
            
            if (i_menuSelection == 1)
            {
                PlayerVsPlayer();
            }
            else if (i_menuSelection == 2)
            {
                PlayerVsComputer();
            }
        }

        private static void PlayerVsPlayer()
        {
            menuSelection = 1;
            m_GameBoard.GetPlayerName("Player 1 - What is your name? ");
            string name1 = m_GameBoard.GetName();
            m_GameEngine.CreateFirstPlayer(name1);
            m_GameBoard.GetPlayerName("Player 2 - What is your name? ");
            string name2 = m_GameBoard.GetName();
            m_GameEngine.CreateSecondPlayer(name2);
            gamefirstMove();
        }

        private static void PlayerVsComputer()
        {
            menuSelection = 2;
            m_GameBoard.GetPlayerName("Player 1 - What is your name?");
            string name = m_GameBoard.GetName();
            m_GameEngine.CreateFirstPlayer(name);
            m_GameEngine.CreateComputerPlayer();
            gamefirstMove();
        }

        internal static void gamefirstMove()
        {
            m_GameBoard.Show();
            m_GameEngine.CreateBoard(OtheloBoard.BoardSize);
            m_GameBoard.FirstDrawBoard(m_GameEngine.Board);
            m_GameBoard.DrawBoard(m_GameEngine.Board);
            ShowMoves();
        }

        private static void ShowMoves()
        {
            Point[] validpointlist;
            if (isPlayerOne)
            {
                validpointlist = m_GameEngine.AvalibleMoves(m_GameEngine.Board, Piece.Black, Piece.White);
            }
            else
            {
                validpointlist = m_GameEngine.AvalibleMoves(m_GameEngine.Board, Piece.White, Piece.Black);
            }
            m_GameBoard.DrawMoves(m_GameEngine.Board, validpointlist);
        }
        internal static void gameMoves(Point playerPoint)
        { 
            if (isPlayerOne)
            {
                   m_GameEngine.HumanMove(playerPoint, isPlayerOne);
            }
            else
            {
                if (menuSelection == 1)
                {
                    m_GameEngine.HumanMove(playerPoint, isPlayerOne);
                }
                else if (menuSelection == 2)
                {
                    m_GameEngine.MakePcMove(m_GameEngine.Board, Piece.White, Piece.Black);
                }
            }
        }

            internal static void gameNextMoves()
        {
            int numOfValidMovesForPlayer1 = m_GameEngine.AvalibleMoves(m_GameEngine.Board, Piece.Black, Piece.White).Length;
            int numOfValidMovesForPlayer2 = m_GameEngine.AvalibleMoves(m_GameEngine.Board, Piece.White, Piece.Black).Length;
            m_GameBoard.DrawBoard(m_GameEngine.Board);
            if (numOfValidMovesForPlayer1 == 0 && numOfValidMovesForPlayer2 == 0)
            {
                gameOver = true;
            }
            else if (isPlayerOne)
            {
                if (numOfValidMovesForPlayer1 == 0 && numOfValidMovesForPlayer2 != 0)
                {
                    MessageBox.Show("Black, No Moves! Next Turn....");
                    isPlayerOne = !isPlayerOne;
                }
            }
            else if (!isPlayerOne)
            {
                if (numOfValidMovesForPlayer1 != 0 && numOfValidMovesForPlayer2 == 0)
                {
                    MessageBox.Show("White, No Moves! Next Turn....");
                    isPlayerOne = !isPlayerOne;
                }
            }

            Point scoreOfPlayers = m_GameEngine.ScoreCount(m_GameEngine.Board);
            int scoreOfPlayer1 = scoreOfPlayers.X;
            int scoreOfPlayer2 = scoreOfPlayers.Y;
            string winnerPlayer = "None";
            if (scoreOfPlayer1 > scoreOfPlayer2)
            {
                winnerPlayer = m_GameEngine.Player1.PlayerName;
            }
            else if (scoreOfPlayer1 < scoreOfPlayer2)
            {
                winnerPlayer = m_GameEngine.Player2.PlayerName;
            }
            else
            {
                winnerPlayer = "No winner here, this is even...";
            }

            if (gameOver)
            {
                string msg = string.Format("GameOver! Black: {0}, White: {1}, And the winner is : {2}", m_GameEngine.ScoreCount(m_GameEngine.Board).X, m_GameEngine.ScoreCount(m_GameEngine.Board).Y, winnerPlayer);
                DialogResult  result = MessageBox.Show(msg, "capt", MessageBoxButtons.YesNo);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    m_GameBoard.Dispose();
                    gameOver = false;
                    isPlayerOne = true;
                    StartPlay(OtheloBoard.BoardSize, menuSelection);
                }
                else
                {
                    Application.Exit();
                }
            }
            ShowMoves();
        }
    }
}

