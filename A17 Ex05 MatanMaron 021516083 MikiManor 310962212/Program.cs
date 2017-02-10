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
        private static GameEngine m_GameEngine;
        private static GameBoard m_GameBoard;
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
            bool keepPlaying = true;
        }

        private static void PlayerVsPlayer()
        {
            Console.WriteLine("Player 1 - What is your name? ");
            m_GameEngine.CreateFirstPlayer(Console.ReadLine());
            Console.WriteLine("Player 2 - What is your name? ");
            m_GameEngine.CreateSecondPlayer(Console.ReadLine());
        }

        private static void PlayerVsComputer()
        {
            m_GameBoard.GetPlayerName("Player 1 - What is your name?");
            string name = m_GameBoard.ReadPlayerName();
            m_GameEngine.CreateFirstPlayer(name);
            m_GameEngine.CreateComputerPlayer();
            gameMoves();
        }

        internal static void gameMoves()
        {
            GameBoard gameBoard = new GameBoard();
            m_GameEngine.CreateBoard(OtheloBoard.BoardSize);
            m_GameBoard.FirstDrawBoard(m_GameEngine.Board);
            m_GameBoard.DrawBoard(m_GameEngine.Board);
      

        }

    }
}

