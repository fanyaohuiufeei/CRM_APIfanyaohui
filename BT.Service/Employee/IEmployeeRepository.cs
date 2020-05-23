﻿using BT.Data.Entities;
using BT.Data.Repository;
using BT.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BT.Service.Employee
{
    
    /// <summary>
    /// 员工仓储接口
    /// </summary>
    public interface IEmployeeRepository: IBaseRepositoryT<Employees>,IBaseRepositoryTID<Employees,int>
    {

        //员工登录
        Task<Employees> LoginAsync(LoginDto loginDto);

    }
}
