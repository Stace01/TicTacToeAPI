using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using DrfLikePaginations;
using Microsoft.AspNetCore.Http.Extensions;
using Serilog;
using System;
using TicTacToeAPI.Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using TicTacToeAPI.Presentation.Models;
using TicTacToeAPI.Core.Queries.GetList;
using TicTacToeAPI.Core.Commands;
using TicTacToeAPI.Core.Queries.GetDetails;

namespace TicTacToeAPI.Presentation.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlayersController : BaseController
    {
        private readonly AppDbContext _context;
        private readonly IPagination _pagination;
        private readonly IMapper _mapper;

        public PlayersController(AppDbContext context, IPagination pagination, IMapper mapper)
        {
            _context = context;
            _pagination = pagination;
            _mapper = mapper;
        }

        /// <summary>
        /// Получаем данные всех игроков.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "GetPlayer")]
        public async Task<ActionResult<PlayerlistVm>> GetAllPlayers()
        {
            Log.Information("Получение всех игроков...");

            var query = new GetPlayerlistQuery
            {
                PlayerId = PlayerId
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Получаем данные игрока по его логину.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{name}")]
        public async Task<ActionResult<PlayerDetailsVm>> GetPlayerData(string name)
        {
            Log.Information("Получение игрока по его имени: {Id}", name);

            var query = new GetPlayerDetailsQuery
            {
                PlayerId = PlayerId
            };

            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Добавляем нового игрока.
        /// Атрибут "[FromBody]" указывает,
        /// что параметр метода контроллера должен
        /// быть извлечён из данных тела
        /// HTTP запроса и затем, сериализован.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<PlayerlistVm>> AddNewPlayer([FromBody] PlayerDTO playerDTO)
        {
            var command = _mapper.Map<CreatePlayerCommand>(playerDTO);
            command.PlayerName = playerDTO.PlayerName;
            var playerName = await Mediator.Send(command);
            return Ok(playerName);
        }

        /// <summary>
        /// Обновляем данные игрока.
        /// </summary> 
        /// <param name="playerData"></param>
        /// <returns></returns>
        [HttpPut("UpdatePlayer")]
        public async Task<IActionResult> UpdatePlayerData([FromBody] PlayerDTO playerDTO)
        {
            var command = _mapper.Map<UpdatePlayerCommand>(playerDTO);

            try
            {
                command.PlayerName = playerDTO.PlayerName;
                await Mediator.Send(command);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Players.Any(p => p.PlayerId == playerDTO.PlayerId) is false)
                    return NotFound();
                throw;
            }

            return NoContent();
        }

        /// <summary>
        /// Удаляем игрока.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpDelete("DeletePlayer")]
        public async Task<ActionResult> DeletePlayer(string name, Guid layerId)
        {
            var player = await _context.Players.FindAsync(name);

            var command = new DeletePlayerCommand
            {
                PlayerId = layerId,
                PlayerName = name
            };

            if (player is null)
            {
                Log.Information("Игрок не найден");
                return NotFound();
            }

            await Mediator.Send(command);

            return NoContent();
        }
    }
}
