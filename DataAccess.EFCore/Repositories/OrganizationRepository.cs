
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.EFCore.Repositories
{
    public class OrganizationRepository : GenericRepository<TblOrganization>, IOrganizationRepository
    {
        public OrganizationRepository(db_cubicall_game_devContext context) : base(context)
        {
        }
        public List<TblOrganization> GetOrganization()
        {
            var Organization = (from x in _context.TblOrganization
                        where x.Status =="A"
                        select x).ToList();
            return Organization;
        }

        public bool IsOrganizationCodeExist(string OrganizationCode)
        {
            bool isExist = false;
            isExist = _context.TblOrganization.Where(x => x.OrganizationCode == OrganizationCode).Any();
            return isExist;
        }


    }
}
