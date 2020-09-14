using System.Threading;
using System.Windows;

namespace CronometroAncert
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Clock clock;
        private string _clockTime = "00:00:00";
        public string clockTime
        {
            get
            {
                return this._clockTime;
            }

            set
            {
                if (value != this._clockTime)
                {
                    this._clockTime = value;
                    this.Dispatcher.Invoke(() =>
                    {
                        Time.Content = value;
                    });
                }
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            clock = new Clock();
            Thread thread = new Thread(RefreshTime);
            thread.Start();
        }

        public void StartClock(object sender, RoutedEventArgs args)
        {
            clock.StartClock();
        }
        public void PauseClock(object sender, RoutedEventArgs args)
        {
            clock.PauseClock();
        }
        public void StopClock(object sender, RoutedEventArgs args)
        {
            clock.StopClock();
        }

        public void RefreshTime()
        {
            while (true)
            {
                clockTime = clock.GetClockTime();
                Thread.Sleep(500);
            }
        }
    }
}
