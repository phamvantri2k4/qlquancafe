using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using QLquanCafe.Models;
using System.ComponentModel.DataAnnotations;


namespace QLquanCafe.Models
{
    public class Table
    {
        [Key]
        public int TableId { get; set; }
        public int TableName { get; set; }
        public bool Status { get; set; }
    }
}
