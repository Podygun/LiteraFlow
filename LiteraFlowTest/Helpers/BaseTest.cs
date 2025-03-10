﻿#region Usings
using LiteraFlow.Web.BL.Auth;
using LiteraFlow.Web.BL.Books;
using LiteraFlow.Web.BL.DBSession;
using LiteraFlow.Web.BL.Profiles;
using LiteraFlow.Web.BL.WebCookie;
using LiteraFlow.Web.DAL.Auth;
using LiteraFlow.Web.DAL.Books;
using LiteraFlow.Web.DAL.DBSession;
using LiteraFlow.Web.DAL.Profiles;
using LiteraFlow.Web.DAL.UserToken; 
#endregion

namespace LiteraFlowTest.Helpers;


public class BaseTest
{
    //DAL
    protected readonly IDBSessionDAL dBSessionDAL = new DBSessionDAL();
    protected readonly IUserTokenDAL userTokenDAL = new UserTokenDAL();
    protected readonly IAuthDAL authDAL = new AuthDAL();
    protected readonly IChaptersDAL chaptersDAL = new ChaptersDAL();
    protected readonly IBooksDAL booksDAL = new BooksDAL();
    protected readonly IProfileDAL profileDAL = new ProfileDAL();

    protected IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();

    //BL
    protected readonly IAuth auth;   
    protected readonly IDBSession dBSession;   
    protected readonly IWebCookie webCookie;
    protected readonly IBooks books;
    protected readonly IProfile profile;


    public BaseTest()
    {
        webCookie = new TestCookie();
        dBSession = new DBSession(dBSessionDAL, webCookie);
        auth = new Auth(authDAL, dBSession, userTokenDAL, webCookie);
        books = new Books(booksDAL, chaptersDAL);
        profile = new Profile(profileDAL);
    }

}
