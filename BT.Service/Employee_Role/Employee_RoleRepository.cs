using BT.Data.Entities;
using BT.Data.Repository;
using BT.Service.Employee;
using BT.Service.LogCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT.Service.Employee_Role
{
    /// <summary>
    /// 员工角色仓储实现类
    /// </summary>
    public class Employee_RoleRepository : BaseRepository<Employees_Roles, int>, 
        IEmployee_RoleRepository
    {
        public Employee_RoleRepository(DbContext context) : base(context)
        {

        }

        public Task<IQueryable<Employees_Roles>> GetEmployees_RolesAsync(int employeeID)
        {
            return Task.FromResult(context.Set<Employees_Roles>().
                Where(m => m.EmployeeID == employeeID && m.IsDel == 0));
            
        }
    }
}
