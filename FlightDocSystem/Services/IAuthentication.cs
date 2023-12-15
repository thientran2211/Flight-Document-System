using FlightDocSystem.Authentication;
using FlightDocSystem.DTO;
using FlightDocSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocSystem.Services
{
    public interface IAuthentication
    {
        public bool ValidateUserCredentials(string email, string password);

         public string GenerateJwtToken(string email);
    }
}
