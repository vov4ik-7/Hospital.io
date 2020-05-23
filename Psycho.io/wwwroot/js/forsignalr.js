const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/chat")
    .configureLogging(signalR.LogLevel.Information)
    .build();

hubConnection.on("LiveChatMessageReceived", function (fromUserId, fromUserEmail, message) {
    addMessage(message, fromUserEmail, fromUserId);
});

function sendMessageButtonClick(toUserId, toUserEmail) {
    var messageContainer = $(`#message-${toUserId}`);
    var message = messageContainer.val();
    messageContainer.val("");
    addMessage(message, "Me", toUserId);

    var data = {
        toUserId: toUserId,
        toUserEmail: toUserEmail,
        message: message
    };
    $.ajax({
        url: $('#chatUtils').attr('send-message-url'),
        data: data,
        success: function (response) {}
    });
}

function openChat(friendId) {
    $(".chat-content").addClass("display-none");
    $(`#chat-${friendId}`).removeClass("display-none");
    scrollToBottom(friendId);
}

function addMessage(message, senderEmail, friendId) {
    var message = '<li class="d-flex justify-content-between mb-4">' +
        '     <div class="chat-body white p-3 ml-2 z-depth-1">' +
        '    <div class="header">' +
        '        <strong class="primary-font">' + senderEmail + '</strong>' +
        '    </div>' +
        '   <hr class="w-100">' +
        '   <p class="mb-0">' + message + '</p>' +
        '    </div>' +
        '</li>';

    $(`#chat-messages-${friendId}`).append(message);
    scrollToBottom(friendId);
}

function scrollToBottom(friendId) {
    var messagesContainer = $(`#chat-scroll-${friendId}`)[0];
    messagesContainer.scrollTop = messagesContainer.scrollHeight;
}

function openChatOrAddIfNotExists(friendId, friendEmail) {
    if (!friendId) {
        return;
    }

    if ($(`#user-${friendId}`).length) {
        openChat(friendId);
        return;
    }

    $(".no-active-chat-yet").addClass("display-none");

    //Add chat to list
    var user = `<li id="user-${friendId}" class="lighten-3 p-2">` +
        `<a href="#" onclick="openChat(${friendId})" class="d-flex justify-content-between">` +
        '<img src="../img/man.png" alt="avatar" class="avatar rounded-circle d-flex align-self-center mr-2 z-depth-1">' +
        `<div class="text-small">${friendEmail}</div>` +
        '<div class="chat-footer"></div>' +
        '</a>' +
        '</li>';
    $(".friend-list").append(user);

    //Add chat content
    var chatContainer = `<div id="chat-${friendId}" class="chat-content display-none">` +
        `<div id="chat-scroll-${friendId}" class="chat-message" style="height: 460px; overflow: auto;">` +
        `<ul id="chat-messages-${friendId}" class="list-unstyled chat">` +
        '</ul>' +
        '</div>' +
        '<div><div class="white"><div class="form-group basic-textarea">' +
        `<textarea class="form-control pl-2 my-0" id="message-${friendId}" rows="3" placeholder="Type your message here..."></textarea>` +
        '</div></div>' +
        `<button onclick="sendMessageButtonClick('${friendId}', '${friendEmail}')" type="button" class="btn btn-info btn-rounded btn-sm waves-effect waves-light float-right">Send</button>` +
        '</div></div>';

    $(".main-chat-container").append(chatContainer);

    openChat(friendId);
}

hubConnection.start();
