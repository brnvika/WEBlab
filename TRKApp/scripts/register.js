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
        subscribe: document.getElementById('subscribe').checked
    };
    
    // Проверка совпадения паролей
    if (formData.password !== formData.confirmPassword) {
        showError('password_confirm', 'Пароли не совпадают');
        return;
    }
    
    try {
        const response = await fetch('/api/users/register', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(formData)
        });
        
        const result = await response.json();
        
        if (response.ok) {
            alert(result.message || 'Регистрация успешна!');
            window.location.href = '/pages/authorization.shtml';
        } else {
            if (result.error) {
                showGeneralError(result.error);
            } else if (result.errors) {
                for (const [field, errors] of Object.entries(result.errors)) {
                    showError(field.toLowerCase(), errors.join(', '));
                }
            }
        }
    } catch (error) {
        showGeneralError('Ошибка соединения с сервером');
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

function clearErrors() {
    document.querySelectorAll('.error-message, .general-error').forEach(el => el.remove());
    
    document.querySelectorAll('input').forEach(input => {
        input.style.borderColor = '';
    });
}
