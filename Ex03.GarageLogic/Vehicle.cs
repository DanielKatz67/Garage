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
        get { return this.r_LicenseLicensePlate; }
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
        EnergySource i_EnergySource, VehicleOwner i_VehicleOwner)
    {
        r_LicenseLicensePlate = i_LicensePlate;
        r_NumberOfWheels = i_NumberOfWheels;
        r_ModelName = i_ModelName;
        addWheelsToVehicle(i_WheelsManufacturer, i_WheelsMaximumAirPressure, i_WheelsCurrentAirPressure);
        r_EnergySource = i_EnergySource;
        updateEnergyRemainingPercentage();
        m_VehicleOwner = i_VehicleOwner;
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
}