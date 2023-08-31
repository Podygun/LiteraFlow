using LiteraFlow.Web.BL.Books;
using LiteraFlow.Web.DAL.BooksRelaltions;
using LiteraFlow.Web.Middleware;
using System;

namespace LiteraFlow.Web.Controllers
{

    [Controller]
    [SiteAuthenticate]
    public class NewBookController : Controller
    {
        private readonly IBooksRelationDAL relationDAL;
        private readonly IBooks booksBL;

        public NewBookController(IBooksRelationDAL relationDAL, IBooks booksBL)
        {
            this.relationDAL = relationDAL;
            this.booksBL = booksBL;
        }
        [HttpGet]
        [Route("/newbook/basic")]
        public async Task<IActionResult> Basic()
        {
            var types =await relationDAL.GetBookTypes();
            return (View("Index", types));
        }

        [HttpGet]
        [Route("/newbook/settings/{typeId}")]
        public async Task<IActionResult> Settings([FromRoute] int typeId)
        {
            var viewModel = new BookSettingsViewModel() { 
                Book = new () { TypeId = typeId },
                Genres = await relationDAL.GetBookGenres(),
                Permissions = await relationDAL.GetPermissions(),
                Tags = await relationDAL.GetBookTags(),
                Statuses = await relationDAL.GetBookStatuses(),
        };

            return View("Settings", viewModel);
        }


        [HttpPost]
        [Route("/newbook/save")]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> CreateBook(BookSettingsViewModel viewModel, [FromServices] ICurrentUser user)
        {
            //TODO Выбор тэгов
            //TODO Возможность выбрать несколько авторов

            if (!ModelState.IsValid)
            {
                var vm = new BookSettingsViewModel()
                {
                    Book = new() { TypeId = viewModel.Book.TypeId },
                    Genres = await relationDAL.GetBookGenres(),
                    Permissions = await relationDAL.GetPermissions(),
                    Tags = await relationDAL.GetBookTags(),
                    Statuses = await relationDAL.GetBookStatuses(),
                };
                return View("Settings", vm);
            }

            var profile = await user.GetProfile();
            if (profile?.ProfileId == null)
                throw new Exception("Нет профиля");

            BookModel book = BookMapper.ViewModelToModel(viewModel.Book);
            int? bookId = await booksBL.CreateAsync(book, (int)profile.ProfileId);

            return Redirect($"/mybooks/book/{bookId}");
        }

    }
}
