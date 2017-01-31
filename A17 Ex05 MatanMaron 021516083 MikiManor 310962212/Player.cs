using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_Othelo
{
    public class Player
    {
        private readonly Piece r_Symbol;
        private readonly string r_PlayerName;
        private static int m_Score = 0;

        public string PlayerName
        {
            get { return r_PlayerName; }
        }

        public Piece Symbol
        {
            get { return r_Symbol; }
        }

        public int Score
        {
            get { return m_Score; }
        }

        public void IncreaseScore()
        {
            m_Score += m_Score;
        }

        public Player(Piece i_Symbol, string io_PlayerName)
        {
            r_PlayerName = io_PlayerName;
            m_Score = 0;
            r_Symbol = i_Symbol;
        }
        
    }
}
