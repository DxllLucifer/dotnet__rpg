using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Infrastructure;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace dotnet__rpg.Models
{
    public class SuperHero
    {
        public int Id {get; set;}
        public string? Name {get; set;}        
        public string? FirstName {get; set;}
        public string? LastName {get; set;}
        public string? Place {get; set;}
    }
}