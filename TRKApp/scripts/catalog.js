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
    
    // Инициализируем слайдеры после отрисовки
    initializeSliders();
}

function createShopCard(shop) {
    const categoryClass = getCategoryClass(shop.categoryName);
    const shopPageUrl = getShopPageUrl(shop.shopName);
    
    // Используем imagePaths если есть, иначе fallback на shopCardImage
    const images = shop.imagePaths && shop.imagePaths.length > 0 
        ? shop.imagePaths 
        : [shop.shopCardImage];
    
    const sliderHtml = createImageSlider(images, shop.shopName);
    
    return `
        <div class="store-card-container" data-category="${categoryClass}">
            <div class="store-card">
                ${sliderHtml}
                <div class="store-card__logo">
                    <p>${shop.shopName}</p>
                </div>
                <div class="store-card__bottom-bar">
                    <div class="store-card__text-content">
                        <h3 class="store-card__name">${shop.shopName}</h3>
                        <p class="store-card__category">${shop.categoryName || 'Без категории'}</p>
                    </div>
                    <a href="${shopPageUrl}" class="store-card__map-button">
                        НА КАРТЕ
                    </a>
                </div>
            </div>
        </div>
    `;
}

function createImageSlider(images, shopName) {
    const sliderId = `slider-${shopName.replace(/\s+/g, '-')}`;
    
    console.log(`Создаю слайдер для ${shopName}, изображений: ${images.length}`, images);
    
    const imagesHtml = images.map((img, index) => 
        `<img src="${img}" alt="${shopName} ${index + 1}" class="slider-image ${index === 0 ? 'active' : ''}">`
    ).join('');
    
    const indicatorsHtml = images.map((_, index) => 
        `<span class="slider-indicator ${index === 0 ? 'active' : ''}" data-index="${index}"></span>`
    ).join('');
    
    console.log(`Условие images.length > 1: ${images.length} > 1 = ${images.length > 1}`);
    
    return `
        <div class="slider-container" id="${sliderId}">
            <div class="slider-images">
                ${imagesHtml}
            </div>
            ${images.length > 1 ? `
                <button class="slider-btn slider-btn-prev" aria-label="Предыдущее изображение">
                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
                        <path d="M15 18L9 12L15 6" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg>
                </button>
                <button class="slider-btn slider-btn-next" aria-label="Следующее изображение">
                    <svg width="24" height="24" viewBox="0 0 24 24" fill="none">
                        <path d="M9 18L15 12L9 6" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"/>
                    </svg>
                </button>
                <div class="slider-indicators">
                    ${indicatorsHtml}
                </div>
            ` : ''}
        </div>
    `;
}

function initializeSliders() {
    const sliders = document.querySelectorAll('.slider-container');
    
    sliders.forEach(slider => {
        const images = slider.querySelectorAll('.slider-image');
        const indicators = slider.querySelectorAll('.slider-indicator');
        const prevBtn = slider.querySelector('.slider-btn-prev');
        const nextBtn = slider.querySelector('.slider-btn-next');
        
        if (images.length <= 1) return;
        
        let currentIndex = 0;
        
        const showSlide = (index) => {
            images.forEach(img => img.classList.remove('active'));
            indicators.forEach(ind => ind.classList.remove('active'));
            
            images[index].classList.add('active');
            indicators[index].classList.add('active');
        };
        
        const nextSlide = () => {
            currentIndex = (currentIndex + 1) % images.length;
            showSlide(currentIndex);
        };
        
        const prevSlide = () => {
            currentIndex = (currentIndex - 1 + images.length) % images.length;
            showSlide(currentIndex);
        };
        
        if (prevBtn) {
            prevBtn.addEventListener('click', (e) => {
                e.preventDefault();
                e.stopPropagation();
                prevSlide();
            });
        }
        
        if (nextBtn) {
            nextBtn.addEventListener('click', (e) => {
                e.preventDefault();
                e.stopPropagation();
                nextSlide();
            });
        }
        
        indicators.forEach((indicator, index) => {
            indicator.addEventListener('click', (e) => {
                e.preventDefault();
                e.stopPropagation();
                currentIndex = index;
                showSlide(currentIndex);
            });
        });
    });
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
