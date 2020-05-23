using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BT.Dto;
using BT.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

//身份授权，验证
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using BT.Data.Entities;
using BT.Core;
using Microsoft.Extensions.Options;
using BT.API.Models;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace BT.API.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IWrapperRepository wrapperRepository;
        private readonly IMapper mapper;
        private readonly CustomConfigOptions configOptions;

        public EmployeesController(IWrapperRepository wrapperRepository,IOptions<CustomConfigOptions> options,
            IMapper mapper
            )
        {
            this.wrapperRepository = wrapperRepository;
            this.mapper = mapper;
            this.configOptions = options.Value;
        }

        /// <summary>
        /// 员工登录
        /// </summary>
        /// <param name="loginDto">登录对象</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> EmployeeLoginAsync([FromBody]LoginDto loginDto)
        {
            var loginEmp = await wrapperRepository.EmployeeRepository.LoginAsync(loginDto);
            //Employees employees =  new Employees()  { Email="455764189@qq.com",Name= "fsnysohui" ,Phone="44861231"};
            
            if (loginEmp == null)
            {
                return NotFound();
            }

            //生成密钥
            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes("asdadhajhdkjahsdkjahdkj9au8d9adasidoad89asu813e"));

            //生成签名
            var sig = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            //声明你是谁
            List<Claim> claimList = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,loginEmp.ID.ToString()),  //type  //value
                new Claim(ClaimTypes.Email,loginEmp.Email),
                new Claim(JwtClaimTypes.PhoneNumber,loginEmp.Phone),
                new Claim(JwtClaimTypes.NickName,loginEmp.Name),
            };

            //获取员工对应的角色信息
            var roles = await wrapperRepository.Employee_RoleRepository.GetEmployees_RolesAsync(loginEmp.ID);
            foreach (var item in roles)
            {
                claimList.Add(new Claim(ClaimTypes.Role, item.RoleName));
            }


            //if(loginEmp.ID==1)
            //{
            //    claimList.Add(new Claim(ClaimTypes.Role, "管理员"));
            //}
            //else
            //{
            //    claimList.Add(new Claim(ClaimTypes.Role, "员工"));
            //}


            //创建一个jwt安全对象
            var jwtObj = new JwtSecurityToken(
                issuer: "www.zhangquque.com",  //发行人
                audience: "www.gaozijian.com", //签收者
                claims:claimList,
                signingCredentials:sig,
                expires:DateTime.Now.AddHours(2)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtObj);  //书写token
            //发送邮箱登录提醒
            EmailSendHelper.SendLoginEmail(loginEmp.Email, configOptions.EmailPassword);

            //计入登录日志
            await wrapperRepository.LogRepository.AddAsync(
                new Logs
                {
                    Content = $"{loginEmp.Name}在{DateTime.Now}上线",
                    CreateTime = DateTime.Now,
                    CreateID = loginEmp.ID,
                    CategoryID=1
                }
                );

            await wrapperRepository.LogRepository.SaveAsync();
            return Ok(new { token = token });
        }


        /// <summary>
        /// 员工登录获取相应信息和权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<EmployeeInfoDto>> GetEmployeeInfoAsync()
        {
            EmployeeInfoDto info = new EmployeeInfoDto();
            var empId =Convert.ToInt32(User.Identity.Name);

            var employee = await wrapperRepository.EmployeeRepository.GetByIdAsync(empId);

            if(employee==null)
            {
                return NotFound();
            }

            info = mapper.Map<EmployeeInfoDto>(employee);

            //拿到员工的所有角色 角色集合
            IQueryable<Employees_Roles> roles = await wrapperRepository.Employee_RoleRepository.GetEmployees_RolesAsync(employee.ID);

            //权限记录集合
            List<Roles_Permissions> roles_PermissionsList = new List<Roles_Permissions>();

            //遍历查询到所有的权限记录
            foreach (Employees_Roles item in roles.ToList())
            {
                IQueryable<Roles_Permissions> roles_Permissions = await wrapperRepository.Role_PermissionRepository.GetRoles_PermissionsByRoleID(item.RoleID);
                roles_PermissionsList.AddRange(roles_Permissions.ToList().ToArray());
            }

            //权限集合
            List<Permissions> permissionLiist = new List<Permissions>();

            //去重
            var distinctPeople = roles_PermissionsList
                .GroupBy(m => m.PermissionID)
                .Select(m => m.First()).ToList();

            //遍历权限记录表 那到相应的权限
            foreach (Roles_Permissions item in distinctPeople)
            {
                Permissions permissions = await wrapperRepository.PermissionRepository.GetByIdAsync(item.PermissionID);
                if (permissions.IsDel == 1)
                {
                    continue;
                }
                permissionLiist.Add(permissions);
            }
            info.Permissions = permissionLiist;
            return Ok(info);
        }
    }
}