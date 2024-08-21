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

    [Fact]
    public void CreateCoolingService_ShouldReturnCorrectServiceType()
    {
        var coolingService = CoolingServiceFactory.CreateCoolingService(CoolingType.PASSIVE_COOLING);
        Assert.IsType<PassiveCoolingService>(coolingService);

        coolingService = CoolingServiceFactory.CreateCoolingService(CoolingType.HI_ACTIVE_COOLING);
        Assert.IsType<HiActiveCoolingService>(coolingService);

        coolingService = CoolingServiceFactory.CreateCoolingService(CoolingType.MED_ACTIVE_COOLING);
        Assert.IsType<MedActiveCoolingService>(coolingService);
    }

    [Fact]
    public void CreateAlertService_ShouldReturnCorrectServiceType()
    {
        var alertService = AlertServiceFactory.CreateAlertService(AlertTarget.TO_CONTROLLER);
        Assert.IsType<ControllerAlertService>(alertService);

        alertService = AlertServiceFactory.CreateAlertService(AlertTarget.TO_EMAIL);
        Assert.IsType<EmailAlertService>(alertService);
    }

    [Fact]
    public void EmailAlertService_ShouldSendCorrectEmail()
    {
        var emailService = new EmailAlertService();
        emailService.SendAlert(BreachType.TOO_LOW);
        emailService.SendAlert(BreachType.TOO_HIGH);

        // Test the absence of email when the breach type is normal
        emailService.SendAlert(BreachType.NORMAL);
    }

    [Fact]
    public void ControllerAlertService_ShouldSendCorrectAlert()
    {
        var controllerService = new ControllerAlertService();
        controllerService.SendAlert(BreachType.TOO_LOW);
        controllerService.SendAlert(BreachType.TOO_HIGH);
    }
}
