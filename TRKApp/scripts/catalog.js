// Загрузка каталога магазинов из API
document.addEventListener('DOMContentLoaded', async () => {
    await loadCatalog();
    setupFilters();
});

let allShops = []; // Хранилище всех магазинов
let currentCategory = null; // Текущая выбранная категория
let currentSort = 'alphabet'; // Текущая сортировка

async function loadCatalog(category = null, sortBy = 'alphabet') {
    try {
        let url = '/api/shops/catalog';
        const params = new URLSearchParams();
        if (sortBy) params.append('sortBy', sortBy);
        if (params.toString()) url += '?' + params.toString();
        
        const response = await fetch(url);
        
        if (!response.ok) {
            throw new Error('Ошибка загрузки каталога');
        }
        
        allShops = await response.json();
        currentCategory = category;
        currentSort = sortBy;
        applyFiltersAndDisplay();
    } catch (error) {
        console.error('Ошибка при загрузке каталога:', error);
        showError('Не удалось загрузить каталог магазинов');
    }
}

function applyFiltersAndDisplay() {
    let filteredShops = [...allShops];
    
    // Фильтр по категории
    if (currentCategory && currentCategory !== 'all') {
        filteredShops = filteredShops.filter(shop => {
            const categoryClass = getCategoryClass(shop.categoryName);
            return categoryClass === currentCategory;
        });
    }
    
    // Фильтр по поиску
    const searchInput = document.getElementById('searchInput');
    if (searchInput && searchInput.value.trim() !== '') {
        const searchTerm = searchInput.value.toLowerCase().trim();
        filteredShops = filteredShops.filter(shop => 
            shop.shopName.toLowerCase().includes(searchTerm)
        );
    }
    
    // Сортировка уже применена на сервере, но если нужна клиентская пересортировка:
    // sortShops(filteredShops, currentSort);
    
    displayShops(filteredShops);
}

function displayShops(shops) {
    const catalogGrid = document.querySelector('.catalog__grid section');
    
    if (!catalogGrid) {
        console.error('Контейнер для каталога не найден');
        return;
    }
    
    if (shops.length === 0) {
        catalogGrid.innerHTML = '<p style="text-align: center; width: 100%; padding: 40px;">Магазины не найдены</p>';
        return;
    }
    
    catalogGrid.innerHTML = shops.map(shop => createShopCard(shop)).join('');
}

function createShopCard(shop) {
    const categoryClass = getCategoryClass(shop.categoryName);
    const shopPageUrl = getShopPageUrl(shop.shopName);
    
    return `
        <div class="store-card-container" data-category="${categoryClass}">
            <div class="store-card">
                <a href="${shopPageUrl}">
                    <img src="${shop.shopCardImage}" alt="${shop.shopName}" class="store-card__background-img">
                </a>
                <div class="store-card__logo">
                    <p>${shop.shopName}</p>
                </div>
                <div class="store-card__bottom-bar">
                    <div class="store-card__text-content">
                        <h3 class="store-card__name">${shop.shopName}</h3>
                        <p class="store-card__category">${shop.categoryName || 'Без категории'}</p>
                    </div>
                    <a href="#" class="store-card__map-button">
                        НА КАРТЕ
                    </a>
                </div>
            </div>
        </div>
    `;
}

function getCategoryClass(categoryName) {
    const categoryMap = {
        'Одежда': 'clothes',
        'Обувь': 'shoes',
        'Ювелирные изделия': 'jewelry'
    };
    return categoryMap[categoryName] || 'other';
}

function getShopPageUrl(shopName) {
    const shopPageMap = {
        '12 STOREEZ': '../pages/shop12Storeez.shtml',
        'LIME': '../pages/shopLIME.shtml',
        'Love Republic': '../pages/shopLoveRepublic.shtml',
        'EKONIKA': '../pages/shopEKONIKA.shtml',
        'Baldinini': '../pages/shopBaldinini.shtml',
        'ECCO': '../pages/shopEcco.shtml',
        'Sokolov': '../pages/shopSokolov.shtml',
        'Sunlight': '../pages/shopSunlight.shtml',
        'Karatov': '../pages/shopKaratov.shtml'
    };
    return shopPageMap[shopName] || '#';
}

function setupFilters() {
    const categorySelect = document.getElementById('selectCategory');
    const searchInput = document.getElementById('searchInput');
    const searchButton = document.getElementById('searchButton');
    const sortSelect = document.querySelector('.search-form__sort select');
    
    // Фильтр по категории
    if (categorySelect) {
        categorySelect.addEventListener('change', (e) => {
            currentCategory = e.target.value;
            applyFiltersAndDisplay();
        });
    }
    
    // Сортировка
    if (sortSelect) {
        sortSelect.addEventListener('change', async (e) => {
            currentSort = e.target.value;
            await loadCatalog(currentCategory, currentSort);
        });
    }
    
    // Поиск в реальном времени
    if (searchInput) {
        searchInput.addEventListener('input', () => {
            applyFiltersAndDisplay();
        });
        
        searchInput.addEventListener('keyup', (e) => {
            if (e.key === 'Enter') {
                applyFiltersAndDisplay();
            }
        });
    }
    
    // Кнопка поиска
    if (searchButton) {
        searchButton.addEventListener('click', () => {
            applyFiltersAndDisplay();
        });
    }
}

function showError(message) {
    const catalogGrid = document.querySelector('.catalog__grid section');
    if (catalogGrid) {
        catalogGrid.innerHTML = `<p style="color: red; text-align: center; width: 100%; padding: 40px;">${message}</p>`;
    }
}
