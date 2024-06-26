using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet__rpg.Services.CharacterService
{
    public interface ICharacterService
    {
    
        Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharecters();
        
        Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
        
        Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);
        Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharecters(int id);

    }
}