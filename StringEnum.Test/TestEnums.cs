namespace StringEnum.Test;
public class EmptyStringEnum : StringEnum<EmptyStringEnum>
{
    protected EmptyStringEnum(string value) : base(value)
    {
    }
}

public class MultivalueStringEnum : StringEnum<MultivalueStringEnum>
{
    protected MultivalueStringEnum(string value) : base(value)
    {
    }

    public static readonly MultivalueStringEnum One = new("one");
    public static readonly MultivalueStringEnum Two = new("two");
}