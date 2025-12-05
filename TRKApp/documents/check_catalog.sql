SELECT s."ShopName", s."ShopCardImage", c."CategoryName" 
FROM "Shops" s 
LEFT JOIN "Categories" c ON s."CategoryId" = c."CategoryId" 
ORDER BY s."ShopName" 
LIMIT 5;
