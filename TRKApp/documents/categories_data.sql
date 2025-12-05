-- Добавление категорий магазинов

-- 1. Категория "Обувь"
INSERT INTO "Categories" ("CategoryId", "CategoryName", "Description")
VALUES (
    '11111111-1111-1111-1111-111111111111',
    'Обувь',
    'Магазины обуви - туфли, ботинки, кроссовки, сапоги'
);

-- 2. Категория "Одежда"
INSERT INTO "Categories" ("CategoryId", "CategoryName", "Description")
VALUES (
    '22222222-2222-2222-2222-222222222222',
    'Одежда',
    'Магазины одежды - платья, брюки, верхняя одежда'
);

-- 3. Категория "Ювелирные изделия"
INSERT INTO "Categories" ("CategoryId", "CategoryName", "Description")
VALUES (
    '33333333-3333-3333-3333-333333333333',
    'Ювелирные изделия',
    'Ювелирные магазины - украшения из золота, серебра, драгоценные камни'
);

-- Обновление магазинов с привязкой к категориям

-- ECCO -> Обувь
UPDATE "Shops" SET "CategoryId" = '11111111-1111-1111-1111-111111111111'
WHERE "ShopName" = 'ECCO';

-- Baldinini -> Обувь
UPDATE "Shops" SET "CategoryId" = '11111111-1111-1111-1111-111111111111'
WHERE "ShopName" = 'Baldinini';

-- EKONIKA -> Обувь
UPDATE "Shops" SET "CategoryId" = '11111111-1111-1111-1111-111111111111'
WHERE "ShopName" = 'EKONIKA';

-- 12 STOREEZ -> Одежда
UPDATE "Shops" SET "CategoryId" = '22222222-2222-2222-2222-222222222222'
WHERE "ShopName" = '12 STOREEZ';

-- LIME -> Одежда
UPDATE "Shops" SET "CategoryId" = '22222222-2222-2222-2222-222222222222'
WHERE "ShopName" = 'LIME';

-- Love Republic -> Одежда
UPDATE "Shops" SET "CategoryId" = '22222222-2222-2222-2222-222222222222'
WHERE "ShopName" = 'Love Republic';

-- Karatov -> Ювелирные изделия
UPDATE "Shops" SET "CategoryId" = '33333333-3333-3333-3333-333333333333'
WHERE "ShopName" = 'Karatov';

-- Sunlight -> Ювелирные изделия
UPDATE "Shops" SET "CategoryId" = '33333333-3333-3333-3333-333333333333'
WHERE "ShopName" = 'Sunlight';

-- Sokolov -> Ювелирные изделия
UPDATE "Shops" SET "CategoryId" = '33333333-3333-3333-3333-333333333333'
WHERE "ShopName" = 'Sokolov';
