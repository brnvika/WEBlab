-- Удаляем и пересоздаем категории с правильной кодировкой
UPDATE "Shops" SET "CategoryId" = NULL;
DELETE FROM "Categories";

INSERT INTO "Categories" ("CategoryId", "CategoryName", "Description")
VALUES 
    ('11111111-1111-1111-1111-111111111111', 'Обувь', 'Магазины обуви'),
    ('22222222-2222-2222-2222-222222222222', 'Одежда', 'Магазины одежды'),
    ('33333333-3333-3333-3333-333333333333', 'Ювелирные изделия', 'Ювелирные магазины');

UPDATE "Shops" SET "CategoryId" = '11111111-1111-1111-1111-111111111111' WHERE "ShopName" = 'ECCO';
UPDATE "Shops" SET "CategoryId" = '11111111-1111-1111-1111-111111111111' WHERE "ShopName" = 'Baldinini';
UPDATE "Shops" SET "CategoryId" = '11111111-1111-1111-1111-111111111111' WHERE "ShopName" = 'EKONIKA';
UPDATE "Shops" SET "CategoryId" = '22222222-2222-2222-2222-222222222222' WHERE "ShopName" = '12 STOREEZ';
UPDATE "Shops" SET "CategoryId" = '22222222-2222-2222-2222-222222222222' WHERE "ShopName" = 'LIME';
UPDATE "Shops" SET "CategoryId" = '22222222-2222-2222-2222-222222222222' WHERE "ShopName" = 'Love Republic';
UPDATE "Shops" SET "CategoryId" = '33333333-3333-3333-3333-333333333333' WHERE "ShopName" = 'Karatov';
UPDATE "Shops" SET "CategoryId" = '33333333-3333-3333-3333-333333333333' WHERE "ShopName" = 'Sunlight';
UPDATE "Shops" SET "CategoryId" = '33333333-3333-3333-3333-333333333333' WHERE "ShopName" = 'Sokolov';
