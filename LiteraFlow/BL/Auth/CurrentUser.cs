
using LiteraFlow.Web.BL.DBSession;

namespace LiteraFlow.Web.BL
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IDBSession dBSessionBL;

        public CurrentUser(IDBSession dBSessionBL)
        {
            this.dBSessionBL = dBSessionBL;
        }

        public async Task<bool> IsLoggedIn() => await dBSessionBL.IsLoggedIn();
    }
}
