
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserHierarchyRepository : IGenericRepository<TblUsersMappingwithHierarchy>
    {
        int GetUserIdOrgHierarchy(int UID, int OrgID);
    }
}
