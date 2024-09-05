namespace Ex03.GarageLogic;

public class ElectricEnergySource : EnergySource
{
    public ElectricEnergySource(float i_MaximalChargeHoursCapacity, float i_CurrentChargeCapacity)
        : base(i_MaximalChargeHoursCapacity, i_CurrentChargeCapacity)
    {
    }
    
    public void ChargeBattery(float i_HoursQuantityToAdd)
    {
        if (i_HoursQuantityToAdd + base.CurrentEnergySourceCapacity > base.MaxEnergySourceAmount || i_HoursQuantityToAdd < 0.0F)
        {
            float maximalHoursQuantityToAdd = base.MaxEnergySourceAmount - base.CurrentEnergySourceCapacity;
            
            throw new ValueOutOfRangeException(0.0F, maximalHoursQuantityToAdd, "Invalid number of hours to charge");
        }
        else
        {
            base.CurrentEnergySourceCapacity += i_HoursQuantityToAdd;
        }
    }
    
    public override string ToString()
    {
        return $"Electric energy source: \n" +
               $"Maximal charging hours: {base.MaxEnergySourceAmount}, " +
               $"Remaning charging hours: {base.CurrentEnergySourceCapacity}" ;
    }
    
}