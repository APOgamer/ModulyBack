namespace ModulyBack.Moduly.Domain.Model.Commands;

public record UpdateBeingCommand(
    Guid Id,
    string IdentifierName,
    string Description,
    decimal Price,
    string Category,
    DateTime? ManufactureDate,
    DateTime? ExpiryDate,
    string Origin,
    ICollection<string> Tags,
    bool IsActive,
    decimal? Discount,
    string Currency,
    string ImageUrl,
    Guid ModuleId,
    Guid UserId
);