using BT.Service.Employee;
using BT.Service.Employee_Role;
using BT.Service.Log;
using BT.Service.LogCategory;
using BT.Service.Role;
using System;
using System.Collections.Generic;
using System.Text;
using BT.Service;
using BT.Service.Permission;
using BT.Service.Role_Permission;
using BT.Service.PublicCount;

namespace BT.Service
{
    public interface IWrapperRepository
    {
        IEmployeeRepository EmployeeRepository { get; }

        IRoleRepository RoleRepository { get; }

        IEmployee_RoleRepository Employee_RoleRepository { get; }

        ILogRepository LogRepository { get; }

        ILogCategoryRepository LogCategoryRepository { get; }

        IPermissionRepository PermissionRepository { get; }

        IRole_PermissionRepository Role_PermissionRepository { get; }

        IPublicCountRepository PublicCountRepository { get; }
    }
}
