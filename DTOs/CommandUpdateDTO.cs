using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SixMinAPI.DTOs
{
    public class CommandUpdateDTO
    {
          [Required]
        public string? Howto { get; set; }
        [Required]
        [MaxLength(5)]
        public string? Plateform { get; set; }
    
        [Required]
        public string? CommandLine { get; set; }
    }
}