using Microsoft.AspNetCore.Http;

namespace FileTools.V1.Models
{
    public interface IShrinkable
    {
        int? Resolution { get; set; }

        IFormFile Content { get; set; }
    }
}
