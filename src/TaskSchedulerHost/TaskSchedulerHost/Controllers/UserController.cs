using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TaskSchedulerHost.Extensions;
using TaskSchedulerHost.Filter;
using TaskSchedulerHost.Models;
using TaskSchedulerHost.Utility;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerHost.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : BaseController
    {
        private UserInfoRepository _repository;
        private TaskManageRepository _marepository;
        private ILogger<UserController> _logger;
        private IMemoryCache _cache;
        private Config _config;

        public UserController(UserInfoRepository repository, TaskManageRepository marepository, ILogger<UserController> logger, IMemoryCache cache, Config config)
        {
            this._repository = repository;
            this._marepository = marepository;
            this._logger = logger;
            this._cache = cache;
            this._config = config;
        }

        [HttpPost("login")]
        public Result Login([FromForm] int Userid, [FromForm] string Password)
        {
            try
            {
                var user = _repository.FindFirst(n => n.Id == Userid);
                if (user == null)
                {
                    return Fail("登录失败，用户不存在!");
                }
                if (user.Password != UserUtility.EncryptPassword(Password))
                {
                    return Fail("登录失败，登录密码错误!");
                }

                string token = UserUtility.GetTokenById(user.Id);
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(_config.TokenDays);
                Response.Cookies.Append("token", token, options);

                _cache.SetUser(user);

                return Success(new { Id = user.Id, Name = user.Name, user.Super });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 用户信息
        /// </summary>
        /// <returns></returns>
        [Login]
        [HttpGet("UserInfo")]
        public Result UserInfo()
        {
            return Success(User);
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="Name">用户名称</param>
        /// <param name="Password">用户密码</param>
        /// <returns></returns>
        [HttpPost("Add")]
        [Login]
        public Result Add([FromForm] string name, [FromForm] string password)
        {
            if (!User.Super)
            {
                return Fail("添加用户失败，你没有权限操作");
            }
            try
            {
                if (_repository.Exists(n => n.Name == name))
                {
                    return Fail("用户名称已存在！");
                }
                UserInfo user = new UserInfo { Name = name, Password = UserUtility.EncryptPassword(password) };
                if (_repository.Insert(user) <= 0)
                {
                    return Fail("添加用户失败！");
                }
                user.Password = password;
                return Success(user, "添加成功!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        [HttpGet("{userId?}")]
        public Result UserList(int userId)
        {
            if (!User.Super)
            {
                return Fail("你没有权限操作");
            }
            try
            {
                List<UserInfo> user;
                if (userId <= 0)
                {
                    user = _repository.FindAll().ToList();
                }
                else
                {
                    user = _repository.FindAll().Where(n => n.Id == userId).ToList();
                }
                return Success(new { list = user });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="super"></param>
        /// <returns></returns>
        [HttpPatch]
        public Result UpdateUser([FromForm] int userId, [FromForm] string name, [FromForm] bool super)
        {
            if (userId <= 0)
            {
                return Fail("用户错误");
            }
            try
            {
                var manage = _repository.FindFirst(n => n.Id == userId);
                if (manage == null)
                {
                    return Fail("更新失败，用户不存在");
                }

                _repository.Update(n => n.Id == userId, n => new UserInfo { Name = name, Super = super });
                return Success(manage, "更新成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        public Result DelUser([FromForm] int userId)
        {
            try
            {
                if (_repository.Delete(n => n.Id == userId) <= 0)
                {
                    return Fail("删除失败");
                }
                _marepository.Delete(n => n.UserId == userId);
                return Success(null, "删除成功");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }
    }
}
