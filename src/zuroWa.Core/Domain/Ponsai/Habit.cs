namespace zuroWa.Core.Domain.Ponsai;

public class Habit
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Emoji { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    
    // This helps Habit to have a HabitLogs navigation property:
    public ICollection<HabitLog> HabitLogs { get; set; } = new List<HabitLog>();

    public string UserId { get; set; } = string.Empty;
    public AppUser? User { get; set; } // nav property to EF Core
}