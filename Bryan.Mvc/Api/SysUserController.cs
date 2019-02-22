using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bryan.Mvc.Api
{
    [Route("api/[controller]")]
    public class SysUserController : ControllerBase
    {
        // GET: api/SysUser/5
        [Route("~/api/SysUser/Get")]
        public string Get(string name)
        {
            return "value-" + name;
        }

        // POST: api/SysUser
        [HttpPost]
        [Route("Post")]
        public string Post(string value)
        {
            return "value-" + value;
        }

        // PUT: api/SysUser/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
