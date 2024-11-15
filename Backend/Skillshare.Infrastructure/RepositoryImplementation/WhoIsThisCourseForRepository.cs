using Skillshare.Contract.RepositoryContracts;
using Skillshare.Domain.Entities;
using Skillshare.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class WhoIsThisCourseForRepository : GenericRepository<WhoIsthisCoursefor>, IWhoIsThisCourseForRepository
    {
        public WhoIsThisCourseForRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}