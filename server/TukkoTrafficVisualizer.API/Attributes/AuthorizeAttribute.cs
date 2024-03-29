﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TukkoTrafficVisualizer.Core.Constants;
using TukkoTrafficVisualizer.Infrastructure.Models;
using TukkoTrafficVisualizer.Infrastructure.Models.Responses;

namespace TukkoTrafficVisualizer.API.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute,IAuthorizationFilter
{
    private readonly IList<Role> _roles;
    public AuthorizeAttribute(params Role[]? roles)
    {
        _roles = roles ?? Array.Empty<Role>();
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        // check if anonymous attribute is set
        bool isAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
        // if is set then give access
        if (isAnonymous)
        {
            return;
        }
        // otherwise check if context contains user payload
        JwtPayload? user = (JwtPayload?)context.HttpContext.Items[Constants.UserContextName];
        // if it doesn't contain user payload or authorize attribute has roles, but they don't contain users role
        // return unauthorized response
        if (user == null || (_roles.Any() && !_roles.Contains(user.Role)))
        {
            ErrorResponse errorResponse = new ErrorResponse
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Error = "401 Unauthorized",
                Message = "You don't have enough privileges to perform action"
            };

            context.Result = new JsonResult(errorResponse)
            {
                StatusCode = errorResponse.StatusCode
            };
        }
    }
}