using System.Text;

namespace ChessLogic
{
    public class StateString
    {
        private readonly StringBuilder sb = new StringBuilder();

        public StateString(Player currentPlayer, Board board)
        {
            AddPiecePlacement(board);
            sb.Append(' ');
            AddCurrentPlayer(currentPlayer);
            sb.Append(' ');
            AddCastlingRights(board);
            sb.Append(' ');
            AddEnPassant(board, currentPlayer);
        }

        public override string ToString()
        {
            return sb.ToString();
        }
        private static char PieceChar(Piece piece)
        {
            char c = piece.Type switch
            {
                PieceType.King => 'k',
                PieceType.Queen => 'q',
                PieceType.Rook => 'r',
                PieceType.Bishop => 'b',
                PieceType.Knight => 'n',
                PieceType.Pawn => 'p',
                _ => ' '
            };

            if (piece.Color == Player.White)
            {
                return char.ToUpper(c);
            }
            return c;
        }

        private void AddRowData(Board board, int row)
        {
            int empty = 0;

            for (int col = 0; col < 8; col++)
            {
                if (board[row, col] == null)
                {
                    empty++;
                    continue;
                }

                if (empty > 0)
                {
                    sb.Append(empty);
                    empty = 0;
                }
                sb.Append(PieceChar(board[row, col]));

            }
            if (empty > 0)
            {
                sb.Append(empty);
            }
        }


        private void AddPiecePlacement(Board board)
        {
            for (int row = 0; row < 8; row++)
            {
                if (row != 0)
                {
                    sb.Append('/');
                }
                AddRowData(board, row);
            }
        }

        private void AddCurrentPlayer(Player currentPlayer)
        {
            if (currentPlayer == Player.White)
            {
                sb.Append('w');
            }
            else
            {
                sb.Append('b');
            }
        }

        private void AddCastlingRights(Board board)
        {
            bool castleWKS = board.CastleRightKS(Player.White);
            bool castleWQS = board.CastleRightQS(Player.White);
            bool castleBKS = board.CastleRightKS(Player.Black);
            bool castleBQS = board.CastleRightQS(Player.Black);

            if (!castleWKS || !castleWQS || !castleBKS || !castleBQS)
            {
                sb.Append('-');
                return;
            }
            if (castleWKS)
            {
                sb.Append('K');
            }
            if (castleWQS)
            {
                sb.Append('Q');
            }
            if (castleBKS)
            {
                sb.Append('k');
            }
            if (castleBQS)
            {
                sb.Append('q');
            }
        }

        private void AddEnPassant(Board board, Player currentPlayer)
        {
            if(!board.CanCaptureEnPassant(currentPlayer))
            {
                sb.Append(" -");
                return;
            }
            Position pos = board.GetPawnSkipPosition(currentPlayer.Opponent());
            char file = (char)('a' + pos.Column);
            int rank = 8 - pos.Row;
            sb.Append(file);
            sb.Append(rank);
        }

    }
}

