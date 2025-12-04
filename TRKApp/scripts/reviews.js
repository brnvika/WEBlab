// Получение названия магазина из URL или data-атрибута
function getStoreName() {
    // Получаем название магазина из meta-тега или data-атрибута
    const storeElement = document.querySelector('[data-store-name]');
    return storeElement ? storeElement.dataset.storeName : '';
}

// Проверка авторизации
function isUserLoggedIn() {
    return document.cookie.split(';').some(c => c.trim().startsWith('userId='));
}

// Загрузка отзывов
async function loadReviews(storeName) {
    try {
        const response = await fetch(`/api/reviews/${encodeURIComponent(storeName)}`);
        if (response.ok) {
            const reviews = await response.json();
            displayReviews(reviews);
        } else {
            console.error('Ошибка загрузки отзывов');
        }
    } catch (error) {
        console.error('Ошибка:', error);
    }
}

// Отображение отзывов
function displayReviews(reviews) {
    const container = document.getElementById('reviews-list');
    if (!container) return;

    if (reviews.length === 0) {
        container.innerHTML = '<p class="no-reviews">Отзывов пока нет. Будьте первым!</p>';
        return;
    }

    container.innerHTML = reviews.map(review => `
        <div class="review-item">
            <div class="review-header">
                <span class="review-author">${escapeHtml(review.userName)}</span>
                <span class="review-date">${new Date(review.createdAt).toLocaleDateString('ru-RU')}</span>
            </div>
            <div class="review-ratings">
                <div class="rating-item">
                    <span>Работа магазина:</span>
                    <span class="stars">${renderStars(review.storeWork)}</span>
                </div>
                <div class="rating-item">
                    <span>Консультанты:</span>
                    <span class="stars">${renderStars(review.consultantWork)}</span>
                </div>
                <div class="rating-item">
                    <span>Кассиры:</span>
                    <span class="stars">${renderStars(review.cashierWork)}</span>
                </div>
                <div class="rating-item">
                    <span>Качество товаров:</span>
                    <span class="stars">${renderStars(review.qualityGoods)}</span>
                </div>
            </div>
            <div class="review-comment">${escapeHtml(review.comment)}</div>
        </div>
    `).join('');
}

// Рендер звезд
function renderStars(rating) {
    const filled = '★'.repeat(rating);
    const empty = '☆'.repeat(5 - rating);
    return filled + empty;
}

// Экранирование HTML
function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}

// Отправка отзыва
document.getElementById('review-form')?.addEventListener('submit', async function(e) {
    e.preventDefault();

    if (!isUserLoggedIn()) {
        const goToAuth = confirm('Для оставления отзыва необходимо войти в аккаунт.\n\nЕсли у вас нет аккаунта, нажмите "Отмена" для регистрации.');
        if (goToAuth) {
            window.location.href = '/pages/authorization.shtml';
        } else {
            window.location.href = '/pages/register.shtml';
        }
        return;
    }

    const formData = {
        storeName: getStoreName(),
        storeWork: parseInt(document.querySelector('input[name="storeWork"]:checked')?.value || '0'),
        consultantWork: parseInt(document.querySelector('input[name="consultantWork"]:checked')?.value || '0'),
        cashierWork: parseInt(document.querySelector('input[name="cashierWork"]:checked')?.value || '0'),
        qualityGoods: parseInt(document.querySelector('input[name="qualityGoods"]:checked')?.value || '0'),
        comment: document.getElementById('comment').value
    };

    // Валидация
    if (!formData.storeWork || !formData.consultantWork || !formData.cashierWork || !formData.qualityGoods) {
        alert('Пожалуйста, поставьте все оценки');
        return;
    }

    try {
        const response = await fetch('/api/reviews', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            credentials: 'include',
            body: JSON.stringify(formData)
        });

        const result = await response.json();

        if (response.ok) {
            alert(result.message || 'Отзыв добавлен!');
            document.getElementById('review-form').reset();
            loadReviews(getStoreName());
        } else {
            alert(result.error || 'Ошибка добавления отзыва');
        }
    } catch (error) {
        console.error('Ошибка:', error);
        alert('Ошибка соединения с сервером');
    }
});

// Загрузка отзывов при загрузке страницы
document.addEventListener('DOMContentLoaded', function() {
    const storeName = getStoreName();
    if (storeName) {
        loadReviews(storeName);
    }

    // Показываем/скрываем форму в зависимости от авторизации
    const reviewForm = document.getElementById('review-form');
    const loginPrompt = document.getElementById('login-prompt');
    
    if (isUserLoggedIn()) {
        if (reviewForm) reviewForm.style.display = 'block';
        if (loginPrompt) loginPrompt.style.display = 'none';
    } else {
        if (reviewForm) reviewForm.style.display = 'none';
        if (loginPrompt) loginPrompt.style.display = 'block';
    }
});
