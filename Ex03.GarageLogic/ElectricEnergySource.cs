namespace Ex03.GarageLogic;

public class ElectricEnergySource : EnergySource
{
    public void ChargeBattery(float i_HoursQuantityToAdd)
    {
        if (i_HoursQuantityToAdd + base.CurrentEnergySourceCapacity > base.MaxEnergySourceAmount || i_HoursQuantityToAdd < 0.0F)
        {
            throw new ValueOutOfRangeException(0.0F, base.MaxEnergySourceAmount - base.CurrentEnergySourceCapacity);
        }
        else
        {
            base.CurrentEnergySourceCapacity += i_HoursQuantityToAdd;
        }
    }
    
    public override string ToString()
    {
        return $"Electric energy source:" +
               $"Maximal charging hours: {base.MaxEnergySourceAmount}, " +
               $"Remaning charging hours: {base.CurrentEnergySourceCapacity}" ;
    }
    
}