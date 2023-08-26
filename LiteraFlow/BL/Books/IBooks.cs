﻿namespace LiteraFlow.Web.BL.Books;

public interface IBooks
{
    Task AddChapterAsync(int bookId, ChapterModel chapter);
    Task<int?> CreateAsync(BookModel book, int profileId);
    Task DeleteAsync(int bookId);
    Task DeleteChapterAsync(ChapterModel chapter);
    Task<IList<BookModel>> GetUserBooks(int userId);
    Task<BookModel> Get(int userId);
    Task UpdateChapterAsync(ChapterModel chapter);
}