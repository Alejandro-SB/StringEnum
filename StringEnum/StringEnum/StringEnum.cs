using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StringEnum;

public class StringEnum<T> : IEquatable<StringEnum<T>>
    where T : StringEnum<T>
{
    public string Value { get; }

    protected StringEnum(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }

    public static implicit operator StringEnum<T>(string value) => new(value);
    public static implicit operator string(StringEnum<T> value) => value.Value;

    public static IEnumerable<StringEnum<T>> GetValues()
    {
        var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);

        foreach (var field in fields)
        {
            if (field.GetValue(null) is not StringEnum<T> value)
            {
                throw new InvalidOperationException("Tried to recover a nullable value from StringEnum");
            }

            yield return value;
        }
    }

    public static bool TryParse(string value, [NotNullWhen(true)] out StringEnum<T>? result) => TryParse(value, false, out result);

    public static bool TryParse(string value, bool ignoreCase, [NotNullWhen(true)] out StringEnum<T>? result)
    {
        result = GetValues().FirstOrDefault(v => Equals(v, value, ignoreCase));

        return result is not null;
    }

    public override bool Equals(object? obj) => obj is StringEnum<T> se && Equals(se);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;
    public bool Equals(StringEnum<T>? other) => other is not null && Equals(this, other, false);
    public static bool Equals(StringEnum<T> first, StringEnum<T> second, bool ignoreCase)
        => string.Equals(first.Value, second.Value, GetComparisonFromCasing(ignoreCase));
    public static bool Equals(StringEnum<T> stringEnum, string s, bool ignoreCase)
        => string.Equals(stringEnum.Value, s, GetComparisonFromCasing(ignoreCase));
    private static StringComparison GetComparisonFromCasing(bool ignoreCase)
    => ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
}
