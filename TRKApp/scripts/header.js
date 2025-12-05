// Проверка авторизации и обновление header
document.addEventListener('DOMContentLoaded', function() {
    updateHeaderAuth();
});

function updateHeaderAuth() {
    const userId = getCookie('userId');
    const userName = getCookie('userName');
    const userSurname = getCookie('userSurname');
    
    const loginForm = document.querySelector('.login-form');
    
    if (userId && userName) {
        // Пользователь авторизован
        // Очищаем от возможных артефактов и проверяем на валидность
        const cleanUserName = userName && userName.length > 0 && !userName.includes('%') ? userName : 'Мой аккаунт';
        const cleanUserSurname = userSurname && userSurname.length > 0 && !userSurname.includes('%') ? userSurname : '';
        const fullName = `${cleanUserName} ${cleanUserSurname}`.trim();
        
        loginForm.innerHTML = `
            <a href="/pages/profile.shtml" class="btn btn-account">
                👤 ${fullName}
            </a>
            <button class="btn btn-logout" onclick="logout()">Выход</button>
        `;
    } else {
        // Пользователь не авторизован
        loginForm.innerHTML = `
            <a href="/pages/register.shtml"><button class="btn">Регистрация</button></a>
            <a href="/pages/authorization.shtml"><button class="btn">Вход</button></a>
        `;
    }
}

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) {
        const rawValue = parts.pop().split(';').shift();
        // Декодируем URL-кодированное значение
        return decodeURIComponent(rawValue);
    }
    return null;
}

function logout() {
    // Удаляем все куки
    document.cookie = 'userId=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
    document.cookie = 'userName=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
    document.cookie = 'userSurname=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
    document.cookie = 'userEmail=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;';
    
    // Перенаправляем на главную
    window.location.href = '/pages/index.shtml';
}
