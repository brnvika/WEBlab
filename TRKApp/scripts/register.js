document.getElementById('register-form').addEventListener('submit', async function(e) {
    e.preventDefault();
    
    clearErrors();
    
    const formData = {
        surname: document.getElementById('last_name').value,
        name: document.getElementById('first_name').value,
        numberPhone: document.getElementById('telephone').value,
        email: document.getElementById('contact').value,
        password: document.getElementById('password').value,
        confirmPassword: document.getElementById('password_confirm').value,
        birthDate: document.getElementById('birthdate').value || new Date().toISOString(),
        gender: document.querySelector('input[name="gender"]:checked')?.value || '',
        subscribe: document.getElementById('subscribe').checked,
        agreeToTerms: document.getElementById('agree').checked
    };
    
    // Проверка согласия с политикой конфиденциальности
    const agreeCheckbox = document.getElementById('agree');
    if (!agreeCheckbox.checked) {
        showGeneralError('Необходимо согласиться с Политикой конфиденциальности и Условиями использования');
        agreeCheckbox.parentElement.style.color = 'red';
        return;
    }
    
    // Проверка совпадения паролей
    if (formData.password !== formData.confirmPassword) {
        showError('password_confirm', 'Пароли не совпадают');
        return;
    }
    
    try {
        console.log('Отправляем данные регистрации:', formData);
        console.log('JSON данных:', JSON.stringify(formData));
        
        const response = await fetch('/api/users/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });
        
        console.log('Ответ сервера:', response.status, response.statusText);
        
        let result;
        const responseText = await response.text();
        console.log('Сырой ответ сервера:', responseText);
        
        try {
            result = JSON.parse(responseText);
            console.log('Парсированный результат:', result);
        } catch (parseError) {
            console.error('Ошибка парсинга JSON:', parseError);
            showGeneralError('Сервер вернул некорректный ответ');
            return;
        }
        
        if (response.ok) {
            showSuccessMessage(result.message || 'Регистрация успешна!');
            setTimeout(() => {
                window.location.href = '/pages/authorization.shtml';
            }, 2000);
        } else {
            let errorDisplayed = false;
            
            // Обрабатываем конкретные ошибки полей
            if (result.errors && Object.keys(result.errors).length > 0) {
                // Маппинг полей с сервера на поля формы
                const fieldMapping = {
                    'User.Email': 'contact',
                    'User.NumberPhone': 'telephone',
                    'User.Password': 'password',
                    'User.Surname': 'last_name',
                    'User.Name': 'first_name',
                    'email': 'contact',
                    'numberphone': 'telephone',
                    'password': 'password',
                    'surname': 'last_name',
                    'name': 'first_name',
                    'agreeToTerms': 'agree',
                    'birthDate': 'birthdate'
                };
                
                for (const [field, errors] of Object.entries(result.errors)) {
                    const fieldId = fieldMapping[field] || field.toLowerCase();
                    const errorMessage = Array.isArray(errors) ? errors.join(', ') : errors;
                    showError(fieldId, errorMessage);
                    errorDisplayed = true;
                }
            }
            
            // Показываем общую ошибку если есть
            if (result.error) {
                showGeneralError(result.error);
                errorDisplayed = true;
            }
            
            // Показываем сообщение если нет конкретной причины
            if (!errorDisplayed) {
                if (result.message) {
                    showGeneralError(result.message);
                } else {
                    // Определяем ошибку по HTTP статусу
                    switch (response.status) {
                        case 400:
                            showGeneralError('Неверные данные. Проверьте правильность заполнения полей.');
                            break;
                        case 409:
                            showGeneralError('Пользователь с таким email или номером телефона уже существует.');
                            break;
                        case 422:
                            showGeneralError('Данные не прошли валидацию. Проверьте все поля.');
                            break;
                        case 500:
                            showGeneralError('Ошибка сервера. Попробуйте позже.');
                            break;
                        default:
                            showGeneralError(`Ошибка регистрации (код ${response.status}). Попробуйте позже.`);
                    }
                }
            }
        }
    } catch (error) {
        console.error('Registration error:', error);
        showGeneralError('Ошибка соединения с сервером. Проверьте подключение к интернету.');
    }
});

function showError(fieldId, message) {
    const field = document.getElementById(fieldId);
    if (field) {
        const errorDiv = document.createElement('div');
        errorDiv.className = 'error-message';
        errorDiv.style.color = 'red';
        errorDiv.style.fontSize = '14px';
        errorDiv.style.marginTop = '5px';
        errorDiv.textContent = message;
        field.parentElement.appendChild(errorDiv);
        field.style.borderColor = 'red';
    }
}

function showGeneralError(message) {
    const form = document.getElementById('register-form');
    const errorDiv = document.createElement('div');
    errorDiv.className = 'general-error';
    errorDiv.style.color = 'red';
    errorDiv.style.padding = '10px';
    errorDiv.style.marginBottom = '15px';
    errorDiv.style.border = '1px solid red';
    errorDiv.style.borderRadius = '5px';
    errorDiv.style.backgroundColor = '#ffe6e6';
    errorDiv.textContent = message;
    form.insertBefore(errorDiv, form.firstChild);
}

function showSuccessMessage(message) {
    const form = document.getElementById('register-form');
    const successDiv = document.createElement('div');
    successDiv.className = 'success-message';
    successDiv.style.color = '#155724';
    successDiv.style.padding = '15px';
    successDiv.style.marginBottom = '15px';
    successDiv.style.border = '1px solid #28a745';
    successDiv.style.borderRadius = '5px';
    successDiv.style.backgroundColor = '#d4edda';
    successDiv.style.fontSize = '16px';
    successDiv.textContent = message;
    form.insertBefore(successDiv, form.firstChild);
}

function clearErrors() {
    document.querySelectorAll('.error-message, .general-error, .success-message').forEach(el => el.remove());
    
    document.querySelectorAll('input').forEach(input => {
        input.style.borderColor = '';
    });
    
    // Убираем красный цвет с label согласия
    const agreeLabel = document.querySelector('.agree');
    if (agreeLabel) {
        agreeLabel.style.color = '';
    }
}
