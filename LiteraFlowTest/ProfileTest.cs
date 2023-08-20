using LiteraFlow.Web.BL.Profiles;

namespace LiteraFlowTest;


public class ProfileTest : BaseTest
{
    [Test]
    public async Task CreateProfileTest()
    {
        int userIdForProfile = 95;
        using (TransactionScope scope = BaseScope.CreateTransactionScope())
        {
            await profile.CreateOrUpdateAsync(
                new ProfileModel()
                {
                    UserId = userIdForProfile,
                    FullName = "Иванов Иван",
                    About = "Я тестовый человек",
                    DateBirth = DateTime.Now,
                    Gender = "Мужской"                  
                });

            var thisProfile = await profile.GetAsync(userIdForProfile);

            //проверка на наличие созданного
            Assert.That(thisProfile.ProfileId, Is.Not.Null);
            Assert.That(thisProfile.FullName, Is.EqualTo("Иванов Иван"));
            Assert.That(thisProfile.About, Is.EqualTo("Я тестовый человек"));
            Assert.That(thisProfile.Gender, Is.EqualTo("Мужской"));
        }
    }

    [Test]
    public async Task UpdateProfileTest()
    {
        int userIdForProfile = 95;
        using (TransactionScope scope = BaseScope.CreateTransactionScope())
        {
            await profile.CreateOrUpdateAsync(
                new ProfileModel()
                {
                    UserId = userIdForProfile,
                    FullName = "Иванов Иван",
                    About = "Я тестовый человек",
                    DateBirth = DateTime.Now,
                    Gender = "Мужской"
                });

            var thisProfile = await profile.GetAsync(userIdForProfile);

            //проверка на наличие созданного
            Assert.That(thisProfile.ProfileId, Is.Not.Null);

            thisProfile.FullName = "НеИванов НеИван";
            await profile.CreateOrUpdateAsync(thisProfile);

            var updatedProfile = await profile.GetAsync(userIdForProfile);

            //проверка на наличие созданного
            Assert.That(updatedProfile.ProfileId, Is.Not.Null);
            Assert.That(updatedProfile.FullName, Is.EqualTo("НеИванов НеИван"));
        }
    }
}
