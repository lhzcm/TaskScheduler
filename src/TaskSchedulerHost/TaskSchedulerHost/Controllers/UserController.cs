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
        private ILogger<UserController> _logger;
        private IMemoryCache _cache;
        private Config _config;

        public UserController(UserInfoRepository repository, ILogger<UserController> logger, IMemoryCache cache, Config config)
        {
            this._repository = repository;
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

                return Success(new { Id = user.Id, Name = user.Name });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }

        [HttpPost("add")]
        [Login]
        public Result Add([FromForm] string Name, [FromForm] string Password)
        {
            if (!User.Super)
            {
                return Fail("添加用户失败，你没有权限操作");
            }
            try
            {

                UserInfo user = new UserInfo { Name = Name, Password = UserUtility.EncryptPassword(Password) };
                if (_repository.Insert(user) <= 0)
                {
                    return Fail("添加用户失败！");
                }
                user.Password = Password;
                return Success(user, "添加成功!");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message + ex.StackTrace);
                return Fail("系统错误");
            }
        }
        
    }
}
