using System;
using System.Collections.Generic;
using System.Text;

namespace A17_Ex05_MatanMaron_021516083_MikiManor_310962212
{
    public struct Point
    {
        public int X { get; set; }

        public int Y { get; set; }
    }

    public class GameEngine
    {
        private Player m_Player1, m_Player2;
        private string m_ComputerName = Environment.MachineName;
        private OtheloBoard m_Board;

        public Player Player1
        {
            get { return this.m_Player1; }
        }

        public Player Player2
        {
            get { return this.m_Player2; }
        }

        public Piece[,] Board
        {
            get { return this.m_Board.Matrix; }
        }

        public int BoardSize
        {
            get { return OtheloBoard.BoardSize; }
        }

        public string ComputerName
        {
            get { return this.m_ComputerName; }
        }

        public void CreateBoard(int i_MatrixSize)
        {
            this.m_Board = new OtheloBoard(i_MatrixSize);
        }

        public void CreateFirstPlayer(string i_PlayerName)
        {
            this.m_Player1 = new Player(Piece.Black, i_PlayerName);
        }
        
        public void CreateSecondPlayer(string i_PlayerName)
        {
            this.m_Player2 = new Player(Piece.White, i_PlayerName);
        }

        public void CreateComputerPlayer()
        {
            this.m_Player2 = new Player(Piece.White, this.m_ComputerName);
        }
                
        public bool IsUserInputPointInBoundaries(Point i_UserInputPoint)
        {
            if (i_UserInputPoint.X >= this.BoardSize || i_UserInputPoint.X < 0 || i_UserInputPoint.Y >= this.BoardSize || i_UserInputPoint.Y < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool HumanMove(Point i_PlayerPoint, bool i_IsFirstPlayer)
        {
            Piece symbolOfi_CurrentPlayer = Piece.Empty;
            Piece symbolOfi_OtherPlayer = Piece.Empty;
            if (i_IsFirstPlayer)
            {
                symbolOfi_CurrentPlayer = Piece.Black;
                symbolOfi_OtherPlayer = Piece.White;
            }
            else
            {
                symbolOfi_CurrentPlayer = Piece.White;
                symbolOfi_OtherPlayer = Piece.Black;
            }

            if (this.ValidateMove(i_PlayerPoint, this.m_Board.Matrix, symbolOfi_CurrentPlayer, symbolOfi_OtherPlayer))
            {
                this.MakeMove(i_PlayerPoint, this.m_Board.Matrix, symbolOfi_CurrentPlayer, symbolOfi_OtherPlayer);
                return true;
            }
            else
            {
                return false;
            }
        }

        public Point PcAI(Piece[,] i_Board, Point[] validpointlist)
        {
            Point tempscore = new Point();
            tempscore.X = this.BoardSize * this.BoardSize;
            tempscore.Y = 0;
            Point goodplay = new Point();
            for (int k = 0; k < validpointlist.GetLength(0); k++)
            {
                Piece[,] tempboard = new Piece[this.BoardSize, this.BoardSize];
                for (int i = 0; i < this.BoardSize; i++)
                {
                    for (int j = 0; j < this.BoardSize; j++)
                    {
                        tempboard[i, j] = i_Board[i, j];
                    }
                }

                this.MakeMove(validpointlist[k], tempboard, Piece.White, Piece.Black);

                if ((this.ScoreCount(tempboard).Y > tempscore.Y) && (this.ScoreCount(tempboard).X < tempscore.X))
                {
                    tempscore.Y = this.ScoreCount(tempboard).Y;
                    tempscore.X = this.ScoreCount(tempboard).X;
                    goodplay.X = validpointlist[k].X;
                    goodplay.Y = validpointlist[k].Y;
                }
            }

            return goodplay;
        }

        public bool ValidateMove(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (i_Board[Move.Y, Move.X] != Piece.Empty)
            {
                return false;
            }
            else if (ValidateUp(Move, i_Board, i_CurrentPlayer, i_OtherPlayer) || ValidateUpRight(Move, i_Board, i_CurrentPlayer, i_OtherPlayer) || ValidateRight(Move, i_Board, i_CurrentPlayer, i_OtherPlayer) || ValidateDownRight(Move, i_Board, i_CurrentPlayer, i_OtherPlayer) || ValidateDown(Move, i_Board, i_CurrentPlayer, i_OtherPlayer) || ValidateDownLeft(Move, i_Board, i_CurrentPlayer, i_OtherPlayer) || ValidateLeft(Move, i_Board, i_CurrentPlayer, i_OtherPlayer) || ValidateUpLeft(Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MakePcMove(Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            Point[] validpointlist;
            validpointlist = this.AvalibleMoves(i_Board, i_CurrentPlayer, i_OtherPlayer); // list pc moves
            if (validpointlist.Length == 0)
            {
                return;
            }

            Point pcmove = new Point();
            pcmove = this.PcAI(i_Board, validpointlist);
            this.MakeMove(pcmove, i_Board, i_CurrentPlayer, i_OtherPlayer); // pc choose play
        }

        public Point[] AvalibleMoves(Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            Point[] tempvalidpoint = new Point[OtheloBoard.BoardSize * OtheloBoard.BoardSize];
            Point testpoint = new Point();
            int k = 0;
            int counter = 0;
            for (int i = 0; i < OtheloBoard.BoardSize; i++)
            {
                for (int j = 0; j < OtheloBoard.BoardSize; j++)
                {
                    testpoint.X = j;
                    testpoint.Y = i;
                    if (this.ValidateMove(testpoint, i_Board, i_CurrentPlayer, i_OtherPlayer))
                    {
                        tempvalidpoint[k].X = testpoint.X;
                        tempvalidpoint[k].Y = testpoint.Y;
                        counter++;
                        k++;
                    }
                }
            }

            Point[] newValidPoint = new Point[counter];
            for (int i = 0; i < counter; i++)
            {
                newValidPoint[i].X = tempvalidpoint[i].X;
                newValidPoint[i].Y = tempvalidpoint[i].Y;
            }

            return newValidPoint;
        }

        public void MakeMove(Point i_Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            i_Board[i_Move.Y, i_Move.X] = i_CurrentPlayer;

            if (ValidateUp(i_Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = i_Move.Y - 1; i >= 0; i--)
                {
                    if (i_Board[i, i_Move.X] == i_OtherPlayer)
                    {
                        i_Board[i, i_Move.X] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ValidateUpRight(i_Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = i_Move.Y - 1, j = i_Move.X + 1; i >= 0 && j < BoardSize; i--, j++)
                {
                    if (i_Board[i, j] == i_OtherPlayer)
                    {
                        i_Board[i, j] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ValidateRight(i_Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int j = i_Move.X + 1; j < BoardSize; j++)
                {
                    if (i_Board[i_Move.Y, j] == i_OtherPlayer)
                    {
                        i_Board[i_Move.Y, j] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

                if (ValidateDownRight(i_Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
                {
                    for (int i = i_Move.Y + 1, j = i_Move.X + 1; i < BoardSize && j < BoardSize; i++, j++)
                    {
                        if (i_Board[i, j] == i_OtherPlayer)
                        {
                            i_Board[i, j] = i_CurrentPlayer;
                        }
                        else
                        {
                        break;
                    }
                }  
            }

            if (ValidateDown(i_Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = i_Move.Y + 1; i < BoardSize; i++)
                {
                    if (i_Board[i, i_Move.X] == i_OtherPlayer)
                    {
                        i_Board[i, i_Move.X] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ValidateDownLeft(i_Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = i_Move.Y + 1, j = i_Move.X - 1; i < BoardSize && j >= 0; i++, j--)
                {
                    if (i_Board[i, j] == i_OtherPlayer)
                    {
                        i_Board[i, j] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ValidateLeft(i_Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int j = i_Move.X - 1; j >= 0; j--)
                {
                    if (i_Board[i_Move.Y, j] == i_OtherPlayer)
                    {
                        i_Board[i_Move.Y, j] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ValidateUpLeft(i_Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = i_Move.Y - 1, j = i_Move.X - 1; i >= 0 && j >= 0; i--, j--)
                {
                    if (i_Board[i, j] == i_OtherPlayer)
                    {
                        i_Board[i, j] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        public bool ValidateUp(Point i_Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (i_Move.Y <= 0)
            {
                return false;
            }
            else if (i_Board[i_Move.Y - 1, i_Move.X] == i_OtherPlayer)
            {
                for (int i = i_Move.Y - 2; i >= 0; i--)
                {
                    if (i_Board[i, i_Move.X] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i, i_Move.X] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool ValidateUpRight(Point i_Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (i_Move.Y <= 0 || i_Move.X >= BoardSize - 1)
            {
                return false;
            }
            else if (i_Board[i_Move.Y - 1, i_Move.X + 1] == i_OtherPlayer)
            {
                for (int i = i_Move.Y - 2, j = i_Move.X + 2; i >= 0 && j < BoardSize; i--, j++)
                {
                    if (i_Board[i, j] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i, j] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool ValidateRight(Point i_Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (i_Move.X >= BoardSize - 1)
            {
                return false;
            }
            else if (i_Board[i_Move.Y, i_Move.X + 1] == i_OtherPlayer)
            {
                for (int j = i_Move.X + 2; j < BoardSize; j++)
                {
                    if (i_Board[i_Move.Y, j] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i_Move.Y, j] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool ValidateDownRight(Point i_Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (i_Move.Y >= BoardSize - 1 || i_Move.X >= BoardSize - 1)
            {
                return false;
            }
            else if (i_Board[i_Move.Y + 1, i_Move.X + 1] == i_OtherPlayer)
            {
                for (int i = i_Move.Y + 2, j = i_Move.X + 2; i < BoardSize && j < BoardSize; i++, j++)
                {
                    if (i_Board[i, j] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i, j] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool ValidateDown(Point i_Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (i_Move.Y >= BoardSize - 1)
            {
                return false;
            }
            else if (i_Board[i_Move.Y + 1, i_Move.X] == i_OtherPlayer)
            {
                for (int i = i_Move.Y + 2; i < BoardSize; i++)
                {
                    if (i_Board[i, i_Move.X] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i, i_Move.X] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool ValidateDownLeft(Point i_Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (i_Move.Y >= BoardSize - 1 || i_Move.X <= 0)
            {
                return false;
            }
            else if (i_Board[i_Move.Y + 1, i_Move.X - 1] == i_OtherPlayer)
            {
                for (int i = i_Move.Y + 2, j = i_Move.X - 2; i < BoardSize && j >= 0; i++, j--)
                {
                    if (i_Board[i, j] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i, j] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool ValidateLeft(Point i_Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (i_Move.X <= 0)
            {
                return false;
            }
            else if (i_Board[i_Move.Y, i_Move.X - 1] == i_OtherPlayer)
            {
                for (int j = i_Move.X - 2; j >= 0; j--)
                {
                    if (i_Board[i_Move.Y, j] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i_Move.Y, j] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public bool ValidateUpLeft(Point i_Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (i_Move.Y <= 0 || i_Move.X <= 0)
            {
                return false;
            }
            else if (i_Board[i_Move.Y - 1, i_Move.X - 1] == i_OtherPlayer)
            {
                for (int i = i_Move.Y - 2, j = i_Move.X - 2; i >= 0 && j >= 0; i--, j--)
                {
                    if (i_Board[i, j] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i, j] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public Point ScoreCount(Piece[,] i_Board)
        {
            Point score = new Point();
            score.X = 0;
            score.Y = 0;
            for (int i = 0; i < this.BoardSize; i++)
            {
                for (int j = 0; j < this.BoardSize; j++)
                {
                    if (i_Board[i, j] == Piece.Black)
                    {
                        score.X++;
                    }
                    else if (i_Board[i, j] == Piece.White)
                    {
                        score.Y++;
                    }
                }
            }

            return score;
        }
    }
}
