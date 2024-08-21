using System;
public static class TypewiseAlert
{
    private static readonly Dictionary<Func<double, double, bool>, BreachType> BreachConditions = 
        new Dictionary<Func<double, double, bool>, BreachType>
        {
            { (value, lowerLimit) => value < lowerLimit, BreachType.TOO_LOW },
            { (value, upperLimit) => value > upperLimit, BreachType.TOO_HIGH }
        };

    public static BreachType InferBreach(double value, double lowerLimit, double upperLimit)
    {
        foreach (var condition in BreachConditions)
        {
            if (condition.Key(value, lowerLimit) || condition.Key(value, upperLimit))
            {
                return condition.Value;
            }
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

