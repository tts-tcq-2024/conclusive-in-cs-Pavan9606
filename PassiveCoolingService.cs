using System;
public class PassiveCoolingService : ICoolingService
{
    public BreachType ClassifyTemperature(double temperatureInC) =>
        TypewiseAlert.InferBreach(temperatureInC, 0, 35);
}
