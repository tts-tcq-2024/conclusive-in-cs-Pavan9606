using System;
public class EmailAlertService : IAlertService
 {
     public void SendAlert(BreachType breachType)
     {
         string recipient = "a.b@c.com";

         if (breachType == BreachType.NORMAL)
         {
             return;
         }

         string message = GetAlertMessage(breachType);
         SendEmail(recipient, message);
     }

     private string GetAlertMessage(BreachType breachType)
     {
         return breachType switch
         {
             BreachType.TOO_LOW => "Hi, the temperature is too low",
             BreachType.TOO_HIGH => "Hi, the temperature is too high",
             _ => throw new ArgumentOutOfRangeException(nameof(breachType), "Invalid breach type")
         };
     }

     private void SendEmail(string recipient, string message)
     {
         Console.WriteLine($"To: {recipient}");
         Console.WriteLine(message);
     }
 }
