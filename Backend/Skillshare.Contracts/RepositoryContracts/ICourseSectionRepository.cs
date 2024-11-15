using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skillshare.Contract.RepositoryContracts;
using Skillshare.Domain.Entities;

namespace Skillshare.Contracts.RepositoryContracts
{
    public interface ICourseSectionRepository : IGenericRepository<Section>
    {
    }
}