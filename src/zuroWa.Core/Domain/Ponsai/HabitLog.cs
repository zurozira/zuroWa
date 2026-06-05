namespace zuroWa.Core.Domain.Ponsai;

public class HabitLog
{
    public int Id { get; set; }
    public int HabitId { get; set; }
    public Habit Habit { get; set; } = null!; // EF Pattern -> EF will fill this, trust me :)
    public DateOnly LoggedOn { get; set; }
}