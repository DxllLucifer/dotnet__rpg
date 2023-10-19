using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet__rpg.Services.SuperHeroService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet__rpg.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SuperHeroController : ControllerBase
    {
        private readonly ISuperHeroService _superHeroService;
        public SuperHeroController(ISuperHeroService superHeroService)
        {
            _superHeroService = superHeroService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get(){
            return Ok( await _superHeroService.GetAllSuperHeros());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> GetSuperHero(int id){
            var response = await _superHeroService.GetSuperHero(id);
            if(response.Data is null){
                return NotFound(response);
            }
            return Ok( response );
        }
    }
}