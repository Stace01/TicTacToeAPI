﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using TicTacToeAPI.Presentation.Models;
using System.Security.Claims;

namespace TicTacToeAPI.Presentation.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        // Поле медиатора для использования его
        // для формирования команд при выполнении
        // запросов.
        private IMediator _mediator;
        protected IMediator Mediator =>
            _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        // Поле Id пользователя.
        internal Guid PlayerId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
    }
}