
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ILoginRepository Login { get; }
        ILoginUserLogRepository LoginUserLog { get; }
        IOrganizationRepository Organization { get; }
        IUserHierarchyRepository UserHierarchy { get; }
        int Complete();
    }
}
