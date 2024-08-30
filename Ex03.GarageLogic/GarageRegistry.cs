namespace Ex03.GarageLogic;

public class GarageRegistry
{
    private readonly Vehicle r_RegisteredVehcle;
    private eVehicleStatus m_VehicleStatus;

    public Vehicle RegisteredVehcle
    {
        get
        {
            return r_RegisteredVehcle;
        }
    }
    
    public eVehicleStatus VehicleStatus
    {
        get
        {
            return m_VehicleStatus;
        }
        set
        {
            m_VehicleStatus = value;
        }
    }

    public GarageRegistry(Vehicle iRegisteredVehcle, eVehicleStatus iVehicleStatus)
    {
        r_RegisteredVehcle = iRegisteredVehcle;
        m_VehicleStatus = iVehicleStatus;
    }
}