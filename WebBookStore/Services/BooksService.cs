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
    public class BooksService
    {
        string mainPath = $"http://localhost:{Localhost.port}/api/books";

        public async Task<IEnumerable<BookViewModel>> GetAll()
        {

            var client = new HttpClient();
            var response = client.GetAsync(mainPath).Result;
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<BookViewModel>>(result);

        }
        public async Task<BookViewModel> Get(int id)
        {
            var path = mainPath + "/" + id;
            var client = new HttpClient();
            var response = client.GetAsync(path).Result;
            var result = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BookViewModel>(result);
        }
        public async Task Add(string name, string author, string isbn)
        {
            var path = mainPath + "/?name=" + name + "&author=" + author + "&isbn=" + isbn;
            var client = new HttpClient();
            await client.PostAsync(path, null);
        }
        public async Task Delete(int id)
        {
            var path = mainPath + "/"+id;
            var client = new HttpClient();
            await client.DeleteAsync(path);
        }

        public async Task Edit(BookViewModel model)
        {
            var path = mainPath+"/?id="+model.Id+"/?name=" + model.Name + "&author=" + model.Author + "&isbn=" + model.ISBN;
            var client = new HttpClient();
            await client.PutAsync(path,null);
        }
    }
}