using System;
public class HiActiveCoolingService : ICoolingService
{
    public BreachType ClassifyTemperature(double temperatureInC) =>
        TypewiseAlert.InferBreach(temperatureInC, 0, 45);
}
