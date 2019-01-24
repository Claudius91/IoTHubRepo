using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SharedLib;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IoTViewModel viewModel = new IoTViewModel(new UrlService(), null); //ApplicationServices.Instance.ServiceProvider.GetService<IoTViewModel>();
            viewModel.Connect();
        }
    }
}
