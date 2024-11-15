using Skillshare.Contract.RepositoryContracts;
using Skillshare.Domain.Entities;
using Skillshare.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class CourseLectureRepository : GenericRepository<Lecture>, ICourseLectureRepository
    {
        public CourseLectureRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}