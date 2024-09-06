namespace Ex03.GarageLogic;

public class GarageRegistry
{
    private readonly Vehicle r_RegisteredVehicle;
    private eVehicleStatus m_VehicleStatus;

    public Vehicle RegisteredVehicle
    {
        get
        {
            return r_RegisteredVehicle;
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

    public GarageRegistry(Vehicle i_RegisteredVehicle, eVehicleStatus i_VehicleStatus)
    {
        r_RegisteredVehicle = i_RegisteredVehicle;
        m_VehicleStatus = i_VehicleStatus;
    }
}