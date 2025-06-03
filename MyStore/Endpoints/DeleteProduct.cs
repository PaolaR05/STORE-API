using FastEndpoints;
using Microsoft.AspNetCore.Mvc;

public class DeleteProduct : Endpoint<DeleteProductRequest>
{
    private readonly AppDbContext _db;

    public DeleteProduct(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Delete("/products/{id}");
        AllowAnonymous();
        Summary(s => s.Summary = "Delete a product by ID");
    }

    public override async Task HandleAsync(DeleteProductRequest req, CancellationToken ct)
    {
        var product = await _db.Products.FindAsync(req.Id);
        if (product is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        _db.Products.Remove(product);
        await _db.SaveChangesAsync(ct);
        await SendOkAsync(ct);
    }
}

public class DeleteProductRequest
{
    [FromRoute]
    public int Id { get; set; }
}
