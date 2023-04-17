using System;
using System.Collections.Generic;
using System.IO;

namespace LabDarbas3_19.Class
{
    public class InOutUtils
    {
        public static LinkList<GeneralProductInfo> ReadInformations(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                LinkList<GeneralProductInfo> linkedInformations = new LinkList<GeneralProductInfo>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] Parts = line.Split(';');

                    string name = Parts[0];
                    int validity = int.Parse(Parts[1]);
                    float price = float.Parse(Parts[2]);

                    GeneralProductInfo information = new GeneralProductInfo(name, validity, price);

                    linkedInformations.Add(information);
                }
                return linkedInformations;
            }
        }

        public static LinkList<Shop> ReadShops(string fileName, LinkList<GeneralProductInfo> linkedInformations)
        {
            using (var reader = new StreamReader(fileName))
            {
                LinkList<Shop> linkedShops = new LinkList<Shop>();
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] Parts = line.Split(';');

                    string shopName = Parts[0];
                    string productName = Parts[1];
                    DateTime arrived = DateTime.Parse(Parts[2]);
                    int sold = int.Parse(Parts[3]);
                    int stock = int.Parse(Parts[4]);

                    GeneralProductInfo requiredInfo = new GeneralProductInfo(productName, 0, 0f);
                    if (linkedInformations.Contains(requiredInfo))
                    {
                        requiredInfo = linkedInformations.Find(requiredInfo).Data;
                    }

                    ShopProductInfo product = new ShopProductInfo(productName, arrived, sold, stock, requiredInfo);

                    Shop requiredShop = new Shop(shopName);
                    if (linkedShops.Contains(requiredShop))
                    {
                        linkedShops.Find(requiredShop).Data.ProductsAdd(product);
                        linkedShops.Find(requiredShop).Data.ProductsSort();
                    }
                    else
                    {
                        requiredShop.ProductsAdd(product);
                        linkedShops.Add(requiredShop);
                    }
                }
                return linkedShops;
            }
        }

        public static void Print<T>(string fileName, string title, IEnumerable<T> list, string format = null) where T : IComparable<T>, IEquatable<T>, IFormattable, ITableGeneric, new()
        {
            using (var writer = new StreamWriter(fileName, true))
            {
                string header = new T().Header(format);
                string line = new string('-', header.Length);
                string title_line = "| " + title + new string(' ', header.Length - title.Length - 3) + '|';

                writer.WriteLine(line);
                writer.WriteLine(title_line);
                writer.WriteLine(line);
                writer.WriteLine(header);
                writer.WriteLine(line);
                foreach (T item in list)
                {
                    writer.WriteLine(item.ToString(format, null));
                }
                writer.WriteLine(line);
            }
        }
    }
}