using BT.Data.Entities;
using BT.Data.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BT.Service.Role
{
    public class RoleRepository:BaseRepository<Roles,int>,IRoleRepository
    {
        public RoleRepository(DbContext context):base(context)
        {

        }

    }
}
