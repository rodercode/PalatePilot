using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PalatePilot.Server.Models; // Add missing import statements for LoginDto and RegistrationDto

namespace PalatePilot.Server.Services
{
    public interface IAuthService
    {
        Task Registration(RegistrationRequestDto request);
        Task<string> Login(LoginRequestDto request);
        Task EmailConfirmation(string token, string email);
    }
}