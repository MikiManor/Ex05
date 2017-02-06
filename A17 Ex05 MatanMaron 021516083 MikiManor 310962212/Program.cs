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
            Application.Run(new MainMenu());

        }

        public class OtheloUI
        {
            private static GameEngine m_GameEngine;

            public OtheloUI()
            {
                m_GameEngine = new GameEngine();
            }

            internal static void StartPlay(int i_boardSize, int i_menuSelection)
            {
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
                GameBoard.ActiveForm.ShowDialog();
                Console.WriteLine("Player 1 - What is your name? ");
                m_GameEngine.CreateFirstPlayer(Console.ReadLine());
                Console.WriteLine("Player 2 - What is your name? ");
                m_GameEngine.CreateSecondPlayer(Console.ReadLine());
            }

            private static void PlayerVsComputer()
            {
                GameBoard.ActiveForm.ShowDialog();
                
                string name= GameBoard.ActiveForm.GetPlayerName("Player 1 - What is your name?");
                m_GameEngine.CreateFirstPlayer(name);
                m_GameEngine.CreateComputerPlayer();
            }
        }
    }
}
