using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Groops.Models;
using Groops.Services;

namespace Groops.Controllers
{
    public class GroopsAPIController : ApiController
    {

        private GroopsRepository groopsRepository;

        public GroopsAPIController()
        {
            this.groopsRepository = new GroopsRepository();
        }


        public GroopsAPI[] Get()
        {
            return groopsRepository.GetAllContacts();
        }



        public HttpResponseMessage Post(GroopsAPI contact)
        {
            this.groopsRepository.SaveGroopsAPI(contact);

            var response = Request.CreateResponse<GroopsAPI>(System.Net.HttpStatusCode.Created, contact);

            return response;
        }

    }
}
