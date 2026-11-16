using wt_lab6_6minimalapi_vasileva.Data;
using wt_lab6_6minimalapi_vasileva.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавь эти две строки вместо builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


// ... остальной код с эндпоинтами ...

// Корень
app.MapGet("/", () => "Minimal API работает!");

// Инфо
app.MapGet("/api/info", () =>
{
    return new
    {
        Application = "Web Technologies",
        Technology = "ASP.NET Core Minimal API",
        Version = "1.0"
    };
});

// 3.1 Получить список всех продуктов + 3.3 Поиск по имени (search)
app.MapGet("/api/products", (string? search) =>
{
    var products = ProductStore.Products.AsEnumerable();

    if (!string.IsNullOrWhiteSpace(search))
    {
        products = products.Where(x => 
            x.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
    }

    return Results.Ok(products);
});

// 3.2 Получить продукт по ID
app.MapGet("/api/products/{id}", (int id) =>
{
    var product = ProductStore.Products.FirstOrDefault(x => x.Id == id);
    
    if (product == null) 
        return Results.NotFound();
        
    return Results.Ok(product);
});

// 4. POST - Создание нового продукта
app.MapPost("/api/products", (Product product) =>
{
    // Автоматическая генерация ID
    var newId = ProductStore.Products.Count == 0 
        ? 1 
        : ProductStore.Products.Max(x => x.Id) + 1;

    product.Id = newId;
    ProductStore.Products.Add(product);

    // Возвращаем 201 Created и ссылку на новый ресурс
    return Results.Created($"/api/products/{product.Id}", product);
});

// 5. PUT - Изменение продукта
app.MapPut("/api/products/{id}", (int id, Product updatedProduct) =>
{
    var product = ProductStore.Products.FirstOrDefault(x => x.Id == id);
    
    if (product == null) 
        return Results.NotFound();

    // Обновляем поля существующего объекта
    product.Name = updatedProduct.Name;
    product.Category = updatedProduct.Category;
    product.Price = updatedProduct.Price;
    product.IsAvailable = updatedProduct.IsAvailable;

    return Results.Ok(product);
});

// 5. DELETE - Удаление продукта
app.MapDelete("/api/products/{id}", (int id) =>
{
    var product = ProductStore.Products.FirstOrDefault(x => x.Id == id);
    
    if (product == null) 
        return Results.NotFound();

    ProductStore.Products.Remove(product);
    
    // Возвращаем 204 No Content (успешно, но без тела ответа)
    return Results.NoContent();
});

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");