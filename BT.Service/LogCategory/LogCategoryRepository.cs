using BT.Data.Entities;
using BT.Data.Repository;
using BT.Service.Employee;
using BT.Service.LogCategory;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BT.Service.LogCategory
{
    public class LogCategoryRepository : BaseRepository<LogCategories, int>, ILogCategoryRepository
    {
        public LogCategoryRepository(DbContext context) : base(context)
        {

        }
    }
}
