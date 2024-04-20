using System;

public class RangedAttribute : Attribute
{
    private double minValue;
    private double maxValue;

    public RangedAttribute(string descrID, double default_value, double min, double max) : base(descrID, default_value)
    {
        minValue = min;
        maxValue = max;

        if (minValue > maxValue)
        {
            throw new ArgumentException("Minimum value cannot be bigger than maximum value!");
        }
        else if (default_value < min)
        {
            throw new ArgumentException("Default value cannot be lower than minimum value!");
        }
        else if (default_value > max)
        {
            throw new ArgumentException("Default value cannot be bigger than maximum value!");
        }
    }

    public double GetMinValue() { return minValue; }
    public double GetMaxValue() { return maxValue; }

    public double repairValue(double val) {
        return double.IsNaN(val) ? minValue : Math.Clamp(val, minValue, maxValue);
    }
}