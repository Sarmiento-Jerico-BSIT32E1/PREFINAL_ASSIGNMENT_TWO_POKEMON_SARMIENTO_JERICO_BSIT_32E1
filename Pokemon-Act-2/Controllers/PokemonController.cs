using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pokemon_Act_2.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Pokemon_Act_2.Controllers
{
    public class PokemonController : Controller
    {
        private readonly HttpClient _httpClient;

        public PokemonController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public class PokeApiResponse
        {
            public int Count { get; set; }
            public string Next { get; set; }
            public string Previous { get; set; }
            public List<PokemonDetail> Results { get; set; }
        }

        public class PokemonDetail
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }

        //Handles the navigation for index.cshtml
        public async Task<IActionResult> Index(int page = 1, int pageSize = 20)
        {
            int offset = (page - 1) * pageSize;

            HttpResponseMessage response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon?offset={offset}&limit={pageSize}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<PokeApiResponse>(content);
                var pokemonList = apiResponse.Results.Select(p => new Pokemon { Name = p.Name }).ToList();

                ViewData["Page"] = page;
                ViewData["PageSize"] = pageSize;
                ViewData["TotalCount"] = apiResponse.Count;

                return View(pokemonList);
            }
            return View("Error");
        }

        // Handles the navigation for Details.cshtml
        public async Task<IActionResult> Details(string name)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"https://pokeapi.co/api/v2/pokemon/{name.ToLower()}");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                var pokemon = JsonConvert.DeserializeObject<Pokemon>(content);

                return View(pokemon);
            }
            return View("Error");
        }


    }
}
