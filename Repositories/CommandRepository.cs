using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SixMinAPI.Data;
using SixMinAPI.Models;

namespace SixMinAPI.Repositories
{
    public class CommandRepository : ICommandRepository
    {

        private readonly AppDataContext _context;
        public CommandRepository(AppDataContext context)
        {
            this._context= context;
        }
        public async Task CreateCommand(Command model)
        {
            
            if(model is null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            await _context.Commands.AddAsync(model);

        }

        public void DeleteCommand(Command model)
        {
            if(model is null)   
            {
                throw new ArgumentNullException(nameof(model));
            }
            _context.Commands.Remove(model);
        }

        public async Task<IEnumerable<Command>> GetAllCommands()
        {
            return await _context.Commands.ToListAsync();
        }

        public async Task<Command?> GetCommandById(int id)
        {
            return await _context.Commands.FirstOrDefaultAsync(s=>s.Id==id);
        }

        public async Task SaveChanges() => await _context.SaveChangesAsync();
    }
}