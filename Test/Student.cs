
namespace Test;

public class Student : IId, IBaseEntity, IStudent
{
    // IId
    public Guid Id { get; set; } 

    //IBaseEntity
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string Name { get; set; } = null!;
}
