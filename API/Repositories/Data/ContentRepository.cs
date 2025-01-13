using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class ContentRepository : GeneralRepository<MyContext, Content, int>
    {
        private readonly MyContext myContext;
        public ContentRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
