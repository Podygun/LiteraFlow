using LiteraFlow.Web.Middleware;

namespace LiteraFlow.Web.Controllers
{

    [Controller]
    [SiteAuthenticate]
    public class NewBookController : Controller
    {
        [HttpGet]
        [Route("/newbook/basic")]
        public IActionResult Basic(BookViewModel book)
        {
            return (View("Index", book));
        }

        [HttpPost]
        [Route("/newbook/settings")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Settings(BookViewModel book)
        {
            var foo = book;
            return View("Privacy");
        }



        [HttpPost]
        [Route("/newbook/save")]
        public IActionResult CreateBook(BookViewModel book)
        {
            if (!ModelState.IsValid)
            {
                return Redirect("/mybooks");
            }
            return Redirect("/mybooks");
        }

        [HttpGet]
        [Route("/newbook/notfoo")]
        public IActionResult foo()
        {
            return View("Privacy");
        }

        //[HttpPost]
        //[Route("/mybook/chapter")]
        //public IActionResult PostChapter(ChapterViewModel chapter)
        //{
        //    return View();
        //}

    }
}
