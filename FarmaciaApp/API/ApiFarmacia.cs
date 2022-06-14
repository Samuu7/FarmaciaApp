using FarmaciaApp.Entity;
using MongoExample.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FarmaciaApp.API
{
    class ApiFarmacia
    {

        string BaseUri;

        public ApiFarmacia()
        {
            BaseUri = ConfigurationManager.AppSettings["BaseUri"];
            
        }
        //Get Farmacies
        public async Task<List<Farmacia>> GetFarmaciaAsync()
        {
            List<Farmacia> farmacies = new List<Farmacia>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem una petició GET a /farmacia.
                HttpResponseMessage response = await client.GetAsync("farmacia");
                if (response.IsSuccessStatusCode)
                {
                    //Obtenim i posa el resultat a la llista de farmacies.
                    farmacies = await response.Content.ReadAsAsync<List<Farmacia>>();
                    response.Dispose();
                }
                else
                {
                    farmacies = null;
                }
            }
            return farmacies;
        }


        //GET Farmacies
        public async Task<Farmacia> GetFarmaciaAsync(String Id)
        {
            Farmacia farmacia = new Farmacia();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"farmacia/{Id}");
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        farmacia = null;
                    }
                    else
                    {
                        farmacia = await response.Content.ReadAsAsync<Farmacia>();
                        response.Dispose();
                    }
                }
                else
                {
                    farmacia = null;
                }
            }
            return farmacia;
        }

        //Afegir farmacia
        public async Task AddAsync(Farmacia farmacia)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync("farmacia", farmacia);
                response.EnsureSuccessStatusCode();
            }
        }

        //Modificar farmacia
        public async Task UpdateAsync(Farmacia farmacia, string Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync($"farmacia/{Id}", farmacia);
                response.EnsureSuccessStatusCode();
            }
        }

        //Delete farmacies
        public async Task DeleteAsync(string Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.DeleteAsync($"farmacia/{Id}");
                response.EnsureSuccessStatusCode();
            }
        }
    }
}