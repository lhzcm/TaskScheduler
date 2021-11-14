using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TaskSchedulerHost.Extensions;
using TaskSchedulerHost.Models;
using TaskSchedulerHost.Utility;
using TaskSchedulerModel.Models;
using TaskSchedulerRepository.Repositorys;

namespace TaskSchedulerHost.Controllers
{
    public class UserController : BaseController
    {
        private UserInfoRepository _repository;
        private ILogger<UserController> _logger;
        private IMemoryCache _cache;

        public UserController(UserInfoRepository repository, ILogger<UserController> logger, IMemoryCache cache)
        {
            this._repository = repository;
            this._logger = logger;
            this._cache = cache;
        }

        [HttpPost("login")]
        public Result Login([FromForm] int Userid, [FromForm] string Password)
        {
             var user = _repository.FindFirst(n => n.Id == Userid);
            if (user == null)
            {
                return Fail("登录失败，用户不存在!");
            }
            if (user.Password != Password)
            {
                return Fail("登录失败，登录密码错误!");
            }

            string token = UserUtility.GetTokenById(user.Id);
            Response.Cookies.Append("token", token);

            _cache.SetUser(user);

            return Success(new { Id = user.Id, Name = user.Name });
        }

        
    }
}
