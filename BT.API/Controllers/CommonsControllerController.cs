using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using AutoMapper;
using BT.Data;
using BT.Data.Entities;
using BT.Data.Migrations;
using BT.Dto;
using BT.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace BT.API.Controllers
{
    [Route("api/commons")]
    [ApiController]
    [Authorize]
    public class CommonsControllerController : ControllerBase
    {
        private readonly IWrapperRepository wrapperRepository;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;
        private readonly CRMContext context;

        public CommonsControllerController(IWrapperRepository wrapperRepository, IMapper mapper, IDistributedCache distributedCache,CRMContext context)
        {
            this.wrapperRepository = wrapperRepository;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
            this.context = context;
        }


        [HttpGet("WebCount")]
        [AllowAnonymous]
        public async Task<IActionResult> AddWenCountAsync()
        {
            //ip集合
            var ips = await Dns.GetHostAddressesAsync(Dns.GetHostName());

            //获取ip
            var ip = ips.FirstOrDefault(m => m.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).ToString();

            //从缓冲拿数据
            var ipCache = await distributedCache.GetStringAsync("ip");

            if (string.IsNullOrEmpty(ipCache))
            {
                DistributedCacheEntryOptions optionss = new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(2)
                };
                await distributedCache.SetStringAsync("ip", ip, optionss);

                //获取网站的计数对象
                var webCount = await (await wrapperRepository.PublicCountRepository.GetAllAsync()).FirstOrDefaultAsync(m => m.Type == "Web");

                //网站访问量+1
                webCount.Value += 1;
                //更新导数据库
                await wrapperRepository.PublicCountRepository.UpdateAsync(webCount);
                await wrapperRepository.PublicCountRepository.SaveAsync();
            }

            return Ok();
        }

        [HttpGet("ShowCount")]
        public async Task<ActionResult<List<PublicCounts>>> GetPublicCountsAsync()
        {
            return (await wrapperRepository.PublicCountRepository.GetAllAsync()).ToList();
        }

        /// <summary>
        /// 柱状图数据
        /// </summary>
        /// <returns></returns>

        [HttpGet("histogram")]
        [Authorize]
        public async Task<ActionResult> GetHistogramDataAsync()
        {
            var one = await context.TrackRecords.Where(m => m.CreateTime.Month == 1 && m.IsDel == 0).ToListAsync();
            var two = await context.TrackRecords.Where(m => m.CreateTime.Month == 2 && m.IsDel == 0).ToListAsync();
            var three = await context.TrackRecords.Where(m => m.CreateTime.Month == 3 && m.IsDel == 0).ToListAsync();
            var four = await context.TrackRecords.Where(m => m.CreateTime.Month == 4 && m.IsDel == 0).ToListAsync();
            var five = await context.TrackRecords.Where(m => m.CreateTime.Month == 5 && m.IsDel == 0).ToListAsync();
            var six = await context.TrackRecords.Where(m => m.CreateTime.Month == 6 && m.IsDel == 0).ToListAsync();
            var server = await context.TrackRecords.Where(m => m.CreateTime.Month == 7 && m.IsDel == 0).ToListAsync();
            var eight = await context.TrackRecords.Where(m => m.CreateTime.Month == 8 && m.IsDel == 0).ToListAsync();
            var nine = await context.TrackRecords.Where(m => m.CreateTime.Month == 9 && m.IsDel == 0).ToListAsync();
            var ten = await context.TrackRecords.Where(m => m.CreateTime.Month == 10 && m.IsDel == 0).ToListAsync();
            var eleven = await context.TrackRecords.Where(m => m.CreateTime.Month == 11 && m.IsDel == 0).ToListAsync();
            var twelve = await context.TrackRecords.Where(m => m.CreateTime.Month == 12 && m.IsDel == 0).ToListAsync();

            decimal[] data = new decimal[12];
            data[0] = one.Sum(m => m.TotalPrice);
            data[1] = two.Sum(m => m.TotalPrice);
            data[2] = three.Sum(m => m.TotalPrice);
            data[3] = four.Sum(m => m.TotalPrice);
            data[4] = five.Sum(m => m.TotalPrice);
            data[5] = six.Sum(m => m.TotalPrice);
            data[6] = server.Sum(m => m.TotalPrice);
            data[7] = eight.Sum(m => m.TotalPrice);
            data[8] = nine.Sum(m => m.TotalPrice);
            data[9] = ten.Sum(m => m.TotalPrice);
            data[10] = eleven.Sum(m => m.TotalPrice);
            data[11] = twelve.Sum(m => m.TotalPrice);

            return Ok(data);
        }

        /// <summary>
        /// 扇形图数据
        /// </summary>
        /// <returns></returns>
        [HttpGet("sector")]
        [Authorize]
        public async Task<ActionResult> GetSectorDataAsync()
        {
            var one = await context.Customers.Where(m => m.CreateTime.Month == 1 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var two = await context.Customers.Where(m => m.CreateTime.Month == 2 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var three = await context.Customers.Where(m => m.CreateTime.Month == 3 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var four = await context.Customers.Where(m => m.CreateTime.Month == 4 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var five = await context.Customers.Where(m => m.CreateTime.Month == 5 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var six = await context.Customers.Where(m => m.CreateTime.Month == 6 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var server = await context.Customers.Where(m => m.CreateTime.Month == 7 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var eight = await context.Customers.Where(m => m.CreateTime.Month == 8 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var nine = await context.Customers.Where(m => m.CreateTime.Month == 9 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var ten = await context.Customers.Where(m => m.CreateTime.Month == 10 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var eleven = await context.Customers.Where(m => m.CreateTime.Month == 11 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();
            var twelve = await context.Customers.Where(m => m.CreateTime.Month == 12 && m.IsDel == 0 && m.IsReal == 1).ToListAsync();

            SectorDto[] data = new SectorDto[12];
            data[0] = new SectorDto { Name = "1月", Value = one.Count() };
            data[1] = new SectorDto { Name = "2月", Value = two.Count() };
            data[2] = new SectorDto { Name = "3月", Value = three.Count() };
            data[3] = new SectorDto { Name = "4月", Value = four.Count() };
            data[4] = new SectorDto { Name = "5月", Value = five.Count() };
            data[5] = new SectorDto { Name = "6月", Value = six.Count() };
            data[6] = new SectorDto { Name = "7月", Value = server.Count() };
            data[7] = new SectorDto { Name = "8月", Value = eight.Count() };
            data[8] = new SectorDto { Name = "9月", Value = nine.Count() };
            data[9] = new SectorDto { Name = "10月", Value = ten.Count() };
            data[10] = new SectorDto { Name = "11月", Value = eleven.Count() };
            data[11] = new SectorDto { Name = "12月", Value = twelve.Count() };
            return Ok(data);
        }
    }
}