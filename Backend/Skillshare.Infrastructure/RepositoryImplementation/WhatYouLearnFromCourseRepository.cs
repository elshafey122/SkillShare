using Skillshare.Contract.RepositoryContracts;
using Skillshare.Domain.Entities;
using Skillshare.Infrastructure.DbContext;

namespace SimpleEcommerce.Infrastructure.RepositoryImplementation
{
    public class WhatYouLearnFromCourseRepository : GenericRepository<WhatYouLearnFromCourse>, IWhatYouLearnFromCourseRepository
    {
        public WhatYouLearnFromCourseRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}