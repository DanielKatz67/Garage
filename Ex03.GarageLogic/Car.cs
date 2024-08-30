namespace Ex03.GarageLogic;

public class Car : Vehicle
{
    private readonly eCarDoorCount r_CarNumberOfDoors;
    private eCarColor m_CarColor;
    private const float k_MaximumWheelsAirPressure = 33.0F;
    private const eFuelType k_FuelType = eFuelType.Octan95;
    private const float k_MaximalFuelTankCapacity = 49.0F;
    private const float k_MaximalChargeHoursCapacity = 5.0F;
    private const int k_WheelsNumberInCar = 4;

    public Car(string i_LicensePlate, string i_ModelName, string i_WheelsManufacturer, 
        eCarColor i_CarColor, eCarDoorCount i_CarNumberOfDoors, 
        eEnergySourceType i_EnergySourceType, float i_CurrentEnergySourceCapacity,
        eFuelType i_FuelType, float? i_MaximalFuelTankCapacity, 
        float? i_MaximalChargeHoursCapacity, VehicleOwner i_VehicleOwner)
        : base(i_LicensePlate, k_WheelsNumberInCar, i_ModelName, i_WheelsManufacturer, 
            k_MaximumWheelsAirPressure, i_CurrentEnergySourceCapacity,
            i_EnergySourceType, i_CurrentEnergySourceCapacity, i_FuelType, 
            i_MaximalFuelTankCapacity, i_MaximalChargeHoursCapacity, i_VehicleOwner)
    {
        m_CarColor = i_CarColor;
        r_CarNumberOfDoors = i_CarNumberOfDoors;
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}, " +
               $"Color: {m_CarColor}, Number of Doors: {r_CarNumberOfDoors}";
    }
    
}