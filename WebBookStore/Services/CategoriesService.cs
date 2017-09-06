using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WebBookStore.Models;

namespace WebBookStore.Services
{
    public class CategoriesService
    {
        string mainPath = $"http://localhost:{Localhost.port}/api/categories";

        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {

            var client = new HttpClient();
            var response = client.GetAsync(mainPath).Result;
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<CategoryViewModel>>(result);

        }
        public async Task Add(string name)
        {
            var path = mainPath + "/?name=" + name;
            var client = new HttpClient();
            await client.PostAsync(path, null);
        }
        public async Task Delete(int id)
        {
            var path = mainPath + "/" + id;
            var client = new HttpClient();
            await client.DeleteAsync(path);
        }
    }
}