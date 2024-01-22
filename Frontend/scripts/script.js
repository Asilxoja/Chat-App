async function registerUser() {
    const fullName = document.getElementById('fullName').value;
    const phoneNumber = document.getElementById('phoneNumber').value;
    const password = document.getElementById('password').value;

    const registerData = {
        fullName: fullName, 
        phoneNumber: phoneNumber,
        password: password
    };

    try {
        const response = await fetch('https://localhost:44371/Auth/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(registerData)
        });

        if (response.ok) {
            const resultDiv = document.getElementById('result');
            resultDiv.innerText = 'Foydalanuvchi muvaffaqiyatli ro\'yhatdan o\'tdi!';
            resultDiv.style.color = 'green';
            resultDiv.style.display = 'block';

            document.getElementById('errorDisplay').innerText = '';

            setTimeout(() => {
                window.location.href = './login.html';
            }, 2000);
        } else if (response.status === 400) {
            const errorText = await response.text();
            console.error(`Xatolik: ${errorText}`);
            
            const errorDisplay = document.getElementById('errorDisplay');
            errorDisplay.innerText = `Foydalanuvchi allaqachon mavjud. Iltimos, boshqa telefon raqamini tanlang.`;
            errorDisplay.style.color = 'red';
            errorDisplay.style.display = 'block';
        } else {
            const errorText = await response.text();
            console.error(`Xatolik: ${errorText}`);
            document.getElementById('errorDisplay').innerText = `Xatolik: ${errorText}`;
        }
    } catch (error) {
        console.error('Xatolik:', error);
        document.getElementById('errorDisplay').innerText = `Xatolik: ${error}`;
    }
}
