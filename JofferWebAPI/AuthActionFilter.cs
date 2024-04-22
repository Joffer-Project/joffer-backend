using JofferWebAPI.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace JofferWebAPI;

public class AuthActionFilter : IAsyncActionFilter
{
    private readonly DbContextRender _context;

    public AuthActionFilter(DbContextRender context)
    {
        _context = context;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userSubClaim = context.HttpContext.User?.FindFirst(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

        if (userSubClaim == null)
        {
            context.Result = new BadRequestObjectResult("User identifier claim not found. (In other words, user is not logged in)");
            return;
        }

        string userSub = userSubClaim.Value;

        var account = await _context.Accounts.FirstOrDefaultAsync(u => u.Auth0Id == userSub);

        if (account == null)
        {
            context.Result = new NotFoundObjectResult($"Account with Auth0Id {userSub} not found. (Post the Account (talent or company) first.");
            return;
        }

        // Store the account in the HttpContext
        context.HttpContext.Items["UserAccount"] = account;

        await next();
    }
}