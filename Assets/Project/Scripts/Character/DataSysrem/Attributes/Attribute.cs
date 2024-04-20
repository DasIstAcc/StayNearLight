public class Attribute
{
    private double defaultValue;
    private string descriptionID;

    protected Attribute(string descriptionID, double value)
    {
        defaultValue = value;
        this.descriptionID = descriptionID;
    }

    public double GetDefaultValue() { return defaultValue; }
    public string GetDescription() { return descriptionID; }

    public double SanitizeValue(double val) { return val; }
}
