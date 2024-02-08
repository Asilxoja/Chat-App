"use strict";

// Creating a SignalR connection
const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44371/chathub") // URL of your SignalR hub
    .configureLogging(signalR.LogLevel.Information)
    .build();

// Function to start the SignalR connection
const start = async () => {
    try {
        await connection.start();
        console.log("Connected to signal r hub");
    } catch (error) {
        console.log(error);
    }
}

// Function to prompt the user to enter their username and join the chat
const joinUser = async () => {
    const name = window.prompt('Enter the username: ');
    if (name) {
        sessionStorage.setItem('user', name);
        await joinChat(name);
    }
}

// Function to send a message to the hub when a user joins the chat
const joinChat = async (user) => {
    if (!user)
        return;
    try {
        const message = `"${user}" joined the chat`;
        await connection.invoke("JoinChat", user, message);
    } catch (error) {
        console.log(error);
    }
}

// Function to get the current user from sessionStorage
const getUser = () => sessionStorage.getItem('user');

// Function to receive messages from the hub and display them
const receiveMessage = async () => {
    const currentUser = getUser();
    if (!currentUser)
        return;
    try {
        await connection.on("ReceiveMessage", (user, message, timeAgo) => {
            const messageClass = currentUser === user ? "send" : "received";
            appendMessage(user, message, timeAgo, messageClass);
            const alertSound = new Audio('chat-sound.mp3');
            alertSound.play();
        })
    } catch (error) {
        console.log(error);
    }
}

// Function to append a message to the chat window
const appendMessage = (user, message, timeAgo, messageClass) => {
    const messageSectionEl = document.getElementById('messageSection');
    const msgBoxEl = document.createElement("div");
    msgBoxEl.classList.add("msg-box");
    msgBoxEl.classList.add(messageClass);
    msgBoxEl.innerHTML = `<span class="user">${user}:</span> ${message} <span class="timestamp"><br>${timeAgo}</span>`;
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
        await sendMessage(user, msg);
        txtMessageEl.value = "";
    }
})

// Function to send a message to the hub
const sendMessage = async (user, message) => {
    try {
        await connection.invoke('SendMessage', user, message);
    } catch (error) {
        console.log(error);
    }
}

// Function to initialize the application
const startApp = async () => {
    await start();
    await joinUser();   
    await receiveMessage();
}

// Start the application
startApp();
