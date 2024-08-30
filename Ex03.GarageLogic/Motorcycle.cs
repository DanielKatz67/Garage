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
        eMotorcycleLicenseType i_MotorcycleLicenseType, int i_EngineCapacity, 
        eEnergySourceType i_EnergySourceType, float i_CurrentEnergySourceCapacity,
        eFuelType i_FuelType, float? i_MaximalFuelTankCapacity, 
        float? i_MaximalChargeHoursCapacity, VehicleOwner i_VehicleOwner)
        : base(i_LicensePlate, k_WheelsNumberInMotorcycle, i_ModelName, i_WheelsManufacturer, 
            k_MaximumWheelsAirPressure, i_CurrentEnergySourceCapacity,
            i_EnergySourceType, i_CurrentEnergySourceCapacity, i_FuelType, 
            i_MaximalFuelTankCapacity, i_MaximalChargeHoursCapacity, i_VehicleOwner)
    {
        m_MotorcycleLicenseType = i_MotorcycleLicenseType;
        m_EngineCapacity = i_EngineCapacity;
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}, " +
               $"Motorcycle License Type: {m_MotorcycleLicenseType}, Engine Capacity: {m_EngineCapacity}";
    }
}