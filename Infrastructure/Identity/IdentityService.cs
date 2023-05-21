using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.AspNetCore.Identity.Cognito;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Application.Authorization.DTOs;
using Application.Common.DTOs;
using Application.Common.Enums;
using Application.Interfaces;
using Domain.Enum;
using Domain.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity
{
    public class IdentityService: IIdentityService
    {
        private readonly UserManager<CognitoUser> _userManager;
        private readonly CognitoUserPool _pool;
        private readonly AmazonCognitoIdentityProviderClient _adminManager;

        public IdentityService(UserManager<CognitoUser> userManager, CognitoUserPool pool, AmazonCognitoIdentityProviderClient adminManager)
        {
            _userManager = userManager;
            _pool = pool;
            _adminManager = adminManager;
        }

        public async Task<CreateUserResultDto> CreateUser(SignUpDto signupDto)
        {
            await _pool.AdminSignupAsync(signupDto.Email, new Dictionary<string, string>
            {
                { CognitoAttribute.Email.AttributeName, signupDto.Email }
            }, null);

            var result = await _userManager.FindByEmailAsync(signupDto.Email);
            await _userManager.AddToRoleAsync(result, Role.Customer.ToString());
            
            return new CreateUserResultDto
            {
                UserId = Guid.Parse(result.UserID),
                IsSuccess = true,
                Role = Role.Customer
            };
        }

        public Task ForgotPassword(string email)
        {
            try
            {
                return Task.CompletedTask;
                //var user = _pool.GetUser(signupDto.Email);

                //await _userManager.GetEmailAsync(email);
            }
            catch
            {
                throw new Amazon.CognitoIdentity.Model.InvalidParameterException(nameof(email));
            }
        }

        public async Task<LoginResponseDto> LoginUser(LoginDto loginDto)
        {
            var user = _pool.GetUser(loginDto.Email);

            var response = await user.StartWithSrpAuthAsync(new InitiateSrpAuthRequest
            {
                Password = loginDto.Password
            });
            
            return new LoginResponseDto
            {
                UserId = Guid.Parse(user.UserID),
                Email = loginDto.Email,
                ExpiresIn = 30,
                Token = response.AuthenticationResult.AccessToken,
                RefreshToken = response.AuthenticationResult.RefreshToken,
            };
        }
        
        public async Task DeleteUser(string userEmail)
        {
            var user = _pool.GetUser(userEmail);

            AdminDeleteUserRequest deleteUserRequest = new AdminDeleteUserRequest
            {
                Username = user.Username,
                UserPoolId = _pool.PoolID
            };

            await _adminManager
                .AdminDeleteUserAsync(deleteUserRequest);
        }
    }
}