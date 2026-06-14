using Microsoft.EntityFrameworkCore;
using zuroWa.Core.Data;
using zuroWa.Core.Domain.ZicZacZu;

namespace zuroWa.Core.Logic.ZicZacZu;

public class ZicZacZuService(AppDbContext appDbContext)
{
    // Create a new game session
    // Generate 2 random codes for 2 players, since this is just a small app, generate only 4 letters, easier to join
    public async Task<Game> CreateGameAsync()
    {
        string playerXCode = Guid.NewGuid().ToString("N").Substring(0,4).ToUpper();
        string playerOCode = Guid.NewGuid().ToString("N").Substring(0,4).ToUpper();

        while (playerXCode.Equals(playerOCode))
        {
            playerOCode = Guid.NewGuid().ToString("N").Substring(0,4).ToUpper();
        }

        Game game = new Game
        {
            PlayerXCode = playerXCode,
            PlayerOCode = playerOCode,
            GameStatus = Game.Status.Waiting
        };

        appDbContext.Add(game);

        await appDbContext.SaveChangesAsync();

        return game;
    }

    public async Task<Game?> JoinGameAsync(string code)
    {
        // Check if code exists in db, if not, return null
        // If yes, and game status is waiting, change it to inprogress and return it
        var game = await appDbContext.Games.FirstOrDefaultAsync(
            g => g.PlayerOCode == code || g.PlayerXCode == code);

        if (game != null)
        {
            if (game.GameStatus == Game.Status.Finished)
            {
                // If the game is finished, show the result and remove the game from db
                appDbContext.Games.Remove(game);
                await appDbContext.SaveChangesAsync();

                return game;
            }
            
            // Creator already have X Code, the lobby should only transition to InProgress when O joins (someone use O code)
            if (game.GameStatus == Game.Status.Waiting && game.PlayerOCode == code)
            {
                game.GameStatus = Game.Status.InProgress;
                await appDbContext.SaveChangesAsync();
            }

            return game;
        }

        return null;
    }

    public async Task<Game?> MakeMoveAsync(string code, int cellIndex)
    {
        Game? game = await appDbContext.Games
            .FirstOrDefaultAsync(g => g.PlayerXCode == code || g.PlayerOCode == code);

        if (game != null)
        {
            if (game.GameStatus != Game.Status.InProgress ||
                (code == game.PlayerXCode && game.PlayerTurn != 'X') ||
                (code == game.PlayerOCode && game.PlayerTurn != 'O') ||
                cellIndex < 0 || cellIndex > 24 ||
                game.BoardState[cellIndex] != '.')
            {
                return game;
            }

            var board = game.BoardState.ToCharArray();

            char currentPlayer = code == game.PlayerXCode ? 'X' : 'O';
            board[cellIndex] = currentPlayer;

            game.BoardState = new string(board);

            if (!CheckWin(game.BoardState, currentPlayer))
            {
                // Draw
                if (!game.BoardState.Contains('.') && game.Winner == null)
                {
                    game.GameStatus = Game.Status.Finished;
                }
                game.PlayerTurn = currentPlayer == 'X' ? 'O' : 'X';
                await appDbContext.SaveChangesAsync();
                return game;
            }

            // Have a winner
            game.Winner = currentPlayer;
            game.PlayerTurn = currentPlayer == 'X' ? 'O' : 'X';
            game.GameStatus = Game.Status.Finished;

            await appDbContext.SaveChangesAsync();
            return game;
        }

        return null;
    }

    private bool CheckWin(string board, char player)
    {
        // 4 directions -> horizontal, vertical and 2 diagonals
        int[][] directions = [ [ 0, 1 ], [ 1, 0 ], [ 1, 1 ], [ 1, -1 ] ];

        // Loop through all index
        for (int i = 0; i < board.Length; i++)
        {
            // For each index
            
            // Determine what row and col of that index
            int row = i / 5;
            int col = i % 5;

            // Check each direction from that index
            foreach (var direction in directions)
            {
                int count = 0;

                // From each direction, check in 4 steps
                for (int step = 0; step < 4; step++)
                {
                    // Row and Col of each index from that direction
                    // step == 0 -> r == row and c == col
                    int r = row + direction[0] * step;
                    int c = col + direction[1] * step;

                    // Check if out of bound 5x5 grid
                    if (r < 0 || r >= 5 || c < 0 || c >= 5) break; 
                    
                    // Check if not that player move
                    if (board[r * 5 + c] != player) break;

                    // Else plus 1 count
                    count++;
                }

                if (count == 4) return true;
            }
        }

        return false;
    }

    public async Task<Game?> GetByCodeAsync(string code)
    {
        Game? game = await appDbContext.Games
            .FirstOrDefaultAsync(g => g.PlayerXCode == code || g.PlayerOCode == code);

        return game;
    }
}