using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;


namespace dotnet__rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static readonly List<Character> character = new()
        {
            new Character(),
            new Character{Id = 1 , Name="Kayo"}
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var role = _mapper.Map<Character>(newCharacter);
            role.Id = character.Max(c => c.Id) + 1;
            character.Add(role);
            ServiceResponse.Data =  character.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            return  ServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharecters(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
                var role = character.First(c => c.Id == id) ?? throw new Exception ($"character with Id '{id}' not found.");

                character.Remove(role);
                serviceResponse.Data= character.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                
            }
            return serviceResponse;
            
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharecters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>
            {
                Data = character.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()
            };


            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            var role = character.FirstOrDefault(c => c.Id == id);
            ServiceResponse.Data = _mapper.Map<GetCharacterDto>(role) ;
            return ServiceResponse;
           
            
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
                var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var role = character.FirstOrDefault(c => c.Id == updateCharacter.Id) ?? throw new Exception ($"character with Id '{updateCharacter.Id}' not found.");

                role.Name = updateCharacter.Name;
                role.HitPoints = updateCharacter.HitPoints;
                role.Strength = updateCharacter.Strength;
                role.Intelligence = updateCharacter.Intelligence;
                role.Defense = updateCharacter.Defense;
                role.Class  = updateCharacter.Class;

            serviceResponse.Data= _mapper.Map<GetCharacterDto>(role);
            }
            catch (Exception ex)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = ex.Message;
                
            }
            return serviceResponse;
            

        }
    }
}