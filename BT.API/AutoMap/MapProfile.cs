using AutoMapper;
using BT.Data.Entities;
using BT.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BT.API.AutoMap
{
    public class MapProfile:Profile
    {
        /// <summary>
        /// 对象与对象之间的映射器
        /// </summary>
        public MapProfile()
        {              //源        目的地
            CreateMap<Employees, EmployeeInfoDto>();
            CreateMap<EmployeeInfoDto, Employees>();
        }
    }
}
