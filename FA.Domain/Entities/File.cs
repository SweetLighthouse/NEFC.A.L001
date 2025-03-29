using System.ComponentModel.DataAnnotations.Schema;

namespace FA.Domain.Entities;

[Table(nameof(UploadFile))]
public class UploadFile : BaseEntity 
{
    public string Name { get; set; } = null!;
    public string Extension { get; set; } = null!;
    public byte[] Data { get; set; } = []!;
}
