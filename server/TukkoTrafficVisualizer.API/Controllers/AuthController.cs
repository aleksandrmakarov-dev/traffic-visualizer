﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using TukkoTrafficVisualizer.API.Attributes;
using TukkoTrafficVisualizer.Core.Constants;
using TukkoTrafficVisualizer.Core.Options;
using TukkoTrafficVisualizer.Infrastructure.Exceptions;
using TukkoTrafficVisualizer.Infrastructure.Interfaces;
using TukkoTrafficVisualizer.Infrastructure.Models.Requests;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IMailingService _mailingService;
        private readonly ApplicationOptions _applicationOptions;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IMailingService mailingService, IOptions<ApplicationOptions> applicationOptions, ILogger<AuthController> logger)
        {
            _authService = authService;
            _mailingService = mailingService;
            _logger = logger;
            _applicationOptions = applicationOptions.Value;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            EmailVerificationResponse emailVerification = await _authService.SignUpAsync(request);

            // send verification token

            string verificationUrl =
                $"{_applicationOptions.ClientBaseUrl}/auth/verify-email?email={emailVerification.Email}&token={emailVerification.EmailVerificationToken}";

            string html = $"<p>Verify your account: <a href=\"{verificationUrl}\">{verificationUrl}</a></p>";

            await _mailingService.SendAsync(emailVerification.Email,
                "Tukko.con: Verify you account", html);

            // message that user is created and needs to be verified
            MessageResponse response =  new MessageResponse
            {
                Title = "Complete the registration",
                Message = "You will receive verification code to the email address"
            };

            return Ok(response);
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            // try sign in with email and password
            SignInResponse signIn = await _authService.SignInAsync(request);

            // set refresh token cookie to response
            HttpContext.Response.Cookies.Append(
                CookieNameConstants.RefreshToken,
                signIn.RefreshToken,
                new CookieOptions
                {
                    HttpOnly = Constants.CookieHttpOnly,
                    Expires = DateTime.UtcNow.AddDays(7)
                }
            );

            // return session
            return Ok(signIn.Session);
        }

        [HttpPost("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest request)
        {
            // try to verify email
            await _authService.VerifyEmailAsync(request);

            // message that email is verified
            MessageResponse response = new MessageResponse
            {
                Title = "Registration is completed",
                Message = "Your email address is verified. You can sign in to your account"
            };

            return Ok(response);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            // get refresh token from cookie
            string? refreshToken = HttpContext.Request.Cookies[CookieNameConstants.RefreshToken];

            // if refreshToken cookie not found throw an error
            if (refreshToken == null)
            {
                throw new BadRequestException("No refresh token provided");
            }
            
            // try to get session with refresh token
            SessionResponse session = await _authService.RefreshTokenAsync(new RefreshTokenRequest
            {
                Token = refreshToken
            });

            return Ok(session);
        }

        [HttpPost("new-email-verification")]
        public async Task<IActionResult> NewEmailVerification(NewEmailVerificationRequest request)
        {
            EmailVerificationResponse? emailVerification = await _authService.NewEmailVerificationAsync(request);

            if (emailVerification == null)
            {
                MessageResponse alreadyVerified = new MessageResponse
                {
                    Title = "Email verification",
                    Message = "Email is already verified. You can sign in to your account"
                };

                return Ok(alreadyVerified);
            }

            // send email verification

            string verificationUrl =
                $"{_applicationOptions.ClientBaseUrl}/auth/verify-email?email={emailVerification.Email}&token={emailVerification.EmailVerificationToken}";

            await _mailingService.SendAsync(emailVerification.Email,
                "Tukko.com: Verify you account with", $"Verify your account: {verificationUrl}");

            MessageResponse response = new MessageResponse
            {
                Title = "New email verification",
                Message = "New email verification token is sent to your email address"
            };

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpDelete("sign-out")]
        public async Task<IActionResult> ExpireSession()
        {
            // get refresh token from cookie
            string? refreshToken = HttpContext.Request.Cookies[CookieNameConstants.RefreshToken];

            // if refreshToken cookie not found throw an error
            if (refreshToken == null)
            {
                throw new BadRequestException("No refresh token provided");
            }

            await _authService.SignOutAsync(new SignOutRequest { Token = refreshToken });

            HttpContext.Response.Cookies.Delete(CookieNameConstants.RefreshToken);

            return NoContent();
        }
    }
}
