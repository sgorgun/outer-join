SELECT
    surname,
    name,
    birth_date
FROM
    person
INNER JOIN customer
    ON person.id = customer.person_id
LEFT JOIN customer_order
    ON customer.person_id = customer_order.customer_id
WHERE card_number IS NOT NULL AND customer_order.id IS NULL
ORDER BY surname, birth_date