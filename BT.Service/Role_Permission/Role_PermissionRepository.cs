using BT.Data.Entities;
using BT.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Service.Role_Permission
{
    public class Role_PermissionRepository : BaseRepository<Roles_Permissions, int>, IRole_PermissionRepository
    {
        public Role_PermissionRepository(DbContext context) : base(context)
        {

        }

        public Task<IQueryable<Roles_Permissions>> GetRoles_PermissionsByRoleID(int roleId)
        {
            return Task.FromResult(context.Set<Roles_Permissions>().Where(m => m.RoleID == roleId && m.IsDel==0));
        }
    }
}
