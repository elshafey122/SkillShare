using Skillshare.Contract.RepositoryContracts;
using Skillshare.Contracts.RepositoryContracts;
using Skillshare.Domain.Entities;
using Skillshare.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class CourseSectionRepository : GenericRepository<Section>, ICourseSectionRepository
    {
        public CourseSectionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}