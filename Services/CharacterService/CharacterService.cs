using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using dotnet__rpg.Data;


namespace dotnet__rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static readonly List<Character> characters = new()
        {
            new Character(),
            new Character{Id = 1 , Name="Kayo"}
        };
        private readonly IMapper _mapper;
        public readonly DataContext _context ;

        public CharacterService(IMapper mapper, DataContext context )
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
            var dbCharacters = await _context.Characters.ToListAsync();
            ServiceResponse.Data =  dbCharacters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            return  ServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharecters(int id)
        {
             var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            try
            {
               var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id) ?? throw new Exception ($"character with Id '{id}' not found.");

                _context.Characters.Remove(dbCharacter);
                await _context.SaveChangesAsync();
                var dbCharacters = await _context.Characters.ToListAsync();
                serviceResponse.Data= dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
                
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
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            ServiceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter) ;
            return ServiceResponse;
           
            
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter)
        {
                var serviceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updateCharacter.Id) ?? throw new Exception ($"character with Id '{updateCharacter.Id } not found.");
                dbCharacter.Name = updateCharacter.Name;
                dbCharacter.HitPoints = updateCharacter.HitPoints;
                dbCharacter.Strength = updateCharacter.Strength;
                dbCharacter.Intelligence = updateCharacter.Intelligence;
                dbCharacter.Defense = updateCharacter.Defense;
                dbCharacter.Class  = updateCharacter.Class;
            await _context.SaveChangesAsync();
            serviceResponse.Data= _mapper.Map<GetCharacterDto>(dbCharacter);
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