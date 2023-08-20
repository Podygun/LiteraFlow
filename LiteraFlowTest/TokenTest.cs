namespace LiteraFlowTest;


public class TokenTest : BaseTest
{
    [Test]
    public async Task CreateTokenTest()
    {
        using (TransactionScope scope = BaseScope.CreateTransactionScope())
        {
            int userIdForToken = 95;
            var tokenId = await userTokenDAL.CreateAsync(userIdForToken);
            var userId  = await userTokenDAL.GetUserIdAsync(tokenId);

            Assert.That(userId, Is.EqualTo(userIdForToken));
        }
    }
}
