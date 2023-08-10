using LiteraFlow.Web.BL.Excepions;
using LiteraFlow.Web.BL.Exceptions;

namespace LiteraFlowTest;


public class AuthTest : BaseTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task BaseRegistrationTest()
    {
        using (TransactionScope scope = BaseScope.CreateTransactionScope())
        {
            string email = Guid.NewGuid().ToString() + "@test.com";
            string pswd = "pswdfromtest";


            //validate: should NOT be in the db
            Assert.DoesNotThrowAsync(async () => await auth.ValidateEmail(email));

            //creating user
            int userId = await auth.CreateUserAsync(
                new UserModel
                {
                    Email = email,
                    Password = pswd
                });

            Assert.That(userId, Is.GreaterThan(0));

            //searching user with id
            var userDalResult = await authDAL.GetUserAsync(userId);
            Assert.That(email, Is.EqualTo(userDalResult.Email));
            Assert.That(userDalResult.Salt, Is.Not.Null);

            //searching user with email
            userDalResult = await authDAL.GetUserAsync(email);
            Assert.That(email, Is.EqualTo(userDalResult.Email));

            //validate: user should be in the db
            Assert.ThrowsAsync<DuplicateEmailException>(async () => await auth.ValidateEmail(email));

            //check: hashing of password
            string hashedPswd = Encrypter.HashPassword(pswd, userDalResult.Salt);
            Assert.That(hashedPswd, Is.EqualTo(userDalResult.Password));


        }
    }

    [Test]
    public async Task BaseAuthorizationTest()
    {
        using (TransactionScope scope = BaseScope.CreateTransactionScope())
        {
            string email = Guid.NewGuid().ToString() + "@test.com";
            string pswd = "pswd";

            //validate: should NOT be in the db
            Assert.DoesNotThrowAsync(async () => await auth.ValidateEmail(email));

            //creating user
            int userId = await auth.CreateUserAsync(
                new UserModel
                {
                    Email = email,
                    Password = pswd
                });

            Assert.ThrowsAsync<AuthorizationException>(async () => await auth.AuthenticateAsync(email, "fakepswd", false));
            Assert.ThrowsAsync<AuthorizationException>(async () => await auth.AuthenticateAsync("fakeemail", pswd, false));
            Assert.ThrowsAsync<AuthorizationException>(async () => await auth.AuthenticateAsync("fakeemail", "fakepswd", false));
            await auth.AuthenticateAsync(email, pswd, false);
        
        
        }
    }
}