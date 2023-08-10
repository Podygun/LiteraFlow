namespace LiteraFlowTest;


public class SessionTest : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CreateSessionTest()
    {
        using (var scope = BaseScope.CreateTransactionScope())
        {

            var httpSession = await dBSession.GetDBSession();

            var dbSession = await dBSessionDAL.GetAsync(httpSession.DbSessionId);

            Assert.That(dbSession, Is.Not.Null);
            Assert.That(dbSession.DbSessionId, Is.EqualTo(httpSession.DbSessionId));

            httpSession = await dBSession.GetDBSession();


            Assert.That(dbSession.DbSessionId, Is.EqualTo(httpSession.DbSessionId));
        }
    }
}
