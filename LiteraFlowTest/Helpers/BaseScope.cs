using System.Transactions;

namespace LiteraFlowTest.Helpers;


public static class BaseScope
{
    public static TransactionScope CreateTransactionScope(int seconds = 1) =>
       new TransactionScope(
           TransactionScopeOption.Required,
           new TimeSpan(0, 0, seconds),
           TransactionScopeAsyncFlowOption.Enabled);
}
