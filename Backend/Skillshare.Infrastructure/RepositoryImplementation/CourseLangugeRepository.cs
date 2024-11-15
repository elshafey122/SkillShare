using Skillshare.Contract.RepositoryContracts;
using Skillshare.Domain.Entities;
using Skillshare.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class CourseLangugeRepository : GenericRepository<CourseLanguge>, ICourseLangugeRepository
    {
        public CourseLangugeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}