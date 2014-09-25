using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Groops.Services;


namespace Groops.Controllers
{
    public class GroopsAPIUsersController : ApiController
    {

        private GroopsRepository groopsRepository = new GroopsRepository();

        // GET: api/GroopsUsers
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GroopsUsers/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/GroopsUsers
        //public void Post([FromBody]string value)
        //HttpResponseMessage
        public string   Post( GroopsUser userValue)
            //public bool  Post( GroopsUser userValue)
           
        {


            userValue.ID =  Guid.NewGuid();

           bool results = groopsRepository.AddNewUser(userValue);
        //   return results;

         //  var response = Request.CreateResponse(HttpStatusCode.Redirect);

           //from http://stackoverflow.com/questions/11324711/redirect-from-asp-net-web-api-post-action
       //    string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
        //   response.Headers.Location = new Uri (fullyQualifiedUrl + "/home/rooms/?userID=" + userValue.ID);


           //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(args[0]);
           //request.CookieContainer = new CookieContainer();

           //HttpWebResponse responseB = (HttpWebResponse)request.GetResponse();


           //Cookie cookieB = httpcontext.request.cookies.get("UserName");



           //Cookie cookie = new Cookie("UserName", userValue.Name);

            



           return userValue.ID.ToString();

        }

        // PUT: api/GroopsUsers/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/GroopsUsers/5
        public void Delete(int id)
        {
        }
    }
}
