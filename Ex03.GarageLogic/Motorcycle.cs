namespace Ex03.GarageLogic;

public class Motorcycle : Vehicle
{
    private eMotorcycleLicenseType m_MotorcycleLicenseType;
    private int m_EngineCapacity;
    private const float k_MaximumWheelsAirPressure = 31.0F;
    private const eFuelType k_FuelType = eFuelType.Octan98;
    private const float k_MaximalFuelTankCapacity = 6.0F;
    private const float k_MaximalChargeHoursCapacity = 2.7F;
    private const int k_WheelsNumberInMotorcycle = 2;
    
    public Motorcycle(string i_LicensePlate, string i_ModelName, string i_WheelsManufacturer, 
            float i_CurrentWheelsAirPressure, eMotorcycleLicenseType i_MotorcycleLicenseType, 
            int i_EngineCapacity, eEnergySourceType i_EnergySourceType, 
            float i_CurrentEnergySourceCapacity, VehicleOwner i_VehicleOwner)
        : base(i_LicensePlate, k_WheelsNumberInMotorcycle, i_ModelName, i_WheelsManufacturer, 
            k_MaximumWheelsAirPressure, i_CurrentWheelsAirPressure,
            i_EnergySourceType, i_CurrentEnergySourceCapacity, k_FuelType, 
            k_MaximalFuelTankCapacity, k_MaximalChargeHoursCapacity, i_VehicleOwner)
    {
        m_MotorcycleLicenseType = i_MotorcycleLicenseType;
        m_EngineCapacity = i_EngineCapacity;
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"Motorcycle license type: {m_MotorcycleLicenseType}, engine capacity: {m_EngineCapacity}\n";
    }
}