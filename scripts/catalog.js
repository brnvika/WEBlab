(function () {
  'use strict';

  document.addEventListener('DOMContentLoaded', function () {
    const filterBtns = Array.from(document.querySelectorAll('.filter-btn'));
    const searchInput = document.getElementById('catalog-search');
    const sortSelect = document.getElementById('catalog-sort');
    const containerParent = document.querySelector('.catalog section') || document.querySelector('.catalog');
    if (!containerParent) return;

    const cardContainers = () => Array.from(containerParent.querySelectorAll('.store-card-container'));

    // Заполняет data-categories на карточках, если нет
    function ensureDataCategories() {
      cardContainers().forEach(c => {
        const card = c.querySelector('.store-card');
        if (!card) return;
        if (card.dataset.categories) return;
        const catText = (c.querySelector('.store-card__category')?.textContent || '').toLowerCase();
        const cats = [];
        if (catText.includes('жен')) cats.push('female');
        if (catText.includes('муж')) cats.push('male');
        if (catText.includes('дет')) cats.push('kids');
        if (cats.length === 0) cats.push('other');
        card.dataset.categories = cats.join(',');
      });
    }

    // Применяет фильтр и поиск
    function applyFiltersAndSearch() {
      const activeFilter = document.querySelector('.filter-btn.active')?.dataset.filter || 'all';
      const q = (searchInput?.value || '').trim().toLowerCase();
      cardContainers().forEach(c => {
        const card = c.querySelector('.store-card');
        const name = (c.querySelector('.store-card__name')?.textContent || '').toLowerCase();
        const logoText = (c.querySelector('.store-card__logo')?.textContent || '').toLowerCase();
        const cats = (card?.dataset.categories || '').toLowerCase().split(',').map(s => s.trim());
        const matchFilter = activeFilter === 'all' || cats.includes(activeFilter);
        const matchSearch = !q || name.includes(q) || logoText.includes(q);
        c.style.display = (matchFilter && matchSearch) ? '' : 'none';
      });
    }

    // Сортировка видимых карточек (можно изменить на все)
    function applySort() {
      const val = sortSelect?.value || 'default';
      if (val === 'default') return;
      const items = cardContainers().filter(c => c.style.display !== 'none');
      const compareByName = (a, b) => {
        const an = (a.querySelector('.store-card__name')?.textContent || '').trim();
        const bn = (b.querySelector('.store-card__name')?.textContent || '').trim();
        return an.localeCompare(bn, 'ru', { sensitivity: 'base' });
      };
      const compareByCategory = (a, b) => {
        const ac = (a.querySelector('.store-card__category')?.textContent || '').trim();
        const bc = (b.querySelector('.store-card__category')?.textContent || '').trim();
        return ac.localeCompare(bc, 'ru', { sensitivity: 'base' });
      };

      let sorted = items.slice();
      if (val === 'name-asc') sorted.sort(compareByName);
      if (val === 'name-desc') sorted.sort((a, b) => -compareByName(a, b));
      if (val === 'category') sorted.sort(compareByCategory);

      sorted.forEach(node => containerParent.appendChild(node));
    }

    // Привязка событий
    filterBtns.forEach(btn => btn.addEventListener('click', () => {
      filterBtns.forEach(b => b.classList.remove('active'));
      btn.classList.add('active');
      applyFiltersAndSearch();
      applySort();
    }));

    if (searchInput) {
      let timeout;
      searchInput.addEventListener('input', () => {
        clearTimeout(timeout);
        timeout = setTimeout(() => {
          applyFiltersAndSearch();
          applySort();
        }, 150);
      });
    }

    if (sortSelect) sortSelect.addEventListener('change', applySort);

    // Инициализация
    ensureDataCategories();
    applyFiltersAndSearch();
    applySort();
  });
})();