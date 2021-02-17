﻿using Arch.EntityFrameworkCore.UnitOfWork;
using Swashbuckle.AspNetCore.Annotations;
using QuoteServer.Extensions.Controllers;
using Quote.Database.Models;
using Microsoft.Extensions.Caching.Memory;

namespace QuoteServer.v1.Controllers
{
    [SwaggerTag("Quote Category")]
    public class CategoryController : BaseController<spCategory>
    {
        public CategoryController(IUnitOfWork _db, IMemoryCache _cache) : base(_db, _cache) { }
    }
}
