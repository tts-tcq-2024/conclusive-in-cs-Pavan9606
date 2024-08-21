using System;
public class ControllerAlertService : IAlertService
{
    public void SendAlert(BreachType breachType)
    {
        const ushort header = 0xfeed;
        Console.WriteLine($"0x{header:x} : {breachType}");
    }
}
