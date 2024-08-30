namespace Ex03.GarageLogic;

public abstract class EnergySource
{
    private readonly float r_MaximalEnergySourceCapacity;
    private float m_CurrentEnergySourceCapacity;
    
    public float MaxEnergySourceAmount
    {
        get
        {
            return r_MaximalEnergySourceCapacity;
        }
    }
    
    public float CurrentEnergySourceCapacity
    {
        get
        {
            return m_CurrentEnergySourceCapacity;
        }
        set
        {
            m_CurrentEnergySourceCapacity = value;
        }
    }

    public EnergySource(float i_MaximalChargeHoursCapacity, float i_CurrentChargeCapacity)
    {
        r_MaximalEnergySourceCapacity = i_MaximalChargeHoursCapacity;
        m_CurrentEnergySourceCapacity = i_CurrentChargeCapacity;
    }
    
    public override string ToString()
    {
        return $"Maximal Energy Source: {r_MaximalEnergySourceCapacity}, Current Energy Source: {m_CurrentEnergySourceCapacity}." ;
    }

}