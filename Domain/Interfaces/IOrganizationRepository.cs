
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IOrganizationRepository : IGenericRepository<TblOrganization>
    {

        List<TblOrganization> GetOrganization();
        bool IsOrganizationCodeExist(string OrganizationCode);
    }
}
