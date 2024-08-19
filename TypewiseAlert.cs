 public static class TypewiseAlert
 {
     public static void CheckAndAlert(AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC)
     {
         BreachType breachType = TemperatureBreachClassifier.ClassifyTemperatureBreach(
             batteryChar.CoolingType, temperatureInC);

         HandleAlert(alertTarget, breachType);
     }

     private static void HandleAlert(AlertTarget alertTarget, BreachType breachType)
     {
         switch (alertTarget)
         {
             case AlertTarget.TO_CONTROLLER:
                 AlertSender.SendToController(breachType);
                 break;
             case AlertTarget.TO_EMAIL:
                 AlertSender.SendToEmail(breachType);
                 break;
         }
     }
 }
