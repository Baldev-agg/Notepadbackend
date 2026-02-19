using Auth.Api.Models;

namespace Notepad.Api.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        
     
    }
}
