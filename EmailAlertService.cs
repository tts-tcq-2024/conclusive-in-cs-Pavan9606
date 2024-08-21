using System;
public class EmailAlertService : IAlertService
{
    private static readonly Dictionary<BreachType, string> BreachMessages = 
        new Dictionary<BreachType, string>
        {
            { BreachType.TOO_LOW, "Hi, the temperature is too low" },
            { BreachType.TOO_HIGH, "Hi, the temperature is too high" }
        };

    public void SendAlert(BreachType breachType)
    {
        if (breachType == BreachType.NORMAL) return;

        SendEmail("a.b@c.com", BreachMessages[breachType]);
    }

    private void SendEmail(string recipient, string message)
    {
        Console.WriteLine($"To: {recipient}");
        Console.WriteLine(message);
    }
}

