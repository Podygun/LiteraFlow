using LiteraFlow.Web.DAL.BooksRelaltions;
using LiteraFlow.Web.Middleware;

namespace LiteraFlow.Web.Controllers
{

    [Controller]
    [SiteAuthenticate]
    public class NewBookController : Controller
    {
        private readonly IBooksRelationDAL relationDAL;

        public NewBookController(IBooksRelationDAL relationDAL)
        {
            this.relationDAL = relationDAL;
        }
        [HttpGet]
        [Route("/newbook/basic")]
        public async Task<IActionResult> Basic()
        {
            var types =await relationDAL.GetBookTypes();
            return (View("Index", types));
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
