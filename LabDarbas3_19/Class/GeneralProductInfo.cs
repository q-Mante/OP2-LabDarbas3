using System;
using System.Collections.Generic;
using System.Globalization;

namespace LabDarbas3_19.Class
{
    /// <summary>
    /// Represents general product information, including its name, validity, and price.
    /// </summary>
    public class GeneralProductInfo : IComparable<GeneralProductInfo>, IEquatable<GeneralProductInfo>, IFormattable, ITableGeneric
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the validity period of the product (in days).
        /// </summary>
        public int Validity { get; private set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public float Price { get; private set; }

        /// <summary>
        /// Initializes a new instance of the GeneralProductInfo class.
        /// </summary>
        public GeneralProductInfo() { }

        /// <summary>
        /// Initializes a new instance of the GeneralProductInfo class with the specified name, validity period, and price.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="validity">The validity period of the product (in days).</param>
        /// <param name="price">The price of the product.</param>
        public GeneralProductInfo(string name, int validity, float price)
        {
            Name = name;
            Validity = validity;
            Price = price;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">The object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(GeneralProductInfo other)
        {
            if (other is null) return 1;

            if (Name.CompareTo(other.Name).Equals(0))
                return Price.CompareTo(other.Price);

            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">The object to compare with this instance.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(GeneralProductInfo other)
        {
            if (other is null) return false;

            return Name.Equals(other.Name);
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            GeneralProductInfo generalProductInfoObj = obj as GeneralProductInfo;
            if (generalProductInfoObj is null) return false;

            return this.Equals(generalProductInfoObj);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        /// <summary>
        /// Overrides the ToString method to return a formatted string representation of the object.
        /// </summary>
        /// <param name="format">The format of the string. If not specified, the "DEFAULT" format is used.</param>
        /// <param name="provider">The format provider. If not specified, the current culture is used.</param>
        /// <returns>A string representation of the object.</returns>
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

        /// <summary>
        /// Overrides the default ToString method to return the default string representation of the object.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            return this.ToString("DEFAULT");
        }

        /// <summary>
        /// Implements the Header method of the ITableGeneric interface to return a formatted header string.
        /// </summary>
        /// <param name="format">The format of the string. If not specified, the "DEFAULT" format is used.</param>
        /// <returns>A formatted header string.</returns>
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

        /// <summary>
        /// Implements the equality operator to compare two GeneralProductInfo objects.
        /// </summary>
        /// <param name="lhs">The left-hand side GeneralProductInfo object.</param>
        /// <param name="rhs">The right-hand side GeneralProductInfo object.</param>
        /// <returns>true if the two objects are equal; otherwise, false.</returns>
        public static bool operator ==(GeneralProductInfo lhs, GeneralProductInfo rhs)
        {
            if (lhs as object is null || rhs as object is null)
                return Object.Equals(lhs, rhs);

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Implements the inequality operator to compare two GeneralProductInfo objects.
        /// </summary>
        /// <param name="lhs">The left-hand side GeneralProductInfo object.</param>
        /// <param name="rhs">The right-hand side GeneralProductInfo object.</param>
        /// <returns>true if the two objects are not equal; otherwise, false.</returns>
        public static bool operator !=(GeneralProductInfo lhs, GeneralProductInfo rhs) => !(lhs == rhs);
    }
}