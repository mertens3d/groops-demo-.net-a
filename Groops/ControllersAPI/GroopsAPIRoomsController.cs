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
    public class GroopsAPIRoomsController : ApiController
    {

        private GroopsRepository groopsRepository;

        public GroopsAPIRoomsController()
        {
            this.groopsRepository = new GroopsRepository();
        }

        //GroopsRoom[]
        public HttpResponseMessage  Get()
         //   public IEnumerable<GroopsRoom>  Get()
        //    public GroopsRoom[] Get
        
        {
            //return groopsRepository.GetAllRooms();

            GroopsRoom[] roomDataArr;

            

            

            

            roomDataArr = groopsRepository.GetAllRooms();

            var neededData = from oneRoom in roomDataArr
                             select new
                             {
                                 oneRoom.Name,
                                 oneRoom.CreatorUserId,
                                 oneRoom.ID
                             };


                 //var employeeGroups = from employee in Employee.GetAllEmployees()
                 //                group employee by new { employee.Department, employee.Gender } into eGroup
                 //                orderby eGroup.Key.Department, eGroup.Key.Gender
                 //                select new
                 //                {
                 //                    Dept = eGroup.Key.Department,
                 //                    Gender = eGroup.Key.Gender,
                 //                    Employees = eGroup.OrderBy(x => x.Name)
                 //                };




            //string[] roomsArr = new string[roomDataArr.Count()];
            
            //for (int i = 0; i < roomDataArr.Count(); i++)
            //{
            //    roomsArr[i] = roomDataArr[i].Name;
            //}
            

            //HttpResponseMessage response = Request.CreateResponse<string[]>(System.Net.HttpStatusCode.OK, roomsArr);
            //HttpResponseMessage response = Request.CreateResponse<GroopsRoom[]>(System.Net.HttpStatusCode.OK, roomDataArr);
            HttpResponseMessage response = Request.CreateResponse(System.Net.HttpStatusCode.OK, neededData);

            return response;
            //return roomDataArr;
        }



        public HttpResponseMessage Post(GroopsRoom roomData)
        {

            roomData.ID = Guid.NewGuid();

            this.groopsRepository.AddNewRoom(roomData);

            var response = Request.CreateResponse<GroopsRoom>(System.Net.HttpStatusCode.Created, roomData);

            return response;
        }

    }
}
