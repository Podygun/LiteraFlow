using System.Transactions;

namespace LiteraFlow.Web.BL.Helpers;

public static class Helper
{
    public static TransactionScope CreateTransactionScope(int seconds = 60) =>
       new TransactionScope(
           TransactionScopeOption.Required,
           new TimeSpan(0, 0, seconds),
           TransactionScopeAsyncFlowOption.Enabled);

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
