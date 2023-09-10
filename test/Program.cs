using DataLayer;
using Microsoft.Extensions.Hosting;
using System;

namespace test
{
    public class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=.;Initial Catalog=AmirShop_DB;User Id=sa;Password=asadasad;";
            EshopContext db = new EshopContext();
        }
           

    }
    }
}
