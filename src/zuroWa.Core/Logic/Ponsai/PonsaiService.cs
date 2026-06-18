using Microsoft.EntityFrameworkCore;
using zuroWa.Core.Data;
using zuroWa.Core.Domain.Ponsai;

namespace zuroWa.Core.Logic.Ponsai;

public class PonsaiService(AppDbContext appDbContext)
{
    private static int CalculateStreak(IEnumerable<DateOnly> logDates)
    {
        // Check empty list first
        if (!logDates.Any()) return 0;
        
        // Sort the IEnumerable list first
        var logs = logDates.OrderByDescending(d => d).ToList();
        
        // Check if the first log is today or yesterday, 
        // if neither, streak is 0
        if (logs[0] != DateOnly.FromDateTime(DateTime.UtcNow)
            && logs[0] != DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-1))
        {
            return 0;
        } 
        
        // Loop and check if a streak is interrupted
        int count = 0;
        for (int i = 0; i < logs.Count; i++)
        {
            if (i > 0 && logs[i] != logs[i - 1].AddDays(-1))
            {
                break; // Stop counting but keep what we have
            }
            count++;
        }

        return count;
    }

    public async Task<List<HabitsWithStreak>> GetAllHabitsAsync(string userId)
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
        
        List<HabitsWithStreak> hWithStreak = new List<HabitsWithStreak>();
        
        // Include() is a method on IQueryable
        // Habit has a HabitLogs navigation property (Check HabitLog and Habit class)
        // Load the logs with the habits in one query:
        var habits = await appDbContext.Habits
            .Where(h => h.UserId == userId)
            .Include(h => h.HabitLogs)
            .ToListAsync();

        foreach (var habit in habits)
        {
            // CalculateStreak expects IEnumerable<DateOnly> but habit.HabitLogs is ICollection<HabitLogs>
            // Project the logs down to their dates:
            int streak = CalculateStreak(habit.HabitLogs.Select(l => l.LoggedOn));
            hWithStreak.Add(new HabitsWithStreak
            {
                Habit = habit, 
                Streak = streak,
                LoggedToday = habit.HabitLogs.Any(l => l.LoggedOn == today)
            });
        }

        return hWithStreak;
    }

    public async Task LogTodayAsync(int habitId, string userId)
    {
        // Verify habit belong to this user
        bool ownsHabit = await appDbContext.Habits
            .AnyAsync(h => h.Id == habitId && h.UserId == userId);

        if (!ownsHabit) throw new UnauthorizedAccessException();
        
        DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (await appDbContext.HabitLogs
                .AnyAsync(h => h.HabitId == habitId && h.LoggedOn == today))
        {
            // Already logged today, do nothing or throw an exception
            return;
        }
        
        // if not yet logged today, add to db
        HabitLog newHabitLog = new HabitLog {HabitId = habitId, LoggedOn = today};
        appDbContext.HabitLogs.Add(newHabitLog);

        await appDbContext.SaveChangesAsync();
    }

    public async Task AddHabitAsync(string name, string? emoji, string userId)
    {
        Habit newHabit = new Habit { Name = name, Emoji = emoji, UserId = userId};

        appDbContext.Add(newHabit);

        await appDbContext.SaveChangesAsync();
    }
}