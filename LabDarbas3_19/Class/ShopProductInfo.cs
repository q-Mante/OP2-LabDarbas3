using System;
using System.Collections.Generic;
using System.Globalization;

namespace LabDarbas3_19.Class
{
    public class ShopProductInfo : IComparable<ShopProductInfo>, IEquatable<ShopProductInfo>, IFormattable, ITableGeneric
    {
        public string Name { get; private set; }
        public DateTime Arrived { get; private set; }
        public int Sold { get; private set; }
        public int Stock { get; private set; }
        public GeneralProductInfo ProductInfo { get; private set; }

        public DateTime Expire
        {
            get
            {
                return Arrived.AddDays(ProductInfo.Validity);
            }
        }

        public float TotalPrice
        {
            get
            {
                return Stock * ProductInfo.Price;
            }
        }

        public ShopProductInfo() { }

        public ShopProductInfo(string name, DateTime arrived, int sold, int stock, GeneralProductInfo productInfo)
        {
            Name = name;
            Arrived = arrived;
            Sold = sold;
            Stock = stock;
            ProductInfo = productInfo;
        }

        public int CompareTo(ShopProductInfo other)
        {
            if (other is null) return 1;

            if (Name.CompareTo(other.Name).Equals(0))
                return ProductInfo.Price.CompareTo(other.ProductInfo.Price);

            return Name.CompareTo(other.Name);
        }

        public bool Equals(ShopProductInfo other)
        {
            if (other is null) return false;

            return Name.Equals(other.Name) && Arrived.Equals(other.Arrived);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            ShopProductInfo shopProductInfoObj = obj as ShopProductInfo;
            if (shopProductInfoObj is null) return false;

            return this.Equals(shopProductInfoObj);
        }

        public override int GetHashCode()
        {
            int hashCode = 1278469092;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Arrived.GetHashCode();
            return hashCode;
        }

        public string ToString(string format, IFormatProvider provider = null)
        {
            if (string.IsNullOrEmpty(format)) format = "DEFAULT";
            if (provider is null) provider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant())
            {
                case "DEFAULT":
                    return string.Format("| {0,-30} | {1,17} | {2,7} |",
                        Name, Expire.ToString("yyyy-MM-dd", provider), ProductInfo.Price);
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
                    return string.Format("| {0,-30} | {1,-17} | {2,-7} |",
                        "Prekės pavadinimas", "Galiojimo pabaiga", "Kaina \u20AC");
                default:
                    throw new FormatException(string.Format("The {0} format string is not supported.", format));
            }
        }

        public static bool operator ==(ShopProductInfo lhs, ShopProductInfo rhs)
        {
            if (lhs as object is null || rhs as object is null)
                return Object.Equals(lhs, rhs);

            return lhs.Equals(rhs);
        }

        public static bool operator !=(ShopProductInfo lhs, ShopProductInfo rhs) => !(lhs == rhs);
    }
}