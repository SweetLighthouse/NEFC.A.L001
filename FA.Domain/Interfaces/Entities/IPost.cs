using FA.Domain.Enumerations;

namespace FA.Domain.Interfaces.Entities;

public interface IPost
{
    string Title { get; set; } 
    string Content { get; set; }
    double Rate { get; set; }
    int Count { get; set; }
}
