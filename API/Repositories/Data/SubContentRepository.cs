using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class SubContentRepository : GeneralRepository<MyContext, SubContent, int>
    {
        private readonly MyContext myContext;
        public SubContentRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
