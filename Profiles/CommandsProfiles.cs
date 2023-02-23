using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SixMinAPI.Models;
using SixMinAPI.DTOs;
namespace SixMinAPI.Profiles
{
    public class CommandsProfiles :Profile
    {
        public CommandsProfiles()
        {
            CreateMap<Command,CommandReadDTO>();
            CreateMap<CommandCreateDTO,Command>();
            CreateMap<CommandUpdateDTO, Command>();
        }
        
    }
}