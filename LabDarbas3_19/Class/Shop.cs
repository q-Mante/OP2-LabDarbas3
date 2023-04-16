using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Web.UI.WebControls;

namespace LabDarbas3_19.Class
{
    public class Shop : IComparable<Shop>, IEquatable<Shop>, IFormattable, ITableGeneric
    {
        public string Name { get; set; }

        public int AllStock
        {
            get
            {
                int sum = 0;
                foreach (ShopProductInfo info in shopProductInfos)
                {
                    sum += info.Stock;
                }
                return sum;
            }
        }

        public float Value
        {
            get
            {
                float sum = 0f;
                foreach (ShopProductInfo info in shopProductInfos)
                {
                    sum += info.TotalPrice;
                }
                return sum;
            }
        }

        private LinkList<ShopProductInfo> shopProductInfos;

        public Shop() { }

        public Shop(string name)
        {
            Name = name;
            shopProductInfos = new LinkList<ShopProductInfo>();
        }

        public ShopProductInfo FavoriteProduct()
        {
            ShopProductInfo favorite = new ShopProductInfo();
            float ratio = -1f;

            foreach (ShopProductInfo info in shopProductInfos)
            {
                float selectedRatio = info.Sold / (DateTime.Now - info.Arrived).Days;
                if (selectedRatio > ratio)
                {
                    favorite = info;
                    ratio = selectedRatio;
                }
            }
            return favorite;
        }

        public void FindExpires(LinkList<ShopProductInfo> Expires, int days)
        {
            foreach (ShopProductInfo info in shopProductInfos)
            {
                if (info.Expire >= DateTime.Now && info.Expire <= DateTime.Now.AddDays(days))
                    Expires.Add(info);
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach (ShopProductInfo info in shopProductInfos)
            {
                yield return info;
            }
        }

        public int ProductsCount() => shopProductInfos.Count;

        public void ProductsAdd(ShopProductInfo product) => shopProductInfos.Add(product);

        public int CompareTo(Shop other)
        {
            if (other is null) return 1;

            return Name.CompareTo(other.Name);
        }

        public bool Equals(Shop other)
        {
            if (other is null) return false;

            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            Shop shopObj = obj as Shop;
            if (shopObj is null) return false;

            return this.Equals(shopObj);
        }

        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        public string ToString(string format, IFormatProvider provider = null)
        {
            if (string.IsNullOrEmpty(format)) format = "DEFAULT";
            if (provider is null) provider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "DEFAULT":
                    return string.Format("| {0,-40} | {1,7} | {2,10} |",
                        Name, AllStock, Value);
                case "INPUT":
                    StringBuilder newString = new StringBuilder();
                    foreach (ShopProductInfo info in shopProductInfos)
                    {
                        newString.Append(string.Format("| {0,-40} | {1,-30} | {2,13} | {3,8} | {4,7} |\n",
                            Name, info.Name, info.Arrived.ToString("yyyy-MM-dd", provider), info.Sold, info.Stock));
                    }
                    newString.Remove(newString.Length - 1, 1);
                    return newString.ToString();
                default:
                    throw new FormatException(string.Format("The {0} format string is not supported.", format));
            }
        }

        public override string ToString()
        {
            return this.ToString("DEFAULT");
        }

        public string Header(string format = null)
        {
            if (string.IsNullOrEmpty(format)) format = "DEFAULT";

            switch (format.ToUpperInvariant())
            {
                case "DEFAULT":
                    return string.Format("| {0,-40} | {1,-7} | {2,-10} |",
                        "Parduotuvės pavadinimas", "Likutis", "Vertė");
                case "INPUT":
                    return string.Format("| {0,-40} | {1,-30} | {2,-13} | {3,-8} | {4,-7} |",
                        "Parduotuvės pavadinimas", "Prekės pavadinimas", "Atvykimo data", "Parduota", "Likutis");
                default:
                    throw new FormatException(string.Format("The {0} format string is not supported.", format));
            }
        }

        public static bool operator ==(Shop lhs, Shop rhs)
        {
            if (lhs as object is null || rhs as object is null)
                return Object.Equals(lhs, rhs);

            return lhs.Equals(rhs);
        }

        public static bool operator !=(Shop lhs, Shop rhs) => !(lhs == rhs);
    }
}