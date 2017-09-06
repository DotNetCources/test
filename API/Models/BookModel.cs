using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public List<CategoryModel> Categories { get; set; }
    }
}