namespace Ex03.GarageLogic;

public class Wheel
{
    private readonly string r_Manufacturer;
    private readonly float r_MaximumAirPressure;
    private float m_CurrentAirPressure;

    public float MaximumAirPressure
    {
        get
        {
            return r_MaximumAirPressure;
        }
    }
    
    public float CurrentAirPressure
    {
        get
        {
            return m_CurrentAirPressure;
        }
    }
    
    public Wheel(string i_Manufacturer, float i_MaximumAirPressure, float i_CurrentAirPressure)
    {
        r_Manufacturer = i_Manufacturer;
        r_MaximumAirPressure = i_MaximumAirPressure;
        m_CurrentAirPressure = i_CurrentAirPressure;
    }

    public void Inflate(float i_AirPressureToAdd)
    {
        if (i_AirPressureToAdd + m_CurrentAirPressure > r_MaximumAirPressure || i_AirPressureToAdd < 0.0F)
        {
            throw new ValueOutOfRangeException(0.0F, r_MaximumAirPressure - m_CurrentAirPressure, "Invalid air pressure to add");
        }
        else
        {
            m_CurrentAirPressure += i_AirPressureToAdd;
        }
    }

    public override string ToString()
    {
        return $"Manufacture name: {r_Manufacturer}, max air pressure: {r_MaximumAirPressure}, current air pressure: {m_CurrentAirPressure}";
    }
}