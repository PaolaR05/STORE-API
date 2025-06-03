using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace MyStore.Endpoints;

public class GetProducts : EndpointWithoutRequest<List<Product>>
{
    private readonly AppDbContext _db;

    public GetProducts(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var products = await _db.Products.ToListAsync(ct);
        await SendAsync(products);
    }
}
