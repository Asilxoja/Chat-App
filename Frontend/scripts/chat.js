"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:7265/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

const start = async () => {
    try {
        await connection.start();
        console.log("Connected to signal r hub");
    } catch (error) {
        console.log(error);
    }
}

const joinUser = async () => {
    const name = window.prompt('Enter the name: ');
    if (name)
    {
        sessionStorage.setItem('user', name);  
        await joinChat(name);
    }
}

const joinChat = async (user) => {
    if (!user)
       return;
    try {
        const message = `${user} joined`;
        await connection.invoke("JoinChat", user, message);
    } catch (error) {
        console.log(error);
    }
}

const getUser = () => sessionStorage.getItem('user')

const receiveMessage = async () => {
    const currentUser = getUser();
    if (!currentUser)
        return;
    try {
        await connection.on("ReceiveMessage", (user, message) => {
         const messageClass = currentUser === user ? "send" : "received";
            appendMessage(message, messageClass);
            const alertSound = new Audio('./chat-sound.mp3');
            alertSound.play();
       })
    } catch (error) {
        console.log(error);
    }
}

const appendMessage = (message,messageClass) => {
    const messageSectionEl = document.getElementById('messageSection');
    const msgBoxEl = document.createElement("div");
    msgBoxEl.classList.add("msg-box");
    msgBoxEl.classList.add(messageClass);
    msgBoxEl.innerHTML = message;
    messageSectionEl.appendChild(msgBoxEl);
}

document.getElementById('btnSend').addEventListener('click', async (e) => {
    e.preventDefault();
    const user = getUser();
    if (!user)
        return;
    const txtMessageEl = document.getElementById('txtMessage');
    const msg = txtMessageEl.value;
    if (msg) {
        // call the sendmessage api
        await sendMessage(user,`${user}: ${msg}`);  // john: hey guys
        txtMessageEl.value = "";
    }
})

const sendMessage = async (user,message) => {
    
    try {
        await connection.invoke('SendMessage', user, message);
    } catch (error) {
        console.log(error);
    }
}


const startApp = async () => {
    await start();
    await joinUser();
    await receiveMessage();
}

startApp();