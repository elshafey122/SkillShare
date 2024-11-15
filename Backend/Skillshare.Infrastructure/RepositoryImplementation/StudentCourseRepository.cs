using Skillshare.Contract.RepositoryContracts;
using Skillshare.Domain.Entities;
using Skillshare.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class StudentCourseRepository : GenericRepository<UserCourseInrollment>, IStudentCourseRepository
    {
        public StudentCourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}