document.addEventListener('DOMContentLoaded', function() {
    // Автозаполнение полей для авторизованных пользователей
    fillUserDataIfLoggedIn();
    
    const form = document.querySelector('.message-form form');
    
    if (form) {
        form.addEventListener('submit', async function(e) {
            e.preventDefault();
            
            const formData = {
                name: document.getElementById('name').value,
                email: document.getElementById('email').value,
                question: document.getElementById('message').value,
                agreement: document.getElementById('agreement').checked
            };
            
            // Проверка согласия
            if (!formData.agreement) {
                showMessage('Необходимо согласиться с политикой конфиденциальности', 'error');
                return;
            }
            
            // Отправка данных
            try {
                showMessage('Отправка сообщения...', 'info');
                
                const response = await fetch('/api/contacts', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify(formData)
                });
                
                const result = await response.json();
                
                if (response.ok) {
                    showMessage('✅ Сообщение успешно отправлено! Мы свяжемся с вами в ближайшее время.', 'success');
                    form.reset();
                    // Повторно заполняем поля для авторизованного пользователя без уведомления
                    setTimeout(() => {
                        fillUserDataIfLoggedIn(false);
                    }, 100);
                } else {
                    showMessage(result.message || 'Произошла ошибка при отправке', 'error');
                }
            } catch (error) {
                console.error('Ошибка:', error);
                showMessage('Произошла ошибка при отправке сообщения', 'error');
            }
        });
    }
});

function fillUserDataIfLoggedIn(showNotification = true) {
    // Получаем данные пользователя из cookies
    const userId = getCookie('userId');
    const userName = getCookie('userName');
    const userSurname = getCookie('userSurname');
    const userEmail = getCookie('userEmail');
    
    if (userId && userName && userEmail) {
        // Заполняем поле имени
        const nameField = document.getElementById('name');
        const emailField = document.getElementById('email');
        
        if (nameField) {
            const fullName = `${userName} ${userSurname || ''}`.trim();
            nameField.value = fullName;
            nameField.readOnly = true;
            nameField.style.backgroundColor = '#f5f5f5';
            nameField.style.color = '#666';
            nameField.style.cursor = 'not-allowed';
            nameField.title = 'Данные из вашего профиля (нельзя изменить)';
            
            // Добавляем иконку замка только если её ещё нет
            if (!nameField.parentElement.querySelector('.lock-icon')) {
                addLockIcon(nameField);
            }
        }
        
        if (emailField) {
            emailField.value = userEmail;
            emailField.readOnly = true;
            emailField.style.backgroundColor = '#f5f5f5';
            emailField.style.color = '#666';
            emailField.style.cursor = 'not-allowed';
            emailField.title = 'Данные из вашего профиля (нельзя изменить)';
            
            // Добавляем иконку замка только если её ещё нет
            if (!emailField.parentElement.querySelector('.lock-icon')) {
                addLockIcon(emailField);
            }
        }
        
        // Показываем уведомление о заблокированных полях только при первом заполнении
        if (showNotification) {
            showMessage('Поля "Имя" и "Email" заполнены из вашего профиля и заблокированы для редактирования', 'info');
        }
    }
}

function addLockIcon(inputField) {
    // Проверяем, нет ли уже wrapper'а с иконкой
    const parentDiv = inputField.parentElement;
    if (parentDiv.classList && parentDiv.classList.contains('input-wrapper')) return;
    if (parentDiv.querySelector('.lock-icon')) return;
    
    // Создаем контейнер для input с иконкой
    const inputWrapper = document.createElement('div');
    inputWrapper.className = 'input-wrapper';
    inputWrapper.style.position = 'relative';
    inputWrapper.style.display = 'inline-block';
    inputWrapper.style.width = '100%';
    
    // Создаем иконку замка
    const lockIcon = document.createElement('span');
    lockIcon.className = 'lock-icon';
    lockIcon.innerHTML = '🔒';
    lockIcon.style.position = 'absolute';
    lockIcon.style.right = '10px';
    lockIcon.style.top = '50%';
    lockIcon.style.transform = 'translateY(-50%)';
    lockIcon.style.color = '#666';
    lockIcon.style.fontSize = '14px';
    lockIcon.style.pointerEvents = 'none';
    lockIcon.style.zIndex = '1';
    
    // Оборачиваем input в контейнер только если он ещё не обёрнут
    const originalParent = inputField.parentElement;
    originalParent.insertBefore(inputWrapper, inputField);
    inputWrapper.appendChild(inputField);
    inputWrapper.appendChild(lockIcon);
    
    // Добавляем отступ справа для иконки
    inputField.style.paddingRight = '35px';
}

function getCookie(name) {
    const value = `; ${document.cookie}`;
    const parts = value.split(`; ${name}=`);
    if (parts.length === 2) {
        const rawValue = parts.pop().split(';').shift();
        return decodeURIComponent(rawValue);
    }
    return null;
}

function showMessage(text, type) {
    // Удаляем предыдущие сообщения
    const existingMessage = document.querySelector('.contact-message');
    if (existingMessage) {
        existingMessage.remove();
    }
    
    // Создаем новое сообщение
    const messageDiv = document.createElement('div');
    messageDiv.className = `contact-message contact-message--${type}`;
    
    // Создаем контент с кнопкой закрытия
    messageDiv.innerHTML = `
        <span class="message-text">${text}</span>
        <button class="message-close" onclick="this.parentElement.remove()">×</button>
    `;
    
    // Вставляем сообщение внутрь формы обратной связи в самом начале
    const messageForm = document.querySelector('.message-form');
    if (messageForm) {
        messageForm.insertBefore(messageDiv, messageForm.firstChild);
    }
    
    // Автоматически скрываем сообщение через время в зависимости от типа
    const hideTimeout = type === 'info' ? 3000 : 7000;
    setTimeout(() => {
        if (messageDiv.parentNode) {
            messageDiv.style.opacity = '0';
            messageDiv.style.transform = 'translateY(-10px)';
            setTimeout(() => messageDiv.remove(), 300);
        }
    }, hideTimeout);
}