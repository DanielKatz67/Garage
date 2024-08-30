namespace Ex03.GarageLogic;

public class GarageRegistry
{
    private readonly Vehicle r_RegisteredVehcle;
    private eVehicleStatus m_VehicleStatus;

    public GarageRegistry(Vehicle iRegisteredVehcle, eVehicleStatus iVehicleStatus)
    {
        r_RegisteredVehcle = iRegisteredVehcle;
        m_VehicleStatus = iVehicleStatus;
    }
}