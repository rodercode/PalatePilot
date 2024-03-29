using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PalatePilot.Server.Models.Domains;
using PalatePilot.Server.Models.Dto;

namespace PalatePilot.Server.Services.FoodService
{
    public interface IFoodService
    {
        Task<List<FoodDto>> GetAll();
        Task<FoodDto> GetById(int id);
    }
}