using Microsoft.Toolkit.Uwp.Notifications;
using System.Timers;

internal class Program
{
    private static System.Timers.Timer aTimer;
    private static DateTime _timeTreshold;
    private static void Main(string[] args)
    {
        SetTimer();
        Console.ReadLine();
    }

    private static void SetTimer()
    {
    
        _timeTreshold = DateTime.Today.AddHours(20).AddMinutes(00);
  
        // Create a timer with a two second interval.
        aTimer = new System.Timers.Timer(1000);

        aTimer.Start();
        // Hook up the Elapsed event for the timer. 
        aTimer.Elapsed += OnTimerEvent;
        aTimer.AutoReset = true;
        aTimer.Enabled = true;
   
    }

    private static void OnTimerEvent(object sender, EventArgs e)
    {
        if(DateTime.Now >= _timeTreshold)
        {
            _timeTreshold = _timeTreshold.AddDays(1);
            var (totalDiskSpace, totalFreeSpace) = GetDriveData("C:\\");
            new ToastContentBuilder()
               .AddArgument("DiskData", "")
               .AddArgument("conversationId", 9813)
               .AddText($"Diskspace: {totalDiskSpace.ToString("F2")}GB\nFreeSpace: {totalFreeSpace.ToString("F2")}GB")
               .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 6 (or later), then your TFM must be net6.0-windows10.0.17763.0 or greater
        }
    }
    private static (double TotalDiskSpace, double TotalFreeSpace) GetDriveData(string driveName)
    {
        foreach (DriveInfo drive in DriveInfo.GetDrives())
        {
            if (drive.IsReady && drive.Name == driveName)
            {
                return (drive.TotalSize / Math.Pow(1024, 3), drive.TotalFreeSpace / Math.Pow(1024, 3));
            }
        }
        return (-1, -1);
    }
}