using System;
public static class AlertServiceFactory
{
    public static IAlertService CreateAlertService(AlertTarget alertTarget)
    {
        return alertTarget switch
        {
            AlertTarget.TO_CONTROLLER => new ControllerAlertService(),
            AlertTarget.TO_EMAIL => new EmailAlertService(),
            _ => throw new ArgumentOutOfRangeException(nameof(alertTarget), alertTarget, null)
        };
    }
}
