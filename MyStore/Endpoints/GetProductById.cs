using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

public class GetProductById : Endpoint<GetProductByIdRequest, ProductResponse>
{
    private readonly AppDbContext _db;

    public GetProductById(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/products/{id}");
        AllowAnonymous();
        Summary(s => s.Summary = "Get a product by ID");
    }

    public override async Task HandleAsync(GetProductByIdRequest req, CancellationToken ct)
    {
        var product = await _db.Products.FindAsync(req.Id);

        if (product is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        var response = new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
        };

        await SendAsync(response, cancellation: ct);
    }
}

public class GetProductByIdRequest
{
    public int Id { get; set; }
    public string ?Name { get; set; }
    
}