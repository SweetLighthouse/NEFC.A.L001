namespace FA.Domain.Interfaces;

public interface IMetadata
{
    DateTime CreatedAt { get; set; }
    Guid CreatedBy { get; set; }
    DateTime UpdatedAt { get; set; }
    Guid UpdatedBy { get; set; }
}
