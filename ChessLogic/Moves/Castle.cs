﻿namespace ChessLogic
{
    public class Castle : Move
    {
        public override MoveType Type { get; }
        public override Position FromPos { get; }
        public override Position ToPos { get; }

        private readonly Direction kingMoveDir;
        private readonly Position rookFromPos;
        private readonly Position rookToPos;

        public Castle(MoveType type, Position KingPos)
        {
            Type = type;
            FromPos = KingPos;

            if(type == MoveType.CastleKS)
            {
                kingMoveDir = Direction.East;
                ToPos = new Position(KingPos.Row, 6);
                rookFromPos = new Position(KingPos.Row, 7);
                rookToPos = new Position(KingPos.Row, 5);
            }
            else if(type == MoveType.CastleQS)
            {
                kingMoveDir = Direction.West;
                ToPos = new Position(KingPos.Row, 2);
                rookFromPos = new Position(KingPos.Row, 0);
                rookToPos = new Position(KingPos.Row, 3);
            }
        }

        public override bool Execute(Board board)
        {
            new NormalMove(FromPos, ToPos).Execute(board);
            new NormalMove(rookFromPos, rookToPos).Execute(board);

            return false;
        }

        public override bool IsLegal(Board board)
        {
           Player player = board[FromPos].Color;
           if(board.IsInCheck(player))
           {
               return false;
           }
           Board copy = board.Copy();
           Position KingPosInCopy = FromPos;

          for(int i = 0; i < 2; i++)
          {
              new NormalMove(KingPosInCopy, KingPosInCopy + kingMoveDir).Execute(copy);
              KingPosInCopy += kingMoveDir;
              if(copy.IsInCheck(player))
              {
                    return false;
              }
          }
          return true;
        }
    }
}
