using Groops.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Groops.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        public ActionResult Intro()
        {
            return View();
        }

        public ActionResult Chat(string id)
        {
            ViewData["roomId"] = id;

            ViewData["userId"] = Request.QueryString["userId"];

            GroopsRepository groopsRepository = new GroopsRepository();

            //for some reason this is getting called twice. I think the second time 
            //is because of SignalR/ Sockets firing up
            if (id != "undefined")
            {
                Guid roomId = new Guid(id);

                ViewData["roomName"] = groopsRepository.getRoomNameFromId(roomId);
            }

            return View();
        }



        public ActionResult Rooms()
        {

            string userId = Request.QueryString["UserId"];

            ViewData["userId"] = userId;

            return View();
        }


    }
}