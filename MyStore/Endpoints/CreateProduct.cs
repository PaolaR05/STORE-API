using FastEndpoints;

namespace MyStore.Endpoints;

public class CreateProduct : Endpoint<Product>
{
    private readonly AppDbContext _db;

    public CreateProduct(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Post("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Product req, CancellationToken ct)
    {
        _db.Products.Add(req);
        await _db.SaveChangesAsync(ct);
        await SendAsync(req, 201);
    }
}
