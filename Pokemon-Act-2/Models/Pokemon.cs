using Newtonsoft.Json;
using System.Collections.Generic;

namespace Pokemon_Act_2.Models
{
    public class Pokemon
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("sprites")]
        public Sprites Sprites { get; set; }

        [JsonProperty("moves")]
        public List<Move> Moves { get; set; }

        [JsonProperty("abilities")]
        public List<Ability> Abilities { get; set; }
    }

    public class Sprites
    {
        [JsonProperty("front_default")]
        public string FrontDefault { get; set; }
    }

    public class Move
    {
        [JsonProperty("move")]
        public MoveDetail MoveDetail { get; set; }
    }

    public class MoveDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class Ability
    {
        [JsonProperty("ability")]
        public AbilityDetail AbilityDetail { get; set; }
    }

    public class AbilityDetail
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
