using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SharedLib;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            IoTViewModel viewModel = ApplicationServices.Instance.ServiceProvider.GetService<IoTViewModel>();
            viewModel.Connect();
        }
    }
}
