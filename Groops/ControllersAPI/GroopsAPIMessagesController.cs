using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Groops.Controllers
{
    public class GroopsAPIMessagesController : ApiController
    {
        // GET: api/GroopsMessages
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GroopsMessages/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GroopsMessages
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/GroopsMessages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GroopsMessages/5
        public void Delete(int id)
        {
        }
    }
}
