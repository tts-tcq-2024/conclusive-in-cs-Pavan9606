using System;
using Xunit;

public class TypeWiseAlertTests
{
    [Theory]
    [InlineData(CoolingType.PASSIVE_COOLING, 25.0, BreachType.NORMAL)]
    [InlineData(CoolingType.HI_ACTIVE_COOLING, -1.0, BreachType.TOO_LOW)]
    [InlineData(CoolingType.MED_ACTIVE_COOLING, 50.0, BreachType.TOO_HIGH)]
    public void InferBreach_ShouldReturnExpectedBreachType(CoolingType coolingType, double temperature, BreachType expectedBreachType)
    {
        ICoolingService strategy = CoolingServiceFactory.CreateCoolingService(coolingType);
        var result = strategy.ClassifyTemperature(temperature);
        Assert.Equal(expectedBreach, result);
    }

    [Theory]
    [InlineData(AlertTarget.TO_CONTROLLER, BreachType.TOO_LOW)]
    [InlineData(AlertTarget.TO_EMAIL, BreachType.TOO_HIGH)]
    public void CheckAndAlert_ShouldTriggerCorrectAlert(AlertTarget alertTarget, BreachType breachType)
    {
        var batteryChar = new BatteryCharacter { coolingType = CoolingType.PASSIVE_COOLING, brand = "BrandA" };
        var mockAlertService = AlertServiceFactory.CreateAlertService(alertTarget);

        mockAlertService.SendAlert(breachType);
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
