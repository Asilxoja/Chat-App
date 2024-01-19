async function loginUser() {
    const phoneNumber = document.getElementById('phoneNumber').value;
    const password = document.getElementById('password').value;

    const loginData = {
        phoneNumber: phoneNumber,
        password: password
    };

    try {
        const response = await fetch('https://localhost:44371/Auth/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
        });

        if (response.ok) {
            // Muvaffaqiyatli kirish
            const resultDiv = document.getElementById('result');
            resultDiv.innerText = 'Muvaffaqiyatli kirish!';
            resultDiv.style.color = 'green';
            resultDiv.style.display = 'block';

            // Xatolik xabarlari tozalash
            document.getElementById('errorDisplay').innerText = '';

            // Chat sahifasiga yo'naltirish uchun kichik bir kechikish (masalan, 2 sekund)
            setTimeout(() => {
                window.location.href = 'chat.html'; // Chat sahifasi URL'ini o'zgartiring
            }, 2000);
        } else if (response.status == 400) {
            // Kirishda xatolik
            const errorText = await response.text();
            console.error(`Xatolik: ${errorText}`);
            
            const errorDisplay = document.getElementById('errorDisplay');
            errorDisplay.innerText = `Telefon raqam yoki parol xato. Iltimos, qaytadan urinib ko'ring.`;
            errorDisplay.style.color = 'red';
            errorDisplay.style.display = 'block';
        }
    } catch (error) {
        // JavaScript yoki fetch xatoligi
        console.error('Xatolik:', error);
        document.getElementById('errorDisplay').innerText = `Xatolik: ${error}`;
    }
}
