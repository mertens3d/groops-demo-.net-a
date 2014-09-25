using Groops.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Groops.ControllersAPI
{
    public class GroopsAPIChatController : ApiController
    {
        

        // GET: api/Chat
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Chat/5
        public object[] Get(string id, [FromBody]string value)
        {

            GroopsRepository groopsRepository = new GroopsRepository();
            Guid thisRoomId = new Guid(id);
            object[] messages = groopsRepository.getAllMessagesForRoom(thisRoomId);
            //Guid roomId = Request.QueryString["userId"];

            //we need to get and return all the messages for that room


            return messages;
        }

        // POST: api/Chat
        public void Post([FromBody]string value)
        {
        }

        // POST: api/Chat/34957
        public void Post(string roomId, [FromBody]string value)
        {

            int cat = 1;
        }



        // PUT: api/Chat/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Chat/5
        public void Delete(int id)
        {
        }
    }
}
