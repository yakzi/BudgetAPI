﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BudgetAPI.Models
{
    public class Income
    {
        [Key]
        public int IncomeId { get; set; }
        [Required]
        [Column(TypeName = "decimal(16,2)")]
        public decimal Amount { get; set; }
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
