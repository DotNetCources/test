using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace API.Controllers
{
    public class ReportController : ApiController
    {
        private ICategoryService cat_service;
        private IBookService book_service;
        public ReportController(ICategoryService cservice, IBookService bservice)
        {
            cat_service = cservice;
            book_service = bservice;
        }

       
    }
}
