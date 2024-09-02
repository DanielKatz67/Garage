using System.Text;

namespace Ex03.GarageLogic;

public abstract class Vehicle
{
    private readonly string r_LicenseLicensePlate;
    private readonly int r_NumberOfWheels;
    private readonly List<Wheel> r_Wheels = new List<Wheel>();
    private readonly string r_ModelName;
    private readonly EnergySource r_EnergySource;
    private float m_EnergyRemaningPrecentage;
    private VehicleOwner m_VehicleOwner = new VehicleOwner();
    
    public string LicenseLicensePlate
    {
        get
        {
            return r_LicenseLicensePlate;
        }
    }

    public int NumberOfWheels
    {
        get
        {
            return r_NumberOfWheels;
        }
    }
    
    public string ModelName
    {
        get
        {
            return r_ModelName;
        }
    }
    
    public List<Wheel> Wheels
    {
        get
        {
            return r_Wheels;
        }
    }
    
    public VehicleOwner VehicleOwner
    {
        get
        {
            return m_VehicleOwner;
        }
    }
    
    public float EnergyRemaningPrecentage
    {
        get
        {
            return m_EnergyRemaningPrecentage;
        }
    }
    
    protected Vehicle(string i_LicensePlate, int i_NumberOfWheels, string i_ModelName, 
        string i_WheelsManufacturer, float i_WheelsMaximumAirPressure, float i_WheelsCurrentAirPressure, 
        eEnergySourceType i_EnergySourceType, float i_CurrentEnergySourceCapacity, 
        eFuelType? i_FuelType, float? i_MaximalFuelTankCapacity, 
        float? i_MaximalChargeHoursCapacity, VehicleOwner i_VehicleOwner)
    {
        r_LicenseLicensePlate = i_LicensePlate;
        m_VehicleOwner = i_VehicleOwner;
        r_ModelName = i_ModelName;
        r_NumberOfWheels = i_NumberOfWheels;
        addWheelsToVehicle(i_WheelsManufacturer, i_WheelsMaximumAirPressure, i_WheelsCurrentAirPressure);
        r_EnergySource = createEnergySource(i_EnergySourceType, i_CurrentEnergySourceCapacity, i_FuelType, 
            i_MaximalFuelTankCapacity, i_MaximalChargeHoursCapacity);
        updateEnergyRemainingPercentage();
    }
    
    private void addWheelsToVehicle(string i_WheelsManufacturer, float i_WheelsMaximumAirPressure, float i_WheelsCurrentAirPressure)
    {
        if (i_WheelsMaximumAirPressure < i_WheelsCurrentAirPressure)
        {
            throw new ValueOutOfRangeException(0.0F, i_WheelsMaximumAirPressure);
        }
        for (int wheelNumber = 0; wheelNumber < r_NumberOfWheels; wheelNumber++)
        {
            r_Wheels.Add(new Wheel(i_WheelsManufacturer, i_WheelsMaximumAirPressure, i_WheelsCurrentAirPressure));
        }
    }
    
    private void updateEnergyRemainingPercentage()
    {
        m_EnergyRemaningPrecentage = (r_EnergySource.CurrentEnergySourceCapacity / r_EnergySource.MaxEnergySourceAmount) * 100;
    }
    
    protected EnergySource createEnergySource(eEnergySourceType i_EnergySourceType, float i_CurrentEnergySourceCapacity,
        eFuelType? i_FuelType, float? i_MaximalFuelTankCapacity, float? i_MaximalChargeHoursCapacity)
    {
        switch (i_EnergySourceType)
        {
            case eEnergySourceType.Fuel:
                if (i_FuelType == null || i_MaximalFuelTankCapacity == null)
                {
                    throw new ArgumentException("Fuel type and fuel tank capacity must be provided for fuel energy source.");
                }
                return new FuelEnergySource(i_FuelType.Value, i_MaximalFuelTankCapacity.Value, i_CurrentEnergySourceCapacity);

            case eEnergySourceType.Electric:
                if (i_MaximalChargeHoursCapacity == null)
                {
                    throw new ArgumentException("Maximal charge hours capacity must be provided for electric energy source.");
                }
                return new ElectricEnergySource(i_MaximalChargeHoursCapacity.Value, i_CurrentEnergySourceCapacity);

            default:
                throw new ArgumentException("Invalid energy source type");
        }
    }
    
    public void RefuelVehicle(eFuelType i_FuelType, float i_FuelQuantityToAdd)
    {
        if (r_EnergySource is FuelEnergySource fuelEnergySource)
        {
            fuelEnergySource.Refuel(i_FuelQuantityToAdd, i_FuelType);
            updateEnergyRemainingPercentage();
        }
        else
        {
            throw new ArgumentException("Cannot refuel - vehicle energy source is not fuel.");
        }
    }
    
    public void ChargeVehicle(float i_HoursQuantityToAdd)
    {
        if (r_EnergySource is ElectricEnergySource electricEnergySource)
        {
            electricEnergySource.ChargeBattery(i_HoursQuantityToAdd);
            updateEnergyRemainingPercentage();
        }
        else
        {
            throw new ArgumentException("Cannot charge - vehicle energy source is not electric.");
        }
    }

    private string printWheels()
    {
        StringBuilder wheelsString = new StringBuilder();
        int wheelNumber = 1;
        foreach (Wheel wheel in r_Wheels)
        {
            wheelsString.Append($"Wheel number {wheelNumber}: {wheel}.\n");
            wheelNumber++;
        }
        
        return wheelsString.ToString();
    }
    
    public override string ToString()
    {
        return $"License plate: {r_LicenseLicensePlate}, model: {r_ModelName}.\n" +
               $"Number of wheels: {r_NumberOfWheels}, wheels info:\n" +
               $"{printWheels()}\n" + 
               $"Energy source: {r_EnergySource}\n" +
               $"Energy remaining percentage: {m_EnergyRemaningPrecentage}%\n" +
               $"Owner: {m_VehicleOwner}\n";
    }
}