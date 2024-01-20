const connection = new signalR.HubConnectionBuilder()
    .withUrl("https://localhost:44371/chathub")
    .configureLogging(signalR.LogLevel.Information)
    .build();

const start = async () => {
    try {
        await connection.start();
        console.log(`Connection started successfully ${loginUser.fullName}`);
    } catch (error) {
        console.error("Error starting connection:", error);
    }
}

async function loginUser() {
    try {
        const userFullname = loginUser.fullName;
        const phoneNumber = document.getElementById('phoneNumber').value;
        const password = document.getElementById('password').value;

        const loginData = {
            phoneNumber: phoneNumber,
            password: password
        };

        if (userFullname == null) {
            console.error('Xatolik');
            window.location.href = "./login.html";
        }

        if (userFullname) {
            sessionStorage.setItem('user', userFullname);
            await joinChat(userFullname);
        }

        console.log(get_User);

    }
    catch (error) 
    {
        console.error('Xatolik:', error);
        document.getElementById('errorDisplay').innerText = `Xatolik: ${error}`;
    }
}

const get_User = () => sessionStorage.getItem('user');

const startApp = async () => {
    await start();
}

const joinChat = async (user, message) => {
    try {
        await connection.invoke("JoinChat", user, message);
        console.log('Joined the chat');
    } catch (error) {
        console.log(error);
    }
}

startApp();
