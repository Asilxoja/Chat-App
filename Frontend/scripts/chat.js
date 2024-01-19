function sendMessage() {
    const messageInput = document.getElementById('message');
    const message = messageInput.value;

    // Siz bu xabarni serverga yuborish yoki kerakli holatda qabul qilishingiz mumkin
    // Hozirda, xabarni chatMessages divida ko'rsatib turamiz
    const chatMessages = document.getElementById('chatMessages');
    chatMessages.innerHTML += `<p>${message}</p>`;

    // Kiritish maydonini tozalash
    messageInput.value = '';
}
