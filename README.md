# Лабораторная работа №6. REST API на ASP.NET Core Minimal API

## Описание
REST API для предметной области "Интернет-магазин" (сущность Product). 
Реализовано хранение данных в памяти, операции CRUD, фильтрация и документация OpenAPI (Swagger).

## Ссылки
- **GitHub:** https://github.com/daryavasileva/wt-lab6-6minimalapi-vasileva
- **Render (публичный API):** https://wt-lab6-6minimalapi-vasileva-1.onrender.com

## Эндпоинты

| Метод | URL | Назначение |
|---|---|---|
| GET | `/api/products` | получить список всех товаров |
| GET | `/api/products?search=...` | фильтрация по названию |
| GET | `/api/products/{id}` | получить товар по ID |
| POST | `/api/products` | создать новый товар |
| PUT | `/api/products/{id}` | изменить товар |
| DELETE | `/api/products/{id}` | удалить товар |
| GET | `/api/info` | информация о приложении |

## Статусы
- `200 OK` — успешный GET/PUT
- `201 Created` — успешный POST (с заголовком Location)
- `204 No Content` — успешный DELETE
- `400 Bad Request` — некорректные данные
- `404 Not Found` — объект не найден

## Пример запроса POST
```json
{
  "name": "Планшет",
  "category": "Гаджеты",
  "price": 900,
  "isAvailable": true
}