using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Groops.Models;

namespace Groops.Services
{



    public class GroopsRepository
    {

        private const string CacheKey = "GroopsStore";

        public GroopsRepository()
        {


            //cache use is only temporary
            var ctx = HttpContext.Current;
            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var groopsApi = new GroopsAPI[]
                    {

                        new GroopsAPI{
                    Id=1,
                    Name = "blah blah"
                },
                new GroopsAPI{
                    Id=2,
                    Name = "cha cha"
                }

                    };

                    ctx.Cache[CacheKey] = groopsApi;
                }
            }
        }

        public object[] getAllMessagesForRoom(Guid roomId)
        {


            DataClassesContextDataContext db = new DataClassesContextDataContext();
            object[] allMatchingMessages = (from oneMessage in db.GroopsMessages
                                            where oneMessage.RoomId == roomId
                                            select new
                                            {
                                                userName = userIdToUserName(oneMessage.UserId),
                                                message = oneMessage.Content,

                                            }).ToArray();

            return allMatchingMessages;



        }
        public string  getRoomNameFromId(Guid roomId){
            DataClassesContextDataContext db = new DataClassesContextDataContext();
            string roomName = (from oneRoom in db.GroopsRooms
                               where oneRoom.ID == roomId
                               select oneRoom.Name).FirstOrDefault();

            return roomName;
        }

        private string userIdToUserName(Guid userId)
        {
            DataClassesContextDataContext db = new DataClassesContextDataContext();

            GroopsUser thisUser = (from oneUser in db.GroopsUsers
                                   where oneUser.ID == userId
                                   select oneUser).FirstOrDefault();

            // GroopsUser oneUserB = (GroopsUser) thisUser;



            return thisUser.Name;
        }

        public string addNewComment(Guid userId, string message, Guid roomId)
        {
            DataClassesContextDataContext db = new DataClassesContextDataContext();

            GroopsMessages thisMessage = new GroopsMessages();

            thisMessage.Content = message;
            thisMessage.ID = Guid.NewGuid();
            thisMessage.RoomId = roomId;
            thisMessage.UserId = userId;
            thisMessage.Created = DateTime.Now;

            db.GroopsMessages.InsertOnSubmit(thisMessage);

            try
            {

                db.SubmitChanges();
                return userIdToUserName(userId);
            }
            catch (Exception e)
            {

                return null;
            }

        }
        public bool AddNewRoom(GroopsRoom newRoom)
        {

            DataClassesContextDataContext db = new DataClassesContextDataContext();

            var thisRoom = new GroopsRoom();

            thisRoom.Name = newRoom.Name;
            thisRoom.CreatorUserId = newRoom.CreatorUserId;
            thisRoom.ID = newRoom.ID;


            db.GroopsRooms.InsertOnSubmit(thisRoom);

            //submit the changes to the database
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }


        }

        public GroopsRoom[] GetAllRooms()
        {

            //var ctx = HttpContext.Current;

            //if (ctx != null)
            //{
            //    return (GroopsAPI[])ctx.Cache[CacheKey];

            //}

            //return new GroopsAPI[]{

            //    new GroopsAPI{
            //        Id=0,
            //        Name = "Placeholder"
            //    }

            //};


            DataClassesContextDataContext db = new DataClassesContextDataContext();
            GroopsRoom[] allRooms = db.GroopsRooms.Select(x => x).ToArray();

            return allRooms;


        }

        public bool AddNewUser(GroopsUser oneGroopMember)
        {
            //var ctx = HttpContext.Current;
            //if (ctx != null)
            //{
            //    try
            //    {
            //        var currentData = ((GroopsAPI[])ctx.Cache[CacheKey]).ToList();
            //        currentData.Add(oneGroopMember);
            //        ctx.Cache[CacheKey] = currentData.ToArray();

            //        return true;
            //    }


            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.ToString());
            //        return false;
            //    }
            //}



            DataClassesContextDataContext db = new DataClassesContextDataContext();

            var thisUser = new GroopsUser();

            thisUser.Name = oneGroopMember.Name;
            thisUser.Email = oneGroopMember.Email;
            thisUser.ID = oneGroopMember.ID;

            db.GroopsUsers.InsertOnSubmit(thisUser);

            //submit the changes to the database
            try
            {
                db.SubmitChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }


        }
    }
}