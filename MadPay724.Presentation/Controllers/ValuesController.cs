﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadPay724.Data.DatabaseContext;
using MadPay724.Data.Models;
using MadPay724.Repo.Infrastructure;
using MadPay724.Services.Interface;
using MadPay724.Services.Service;
using Microsoft.AspNetCore.Mvc;

namespace MadPay724.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork<MadPayDbContext> _db;
        private readonly IAuthService _authService;
        public ValuesController(IUnitOfWork<MadPayDbContext> dbContext, IAuthService authService)
        {
            _db = dbContext;
            _authService = new AuthService(_db);
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var user = new User()
            {
                Address = "شیراز",
                City = "",
                Name = "حمیدرضا سمیعی نیا",
                UserName = "hamidghalati",
                PhoneNumber = "09351360402",
                IsActive = true,
                PasswordHash = new byte[] { 0x20, 0x20, 0x20, 0x20 },
                PasswordSalt = new byte[] { 0x20, 0x20, 0x20, 0x20 }

            };
            var u = await _authService.Register(user, "hamid2311");

            return Ok(u);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<string> Post([FromBody] string value)
        {
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<string> Put(int id, [FromBody] string value)
        {
            return null;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<string> Delete(int id)
        {
            return null;
        }
    }
}
