using System;
using System.Collections.Generic;
using System.Text;

namespace A17_Ex05_MatanMaron_021516083_MikiManor_310962212
{
    public class Player
    {
        private static int m_Score = 0;
        private readonly Piece r_Symbol;
        private readonly string r_PlayerName;
        public Player(Piece i_Symbol, string io_PlayerName)
        {
            r_PlayerName = io_PlayerName;
            m_Score = 0;
            r_Symbol = i_Symbol;
        }

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
    }
}