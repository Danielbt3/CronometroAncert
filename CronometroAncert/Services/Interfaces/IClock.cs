namespace CronometroAncert.Services.Interfaces
{
    public interface IClock
    {
        void StartClock();

        void PauseClock();

        void StopClock();

        string GetClockTime();
    }
}
