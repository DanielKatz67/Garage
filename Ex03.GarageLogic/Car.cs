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
            float i_CurrentWheelsAirPressure, eCarColor i_CarColor, eCarDoorCount i_CarNumberOfDoors, 
            eEnergySourceType i_EnergySourceType, float i_CurrentEnergySourceCapacity, 
            VehicleOwner i_VehicleOwner)
        : base(i_LicensePlate, k_WheelsNumberInCar, i_ModelName, i_WheelsManufacturer, 
            k_MaximumWheelsAirPressure, i_CurrentWheelsAirPressure,
            i_EnergySourceType, i_CurrentEnergySourceCapacity, k_FuelType, 
            k_MaximalFuelTankCapacity, k_MaximalChargeHoursCapacity, i_VehicleOwner)
    {
        m_CarColor = i_CarColor;
        r_CarNumberOfDoors = i_CarNumberOfDoors;
    }
    
    public override string ToString()
    {
        return $"{base.ToString()}\n" +
               $"Color: {m_CarColor}, number of doors: {r_CarNumberOfDoors}\n";
    }
    
}