using BT.Data.Entities;
using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Security;
using System.Text;

namespace BT.Dto
{
    /// <summary>
    /// 员工信息传输对象
    /// </summary>
    public class EmployeeInfoDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string HeadImage { get; set; }

        //权限集合
        public List<Permissions> Permissions { get; set; } = new List<Permissions>();
    }
}
