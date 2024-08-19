 public static class AlertSender
 {
     public static void SendToController(BreachType breachType)
     {
         const ushort header = 0xfeed;
         PrintToController(header, breachType);
     }

     public static void SendToEmail(BreachType breachType)
     {
         string message = GetEmailMessage(breachType);
         if (message != null)
         {
             PrintEmail(message);
         }
     }

     private static void PrintToController(ushort header, BreachType breachType)
     {
         Console.WriteLine($"{header} : {breachType}\n");
     }

     private static void PrintEmail(string message)
     {
         string recipient = "a.b@c.com";
         Console.WriteLine($"To: {recipient}\n{message}");
     }

     private static string GetEmailMessage(BreachType breachType)
     {
         return breachType switch
         {
             BreachType.TOO_LOW => "Hi, the temperature is too low\n",
             BreachType.TOO_HIGH => "Hi, the temperature is too high\n",
             _ => null
         };
     }
 }
