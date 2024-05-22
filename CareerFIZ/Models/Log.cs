using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CareerFIZ.Common;
namespace CareerFIZ.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Action { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime ActionTime { get; set; }
        public string IPAddress { get; set; }
        public Guid AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        
    }
}
