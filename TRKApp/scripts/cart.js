// Проверка авторизации
function isUserLoggedIn() {
    return document.cookie.split(';').some(c => c.trim().startsWith('userId='));
}

// Загрузка корзины
async function loadCart() {
    if (!isUserLoggedIn()) {
        showUnauthorized();
        return;
    }
    
    try {
        const response = await fetch('/api/cart', {
            credentials: 'include'
        });
        
        if (response.status === 401) {
            showUnauthorized();
            return;
        }
        
        if (response.ok) {
            const cart = await response.json();
            displayCart(cart);
        } else {
            showMessage('Ошибка загрузки корзины', 'error');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        showMessage('Ошибка соединения с сервером', 'error');
    }
}

// Отображение корзины
function displayCart(cart) {
    const container = document.getElementById('cart-content');
    
    if (!cart || !cart.items || cart.items.length === 0) {
        container.innerHTML = `
            <div class="cart-empty">
                <p>Ваша корзина пуста</p>
                <a href="/pages/rentalSpaces.shtml">Перейти к выбору площадей</a>
            </div>
        `;
        return;
    }
    
    container.innerHTML = `
        <div class="cart-items">
            ${cart.items.map(item => `
                <div class="cart-item" data-item-id="${item.itemId}">
                    <img src="${item.imageUrl || '/images/placeholder-space.png'}" 
                         alt="${escapeHtml(item.spaceName)}" 
                         class="cart-item-image"
                         onerror="this.src='/images/placeholder-space.png'">
                    <div class="cart-item-details">
                        <div class="cart-item-name">${escapeHtml(item.spaceName)}</div>
                        <div class="cart-item-category">${escapeHtml(item.category)}</div>
                        ${item.shopName ? `<div class="cart-item-info">
                            <strong>🏪 Магазин:</strong> ${escapeHtml(item.shopName)}
                        </div>` : ''}
                        <div class="cart-item-info">
                            <strong>Номер:</strong> ${escapeHtml(item.spaceNumber)} | 
                            <strong>Этаж:</strong> ${item.floor}
                        </div>
                        <div class="cart-item-info">
                            <strong>Площадь:</strong> ${item.squareMeters} м²
                        </div>
                        <div class="cart-item-info">
                            <strong>Срок аренды:</strong> ${item.months} мес.
                        </div>
                    </div>
                    <div class="cart-item-price">
                        <div class="cart-item-subtotal">${formatPrice(item.subtotal)} ₽</div>
                        <div class="cart-item-monthly">${formatPrice(item.pricePerMonth)} ₽/мес</div>
                        <button class="btn-remove" onclick="removeFromCart('${item.itemId}')">
                            🗑️ Удалить
                        </button>
                    </div>
                </div>
            `).join('')}
        </div>
        
        <div class="cart-summary">
            <h2>Итого</h2>
            <div class="cart-total">
                <span>Общая стоимость:</span>
                <span>${formatPrice(cart.totalPrice)} ₽</span>
            </div>
            <div class="cart-actions">
                <button class="btn-checkout" onclick="checkout()">
                    Оформить заявку
                </button>
                <a href="/pages/rentalSpaces.shtml" class="btn-continue">
                    Продолжить выбор
                </a>
            </div>
        </div>
    `;
}

// Удаление из корзины
async function removeFromCart(itemId) {
    if (!confirm('Вы уверены, что хотите удалить этот элемент из корзины?')) {
        return;
    }
    
    try {
        const response = await fetch(`/api/cart/${itemId}`, {
            method: 'DELETE',
            credentials: 'include'
        });
        
        const result = await response.json();
        
        if (response.ok) {
            showMessage(result.message || 'Элемент удалён', 'success');
            // Перезагружаем корзину
            loadCart();
        } else if (response.status === 401) {
            showMessage('Необходима авторизация', 'error');
            setTimeout(() => window.location.href = '/pages/authorization.shtml', 2000);
        } else {
            showMessage(result.error || 'Ошибка удаления', 'error');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        showMessage('Ошибка соединения с сервером', 'error');
    }
}

// Оформление заказа
function checkout() {
    alert('Спасибо за интерес! Наш менеджер свяжется с вами в ближайшее время для оформления договора аренды.');
    
    // В реальном проекте здесь можно добавить:
    // - Переход на страницу оформления заявки
    // - Отправку данных на сервер
    // - Создание заказа в БД
    // - Отправку уведомлений
    
    // Для демонстрации просто перезагрузим корзину
    setTimeout(() => {
        window.location.href = '/pages/index.shtml';
    }, 2000);
}

// Отображение сообщения о необходимости авторизации
function showUnauthorized() {
    const container = document.getElementById('cart-content');
    container.innerHTML = `
        <div class="cart-empty">
            <p>Для просмотра корзины необходимо войти в аккаунт</p>
            <a href="/pages/authorization.shtml">Войти</a>
            или
            <a href="/pages/register.shtml">Зарегистрироваться</a>
        </div>
    `;
}

// Показать сообщение
function showMessage(text, type = 'info') {
    const container = document.getElementById('message-container');
    const messageDiv = document.createElement('div');
    messageDiv.className = `message ${type}`;
    messageDiv.textContent = text;
    
    container.innerHTML = '';
    container.appendChild(messageDiv);
    
    setTimeout(() => {
        messageDiv.remove();
    }, 5000);
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
        document.getElementById('auth-check-cart').style.display = 'block';
        document.getElementById('cart-main-content').style.display = 'none';
        return;
    }
    
    document.getElementById('auth-check-cart').style.display = 'none';
    document.getElementById('cart-main-content').style.display = 'block';
    
    loadCart();
});
