public enum ProductType
{
    [EnumStringValue("BOOK")]
    BOOK,

    [EnumStringValue("CLOTHING")]
    CLOTHING,

    [EnumStringValue("ELECTRONIC")]
    ELECTRONIC,

    [EnumStringValue("FURNITURE")]
    FURNITURE
}

[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
public sealed class EnumStringValueAttribute : Attribute
{
    public string Value { get; }

    public EnumStringValueAttribute(string value)
    {
        Value = value;
    }
}
