using BT.Data.Entities;
using BT.Data.Repository;
using BT.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Service.Employee_Role
{
    /// <summary>
    /// 员工角色仓储接口
    /// </summary>
    public interface IEmployee_RoleRepository : IBaseRepositoryT<Employees_Roles>, 
        IBaseRepositoryTID<Employees_Roles, int>
    {
        Task<IQueryable<Employees_Roles>> GetEmployees_RolesAsync(int employeeID);
    }
}
