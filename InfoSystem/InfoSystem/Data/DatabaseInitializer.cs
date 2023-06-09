﻿using System.Security.Claims;
using InfoSystem.Entities;
using Microsoft.AspNetCore.Identity;

namespace InfoSystem.Data;

public static class DatabaseInitializer
{
    public static void Init(IServiceProvider scopeServiceProvider)
    {
        var userManager = scopeServiceProvider.GetService<UserManager<User>>();
        if (userManager is null)
            return;

        if (userManager.FindByNameAsync("admin").GetAwaiter().GetResult() is not null)
            return;
        
        var user = new User { UserName = "admin" };

        var result = userManager.CreateAsync(user, "Admin1!")
            .GetAwaiter()
            .GetResult();
        
        if (result.Succeeded)
            userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin")).GetAwaiter().GetResult();
    }
}