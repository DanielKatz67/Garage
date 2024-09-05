using Microsoft.Win32;

namespace Ex03.GarageLogic;

public class Garage
{
    private readonly Dictionary<string, GarageRegistry> r_LicensePlateToRegistry = new Dictionary<string, GarageRegistry>();

    public void AssignNewVehicle(Vehicle i_Vehicle, eVehicleStatus i_Status)
    {
        if (r_LicensePlateToRegistry.ContainsKey(i_Vehicle.LicensePlate))
        {
            throw new ArgumentException("A vehicle with this license plate is already registered in the garage.");
        }
        
        r_LicensePlateToRegistry[i_Vehicle.LicensePlate] = new GarageRegistry(i_Vehicle, i_Status);
    }

    public bool IsVehicleExists(string i_LicensePlate)
    {
        return r_LicensePlateToRegistry.ContainsKey(i_LicensePlate);
    }
    
    public List<string> SearchLicensePlates(eVehicleStatus? i_FilterByStatus)
    {
        List<string> vehicleList = new List<string>();

        foreach (KeyValuePair<string, GarageRegistry> licensePlateToRegistryEntry in r_LicensePlateToRegistry)
        {
            if (i_FilterByStatus == null || i_FilterByStatus == licensePlateToRegistryEntry.Value.VehicleStatus)
            {
                vehicleList.Add(licensePlateToRegistryEntry.Key);
            }
        }

        return vehicleList;
    }

    public void SetStatus(string i_LicensePlate, eVehicleStatus i_NewStatus)
    {
        GarageRegistry garageRegistry = getRegistry(i_LicensePlate);
        garageRegistry.VehicleStatus = i_NewStatus;
    }
    
    public void InflateWheelToMax(string i_LicensePlate)
    {
        GarageRegistry garageRegistry = getRegistry(i_LicensePlate);
        foreach (Wheel wheel in garageRegistry.RegisteredVehcle.Wheels)
        {
            wheel.Inflate(wheel.MaximumAirPressure - wheel.CurrentAirPressure);
        }
    }

    public void RefuelVehicle(string i_LicensePlate, eFuelType i_FuelType, float i_FuelQuantityToAdd)
    {
        GarageRegistry garageRegistry = getRegistry(i_LicensePlate);
        garageRegistry.RegisteredVehcle.RefuelVehicle(i_FuelType, i_FuelQuantityToAdd);
    }

    public void Charge(string i_LicensePlate, float i_TimeToCharge)
    {
        GarageRegistry garageRegistry = getRegistry(i_LicensePlate);
        garageRegistry.RegisteredVehcle.ChargeVehicle(i_TimeToCharge);
    }

    public Vehicle GetVehicleDetails(string i_LicensePlate)
    {
        GarageRegistry garageRegistry = getRegistry(i_LicensePlate);
        return garageRegistry.RegisteredVehcle;
    }

    private GarageRegistry getRegistry(string i_LicensePlate)
    {
        bool isVehicleInTheGarage =
            r_LicensePlateToRegistry.TryGetValue(i_LicensePlate, out GarageRegistry vehicleRegistry);

        if (!isVehicleInTheGarage)
        {
            throw new ArgumentException("The vehicle did not register to the garage");
        }

        return vehicleRegistry;
    }
}