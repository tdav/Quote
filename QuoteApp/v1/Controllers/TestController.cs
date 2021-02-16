﻿using QuoteServer.Database.Services;
using QuoteServer.Views;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace QuoteServer.v1.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [SwaggerTag("Test")]
    public class TestController : ControllerBase
    {
        public readonly IGenSqlService gen;

        public TestController(IGenSqlService _gen) => gen = _gen;

        [HttpGet]
        public async Task<string> GetAsync()
        {
            throw new Exception("jkdskjdsd");
            var r = await gen.GetMainSqlAsync(new TransferModel()
            {
                LastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                IsMainOffice = true,
                DrugStoreId = Guid.Parse("30c47f1a-ad48-4270-8cab-88841d733a4e")
            });

            return r.Item1;
        }


        [HttpGet("{ss}")] 
        public string GetTest(string ss)
        {
            return ss;
        }
    }
}