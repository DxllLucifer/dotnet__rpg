using System.Text.Json.Serialization;

namespace dotnet__rpg.Models
{   
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RpgClass
    {
        Initiator = 1,
        Duelist = 2,
        Controller = 3,
        Sentinal= 4,
    }
}