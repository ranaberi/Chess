namespace ChessLogic
{
    /// <summary>
    /// The player enum is used to store which player turn it is and who won the game.
    /// It is also used to represent the color of the chess pieces.
    /// </summary>
    public enum Player
    {
        //None is used to represent the game state when no player has won (in case of a draw).
        None,
        White,
        Black
    }

    
    public static class PlayerExtensions 
    {
        /// <summary>
        /// Finds the opponent of a given player.
        /// </summary>
        /// <param name="player"></param>
        /// <returns>the player's opponent</returns>
        public static Player Opponent (this Player player)
        {
            return player switch
            {
                Player.Black => Player.White,
                Player.White => Player.Black,
                _ => Player.None,
            };
        }
    }
}
