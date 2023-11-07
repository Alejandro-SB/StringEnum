namespace StringEnum.Test;

public class StringEnumTests
{
    [Fact]
    public void GetValuesReturnsNoneWhenNoValuesAreDefined()
    {
        var values = EmptyStringEnum.GetValues();

        Assert.Empty(values);
    }

    [Fact]
    public void GetValuesReturnsValuesWhenValuesAreDefined()
    {
        var values = MultivalueStringEnum.GetValues();

        Assert.NotEmpty(values);
    }

    [Fact]
    public void ParseReturnsParsedValue()
    {
        var stringValue = "one";

        var conversionResult = MultivalueStringEnum.TryParse(stringValue, out var result);

        Assert.True(conversionResult);
        Assert.Equal(MultivalueStringEnum.One, result);
    }

    [Fact]
    public void ParseReturnsFalseWhenFailed()
    {
        string value = "ONE";

        var conversionResult = MultivalueStringEnum.TryParse(value, out var result);

        Assert.False(conversionResult);
        Assert.Null(result);
    }

    [Fact]
    public void ParseReturnsTrueWhenMatchesCaseInsensitive()
    {
        var value = "OnE";

        var conversionResult = MultivalueStringEnum.TryParse(value, true, out var result);

        Assert.True(conversionResult);
        Assert.Equal(MultivalueStringEnum.One, result);
    }
}