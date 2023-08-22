namespace LiteraFlow.Web.DAL.Books;

public class ChaptersDAL : IChaptersDAL
{
    public async Task AddRangeAsync(int bookId, IEnumerable<ChapterModel> chapters)
    {
        foreach (var chapter in chapters)
        {
            chapter.BookId = bookId;
            await DBHelper.ExecuteAsync(
                "insert into Chapters (title, text, bookid, updatedon)" +
                "values (@title, @text, @bookid, NOW())"
                , chapter);
        }
    }

    public async Task<int?> AddAsync(int bookId, ChapterModel chapter)
    {

        chapter.BookId = bookId;
        return await DBHelper.ExecuteScalarAsync<int?>(
            "insert into Chapters (title, text, bookid, updatedon)" +
            "values (@title, @text, @bookid, NOW()) returning chapterid"
            , chapter);

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chapter"></param>
    /// <returns>ChapterId of updated Chapter</returns>
    public async Task<int> UpdateAsync(ChapterModel chapter)
    {
        var result = await DBHelper.ExecuteScalarAsync<int>(
            @"update Chapters set 
              title=@title, text=@text, updatedon=NOW() where chapterid = @ChapterId
              returning ChapterId",
            chapter);
        return result;
    }


    public async Task<ChapterModel> GetAsync(int chapterId)
    {
        var result = await DBHelper.QueryScalarAsync<ChapterModel>(
            @"select * from Chapters where chapterid = @ChapterId",
            chapterId);
        return result;
    }





}
