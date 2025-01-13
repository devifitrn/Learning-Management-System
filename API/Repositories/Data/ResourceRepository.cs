using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class ResourceRepository : GeneralRepository<MyContext, Resource, int>
    {
        private readonly MyContext myContext;
        public ResourceRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
