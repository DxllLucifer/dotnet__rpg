using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet__rpg.Dtos.SuperHero
{
    public class UpdateSuperHeroDto
    {
        public int Id {get; set;}
        public string? Name {get; set;}        
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? Place {get; set;}
    }
}