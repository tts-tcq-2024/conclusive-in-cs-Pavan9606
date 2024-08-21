using System;
public static class CoolingServiceFactory
{
    public static ICoolingService CreateCoolingService(CoolingType coolingType)
    {
        return coolingType switch
        {
            CoolingType.PASSIVE_COOLING => new PassiveCoolingService(),
            CoolingType.HI_ACTIVE_COOLING => new HiActiveCoolingService(),
            CoolingType.MED_ACTIVE_COOLING => new MedActiveCoolingService(),
            _ => throw new ArgumentOutOfRangeException(nameof(coolingType), coolingType, null)
        };
    }
}
