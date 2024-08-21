using System;
using Xunit;

public class TypeWiseAlertTests
{
   [Theory]
    [InlineData(25, 20, 30, BreachType.NORMAL)]
    [InlineData(15, 20, 30, BreachType.TOO_LOW)]
    [InlineData(35, 20, 30, BreachType.TOO_HIGH)]
    public void InferBreach_ShouldReturnCorrectBreachType(double temperature, double lowerLimit, double upperLimit, BreachType expectedBreach)
    {
        var result = TypewiseAlert.InferBreach(temperature, lowerLimit, upperLimit);
        Assert.Equal(expectedBreach, result);
    }

    [Theory]
    [InlineData(CoolingType.PASSIVE_COOLING, 20, BreachType.NORMAL)]
    [InlineData(CoolingType.PASSIVE_COOLING, -5, BreachType.TOO_LOW)]
    [InlineData(CoolingType.PASSIVE_COOLING, 40, BreachType.TOO_HIGH)]
    [InlineData(CoolingType.HI_ACTIVE_COOLING, 20, BreachType.NORMAL)]
    [InlineData(CoolingType.HI_ACTIVE_COOLING, -5, BreachType.TOO_LOW)]
    [InlineData(CoolingType.HI_ACTIVE_COOLING, 50, BreachType.TOO_HIGH)]
    [InlineData(CoolingType.MED_ACTIVE_COOLING, 20, BreachType.NORMAL)]
    [InlineData(CoolingType.MED_ACTIVE_COOLING, -5, BreachType.TOO_LOW)]
    [InlineData(CoolingType.MED_ACTIVE_COOLING, 45, BreachType.TOO_HIGH)]
    public void ClassifyTemperatureBreach_ShouldReturnCorrectBreachType(CoolingType coolingType, double temperature, BreachType expectedBreach)
    {
        ICoolingService strategy = CoolingServiceFactory.CreateCoolingService(coolingType);
        var result = strategy.ClassifyTemperature(temperature);
        Assert.Equal(expectedBreach, result);
    }

    [Theory]
    [InlineData(AlertTarget.TO_CONTROLLER, typeof(ControllerAlertService))]
    [InlineData(AlertTarget.TO_EMAIL, typeof(EmailAlertService))]
    public void CreateAlert_ShouldReturnCorrectAlertInstance(AlertTarget alertTarget, Type expectedType)
    {
        var alert = AlertServiceFactory.CreateAlertService(alertTarget);
        Assert.IsType(expectedType, alert);
    }

    [Theory]
    [InlineData(CoolingType.PASSIVE_COOLING, typeof(PassiveCoolingService))]
    [InlineData(CoolingType.HI_ACTIVE_COOLING, typeof(HiActiveCoolingService))]
    [InlineData(CoolingType.MED_ACTIVE_COOLING, typeof(MedActiveCoolingService))]
    public void CreateCoolingStrategy_ShouldReturnCorrectStrategyInstance(CoolingType coolingType, Type expectedType)
    {
        var strategy = CoolingServiceFactory.CreateCoolingService(coolingType);
        Assert.IsType(expectedType, strategy);
    }

    [Fact]
    public void CreateAlert_ShouldThrowExceptionForInvalidAlertTarget()
    {
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => AlertServiceFactory.CreateAlertService((AlertTarget)999));
    }

    [Fact]
    public void CreateCoolingStrategy_ShouldThrowExceptionForInvalidCoolingType()
    {
        // Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => CoolingServiceFactory.CreateCoolingService((CoolingType)999));
    }
}
