const hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:5001/chat")
    .configureLogging(signalR.LogLevel.Information)
    .build();

var currid = 1;
var username = $('#'+currid+'un').text();
var img = '';
    if (ispsycho == 'False') {
        img = "../img/user.png";
    }
    else {
        img = "../img/man.png";
    }
hubConnection.on("Receive", function (message, userName) {

    // создаем элемент <b> для имени пользователя
    //let userNameElem = document.createElement("b");
    //userNameElem.appendChild(document.createTextNode(userName + ": "));

    // '    <img src="'+ img +'" alt="avatar" class="avatar rounded-circle mr-2 ml-lg-3 ml-0 z-depth-1">' +

    var kek = '<li class="d-flex justify-content-between mb-4">' +
         
          '     <div class="chat-body white p-3 ml-2 z-depth-1">' +
          '    <div class="header">' +
          '        <strong class="primary-font">' + userName + '</strong>' +
        '    </div>' +
        '   <hr class="w-100">' +
        '   <p class="mb-0">' + message + '</p>' +
          '    </div>' +
          '</li>';


    $('#i'+currid).append(kek);

    // создает элемент <p> для сообщения пользователя
    /*let elem = document.createElement("p");
    elem.appendChild(userNameElem);
    elem.appendChild(document.createTextNode(message));

    var firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);*/
});
hubConnection.on("Notify", function (message) {

    // создает элемент <p> для сообщения пользователя
    /*let elem = document.createElement("p");
    elem.appendChild(document.createTextNode(message));

    var firstElem = document.getElementById("chatroom").firstChild;
    document.getElementById("chatroom").insertBefore(elem, firstElem);*/

    var lol = '<li class="d-flex justify-content-between mb-4">' +
          '     <div class="chat-body white p-3 ml-2 z-depth-1">' +
          '    <div class="header">' +
          '        <strong class="primary-font"> Notification </strong>' +
        '    </div>' +
        '   <hr class="w-100">' +
        '   <p class="mb-0">' + message + '</p>' +
          '    </div>' +
          '</li>';

    $('#i'+currid).append(lol);
});

// отправка сообщения на сервер
document.getElementById("sendBtn").addEventListener("click", function (e) {
    let message = document.getElementById("message").value;
    let to = username;//document.getElementById("receiver").textContent;
    document.getElementById("message").value = '';
    hubConnection.invoke("Send", message, to);
});

hubConnection.start();       // начинаем соединение с хабом
