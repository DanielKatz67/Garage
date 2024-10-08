namespace Ex03.GarageLogic;

public class Truck : Vehicle
{
    private bool m_IsCarryingDangerousMaterials;
    private float m_TrunkCargoVolume;
    private const float k_MaximumWheelsAirPressure = 28.0F;
    private const eFuelType k_FuelType = eFuelType.Soler;
    private const float k_MaximalFuelTankCapacity = 130.0F;
    private const int k_WheelsNumberInTruck = 14;

    public Truck(string i_LicensePlate, string i_ModelName, string i_WheelsManufacturer,
            float i_CurrentWheelsAirPressure, bool i_IsCarryingDangerousMaterials, float i_TrunkCargoVolume, 
            eEnergySourceType i_EnergySourceType, float i_CurrentEnergySourceCapacity, VehicleOwner i_VehicleOwner)
        : base(i_LicensePlate, k_WheelsNumberInTruck, i_ModelName, i_WheelsManufacturer, 
            k_MaximumWheelsAirPressure, i_CurrentWheelsAirPressure,
            i_EnergySourceType, i_CurrentEnergySourceCapacity, k_FuelType, 
            k_MaximalFuelTankCapacity, null, i_VehicleOwner)
    {
        m_IsCarryingDangerousMaterials = i_IsCarryingDangerousMaterials;
        m_TrunkCargoVolume = i_TrunkCargoVolume;
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"The truc is {(m_IsCarryingDangerousMaterials ? "" : "not ")}carrying dangerous materials\n" +
               $"Trunk cargo volume: {m_TrunkCargoVolume}\n";
    }
    
}