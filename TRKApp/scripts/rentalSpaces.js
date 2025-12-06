// Проверка авторизации
function isUserLoggedIn() {
    return document.cookie.split(';').some(c => c.trim().startsWith('userId='));
}

// Получение cookie
function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) return parts.pop().split(';').shift();
    return null;
}

// Фильтрация по категории
let currentCategory = '';

// Загрузка списка торговых площадей
async function loadRentalSpaces(category = '') {
    try {
        const url = category 
            ? `/api/rentalspaces?category=${encodeURIComponent(category)}` 
            : '/api/rentalspaces';
        
        const response = await fetch(url);
        
        if (response.ok) {
            const spaces = await response.json();
            displayRentalSpaces(spaces);
        } else {
            console.error('Ошибка загрузки площадей');
            showMessage('Ошибка загрузки торговых площадей', 'error');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        showMessage('Ошибка соединения с сервером', 'error');
    }
}

// Отображение торговых площадей
function displayRentalSpaces(spaces) {
    const container = document.getElementById('spaces-grid');
    
    if (!spaces || spaces.length === 0) {
        container.innerHTML = '<div class="no-spaces">Нет доступных площадей</div>';
        return;
    }
    
    container.innerHTML = spaces.map(space => `
        <div class="space-card" data-space-id="${space.id}">
            <div class="space-image">
                <img src="${space.imageUrl || '/images/placeholder-space.png'}" 
                     alt="${escapeHtml(space.name)}"
                     onerror="this.src='/images/placeholder-space.png'">
            </div>
            <div class="space-details">
                <div class="space-name">${escapeHtml(space.name)}</div>
                <div class="space-category">${escapeHtml(space.category)}</div>
                ${space.shopName ? `<div class="space-shop-info">
                    <strong>🏪 Магазин:</strong> ${escapeHtml(space.shopName)}
                </div>` : ''}
                <div class="space-info">
                    <strong>Номер:</strong> ${escapeHtml(space.spaceNumber)} | 
                    <strong>Этаж:</strong> ${space.floor}
                </div>
                <div class="space-info">
                    <strong>Площадь:</strong> ${space.squareMeters} м²
                </div>
                <div class="space-description">${escapeHtml(space.description)}</div>
                <div class="space-price">${formatPrice(space.pricePerMonth)} ₽/мес</div>
                <div class="months-selector">
                    <label for="months-${space.id}">Месяцев:</label>
                    <input type="number" 
                           id="months-${space.id}" 
                           min="1" 
                           max="120" 
                           value="1">
                </div>
                <div class="space-actions">
                    <button class="btn-add-to-cart" 
                            onclick="addToCart('${space.id}')"
                            ${!space.isAvailable ? 'disabled' : ''}>
                        ${space.isAvailable ? '🛒 Добавить в корзину' : 'Недоступно'}
                    </button>
                </div>
            </div>
        </div>
    `).join('');
}

// Добавление в корзину
async function addToCart(spaceId) {
    if (!isUserLoggedIn()) {
        const goToAuth = confirm('Для добавления в корзину необходимо войти в аккаунт.\n\nЕсли у вас нет аккаунта, нажмите "Отмена" для регистрации.');
        if (goToAuth) {
            window.location.href = '/pages/authorization.shtml';
        } else {
            window.location.href = '/pages/register.shtml';
        }
        return;
    }
    
    const monthsInput = document.getElementById(`months-${spaceId}`);
    const months = parseInt(monthsInput?.value || 1);
    
    if (months < 1 || months > 120) {
        alert('Количество месяцев должно быть от 1 до 120');
        return;
    }
    
    try {
        const response = await fetch('/api/cart', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify({
                rentalSpaceId: spaceId,
                months: months
            })
        });
        
        const result = await response.json();
        
        if (response.ok) {
            // Показываем уведомление об успехе
            showNotification('✅ ' + (result.message || 'Добавлено в корзину!'));
            
            // Предлагаем перейти в корзину
            if (confirm('Площадь добавлена в корзину! Перейти в корзину?')) {
                window.location.href = '/pages/cart.shtml';
            }
        } else if (response.status === 401) {
            alert('Необходима авторизация');
            window.location.href = '/pages/authorization.shtml';
        } else {
            alert(result.error || 'Ошибка добавления в корзину');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        alert('Ошибка соединения с сервером');
    }
}

// Уведомление
function showNotification(message) {
    const notification = document.createElement('div');
    notification.className = 'notification';
    notification.textContent = message;
    notification.style.cssText = `
        position: fixed;
        top: 80px;
        right: 20px;
        background: linear-gradient(135deg, #8a2be2 0%, #9d50f2 100%);
        color: white;
        padding: 15px 25px;
        border-radius: 12px;
        box-shadow: 0 0 20px rgba(138, 43, 226, 0.5);
        z-index: 10000;
        animation: slideIn 0.3s ease-out;
        font-weight: bold;
    `;
    
    document.body.appendChild(notification);
    
    setTimeout(() => {
        notification.style.animation = 'slideOut 0.3s ease-out';
        setTimeout(() => notification.remove(), 300);
    }, 3000);
}

// Форматирование цены
function formatPrice(price) {
    return new Intl.NumberFormat('ru-RU').format(price);
}

// Экранирование HTML
function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text || '';
    return div.innerHTML;
}

// Инициализация при загрузке страницы
document.addEventListener('DOMContentLoaded', function() {
    // Проверка авторизации
    if (!isUserLoggedIn()) {
        document.getElementById('auth-check').style.display = 'block';
        document.getElementById('content-area').style.display = 'none';
        return;
    }
    
    document.getElementById('auth-check').style.display = 'none';
    document.getElementById('content-area').style.display = 'block';
    
    // Загружаем все площади
    loadRentalSpaces();
    
    // Обработчики фильтров
    document.querySelectorAll('.filter-btn').forEach(btn => {
        btn.addEventListener('click', function() {
            // Снимаем активный класс со всех кнопок
            document.querySelectorAll('.filter-btn').forEach(b => b.classList.remove('active'));
            // Добавляем активный класс на текущую
            this.classList.add('active');
            
            // Загружаем с фильтром
            const category = this.getAttribute('data-category');
            currentCategory = category;
            loadRentalSpaces(category);
        });
    });
});

// Добавляем стили для анимации
const style = document.createElement('style');
style.textContent = `
    @keyframes slideIn {
        from {
            opacity: 0;
            transform: translateX(100px);
        }
        to {
            opacity: 1;
            transform: translateX(0);
        }
    }
    
    @keyframes slideOut {
        from {
            opacity: 1;
            transform: translateX(0);
        }
        to {
            opacity: 0;
            transform: translateX(100px);
        }
    }
`;
document.head.appendChild(style);
