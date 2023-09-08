using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IEL.Models.Services
{
    public class EstadoCidadeService
    {
        private readonly IMemoryCache _memoryCache;

        public EstadoCidadeService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<List<Estado>> GetEstadosAsync()
        {
            // Tente recuperar dados do cache
            if (!_memoryCache.TryGetValue("Estados", out List<Estado> estados))
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync("https://servicodados.ibge.gov.br/api/v1/localidades/estados");
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        estados = JsonConvert.DeserializeObject<List<Estado>>(content);

                        // Salve no cache por 1 dia
                        _memoryCache.Set("Estados", estados, TimeSpan.FromDays(1));
                    }
                    else
                    {
                        Console.WriteLine("the API call failed");
                    }
                }
            }
            return estados;
        }
    }
}
