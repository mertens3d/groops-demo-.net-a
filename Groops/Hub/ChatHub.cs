using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Groops.Services;

namespace Groops.Hubs
{
    public class ChatHub : Hub
    {


        public void Send(Guid userId, string message, Guid  roomId)
        {

            GroopsRepository groopsRepository = new GroopsRepository();
            string userName = groopsRepository.addNewComment(userId, message, roomId);

            //add the message to the database
            //how to do that now that we are on the server side
            //I don't think this would be considered part of the API, so we really need to make an api call
            //from here.




            //  Call the addNewMessageToPage method to update clients
            Clients.All.addNewMessageToPage(userName, message, roomId);
        }
    }
}