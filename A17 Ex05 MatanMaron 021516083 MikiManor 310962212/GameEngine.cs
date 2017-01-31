using System;
using System.Collections.Generic;
using System.Text;

namespace Ex05_Othelo
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
            get { return m_Player1; }
        }

        public Player Player2
        {
            get { return m_Player2; }
        }

        public Piece [,] Board
        {
            get { return m_Board.Matrix; }
        }

        public int BoardSize
        {
            get { return BoardSize; }
        }

        public string ComputerName
        {
            get { return m_ComputerName; }
        }

        public void CreateBoard(int i_MatrixSize)
        {
            m_Board = new OtheloBoard(i_MatrixSize);
        }

        public void CreateFirstPlayer(string i_PlayerName)
        {
            m_Player1 = new Player(Piece.Black, i_PlayerName);
        }
        
        public void CreateSecondPlayer(string i_PlayerName)
        {
            m_Player2 = new Player(Piece.White, i_PlayerName);
        }

        public void CreateComputerPlayer()
        {
            m_Player2 = new Player(Piece.White, m_ComputerName);
        }
                
        public bool IsUserInputPointInBoundaries(Point i_UserInputPoint)
        {
            if (i_UserInputPoint.X >= BoardSize || i_UserInputPoint.X < 0 || i_UserInputPoint.Y >= BoardSize || i_UserInputPoint.Y < 0)
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

            if (ValidateMove(i_PlayerPoint, m_Board.Matrix, symbolOfi_CurrentPlayer, symbolOfi_OtherPlayer))
            {
                MakeMove(i_PlayerPoint, m_Board.Matrix, symbolOfi_CurrentPlayer, symbolOfi_OtherPlayer);
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
            tempscore.X = BoardSize * BoardSize;
            tempscore.Y = 0;
            Point goodplay = new Point();
            for (int k = 0; k < validpointlist.GetLength(0); k++)
            {
                Piece[,] tempboard = new Piece[BoardSize , BoardSize];
                for (int i = 0; i < BoardSize; i++)
                {
                    for (int j = 0; j < BoardSize; j++)
                    {
                        tempboard[i,j] = i_Board[i,j];
                    }
                }

                MakeMove(validpointlist[k], tempboard, Piece.White, Piece.Black);

                if ((ScoreCount(tempboard).Y > tempscore.Y) && (ScoreCount(tempboard).X < tempscore.X))
                {
                    tempscore.Y = ScoreCount(tempboard).Y;
                    tempscore.X = ScoreCount(tempboard).X;
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
            validpointlist = AvalibleMoves(i_Board, i_CurrentPlayer, i_OtherPlayer); // list pc moves
            if (validpointlist.Length == 0)
            {
                Console.WriteLine("No moves!");
                return;
            }

            Point pcmove = new Point();
            pcmove = PcAI(i_Board, validpointlist);
            MakeMove(pcmove, i_Board, i_CurrentPlayer, i_OtherPlayer);//pc choose play
            Console.WriteLine("[PC will Play : {0},{1}] ", pcmove.Y +1, (char)((pcmove.X +1)+64));
            System.Console.ReadLine();
        }

        public Point[] AvalibleMoves(Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            Point[] Tempvalidpoint = new Point[BoardSize * BoardSize];
            Point Testpoint = new Point();
            int k = 0;
            int counter = 0;
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    Testpoint.X = j;
                    Testpoint.Y = i;
                    if (ValidateMove(Testpoint, i_Board, i_CurrentPlayer, i_OtherPlayer))
                    {
                        Tempvalidpoint[k].X = Testpoint.X;
                        Tempvalidpoint[k].Y = Testpoint.Y;
                        counter++;
                        k++;
                    }
                }
            }

            Point[] NewValidPoint = new Point[counter];
            for (int i = 0; i < counter; i++)
            {
                NewValidPoint[i].X = Tempvalidpoint[i].X;
                NewValidPoint[i].Y = Tempvalidpoint[i].Y;
            }

            return NewValidPoint;
        }

        public void MakeMove(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            i_Board[Move.Y,Move.X] = i_CurrentPlayer;

            if (ValidateUp(Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = Move.Y - 1; i >= 0; i--)
                {
                    if (i_Board[i, Move.X] == i_OtherPlayer)
                    {
                        i_Board[i, Move.X] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ValidateUpRight(Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = Move.Y - 1, j = Move.X + 1; i >= 0 && j < BoardSize; i--, j++)
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

            if (ValidateRight(Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int j = Move.X + 1; j < BoardSize; j++)
                {
                    if (i_Board[Move.Y, j] == i_OtherPlayer)
                    {
                        i_Board[Move.Y, j] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

                if (ValidateDownRight(Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
                {
                    for (int i = Move.Y + 1, j = Move.X + 1; i < BoardSize && j < BoardSize; i++, j++)
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

            if (ValidateDown(Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = Move.Y + 1; i < BoardSize; i++)
                {
                    if (i_Board[i, Move.X] == i_OtherPlayer)
                    {
                        i_Board[i, Move.X] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ValidateDownLeft(Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = Move.Y + 1, j = Move.X - 1; i < BoardSize && j >= 0; i++, j--)
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

            if (ValidateLeft(Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int j = Move.X - 1; j >= 0; j--)
                {
                    if (i_Board[Move.Y, j] == i_OtherPlayer)
                    {
                        i_Board[Move.Y, j] = i_CurrentPlayer;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (ValidateUpLeft(Move, i_Board, i_CurrentPlayer, i_OtherPlayer))
            {
                for (int i = Move.Y - 1, j = Move.X - 1; i >= 0 && j >= 0; i--, j--)
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

        public bool ValidateUp(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer)
        {
            if (Move.Y <= 0)
            {
                return false;
            }
            else if (i_Board[Move.Y - 1, Move.X] == i_OtherPlayer)
            {
                for (int i = Move.Y - 2; i >= 0; i--)
                {
                    if (i_Board[i, Move.X] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i, Move.X] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }
		
        public bool ValidateUpRight(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer )
        {
            if (Move.Y <= 0 || Move.X >= BoardSize - 1)
            {
                return false;
            }
            else if (i_Board[Move.Y - 1, Move.X + 1] == i_OtherPlayer)
            {
                for (int i = Move.Y - 2, j = Move.X + 2; i >= 0 && j < BoardSize; i--, j++)
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
		
        public bool ValidateRight(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer )
        {
            if (Move.X >= BoardSize - 1)
            {
                return false;
            }
            else if (i_Board[Move.Y, Move.X + 1] == i_OtherPlayer)
            {
                for (int j = Move.X + 2; j < BoardSize; j++)
                {
                    if (i_Board[Move.Y, j] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[Move.Y, j] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }
		
        public bool ValidateDownRight(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer )
        {
            if (Move.Y >= BoardSize - 1 || Move.X >= BoardSize - 1)
            {
                return false;
            }
            else if (i_Board[Move.Y + 1, Move.X + 1] == i_OtherPlayer)
            {
                for (int i = Move.Y + 2, j = Move.X + 2; i < BoardSize && j < BoardSize; i++, j++)
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
		
        public  bool ValidateDown(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer )
        {
            if (Move.Y >= BoardSize - 1)
            {
                return false;
            }
            else if (i_Board[Move.Y + 1, Move.X] == i_OtherPlayer)
            {
                for (int i = Move.Y + 2; i < BoardSize; i++)
                {
                    if (i_Board[i, Move.X] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[i, Move.X] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }
		
        public bool ValidateDownLeft(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer )
        {
            if (Move.Y >= BoardSize - 1 || Move.X <= 0)
            {
                return false;
            }
            else if (i_Board[Move.Y + 1, Move.X - 1] == i_OtherPlayer)
            {
                for (int i = Move.Y + 2, j = Move.X - 2; i < BoardSize && j >= 0; i++, j--)
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
		
        public bool ValidateLeft(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer )
        {
            if (Move.X <= 0)
            {
                return false;
            }
            else if (i_Board[Move.Y, Move.X - 1] == i_OtherPlayer)
            {
                for (int j = Move.X - 2; j >= 0; j--)
                {
                    if (i_Board[Move.Y, j] == i_CurrentPlayer)
                    {
                        return true;
                    }
                    else if (i_Board[Move.Y, j] == Piece.Empty)
                    {
                        return false;
                    }
                }
            }

            return false;
        }
		
        public bool ValidateUpLeft(Point Move, Piece[,] i_Board, Piece i_CurrentPlayer, Piece i_OtherPlayer )
        {
            if (Move.Y <= 0 || Move.X <= 0)
            {
                return false;
            }
            else if (i_Board[Move.Y - 1, Move.X - 1] == i_OtherPlayer)
            {
                for (int i = Move.Y - 2, j = Move.X - 2; i >= 0 && j >= 0; i--, j--)
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
            for (int i = 0; i < BoardSize; i++)
            {
                for (int j = 0; j < BoardSize; j++)
                {
                    if (i_Board[i,j] == Piece.Black)
                    {
                        score.X++;
                    }
                    else if (i_Board[i,j] == Piece.White)
                    {
                        score.Y++;
                    }
                }
            }

            return score;
        }

         public static void MakeNewGame()
        {
            GameBoard newGame = new GameBoard();
            newGame.DrawBoard();
            m_Board
        }
    }
}
