using BLL.Services;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Infrastructure
{
    public class NinjectDependencyResolver : System.Web.Mvc.IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<IBookService>().To<BookService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
        }
    }
}