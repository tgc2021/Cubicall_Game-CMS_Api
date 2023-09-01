
using DataAccess.EFCore.Data;
using Domain.Entities;
using Domain.Interfaces;

namespace DataAccess.EFCore.Repositories
{
    public class LoginUserLogRepository : GenericRepository<TblUsersLog>, ILoginUserLogRepository
    {
        public LoginUserLogRepository(db_cubicall_game_devContext context) : base(context)
        {
        }
        
    }
}
