using BT.Data.Entities;
using BT.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BT.Service.Permission
{
   public class PermissionRepository : BaseRepository<Permissions, int>, IPermissionRepository
    {
        public PermissionRepository(DbContext context) : base(context)
        {

        }
    }
}
