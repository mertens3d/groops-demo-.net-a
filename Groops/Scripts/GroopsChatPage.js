$(function () {

    //------- from nodeJs groops*@
    //Tiny helper to keep the list scrolled ot the bottom on each new message*@
    function scrollBody() {

        

        $('.body').animate({
            scrollTop: $('#messages').height()
        }, 200);
    }




    //since we are dynamically appending raw content in socket event handleers it's
    //up to us to escape the content manually to avoid xss and other nastiness

    function htmlEscape(str) {
        return String(str)
        .replace(/&/g, '&amp;')
        .replace(/"/g, '&quot;')
        .replace(/'/g, '&#39;')
        .replace(/</g, '&lt;')
        .replace(/>/g, '&gt;');
    }


    //since this can be executed from multiple event handlers we pulled the
    //logic for posting a message into its own helper function.
    //function postMessage(msg) {

    //    debugger;
    //    msg = $.trim(msg || $('input[name=message]').val());

    //    //if the message is blank skip it
    //    if (msg.length) {
    //        //todo roomId
    //        $.ajax({
    //            url: "/api/chat/" + $("#roomId").val,  // + "roomid/message',
    //            method: 'POST',
    //            context: this,
    //            data: {
    //                message: msg,
    //                //todo authorId
    //                userId: $("#userId").val()
    //            }


    //        })
    //        .success(function (data) {
    //            //clear the input for the next message
    //            $('input[name=message]').val('');

    //        });
    //    }
    //    else {
    //        $('input[name=message]').val('');
    //    }

    //}





    //immediatly scroll  the list to the bottom on load
    scrollBody();



    //init socket.io so that we're ready to handle events
    //var socket = io();



    //notify the server (and other clients) that we have joined this room
    //socket.emit('join', {
    //   room: 'toDo roomID',
    //    user: 'toDo userID'
    //});

    function drawOneMessage(messageData) {

        //console.log('Message created', data);
        var row = "<tr><td><img src='" + userId.img + "' align='left' class='grp-gravatar' />";
        row += htmlEscape(messageData.message) + "<br /><span class='name'>" + htmlEscape(messageData.userName);
        row += "</span></td></tr>";

        $('#messages > tbody:last').append(row);
        scrollBody();


    }




    //once we have joined the room, set up a proper communication channel
    //socket.on('joined', function () {
    //    //when a message changes state, update the ui accordingly
    //    socket.on('message', function (data) {
    //        if (data.action === 'create') {
    //           drawOneMessage(data)
    //        }

    //    });

    //    //todo when a users status changes, update the ui accodingly
    //    socket.on('user', function (data) {
    //        if (data.action === 'join') {
    //            console.log('A new user joined the room', data);
    //        }
    //        else {
    //            console.log('A user left the room', data);
    //        }



    //    });

    //});

    //if there's an error, let the user know (ideally more nicely than this...)
    //socket.on('exception', function (msg) {
    //    alert(msg);
    //});



    //------------------- signalR script below







    // SignalR script to update the chat page and send messages.


    //refereence the auto-generated proxy for the hub.
    var chat = $.connection.chatHub;
    //Create a function that the hub can call back to display messages.
    chat.client.addNewMessageToPage = function (userName, message, roomId) {
        
        drawOneMessage(userName, message, roomId);


    };
    
    //Set initial focus to message input box.
    $('#message').focus();


    //Handle the "send" button click event
    //$("#send").on('click', function () {
    //    debugger;
    //    postMessage();
    //});

    
    //Start the connection.
    //once it's set up, add the click and enter event handlers
    $.connection.hub.start().done(function () {
        $('#sendmessage').click(deliverGeneric);
        $('#message').bind("enterKey", function (e) {
            deliverGeneric();
        });
        $('#message').keyup(function (e) {
            if (e.keyCode == 13) {
                $(this).trigger("enterKey");
            }
        })
    });







    deliverGeneric = function () {
        //function(){
        //Call the send method on the hub
        
        chat.server.send($('#userId').val(), $('#message').val(), $('#roomId').val());


        //clear the text box and reset focus for next comment
        $('#message').val('').focus;
    }


    //This optional function html-encodes messages for display in the page
    function htmlEncode(value) {
        var encodedValue = $('<div />').text(value).html();
        return encodedValue;
    }




        function callToGetAllPriorMessages() {
            $.ajax({
                datatype: "json",
                url: "/api/GroopsAPIChat/" +  $("#roomId").val(),
                type: "GET"
                
            }).done(function (data) { populateAllMessages(data); }).error(function (data) {
                //todo clean up error message
                debugger;
                alert("error"); var cat = data;
            });



        }


        function populateAllMessages(messagesData) {
                
            //first clear out any rooms that may already be in the list
            //we are doing this because the user may have just added a new room and
            //we need to redraw the list

            for (var idx = 0; idx < messagesData.length; idx++) {
                drawOneMessage(messagesData[idx]);
            }
                
        }

        callToGetAllPriorMessages();

    

    
});