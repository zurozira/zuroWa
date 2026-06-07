namespace zuroWa.Core.Domain.ZicZacZu;

public class Game
{
    public int Id { get; set; }
    
    public string PlayerXCode { get; set; } = string.Empty;
    public string PlayerOCode { get; set; } = string.Empty;
    
    public string BoardState { get; set; } = "........................."; // 5x5 grid
    
    public char PlayerTurn { get; set; } = 'X'; // 'X' for X, 'O' for O
    
    public char? Winner { get; set; } // 'X' for X, 'O' for O -> char? means nullable
    
    public enum Status { Waiting, InProgress, Finished }
    public Status GameStatus { get; set; } = Status.Waiting;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}