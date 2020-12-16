namespace CasesNET.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CasesNET.Data.Models;

    public class DevicesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Devices.Any())
            {
                return;
            }

            var manufacturersAndDevices = new Dictionary<string, List<string>>
            {
                {
                    "Samsung", new List<string>
                {
                    "Galaxy M21s",
                    "Galaxy M31 Prime",
                    "Galaxy F41",
                    "Galaxy Tab Active3",
                    "Galaxy S20 FE 5G",
                    "Galaxy S20 FE",
                    "Galaxy A42 5G",
                    "Galaxy M51",
                    "Galaxy A51 5G UW",
                    "Galaxy Z Fold2 5G",
                    "Galaxy Note20 Ultra 5G",
                    "Galaxy Note20 Ultra",
                    "Galaxy Note20 5G",
                    "Galaxy Note20",
                    "Galaxy Tab S7+",
                    "Galaxy Tab S7",
                    "Galaxy Z Flip 5G",
                    "Galaxy M31s",
                    "Galaxy M01s",
                    "Galaxy M01 Core",
                    "Galaxy A01 Core",
                    "Galaxy A71 5G UW",
                    "Galaxy M01",
                    "Galaxy A21s",
                    "Galaxy A Quantum",
                    "Galaxy A71 5G",
                    "Galaxy A51 5G",
                    "Galaxy A21",
                    "Galaxy Tab S6 Lite",
                    "Galaxy M11",
                    "Galaxy A31",
                    "Galaxy A41",
                    "Galaxy M21",
                    "Galaxy A11",
                    "Galaxy M31",
                    "Galaxy S20 Ultra 5G",
                    "Galaxy S20 Ultra",
                    "Galaxy S20+ 5G",
                    "Galaxy S20+",
                    "Galaxy S20 5G UW",
                    "Galaxy S20 5G",
                    "Galaxy S20",
                    "Galaxy Z Flip",
                    "Galaxy Tab S6 5G",
                    "Galaxy Xcover Pro",
                    "Galaxy Note10 Lite",
                    "Galaxy S10 Lite",
                    "Galaxy A01",
                    "Galaxy A71",
                    "Galaxy A51",
                    "Galaxy Xcover FieldPro",
                    "Galaxy A70s",
                    "Galaxy A20s",
                    "Galaxy M30s",
                    "Galaxy M10s",
                    "Galaxy Fold 5G",
                    "Galaxy Fold",
                    "Galaxy Tab Active Pro",
                    "Galaxy A90 5G",
                    "Galaxy A30s",
                    "Galaxy A50s",
                    "Galaxy Note10+ 5G",
                    "Galaxy Note10+",
                    "Galaxy Note10 5G",
                    "Galaxy Note10",
                    "Galaxy A10s",
                    "Galaxy A10e",
                    "Galaxy Tab S6",
                    "Galaxy Xcover 4s",
                    "Galaxy S10 5G",
                    "Galaxy S10+",
                    "Galaxy S10",
                    "Galaxy S10e",
                    "Galaxy M40",
                    "Galaxy M30",
                }
                },
                {
                    "Apple", new List<string>
                {
                    "iPhone 12 Pro Max",
                    "iPhone 12 Pro",
                    "iPhone 12",
                    "iPhone 12 mini",
                    "iPhone SE",
                    "iPad Pro 12.9",
                    "iPad Pro 11",
                    "iPhone 11 Pro Max",
                    "iPhone 11 Pro",
                    "iPhone 11",
                    "iPad 10.2",
                    "iPad Air",
                    "iPad mini",
                    "iPad Pro 12.9",
                    "iPad Pro 11",
                    "iPhone XS Max",
                    "iPhone XS",
                    "iPhone XR",
                    "iPhone X",
                    "iPhone 8 Plus",
                    "iPhone 8",
                }
                },
                {
                    "Xiaomi", new List<string>
                    {
                        "Redmi K30S",
                        "Poco C3",
                        "Mi 10T Pro 5G",
                        "Mi 10T 5G",
                        "Mi 10T Lite 5G",
                        "Redmi 9AT",
                        "Redmi 9i",
                        "Poco M2",
                        "Poco X3",
                        "Poco X3 NFC",
                        "Mi 10 Ultra",
                        "Redmi K30 Ultra",
                        "Redmi 9 Prime",
                        "Black Shark 3S",
                        "Poco M2 Pro",
                        "Redmi 9A",
                        "Redmi 9C NFC",
                        "Redmi 9C",
                        "Redmi 9",
                        "Redmi 10X Pro 5G",
                        "Redmi 10X 5G",
                        "Redmi 10X 4G",
                        "Redmi K30i 5G",
                        "Poco F2 Pro",
                        "Redmi K30 5G Racing",
                        "Redmi Note 9 Pro",
                        "Redmi Note 9",
                        "Mi Note 10 Lite",
                        "Mi 10 Youth 5G",
                        "Mi 10 Lite 5G",
                        "Redmi K30 Pro Zoom",
                        "Redmi K30 Pro",
                        "Redmi Note 9S",
                        "Redmi Note 9 Pro Max",
                        "Black Shark 3 Pro",
                    }
                },
                {
                    "Huawei", new List<string>
                {
                "nova 8 SE",
                "Mate 40 RS",
                "Mate 40 Pro+",
                "Mate 40 Pro",
                "Mate 40",
                "Mate 30E Pro 5G",
                "nova 7 SE 5G Youth",
                "Y7a",
                "P smart 2021",
                "MatePad 5G",
                "Y9a",
                "Enjoy 20 Plus 5G",
                "Enjoy 20 5G",
                "Watch Fit",
                "MatePad 10.8",
                "MatePad T 10s",
                "Enjoy Tablet 2",
                "Children's Watch 4X",
                "Enjoy 20 Pro",
                "Enjoy Z 5G",
                "P Smart S",
                "Y8p",
                "P40 lite 5G",
                "P30 Pro New Edition",
                "MatePad T8",
                "Y8s",
                "Y6p",
                "Y5p",
                "P smart 2020",
                "nova 7 Pro 5G",
                "nova 7 5G",
                "nova 7 SE",
                "MatePad 10.4",
                "P40 Pro+",
                "P40 Pro",
                "P40",
                "P40 lite E",
                }
                },
            };

            foreach (var manufacturerAndDevices in manufacturersAndDevices)
            {
                var manufacturerName = manufacturerAndDevices.Key;
                var manufacturerId = dbContext.Manufacturers.FirstOrDefault(x => x.Name == manufacturerName).Id;
                if (manufacturerId == null)
                {
                    continue;
                }

                var devices = new List<Device>();
                foreach (var deviceName in manufacturerAndDevices.Value)
                {
                    devices.Add(new Device
                    {
                        Name = deviceName,
                        ManufacturerId = manufacturerId,
                    });
                }

                if (devices.Count > 0)
                {
                 await dbContext.Devices.AddRangeAsync(devices);
                }
            }
        }
    }
}
