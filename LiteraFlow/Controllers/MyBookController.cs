]using LiteraFlow.Web.BL.Books;
using Microsoft.AspNetCore.Mvc;

namespace LiteraFlow.Web.Controllers;

public class MyBookController : Controller
{
    private readonly IBooks books;

    public MyBookController(IBooks books)
    {
        this.books = books;
    }

    [HttpGet]
    [Route("/mybook")]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    [Route("/mybook")]
    public IActionResult PostBook(BookViewModel book)
    {
        if (!ModelState.IsValid)
        {
            return Redirect("/mybook");
        }
        return View();
    }

    [HttpPost]
    [Route("/mybook/chapter")]
    public IActionResult PostChapter(ChapterViewModel chapter)
    {
        return View();
    }
}
