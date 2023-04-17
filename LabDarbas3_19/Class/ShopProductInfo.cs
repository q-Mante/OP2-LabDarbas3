using System;
using System.Collections.Generic;
using System.Globalization;

namespace LabDarbas3_19.Class
{
    /// <summary>
    /// Represents a product sold by a shop, containing information about its name, arrival date, sold quantity, stock quantity, and general product information.
    /// </summary>
    public class ShopProductInfo : IComparable<ShopProductInfo>, IEquatable<ShopProductInfo>, IFormattable, ITableGeneric
    {
        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets or sets the date the product arrived at the shop.
        /// </summary>
        public DateTime Arrived { get; private set; }

        /// <summary>
        /// Gets or sets the quantity of the product sold by the shop.
        /// </summary>
        public int Sold { get; private set; }

        /// <summary>
        /// Gets or sets the quantity of the product in stock at the shop.
        /// </summary>
        public int Stock { get; private set; }

        /// <summary>
        /// Gets or sets the general product information for the product.
        /// </summary>
        public GeneralProductInfo ProductInfo { get; private set; }

        /// <summary>
        /// Gets the date the product will expire, based on its arrival date and validity period.
        /// </summary>
        public DateTime Expire
        {
            get
            {
                return Arrived.AddDays(ProductInfo.Validity);
            }
        }

        /// <summary>
        /// Gets the total price of the product, based on its price and quantity in stock.
        /// </summary>
        public float TotalPrice
        {
            get
            {
                return Stock * ProductInfo.Price;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopProductInfo"/> class with default values.
        /// </summary>
        public ShopProductInfo() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopProductInfo"/> class with the specified values for its properties.
        /// </summary>
        /// <param name="name">The name of the product.</param>
        /// <param name="arrived">The date the product arrived at the shop.</param>
        /// <param name="sold">The quantity of the product sold by the shop.</param>
        /// <param name="stock">The quantity of the product in stock at the shop.</param>
        /// <param name="productInfo">The general product information for the product.</param>
        public ShopProductInfo(string name, DateTime arrived, int sold, int stock, GeneralProductInfo productInfo)
        {
            Name = name;
            Arrived = arrived;
            Sold = sold;
            Stock = stock;
            ProductInfo = productInfo;
        }

        /// <summary>
        /// Compares the current instance with another <see cref="ShopProductInfo"/> instance and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other instance.
        /// </summary>
        /// <param name="other">The <see cref="ShopProductInfo"/> instance to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(ShopProductInfo other)
        {
            if (other is null) return 1;

            if (Name.CompareTo(other.Name).Equals(0))
                return ProductInfo.Price.CompareTo(other.ProductInfo.Price);

            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Determines whether this <see cref="ShopProductInfo"/> object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">The <see cref="ShopProductInfo"/> object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public bool Equals(ShopProductInfo other)
        {
            if (other is null) return false;

            return Name.Equals(other.Name) && Arrived.Equals(other.Arrived);
        }

        /// <summary>
        /// Determines whether this <see cref="ShopProductInfo"/> object is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare with this object.</param>
        /// <returns>true if the current object is equal to the other parameter; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            ShopProductInfo shopProductInfoObj = obj as ShopProductInfo;
            if (shopProductInfoObj is null) return false;

            return this.Equals(shopProductInfoObj);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            int hashCode = 1278469092;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Arrived.GetHashCode();
            return hashCode;
        }


        /// <summary>
        /// Converts the value of this instance to a <see cref="string"/> representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A format string.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A <see cref="string"/> representation of the value of this instance.</returns>
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

        /// <summary>
        /// Converts the value of this instance to a <see cref="string"/> representation.
        /// </summary>
        /// <returns>A <see cref="string"/> representation of the value of this instance.</returns>
        public override string ToString()
        {
            return this.ToString("DEFAULT");
        }

        /// <summary>
        /// Returns a string representation of the table header, based on the specified format.
        /// </summary>
        /// <param name="format">The format to use for the header. If null or empty, the default format is used.</param>
        /// <returns>A string representation of the table header.</returns>
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

        /// <summary>
        /// Compares two ShopProductInfo objects for equality.
        /// </summary>
        /// <param name="lhs">The first ShopProductInfo object to compare.</param>
        /// <param name="rhs">The second ShopProductInfo object to compare.</param>
        /// <returns>True if the objects are equal, false otherwise.</returns>
        public static bool operator ==(ShopProductInfo lhs, ShopProductInfo rhs)
        {
            if (lhs as object is null || rhs as object is null)
                return Object.Equals(lhs, rhs);

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Compares two ShopProductInfo objects for inequality.
        /// </summary>
        /// <param name="lhs">The first ShopProductInfo object to compare.</param>
        /// <param name="rhs">The second ShopProductInfo object to compare.</param>
        /// <returns>True if the objects are not equal, false otherwise.</returns>
        public static bool operator !=(ShopProductInfo lhs, ShopProductInfo rhs) => !(lhs == rhs);
    }
}