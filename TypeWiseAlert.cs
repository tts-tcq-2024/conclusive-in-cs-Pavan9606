using System;
public static class TypewiseAlert
{
    public static BreachType InferBreach(double value, double lowerLimit, double upperLimit)
    {
        if (value < lowerLimit)
        {
            return BreachType.TOO_LOW;
        }
        if (value > upperLimit)
        {
            return BreachType.TOO_HIGH;
        }
        return BreachType.NORMAL;
    }

    public static void CheckAndAlert(AlertTarget alertTarget, BatteryCharacter batteryChar, double temperatureInC)
    {
        var coolingService = CoolingServiceFactory.CreateCoolingService(batteryChar.coolingType);
        BreachType breachType = coolingService.ClassifyTemperature(temperatureInC);

        var alertService = AlertServiceFactory.CreateAlertService(alertTarget);
        alertService.SendAlert(breachType);
    }
}
