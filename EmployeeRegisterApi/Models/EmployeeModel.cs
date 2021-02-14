using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeRegisterApi.Models
{
    public class EmployeeModel
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string EmployeeName { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string Occupation { get; set; }
        
        [Column(TypeName = "nvarchar(150)")]
        public string ImageName { get; set; }
    }
}