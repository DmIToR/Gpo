using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace InfoSystem.Entities;

public class DefaultUserTwoFactorTokenProvider<TUser> : DataProtectorTokenProvider<TUser>, IUserTwoFactorTokenProvider<TUser> where TUser : class
{
    public DefaultUserTwoFactorTokenProvider(
        IDataProtectionProvider dataProtectionProvider, 
        IOptions<DataProtectionTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<TUser>> logger
    ) 
        : base(dataProtectionProvider, options, logger)
    {
    }

    public override async Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<TUser> manager, TUser user)
    {
        var isTwoFactorEnabled = await manager.GetTwoFactorEnabledAsync(user);
        return isTwoFactorEnabled && !string.IsNullOrEmpty(await manager.GetUserNameAsync(user));
    }

    public override async Task<string> GenerateAsync(string purpose, UserManager<TUser> manager, TUser user)
    {
        if (!await CanGenerateTwoFactorTokenAsync(manager, user))
        {
            throw new NotSupportedException("Cannot generate two-factor authentication token for this user.");
        }

        return await base.GenerateAsync(purpose, manager, user);
    }

    public override async Task<bool> ValidateAsync(string purpose, string token, UserManager<TUser> manager, TUser user)
    {
        if (!await CanGenerateTwoFactorTokenAsync(manager, user))
        {
            throw new NotSupportedException("Cannot validate two-factor authentication token for this user.");
        }

        return await base.ValidateAsync(purpose, token, manager, user);
    }
}