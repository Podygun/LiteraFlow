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

    public async Task<int?> AddAsync(ChapterModel chapter)
    {
        var result = await DBHelper.ExecuteScalarAsync<int?>(
            "insert into Chapters (title, text, bookid, updatedon)" +
            "values (@title, @text, @bookid, NOW()) returning chapterid"
            , chapter);
        return result;

    }


    /// <returns>ChapterId of updated Chapter</returns>
    public async Task<int?> UpdateAsync(ChapterModel chapter)
    {
        var result = await DBHelper.ExecuteScalarAsync<int?>(
            @"update Chapters set 
              title=@title, text=@text, updatedon=NOW() where chapterid = @ChapterId
              returning ChapterId",
            chapter);
        return result;
    }


    public async Task<string> GetTextAsync(int chapterId)
    {
        var result = await DBHelper.QueryScalarAsync<string>(
            @"select text
              from Chapters 
              where chapterid = @id",
            new { id = chapterId });
        return result;
    }


    /// <summary>
    /// Get only chapterid, title, serialnumber. Coz, getting text with ajax
    /// </summary>
    /// <param name="bookId"></param>
    /// <returns>List<ChapterModel></returns>
    public async Task<List<ChapterModel>> GetAsync(int bookId)
    {
        var result = await DBHelper.QueryCollectionAsync<ChapterModel>(
            @"select chapterid, title, serialnumber 
              from Chapters 
              where bookid = @id 
              order by serialnumber",
            new { id = bookId }) ?? new List<ChapterModel>();
        return result;
    }

}
