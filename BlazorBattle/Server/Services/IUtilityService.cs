﻿using BlazorBattle.Shared;

namespace BlazorBattle.Server.Services
{
    public interface IUtilityService
    {
        Task<User> GetUser();
    }
}
