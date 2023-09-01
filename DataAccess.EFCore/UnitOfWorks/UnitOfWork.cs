
using DataAccess.EFCore.Data;
using DataAccess.EFCore.Repositories;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EFCore.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly db_cubicall_game_devContext _context;
        public UnitOfWork(db_cubicall_game_devContext context)
        {
            _context = context;
            Login = new LoginRepository(_context);
            LoginUserLog = new LoginUserLogRepository(_context);
            Organization = new OrganizationRepository(_context);
            UserHierarchy = new UserHierarchyRepository(_context);
        }
        public ILoginRepository Login { get; private set; }
        public ILoginUserLogRepository LoginUserLog { get; private set; }
        public IOrganizationRepository Organization { get; private set; }
        public IUserHierarchyRepository UserHierarchy { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
