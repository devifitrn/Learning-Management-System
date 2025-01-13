using API.Context;
using API.Models;

namespace API.Repositories.Data
{
    public class CatalogueRepository : GeneralRepository<MyContext, Catalogue, int>
    {
        private readonly MyContext myContext;
        public CatalogueRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
    }
}
