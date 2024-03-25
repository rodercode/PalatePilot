using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PalatePilot.Server.Data.Contexts;
using PalatePilot.Server.Models.Domains;
using PalatePilot.Server.Models.Dto;

namespace PalatePilot.Server.Repository
{
    public class FoodRepository : IFoodRepository
    {
        private readonly PalatePilotDbContext _context;

        public FoodRepository(PalatePilotDbContext context)
        {
            _context = context;
        }
        
        public List<Food> GetAll()
        {
            return _context.Foods.ToList();
        }

        public async Task<Food?> GetById(int id)
        {
            return await _context.Foods.FindAsync(id);
        }
    }
}