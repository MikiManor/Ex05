using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_Othelo
{
    public enum Piece
    {
        Empty,
        Black,
        White
    }

    public class OtheloBoard
    {
        private static int m_MatrixSize = 6;
        private static Piece[,] m_MatrixCells;

        public Piece[,] Matrix
        {
            get { return m_MatrixCells; }
        }

        public static int BoardSize
        {
            get { return m_MatrixSize; }
            set
            {
                if (value >= 13)
                {
                    m_MatrixSize = 6;
                }
                else
                {
                    m_MatrixSize = value;
                }  
            }
        }

        public OtheloBoard(int i_MatrixSize)
        {
            m_MatrixSize = i_MatrixSize;
            BoardFirstInitialization();
        }

        private void BoardFirstInitialization()
        {
            m_MatrixCells = new Piece[m_MatrixSize,m_MatrixSize];
            for (int rowsCounter = 0; rowsCounter < m_MatrixSize; rowsCounter++)
                for (int columnsCounter = 0; columnsCounter < m_MatrixSize; columnsCounter++)
                {
                    m_MatrixCells[rowsCounter, columnsCounter]= Piece.Empty;
                }
            m_MatrixCells[(m_MatrixSize / 2) - 1, (m_MatrixSize / 2) - 1 ] = Piece.Black;
            m_MatrixCells[(m_MatrixSize / 2) - 1, (m_MatrixSize / 2) ] = Piece.White;
            m_MatrixCells[(m_MatrixSize / 2), (m_MatrixSize / 2) - 1 ] = Piece.White;
            m_MatrixCells[(m_MatrixSize / 2), (m_MatrixSize / 2) ] = Piece.Black;

            GameBoard.DrawBoard();
        }

        private Piece GetCellValue(int i_RowNumber, int i_ColumnNumber)
        {
            return m_MatrixCells[i_RowNumber, i_ColumnNumber];
        }

        private void SetCellValue(Point i_CellLoaction, Piece i_CellValue)
        {
            int cellRow = i_CellLoaction.Y;
            int cellColumn = i_CellLoaction.X;
            m_MatrixCells[cellRow, cellColumn] = i_CellValue;
        }
    }
}
