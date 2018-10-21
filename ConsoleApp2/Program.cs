﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq.Dynamic.Core;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = new[]
            {
                new Apprentice
                {
                    Id = Guid.NewGuid(),
                    GivenName = "a",
                    FamilyName = "f",
                    Addresses = new []
                    {
                        new ApprenticeAddress
                        {
                            AddressLine1 = "address x"
                        },
                        new ApprenticeAddress
                        {
                            AddressLine1 = "address y"
                        }
                    }
                }
            }.AsQueryable();

            var result = data.Select(x =>
                new
                {
                    x.FamilyName,
                    Addresses = x.Addresses.Select(y => y.AddressLine1)
                });
            Console.WriteLine("result: " + JsonConvert.SerializeObject(result, Formatting.Indented));

            var resultDynamic = data.Select("new (FamilyName as FamilyName, Addresses.Select(AddressLine1) as Addresses)");
            Console.WriteLine("resultDynamic: " + JsonConvert.SerializeObject(resultDynamic, Formatting.Indented));
        }
    }
}
