using System;
using System.Collections.Generic;
using System.Globalization;

namespace LabDarbas3_19.Class
{
    public class GeneralProductInfo : IComparable<GeneralProductInfo>, IEquatable<GeneralProductInfo>, IFormattable, ITableGeneric
    {
        public string Name { get; private set; }
        public int Validity { get; private set; }
        public float Price { get; private set; }

        public GeneralProductInfo() { }

        public GeneralProductInfo(string name, int validity, float price)
        {
            Name = name;
            Validity = validity;
            Price = price;
        }

        public int CompareTo(GeneralProductInfo other)
        {
            if (other is null) return 1;

            if (Name.CompareTo(other.Name).Equals(0))
                return Price.CompareTo(other.Price);

            return Name.CompareTo(other.Name);
        }

        public bool Equals(GeneralProductInfo other)
        {
            if (other is null) return false;

            return Name.Equals(other.Name);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            GeneralProductInfo generalProductInfoObj = obj as GeneralProductInfo;
            if (generalProductInfoObj is null) return false;

            return this.Equals(generalProductInfoObj);
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
                    return string.Format("| {0,-30} | {1,25} | {2,7} |",
                        Name, Validity, Price);
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
                    return string.Format("| {0,-30} | {1,-25} | {2,-7} |",
                    "Prekės pavadinimas", "Galiojimo laikotarpis (d)", "Kaina \u20AC");
                default:
                    throw new FormatException(string.Format("The {0} format string is not supported.", format));
            }
        }

        public static bool operator ==(GeneralProductInfo lhs, GeneralProductInfo rhs)
        {
            if (lhs as object is null || rhs as object is null)
                return Object.Equals(lhs, rhs);

            return lhs.Equals(rhs);
        }

        public static bool operator !=(GeneralProductInfo lhs, GeneralProductInfo rhs) => !(lhs == rhs);
    }
}