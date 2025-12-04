// Функция получения cookie
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
    return null;
}

// Загрузка данных профиля
async function loadProfile() {
    try {
        const response = await fetch('/api/users/profile', {
            method: 'GET',
            credentials: 'include'
        });

        if (response.ok) {
            const profile = await response.json();
            displayProfile(profile);
        } else {
            // Пользователь не авторизован
            alert('Необходима авторизация');
            window.location.href = '/pages/authorization.shtml';
        }
    } catch (error) {
        console.error('Ошибка загрузки профиля:', error);
        alert('Ошибка загрузки данных');
    }
}

// Отображение данных профиля
function displayProfile(profile) {
    document.getElementById('loading').style.display = 'none';
    document.getElementById('profile-content').style.display = 'block';

    document.getElementById('surname').textContent = profile.surname;
    document.getElementById('name').textContent = profile.name;
    document.getElementById('email').textContent = profile.email;
    document.getElementById('phone').textContent = profile.numberPhone;
    
    // Форматирование даты
    const birthDate = new Date(profile.birthDate);
    document.getElementById('birthdate').textContent = birthDate.toLocaleDateString('ru-RU');
    
    document.getElementById('gender').textContent = profile.gender || 'Не указан';
    document.getElementById('subscribe').textContent = profile.subscribe ? 'Подписан' : 'Не подписан';
}

// Выход из аккаунта
document.getElementById('logout-btn').addEventListener('click', async function() {
    try {
        const response = await fetch('/api/users/logout', {
            method: 'POST',
            credentials: 'include'
        });

        if (response.ok) {
            // Удаляем все cookie
            document.cookie.split(";").forEach(function(c) { 
                document.cookie = c.replace(/^ +/, "").replace(/=.*/, "=;expires=" + new Date().toUTCString() + ";path=/"); 
            });
            
            alert('Вы вышли из аккаунта');
            window.location.href = '/pages/authorization.shtml';
        }
    } catch (error) {
        console.error('Ошибка выхода:', error);
        alert('Ошибка выхода из аккаунта');
    }
});

// Загрузка при открытии страницы
loadProfile();
