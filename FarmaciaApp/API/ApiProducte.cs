using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using MongoExample.Entity;

namespace FarmaciaApp.API
{
    class ApiProducte
    {
        String BaseUri;

        public ApiProducte()
        {
            BaseUri = ConfigurationManager.AppSettings["BaseUri"];
        }
        //GET totes les productes.
        public async Task<List<Producte>> GetProducteAsync()
        {
            List<Producte> productes = new List<Producte>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("producte");
                if (response.IsSuccessStatusCode)
                {
                    
                    productes = await response.Content.ReadAsAsync<List<Producte>>();
                    response.Dispose();
                }
                else
                {
                    productes = null;
                }
            }
            return productes;
        }
        //POST (Afegir tasca)
        public async Task AddAsync(Producte producte)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

               
                HttpResponseMessage response = await client.PostAsJsonAsync("producte", producte);
                response.EnsureSuccessStatusCode();
            }
        }
        //PUT (Modificar) 
        public async Task UpdateAsync(Producte producte)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem petició 
                HttpResponseMessage response = await client.PutAsJsonAsync("producte", producte);
                response.EnsureSuccessStatusCode();
            }
        }
        //DELETE (Borrar)
        public async Task DeleteAsync(string Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Enviem petició DELETE 
                HttpResponseMessage response = await client.DeleteAsync($"producte/{Id}");
                response.EnsureSuccessStatusCode();
            }
        }
        //GET Productes
        public async Task<Producte> GetProducteAsync(string Id)
        {
            Producte producte = new Producte();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUri);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Enviem petició GET 
                HttpResponseMessage response = await client.GetAsync($"producte/{Id}");
                if (response.IsSuccessStatusCode)
                {
                   
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        producte = null;
                    }
                    else
                    {
                        
                        producte = await response.Content.ReadAsAsync<Producte>();
                        response.Dispose();
                    }
                }
                else
                {
                    producte = null;
                }
            }
            return producte;
        }
    }
}

