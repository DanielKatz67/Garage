namespace Ex03.GarageLogic;

public class FuelEnergySource : EnergySource
{
    private readonly eFuelType r_FuelType;
    
    public eFuelType FuelType
    {
        get
        {
            return r_FuelType;
        }
    }
    
    public FuelEnergySource(eFuelType i_FuelType, float i_MaximalFuelTankCapacity, float i_CurrentFuelCapacity)
        : base(i_MaximalFuelTankCapacity, i_CurrentFuelCapacity)
    {
        r_FuelType = i_FuelType;
    }

    public void Refuel(float i_FuelQuantityToAdd, eFuelType i_FuelType)
    {
        if (i_FuelType != r_FuelType)
        {
            throw new ArgumentException("The fuel type does not match");
        }
        else if (i_FuelQuantityToAdd + base.CurrentEnergySourceCapacity > base.MaxEnergySourceAmount || i_FuelQuantityToAdd < 0.0F)
        {
            throw new ValueOutOfRangeException(0.0F, base.MaxEnergySourceAmount - base.CurrentEnergySourceCapacity);
        }
        else
        {
            base.CurrentEnergySourceCapacity += i_FuelQuantityToAdd;
        }
    }
    
    public override string ToString()
    {
        return $"Fuel energy source:\n" +
               $"Maximal fuel capacity: {base.MaxEnergySourceAmount} Liters, " +
               $"Remaning fuel capacity: {base.CurrentEnergySourceCapacity} Liters." ;
    }

}