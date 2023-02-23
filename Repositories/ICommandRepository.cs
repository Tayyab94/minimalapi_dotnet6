using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SixMinAPI.Models;

namespace SixMinAPI.Repositories
{
    public interface ICommandRepository
    {
        Task SaveChanges();
        Task<Command?>GetCommandById(int id); 
        Task<IEnumerable<Command>>GetAllCommands();
        Task CreateCommand(Command model);

        void DeleteCommand(Command model);
    }
}