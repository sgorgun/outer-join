SELECT
    surname,
    name,
    COALESCE(SUM(order_details.price_with_discount * order_details.product_amount), 0) AS sum
FROM person
LEFT JOIN customer
    ON person.id = customer.person_id
LEFT JOIN customer_order
    ON customer.person_id = customer_order.customer_id
LEFT JOIN order_details
    ON customer_order.id = order_details.customer_order_id
WHERE customer.card_number IS NOT NULL
GROUP BY person.id
UNION ALL
SELECT
    NULL,
    NULL,
    COALESCE(SUM(order_details.price_with_discount * order_details.product_amount), 0) AS sum
FROM customer_order
LEFT JOIN order_details
    ON customer_order.id = order_details.customer_order_id
WHERE customer_order.customer_id IS NULL
ORDER BY sum, surname