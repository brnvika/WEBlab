-- Сначала обнулим CategoryId у всех магазинов
UPDATE "Shops" SET "CategoryId" = NULL;

-- далим существующие категории
DELETE FROM "Categories";

-- обавим категории заново
INSERT INTO "Categories" ("CategoryId", "CategoryName", "Description")
VALUES 
    ('11111111-1111-1111-1111-111111111111', 'бувь', 'агазины обуви'),
    ('22222222-2222-2222-2222-222222222222', 'дежда', 'агазины одежды'),
    ('33333333-3333-3333-3333-333333333333', 'велирные изделия', 'велирные магазины');

-- бновим магазины
UPDATE "Shops" SET "CategoryId" = '11111111-1111-1111-1111-111111111111' WHERE "ShopName" = 'ECCO';
UPDATE "Shops" SET "CategoryId" = '11111111-1111-1111-1111-111111111111' WHERE "ShopName" = 'Baldinini';
UPDATE "Shops" SET "CategoryId" = '11111111-1111-1111-1111-111111111111' WHERE "ShopName" = 'EKONIKA';
UPDATE "Shops" SET "CategoryId" = '22222222-2222-2222-2222-222222222222' WHERE "ShopName" = '12 STOREEZ';
UPDATE "Shops" SET "CategoryId" = '22222222-2222-2222-2222-222222222222' WHERE "ShopName" = 'LIME';
UPDATE "Shops" SET "CategoryId" = '22222222-2222-2222-2222-222222222222' WHERE "ShopName" = 'Love Republic';
UPDATE "Shops" SET "CategoryId" = '33333333-3333-3333-3333-333333333333' WHERE "ShopName" = 'Karatov';
UPDATE "Shops" SET "CategoryId" = '33333333-3333-3333-3333-333333333333' WHERE "ShopName" = 'Sunlight';
UPDATE "Shops" SET "CategoryId" = '33333333-3333-3333-3333-333333333333' WHERE "ShopName" = 'Sokolov';
