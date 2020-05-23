using BT.Core;
using BT.Data;
using BT.Data.Entities;
using BT.Data.Repository;
using BT.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
namespace BT.Service.Employee
{
    /// <summary>
    /// 员工仓储实现类
    /// </summary>
    public class EmployeeRepository:BaseRepository<Employees, int>,IEmployeeRepository
    {
        public EmployeeRepository(DbContext context):base(context)
        { 
        
        }

        public async Task<Employees> LoginAsync(LoginDto loginDto)
        {
            var encryptPassword = MD5Encrypt.Encrypt(loginDto.Password);

            //md5 加密密码 与数据库密码进行比对

            var emp = await context.Set<Employees>().FirstOrDefaultAsync(
                x => x.Email == loginDto.Email && x.Password == encryptPassword
                && x.IsDel == 0
                );

            return emp;
        }
    }
}
