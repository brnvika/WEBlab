(function() {
    const input = document.getElementById('searchInput');
    const category = document.getElementById('selectCategory');
    const btn = document.getElementById('searchButton');

    function normalize(s){ return (s||'').trim().toLowerCase(); }

    function filterStores(){
    const q = normalize(input.value);
    const cat = category.value; 
    const cards = document.querySelectorAll('.store-card-container');
    cards.forEach(card => {
    const cardCat = card.getAttribute('data-category') || 'all';
    const nameEl = card.querySelector('.store-card__name');
    const catTextEl = card.querySelector('.store-card__category');
    const name = normalize(nameEl ? nameEl.textContent : '');
    const catText = normalize(catTextEl ? catTextEl.textContent : '');

    const matchesCategory = (cat === 'all') || (cardCat === cat) || (catText.indexOf(cat) !== -1);
    const matchesQuery = q === '' || name.indexOf(q) !== -1 || catText.indexOf(q) !== -1;

    if (matchesCategory && matchesQuery) {
    card.classList.remove('hidden');
} else {
    card.classList.add('hidden');
}
});
}

    btn.addEventListener('click', filterStores);
    
    input.addEventListener('keydown', function(e){
    if (e.key === 'Enter') { e.preventDefault(); filterStores(); }
});
    category.addEventListener('change', filterStores);
})();
