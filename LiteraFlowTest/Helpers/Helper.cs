namespace LiteraFlowTest.Helpers;

public static class Helper
{
    public static int? StringToIntOrDefault(string str, int? def)
    {
        if (int.TryParse(str, out int value))
            return value;
        return def;
    }

    public static Guid? StringToGuidOrDefault(string str)
    {
        if (Guid.TryParse(str, out Guid value))
            return value;
        return null;
    }
}
