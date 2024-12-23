﻿using Microsoft.AspNetCore.Mvc;
using QuickTickets.Api.Dto;

namespace QuickTickets.Api.Services
{
    public interface IUserEventHistoryService
    {
        public Task<IActionResult> GetPredictedEvents(Guid userId);
    }
}
