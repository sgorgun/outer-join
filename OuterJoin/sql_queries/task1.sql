SELECT
    product_category.name AS category,
    product_title.title AS product
FROM
    product_category
INNER JOIN product_title
    ON product_category.id = product_title.product_category_id
INNER JOIN product
    ON product_title.id = product.product_title_id
LEFT JOIN order_details
    ON product.id = order_details.product_id
WHERE order_details.product_id IS NULL
ORDER BY product.id;