using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SixMinAPI.DTOs
{
    public class CommandReadDTO
    {
        
        public int Id { get; set; } 
      
        public string? Howto { get; set; }
        
        public string? Plateform { get; set; }
    
        public string? CommandLine { get; set; }
    }
}