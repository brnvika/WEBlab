-- Пример добавления магазина ECCO с характеристиками

-- 1. Добавляем магазин
INSERT INTO "Shops" (
    "ShopId", 
    "ShopName", 
    "ShopSait", 
    "ShopDescription", 
    "ShopInformation", 
    "ShopLogo", 
    "ShopTRK", 
    "ShopAdvert"
) VALUES (
    gen_random_uuid(),
    'ECCO',
    'https://ru.ecco.com/',
    'Добро пожаловать в мир ECCO – бренда, который уже много десятилетий является синонимом бескомпромиссного комфорта, инновационного дизайна и высочайшего качества.',
    'ECCO – датская обувная компания, основанная в 1963 году Карлом Тусби. С самого начала целью было создание обуви, которая идеально подходит для ног, а не наоборот.',
    '../images/logoEcco.png',
    '../images/trkEcco.jpg',
    '../images/ecco_banner.jpg'
);

-- 2. Получаем ID созданного магазина
-- Замените 'your-shop-id' на реальный ID из предыдущего запроса
-- Или используйте подзапрос:

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Философия',
    'Бескомпромиссный комфорт, инновационный дизайн и высочайшее качество, созданные для естественных анатомических форм стопы.'
FROM "Shops" WHERE "ShopName" = 'ECCO';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Технологии',
    'Пионер в использовании технологии литья подошвы под давлением (DIP) для гибкости, легкости и долговечности без клея и швов.'
FROM "Shops" WHERE "ShopName" = 'ECCO';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Материалы',
    'Использование натуральной кожи высокого качества с собственных кожевенных заводов ECCO, обеспечивающее мягкость, прочность и воздухопроницаемость.'
FROM "Shops" WHERE "ShopName" = 'ECCO';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Дизайн',
    'Скандинавский минимализм, чистые линии и функциональность, сочетающие элегантность и практичность.'
FROM "Shops" WHERE "ShopName" = 'ECCO';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Ассортимент',
    'Широкий выбор обуви и аксессуаров для мужчин, женщин и детей: классика, повседневная, спортивная обувь и модные новинки.'
FROM "Shops" WHERE "ShopName" = 'ECCO';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Обслуживание',
    'Квалифицированные консультанты, помогающие подобрать идеальную пару ECCO под индивидуальные особенности и стиль жизни.'
FROM "Shops" WHERE "ShopName" = 'ECCO';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Локация',
    'Удобное расположение в ТРК Атриум'
FROM "Shops" WHERE "ShopName" = 'ECCO';


-- Пример для магазина 12 STOREEZ
INSERT INTO "Shops" (
    "ShopId", 
    "ShopName", 
    "ShopSait", 
    "ShopDescription", 
    "ShopInformation", 
    "ShopLogo", 
    "ShopTRK", 
    "ShopAdvert"
) VALUES (
    gen_random_uuid(),
    '12 STOREEZ',
    'https://12storeez.com/',
    'Российский бренд современной женской одежды с акцентом на минимализм и качество.',
    '12 STOREEZ – это молодой российский бренд, создающий базовую одежду для современных женщин.',
    '../images/logo12Storeez.jpg',
    '../images/12Storeez_atrium.jpg',
    '../images/new12.jpg'
);

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Ассортимент',
    'Основная специализация — мужская и женская одежда, включая платья, брюки, джинсы, верхнюю одежду, трикотаж. Также представлены обувь и аксессуары.'
FROM "Shops" WHERE "ShopName" = '12 STOREEZ';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Стиль',
    'Современный минимализм, casual chic, с акцентом на качество, лаконичные силуэты и нейтральную палитру. Идеально для создания базового гардероба.'
FROM "Shops" WHERE "ShopName" = '12 STOREEZ';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Качество',
    'Высокое качество материалов (натуральные ткани: хлопок, шерсть, лен) и пошива, внимание к деталям. Ориентация на долговечность изделий.'
FROM "Shops" WHERE "ShopName" = '12 STOREEZ';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Обновление коллекций',
    'Регулярные поступления новых моделей, соответствующие последним тенденциям моды, с фокусом на сезонные коллекции.'
FROM "Shops" WHERE "ShopName" = '12 STOREEZ';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Ценовой сегмент',
    'Средний+, ориентирован на покупателей, ценящих качество, стиль и комфорт.'
FROM "Shops" WHERE "ShopName" = '12 STOREEZ';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT 
    gen_random_uuid(),
    "ShopId",
    'Локация',
    'Удобное расположение в ТРК Атриум, с легким доступом.'
FROM "Shops" WHERE "ShopName" = '12 STOREEZ';


-- Пример для магазина Baldinini
INSERT INTO "Shops" (
    "ShopId", 
    "ShopName", 
    "ShopSait", 
    "ShopDescription", 
    "ShopInformation", 
    "ShopLogo", 
    "ShopTRK", 
    "ShopAdvert"
) VALUES (
    gen_random_uuid(),
    'Baldinini',
    'https://www.baldinini-shop.com/',
    'BALDININI — это бутик обуви, созданной по формуле: «вдохновение + дизайн + стиль». В каждой коллекции воспоминания сочетаются и смешиваются с трендовыми представлениями о модных туфлях «прет-а-порте».',
    'BALDININI - итальянский бренд, специализирующийся на производстве элитной обуви. Компания была основана в 1910 году в городе Сан-Мауро-Пасколи. В 1970-х годах в компанию пришёл Джимми Балдинини. У компании 100 магазинов по всему миру.',
    '../images/logoBaldinini.jpeg',
    '../images/atriumBaldinini.jpg',
    '../images/saleBaldinini.jpg'
);

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ассортимент', 
'Широкий выбор обуви (от туфель и ботильонов до кроссовок и сапог).'
FROM "Shops" WHERE "ShopName" = 'Baldinini';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Стиль', 
'Элегантный, современный, функциональный. Сочетание классических форм с актуальными модными тенденциями.'
FROM "Shops" WHERE "ShopName" = 'Baldinini';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Качество', 
'Использование натуральной кожи высокого качества, замши и других премиальных материалов. Комфортная колодка.'
FROM "Shops" WHERE "ShopName" = 'Baldinini';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Обновление коллекций', 
'Регулярное обновление коллекций в соответствии с мировыми модными трендами и сезонными запросами.'
FROM "Shops" WHERE "ShopName" = 'Baldinini';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ценовой сегмент', 
'Средний+, предлагающий доступную роскошь для ценителей качества и стиля.'
FROM "Shops" WHERE "ShopName" = 'Baldinini';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Локация', 
'Удобное расположение в ТРК Атриум, 2 этаж, с легким доступом.'
FROM "Shops" WHERE "ShopName" = 'Baldinini';


-- Пример для магазина EKONIKA
INSERT INTO "Shops" (
    "ShopId", 
    "ShopName", 
    "ShopSait", 
    "ShopDescription", 
    "ShopInformation", 
    "ShopLogo", 
    "ShopTRK", 
    "ShopAdvert"
) VALUES (
    gen_random_uuid(),
    'EKONIKA',
    'https://ekonika.ru/',
    'EKONIKA — это ведущий российский бренд, специализирующийся на производстве высококачественной обуви, сумок и аксессуаров для женщин. Марка известна своим изысканным дизайном, комфортом и вниманием к деталям.',
    'EKONIKA – российская компания, основанная в 1992 году. Бренд быстро завоевал популярность благодаря сочетанию европейского качества и современного дизайна.',
    '../images/logoEkonika.jpg',
    '../images/ekonika_atrium.jpg',
    '../images/saleEkonika.jpg'
);

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ассортимент', 
'Обувь, сумки и аксессуары для женщин разных возрастов и стилей.'
FROM "Shops" WHERE "ShopName" = 'EKONIKA';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Стиль', 
'Современный, элегантный, трендовый. Сочетание классических форм с актуальными модными тенденциями.'
FROM "Shops" WHERE "ShopName" = 'EKONIKA';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Качество', 
'Натуральная кожа высокого качества, комфортные колодки, внимание к деталям отделки.'
FROM "Shops" WHERE "ShopName" = 'EKONIKA';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ценовой сегмент', 
'Средний, демократичные цены при высоком качестве.'
FROM "Shops" WHERE "ShopName" = 'EKONIKA';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Локация', 
'ТРК Атриум, удобное расположение.'
FROM "Shops" WHERE "ShopName" = 'EKONIKA';


-- Пример для магазина LIME
INSERT INTO "Shops" (
    "ShopId", 
    "ShopName", 
    "ShopSait", 
    "ShopDescription", 
    "ShopInformation", 
    "ShopLogo", 
    "ShopTRK", 
    "ShopAdvert"
) VALUES (
    gen_random_uuid(),
    'LIME',
    'https://limestore.com/',
    'LIME (ООО «Стиль трейд») — российский дизайнерский бренд по производству одежды, аксессуаров и обуви.',
    'LIME – это бренд для тех, кто ценит комфорт и стиль в повседневной жизни. Коллекции созданы с любовью к деталям.',
    '../images/logoLime.jpg',
    '../images/lime_atrium.jpg',
    '../images/saleLime.jpg'
);

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ассортимент', 
'Одежда, обувь и аксессуары для современных женщин.'
FROM "Shops" WHERE "ShopName" = 'LIME';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Стиль', 
'Современный casual, яркие принты, комфортные силуэты.'
FROM "Shops" WHERE "ShopName" = 'LIME';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Качество', 
'Качественные материалы, тщательный пошив, внимание к деталям.'
FROM "Shops" WHERE "ShopName" = 'LIME';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ценовой сегмент', 
'Средний, доступные цены.'
FROM "Shops" WHERE "ShopName" = 'LIME';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Локация', 
'ТРК Атриум.'
FROM "Shops" WHERE "ShopName" = 'LIME';


-- Пример для магазина Karatov
INSERT INTO "Shops" (
    "ShopId", 
    "ShopName", 
    "ShopSait", 
    "ShopDescription", 
    "ShopInformation", 
    "ShopLogo", 
    "ShopTRK", 
    "ShopAdvert"
) VALUES (
    gen_random_uuid(),
    'Karatov',
    'https://karatov.ru/',
    'Karatov — это ювелирный бренд, воплощающий в себе традиции русского ювелирного искусства и современные тенденции. Эксклюзивные украшения из золота и серебра с драгоценными камнями.',
    'Karatov – российский ювелирный бренд, созданный для тех, кто ценит качество и индивидуальность. Каждое украшение создано с любовью.',
    '../images/logoKaratov.jpg',
    '../images/trkKaratov.jpg',
    '../images/saleKaratov.jpg'
);

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ассортимент', 
'Украшения из золота и серебра, кольца, серьги, браслеты, подвески с драгоценными камнями.'
FROM "Shops" WHERE "ShopName" = 'Karatov';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Стиль', 
'Сочетание классических традиций русского ювелирного искусства с современными дизайнерскими решениями.'
FROM "Shops" WHERE "ShopName" = 'Karatov';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Качество', 
'Высококачественные материалы, ручная работа, внимание к каждой детали.'
FROM "Shops" WHERE "ShopName" = 'Karatov';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ценовой сегмент', 
'Средний+, доступная роскошь.'
FROM "Shops" WHERE "ShopName" = 'Karatov';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Локация', 
'ТРК Атриум.'
FROM "Shops" WHERE "ShopName" = 'Karatov';


-- Пример для магазина Sunlight
INSERT INTO "Shops" (
    "ShopId", 
    "ShopName", 
    "ShopSait", 
    "ShopDescription", 
    "ShopInformation", 
    "ShopLogo", 
    "ShopTRK", 
    "ShopAdvert"
) VALUES (
    gen_random_uuid(),
    'Sunlight',
    'https://www.sunlight.net/',
    'Sunlight — популярный ювелирный ритейлер, предлагающий широкий ассортимент украшений из золота и серебра по доступным ценам. Современные и классические модели.',
    'Sunlight – один из крупнейших ювелирных ритейлеров в России, известный доступными ценами и качественными украшениями.',
    '../images/logoSunlight.jpg',
    '../images/trkSunlight.jpg',
    '../images/saleSunlight.jpg'
);

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ассортимент', 
'Широкий выбор украшений из золота, серебра, ювелирная бижутерия.'
FROM "Shops" WHERE "ShopName" = 'Sunlight';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Стиль', 
'Современный и классический дизайн для повседневного ношения и праздников.'
FROM "Shops" WHERE "ShopName" = 'Sunlight';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Качество', 
'Контроль качества на всех этапах производства, сертифицированная продукция.'
FROM "Shops" WHERE "ShopName" = 'Sunlight';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ценовой сегмент', 
'Демократичные цены, доступно широкой аудитории.'
FROM "Shops" WHERE "ShopName" = 'Sunlight';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Локация', 
'ТРК Атриум.'
FROM "Shops" WHERE "ShopName" = 'Sunlight';


-- Пример для магазина Sokolov
INSERT INTO "Shops" (
    "ShopId", 
    "ShopName", 
    "ShopSait", 
    "ShopDescription", 
    "ShopInformation", 
    "ShopLogo", 
    "ShopTRK", 
    "ShopAdvert"
) VALUES (
    gen_random_uuid(),
    'Sokolov',
    'https://sokolov.ru/',
    'Добро пожаловать в мир SOKOLOV – одного из крупнейших ювелирных брендов России, который известен своим безупречным качеством, современным дизайном и широким ассортиментом украшений.',
    'SOKOLOV – это более 30 лет на рынке, современные технологии производства и собственные дизайн-студии, создающие уникальные коллекции.',
    '../images/logoSokolov.jpg',
    '../images/trkAtriumSokolov.jpg',
    '../images/bannerSokolov.jpg'
);

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ассортимент', 
'Украшения из золота, серебра, с драгоценными камнями, часы, аксессуары.'
FROM "Shops" WHERE "ShopName" = 'Sokolov';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Стиль', 
'Современный дизайн, от повседневных аксессуаров до изысканных изделий для особых моментов.'
FROM "Shops" WHERE "ShopName" = 'Sokolov';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Качество', 
'Безупречное качество, контроль на всех этапах производства, современные технологии.'
FROM "Shops" WHERE "ShopName" = 'Sokolov';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Инновации', 
'Собственные дизайн-студии, уникальные коллекции, следование мировым трендам.'
FROM "Shops" WHERE "ShopName" = 'Sokolov';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ценовой сегмент', 
'Средний, доступные цены при высоком качестве.'
FROM "Shops" WHERE "ShopName" = 'Sokolov';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Локация', 
'ТРК Атриум.'
FROM "Shops" WHERE "ShopName" = 'Sokolov';


-- Пример для магазина Love Republic
INSERT INTO "Shops" (
    "ShopId", 
    "ShopName", 
    "ShopSait", 
    "ShopDescription", 
    "ShopInformation", 
    "ShopLogo", 
    "ShopTRK", 
    "ShopAdvert"
) VALUES (
    gen_random_uuid(),
    'Love Republic',
    'https://loverepublic.ru/',
    'Love Republic — российский бренд женской одежды, созданный для современных, чувственных и уверенных в себе женщин. Коллекции Love Republic вдохновлены идеей женственности и роскоши.',
    'Love Republic – это бренд для тех, кто любит яркие образы и не боится экспериментировать с модой.',
    '../images/logoLR.jpg',
    '../images/LR_atrium.jpg',
    '../images/banLR.jpg'
);

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ассортимент', 
'Женская одежда: платья, юбки, блузы, брюки, верхняя одежда, аксессуары.'
FROM "Shops" WHERE "ShopName" = 'Love Republic';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Стиль', 
'Женственный, романтичный, с элементами роскоши. Яркие принты, современные силуэты.'
FROM "Shops" WHERE "ShopName" = 'Love Republic';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Качество', 
'Качественные материалы, внимание к отделке и деталям.'
FROM "Shops" WHERE "ShopName" = 'Love Republic';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Целевая аудитория', 
'Современные, уверенные в себе женщины, ценящие стиль и комфорт.'
FROM "Shops" WHERE "ShopName" = 'Love Republic';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Ценовой сегмент', 
'Средний, доступные цены.'
FROM "Shops" WHERE "ShopName" = 'Love Republic';

INSERT INTO "ShopCharacteristics" ("CharacteristicId", "ShopId", "Parameter", "Description")
SELECT gen_random_uuid(), "ShopId", 'Локация', 
'ТРК Атриум.'
FROM "Shops" WHERE "ShopName" = 'Love Republic';
