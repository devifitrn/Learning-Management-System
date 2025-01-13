using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class AuthorityRepository : GeneralRepository<MyContext, Authority, int>
    {
        private readonly MyContext myContext;
        public AuthorityRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
