﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BT.Data.Entities
{
    /// <summary>
    /// 客户表
    /// </summary>
    public class Customers:BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        public string Alias { get; set; }

        public string Image { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public int CustomerSegmentationID { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string CustomerSegmentationName { get; set; }

        public string Remark { get; set; }

        public int EmployeesID { get; set; }

        /// <summary>
        /// 潜在客户/真实客户  0事潜在客户
        /// </summary>
        public int IsReal { get; set; }
    }
}
