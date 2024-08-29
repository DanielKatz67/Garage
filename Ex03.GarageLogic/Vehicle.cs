namespace Ex03.GarageLogic;

public abstract class Vehicle
{
    private readonly string r_LicenseLicensePlate;
    private readonly int r_NumberOfWheels;
    private readonly List<Wheel> r_Wheels = new List<Wheel>();
    private readonly string r_ModelName;
    private EnergySource m_EnergySource;
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
    
    protected Vehicle(string i_LicensePlate, int i_NumberOfWheels, string i_ModelName)
    {
        r_LicenseLicensePlate = i_LicensePlate;
        r_NumberOfWheels = i_NumberOfWheels;
        r_ModelName = i_ModelName;
    }
    
    //Todo : continue

}