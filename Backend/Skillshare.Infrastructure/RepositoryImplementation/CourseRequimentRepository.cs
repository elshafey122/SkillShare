using Skillshare.Contract.RepositoryContracts;
using Skillshare.Domain.Entities;
using Skillshare.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class CourseRequimentRepository : GenericRepository<CourseRequirment>, ICourseRequimentRepository
    {
        public CourseRequimentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}