using System;
using Xunit;

public class TypeWiseAlertTests
{
   [Fact]
    public void InferBreach_ShouldReturnCorrectBreachType(double temperature, double lowerLimit, double upperLimit, BreachType expectedBreach)
    {
        var result = TypewiseAlert.InferBreach(temperature, lowerLimit, upperLimit);
        Assert.Equal(expectedBreach, result);
    }
  [Fact]
  public void ClassifyTemperatureBreach_ShouldReturnCorrectBreachType(CoolingType coolingType, double temperature, BreachType expectedBreach)
    {
        ICoolingStrategy strategy = AlertFactory.CreateCoolingStrategy(coolingType);
        var result = TypewiseAlert.ClassifyTemperatureBreach(strategy, temperature);
        Assert.Equal(expectedBreach, result);
    }
   [Fact]
  public void CreateAlert_ShouldReturnCorrectAlertInstance(AlertTarget alertTarget, Type expectedType)
    {
        var alert = AlertFactory.CreateAlert(alertTarget);
        Assert.IsType(expectedType, alert);
    }
  [Fact]
   public void CreateCoolingStrategy_ShouldReturnCorrectStrategyInstance(CoolingType coolingType, Type expectedType)
    {
        var strategy = AlertFactory.CreateCoolingStrategy(coolingType);
        Assert.IsType(expectedType, strategy);
    }
   [Fact]
  public void CreateAlert_ShouldThrowExceptionForInvalidAlertTarget()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => AlertFactory.CreateAlert((AlertTarget)999));
    }
   [Fact]
  public void CreateCoolingStrategy_ShouldThrowExceptionForInvalidCoolingType()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => AlertFactory.CreateCoolingStrategy((CoolingType)999));
    }
}
