using FlightDocSystem.Data;
using FlightDocSystem.DTO;
using FlightDocSystem.Helper;
using FlightDocSystem.Interfaces;
using FlightDocSystem.Models;
using FlightDocSystem.Requests;
using FlightDocSystem.Responses;
using Microsoft.EntityFrameworkCore;
using System;

namespace FlightDocSystem.Services
{

    public class TokenService : ITokenService
    {
        private readonly FlightDocsContext _context;

        public TokenService(FlightDocsContext context) 
        {
            _context = context;
        }
      
        public async Task<Tuple<string, string>> GenerateTokensAsync(int userId)
        {
            var accessToken = await TokenHelper.GenerateAccessToken(userId);
            var refreshToken = await TokenHelper.GenerateRefreshToken();

            var userRecord = await _context.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync(e => e.UserID == userId);

            if (userRecord == null)
            {
                return null;
            }

            var salt = PasswordHelper.GetSecureSalt();

            var refreshTokenHashed = PasswordHelper.HashUsingPBKDF2(refreshToken, salt);

            if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                await RemoveRefreshTokenAsync(userRecord);
            }
            userRecord.RefreshTokens?.Add(new RefreshToken
            {
                ExpireDate = DateTime.Now.AddDays(30),
                Ts = DateTime.Now,
                UserId = userId,
                TokenHash = refreshTokenHashed,
                TokenSalt = Convert.ToBase64String(salt)
            });

            await _context.SaveChangesAsync();

            var token = new Tuple<string, string>(accessToken, refreshTokenHashed);

            return token;
        }

        public async Task<bool> RemoveRefreshTokenAsync(User user)
        {
            var userRecord = await _context.Users.Include(o => o.RefreshTokens).FirstOrDefaultAsync(e => e.UserID == user.UserID);

            if (userRecord == null)
            {
                return false;
            }

            if (userRecord.RefreshTokens != null && userRecord.RefreshTokens.Any())
            {
                var currentRefreshToken = userRecord.RefreshTokens.First();

                _context.RefreshTokens.Remove(currentRefreshToken);
            }

            return false;
        }

        public async Task<ValidateRefreshTokenResponse> ValidateRefreshTokenAsync(RefreshTokenRequest refreshTokenRequest)
        {
            var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync
                               (o => o.UserId == refreshTokenRequest.UserID);

            var response = new ValidateRefreshTokenResponse();
            if (refreshToken == null)
            {
                response.Success = false;
                response.Error = "Invalid session or user is already logged out";
                response.ErrorCode = "R02";
                return response;
            }

            var refreshTokenToValidateHash = PasswordHelper.HashUsingPBKDF2
                (refreshTokenRequest.RefreshToken, Convert.FromBase64String(refreshToken.TokenSalt));

            if (refreshToken.TokenHash != refreshTokenToValidateHash)
            {
                response.Success = false;
                response.Error = "Invalid refresh token";
                response.ErrorCode = "R03";
                return response;
            }

            if (refreshToken.ExpireDate < DateTime.Now)
            {
                response.Success = false;
                response.Error = "Refresh token has expired";
                response.ErrorCode = "R04";
                return response;
            }

            response.Success = true;
            response.UserId = refreshTokenRequest.UserID;

            return response;
        }
    }
}
