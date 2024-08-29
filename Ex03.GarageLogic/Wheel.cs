namespace Ex03.GarageLogic;

public class Wheel
{
    private readonly string r_Manufacturer;
    private readonly float r_MaximumAirPressure;
    private float m_CurrentAirPressure;

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
            throw new ValueOutOfRangeException(0.0F, r_MaximumAirPressure - m_CurrentAirPressure);
        }
        else
        {
            m_CurrentAirPressure += i_AirPressureToAdd;
        }
    }

    public override string ToString()
    {
        return $"Wheel information: " +
               $"Manufacture Name: {r_Manufacturer}, Max Air Pressure: {r_MaximumAirPressure}, Current Air Pressure: {m_CurrentAirPressure}";
    }
}