using BT.Data.Entities;
using BT.Data.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BT.Service.PublicCount
{
    public interface IPublicCountRepository : IBaseRepositoryT<PublicCounts>, IBaseRepositoryTID<PublicCounts, int>
    {
    }
}
