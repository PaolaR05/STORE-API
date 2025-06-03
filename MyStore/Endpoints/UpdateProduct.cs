using FastEndpoints;

public class UpdateProduct : Endpoint<Product>
{
    private readonly AppDbContext _db;

    public UpdateProduct(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Put("/products");
        AllowAnonymous();
    }

    public override async Task HandleAsync(Product req, CancellationToken ct)
    {
        var existing = await _db.Products.FindAsync(req.Id);
        if (existing is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        existing.Name = req.Name;
        await _db.SaveChangesAsync(ct);
        await SendOkAsync(existing, ct);
    }
}
