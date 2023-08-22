namespace LiteraFlowTest;


public class BooksRelationsTest : BaseTest
{
    [Test]
    public async Task GetBookTypesTest()
    {
        var data = await relationDAL.GetBookTypes();
        Assert.That(data, Is.Not.Null);
        Assert.That(data.Count, Is.GreaterThan(0));
    }

    [Test]
    public async Task GetBookGenresTest()
    {
        var data = await relationDAL.GetBookGenres();
        Assert.That(data, Is.Not.Null);
        Assert.That(data.Count, Is.GreaterThan(0));
    }

    [Test]
    public async Task GetBookTagsTest()
    {
        var data = await relationDAL.GetBookTags();
        Assert.That(data, Is.Not.Null);
        Assert.That(data.Count, Is.GreaterThan(0));
    }

    [Test]
    public async Task GetBookStatusesTest()
    {
        var data = await relationDAL.GetBookStatuses();
        Assert.That(data, Is.Not.Null);
        Assert.That(data.Count, Is.GreaterThan(0));
    }

    [Test]
    public async Task GetPermissionsTest()
    {
        var data = await relationDAL.GetPermissions();
        Assert.That(data, Is.Not.Null);
        Assert.That(data.Count, Is.GreaterThan(0));
    }

}
