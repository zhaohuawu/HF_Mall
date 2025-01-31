﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bryan.Common;
using Bryan.Common.Extension;
using Bryan.MicroService;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HF.Goods.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GoodsController : BaseController
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var kk = new Domain.DomainModel.Gd_Goods();
            
            return id.ToSafeString();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
