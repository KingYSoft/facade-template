"use strict";  
var connection = abp.signalr.hubs.common;
//  console.log(connection); 

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});
 
// connection.start().then(function(){
//     document.getElementById("sendButton").disabled = false;
// }).catch(function (err) {
//     return console.error(err.toString());
// });

document.getElementById("sendButton").addEventListener("click", function (event) {
    var user = document.getElementById("userInput").value;
    var message = document.getElementById("messageInput").value; 
    connection.invoke("SendMessage", user, message).catch(function (err) {
        //connection.start();
        return console.error(err.toString());
    });
    event.preventDefault();
});
 
abp.event.on('abp.signalr.connected', function() { // Register for connect event
    document.getElementById("sendButton").disabled = false;
    // console.log(connection); 
    connection.invoke('SendMessage', "Hi everybody","I'm connected to the chat!"); // Send a message to the server
});  