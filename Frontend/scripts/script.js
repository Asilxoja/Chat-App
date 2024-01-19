async function registerUser() {
    // Foydalanuvchining ismi, telefon raqami va paroli
    const fullName = document.getElementById('fullName').value;
    const phoneNumber = document.getElementById('phoneNumber').value;
    const password = document.getElementById('password').value;

    // Yuboriladigan ma'lumotlar obyekti
    const registerData = {
        fullName: fullName,
        phoneNumber: phoneNumber,
        password: password
    };

    try {
        // Serverga so'rov yuborish
        const response = await fetch('https://localhost:44371/auth/register', {
            method: 'POST',  // POST so'rovi
            headers: {
                'Content-Type': 'application/json'  // JSON ma'lumotlari
            },
            body: JSON.stringify(registerData)  // Yuboriladigan ma'lumotlar JSON formatda
        });

        // Agar javob muvaffaqiyatli bo'lsa
        if (response.ok) {
            // Foydalanuvchi muvaffaqiyatli ro'yhatdan o'tgan
            const resultDiv = document.getElementById('result');
            resultDiv.innerText = 'Foydalanuvchi muvaffaqiyatli ro\'yhatdan o\'tdi!';
            resultDiv.style.color = 'green';
            resultDiv.style.display = 'block';

            // Xatolik xabarlari tozalash
            document.getElementById('errorDisplay').innerText = '';

            // Login sahifasiga yo'naltirish uchun kichik bir kechikish (masalan, 2 sekund)
            setTimeout(() => {
                window.location.href = './login.html'; // Login sahifasi URL'ini o'zgartiring
            }, 2000);
        } else if (response.status === 400) {
            // Foydalanuvchi allaqachon mavjud
            const errorText = await response.text();
            console.error(`Xatolik: ${errorText}`);
            
            const errorDisplay = document.getElementById('errorDisplay');
            errorDisplay.innerText = `Foydalanuvchi allaqachon mavjud. Iltimos, boshqa telefon raqamini tanlang.`;
            errorDisplay.style.color = 'red';
            errorDisplay.style.display = 'block';
        } else {
            // Boshqa xatoliklar
            const errorText = await response.text();
            console.error(`Xatolik: ${errorText}`);
            document.getElementById('errorDisplay').innerText = `Xatolik: ${errorText}`;
        }
    } catch (error) {
        // JavaScript yoki fetch xatoligi
        console.error('Xatolik:', error);
        document.getElementById('errorDisplay').innerText = `Xatolik: ${error}`;
    }
}
