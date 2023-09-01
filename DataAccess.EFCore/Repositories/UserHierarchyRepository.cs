
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq;

namespace DataAccess.EFCore.Repositories
{
    public class UserHierarchyRepository : GenericRepository<TblUsersMappingwithHierarchy>, IUserHierarchyRepository
    {
        public UserHierarchyRepository(db_cubicall_game_devContext context) : base(context)
        {
        }
  
        public int GetUserIdOrgHierarchy(int UID, int OrgID)
        {
            if (UID != 0)
            {
                int IdOrgHierarchy = 0;
                IdOrgHierarchy = (from t1 in _context.TblUsersMappingwithHierarchy
                                  join t2 in _context.TblUsers
                                  on t1.UserId equals t2.UserId
                                  where t1.UserId == UID &&
                                  t1.IsActive == "A" &&
                                  t2.IdOrganization== OrgID
                                  select t1.IdOrgHierarchy).FirstOrDefault();
                return IdOrgHierarchy;
            }
            else
            {
                return 0;
            }
        }

      
    }
}
