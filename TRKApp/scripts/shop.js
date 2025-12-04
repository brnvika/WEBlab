// Загрузка данных магазина из базы данных
async function loadShopData(shopName) {
    try {
        const response = await fetch(`/api/shops/${encodeURIComponent(shopName)}`);
        
        if (!response.ok) {
            console.error('Магазин не найден в базе данных');
            return;
        }

        const shop = await response.json();
        updateShopPage(shop);
    } catch (error) {
        console.error('Ошибка загрузки данных магазина:', error);
    }
}

// Обновление содержимого страницы магазина
function updateShopPage(shop) {
    // Название магазина
    const titleElement = document.querySelector('h1');
    if (titleElement) {
        titleElement.textContent = `Магазин ${shop.shopName}`;
    }

    // Логотип
    const logoImg = document.querySelector('.shop-image img');
    if (logoImg && shop.shopLogo) {
        // Если путь начинается с ../, заменяем на правильный относительный путь
        logoImg.src = shop.shopLogo.startsWith('../') ? shop.shopLogo : `../${shop.shopLogo}`;
        logoImg.alt = `Логотип магазина ${shop.shopName}`;
    }

    // Описание магазина
    const shopInfoDiv = document.querySelector('.shop-info');
    if (shopInfoDiv) {
        const descriptionP = shopInfoDiv.querySelector('p:first-of-type');
        if (descriptionP) {
            descriptionP.textContent = shop.shopDescription;
        }

        // Ссылка на сайт
        const siteLink = shopInfoDiv.querySelector('a');
        if (siteLink && shop.shopSait) {
            siteLink.href = shop.shopSait;
            siteLink.textContent = `официальный сайт магазина ${shop.shopName}`;
        }
    }

    // Таблица характеристик
    updateShopCharacteristics(shop);

    // Подробная информация
    const fullDescriptionDiv = document.querySelector('.shop-full-description');
    if (fullDescriptionDiv && shop.shopInformation) {
        const infoTitle = fullDescriptionDiv.querySelector('h3');
        if (infoTitle) {
            infoTitle.textContent = `Подробная информация о ${shop.shopName}`;
        }
        
        const infoP = fullDescriptionDiv.querySelector('p');
        if (infoP) {
            infoP.textContent = shop.shopInformation;
        }
    }

    // Изображение в ТРК
    const trkImg = document.querySelector('.shop-in-trk img');
    if (trkImg && shop.shopTRK) {
        trkImg.src = shop.shopTRK.startsWith('../') ? shop.shopTRK : `../${shop.shopTRK}`;
        trkImg.alt = `Витрина магазина ${shop.shopName} в ТРК Атриум`;
        
        const trkLink = document.querySelector('.shop-in-trk a');
        if (trkLink) {
            trkLink.href = shop.shopTRK.startsWith('../') ? shop.shopTRK : `../${shop.shopTRK}`;
        }
    }

    // Баннер
    const bannerImg = document.querySelector('.shop-banner img');
    if (bannerImg && shop.shopAdvert) {
        bannerImg.src = shop.shopAdvert.startsWith('../') ? shop.shopAdvert : `../${shop.shopAdvert}`;
        bannerImg.alt = `Акция магазина ${shop.shopName}`;
    }
}

// Обновление таблицы характеристик
function updateShopCharacteristics(shop) {
    const table = document.querySelector('.product-characteristics-table tbody');
    if (!table) return;

    // Очищаем таблицу
    table.innerHTML = '';

    // Обновляем заголовок таблицы
    const tableHeader = document.querySelector('.product-characteristics-table thead th');
    if (tableHeader) {
        tableHeader.textContent = `Ключевые особенности магазина ${shop.shopName}`;
    }

    // Добавляем характеристики из базы данных
    if (shop.characteristics && shop.characteristics.length > 0) {
        shop.characteristics.forEach(char => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td><strong>${escapeHtml(char.parameter)}</strong></td>
                <td>${escapeHtml(char.description)}</td>
            `;
            table.appendChild(row);
        });
    } else {
        // Если характеристик нет, показываем сообщение
        const row = document.createElement('tr');
        row.innerHTML = `
            <td colspan="2" style="text-align: center;">Характеристики магазина будут добавлены позже</td>
        `;
        table.appendChild(row);
    }
}

// Функция для экранирования HTML
function escapeHtml(text) {
    const div = document.createElement('div');
    div.textContent = text;
    return div.innerHTML;
}

// Загрузка при загрузке страницы
document.addEventListener('DOMContentLoaded', function() {
    // Получаем название магазина из data-атрибута
    const storeElement = document.querySelector('[data-store-name]');
    if (storeElement) {
        const shopName = storeElement.dataset.storeName;
        if (shopName) {
            loadShopData(shopName);
        }
    }
});
