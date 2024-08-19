 public static class TemperatureBreachClassifier
 {
     public static BreachType ClassifyTemperatureBreach(CoolingType coolingType, double temperatureInC)
     {
         var limits = GetLimitsForCoolingType(coolingType);
         return InferBreach(temperatureInC, limits.lowerLimit, limits.upperLimit);
     }

     private static (double lowerLimit, double upperLimit) GetLimitsForCoolingType(CoolingType coolingType)
     {
         return coolingType switch
         {
             CoolingType.PASSIVE_COOLING => (0, 35),
             CoolingType.HI_ACTIVE_COOLING => (0, 45),
             CoolingType.MED_ACTIVE_COOLING => (0, 40),
             _ => (0, 0)
         };
     }

     private static BreachType InferBreach(double value, double lowerLimit, double upperLimit)
     {
         if (value < lowerLimit) return BreachType.TOO_LOW;
         if (value > upperLimit) return BreachType.TOO_HIGH;
         return BreachType.NORMAL;
     }
 }
