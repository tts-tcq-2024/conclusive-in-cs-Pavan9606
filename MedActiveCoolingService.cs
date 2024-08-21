using System;
public class MedActiveCoolingService : ICoolingService
 {
     public BreachType ClassifyTemperature(double temperatureInC) =>
         TypewiseAlert.InferBreach(temperatureInC, 0, 40);
 }
