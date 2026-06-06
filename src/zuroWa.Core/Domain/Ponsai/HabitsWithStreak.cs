namespace zuroWa.Core.Domain.Ponsai;

public class HabitsWithStreak
{
    public Habit Habit { get; set; }
    public int Streak { get; set; }
    
    public bool LoggedToday { get; set; }
}