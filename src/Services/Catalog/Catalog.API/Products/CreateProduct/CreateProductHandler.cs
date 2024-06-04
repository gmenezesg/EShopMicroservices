﻿namespace Catalog.API.Products.CreateProduct;
public record CreateProductCommand(string name, List<string> Category, string Description, string ImageFile, decimal Price) : ICommand<CreateProductResult>;
public record CreateProductResult(Guid Id);

internal class CreateProductCommandHandler(IDocumentSession session) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        // Create Product entity from command object
        var product = new Product
        {
            Name = command.name,
            Category = command.Category,
            Description = command.Description,
            ImageFile = command.ImageFile,
            Price = command.Price
        };

        // Save to Database
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);

        // Return CreateProductResult result

        return new CreateProductResult(product.Id);
    }
}
