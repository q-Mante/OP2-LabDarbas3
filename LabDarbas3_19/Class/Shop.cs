using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LabDarbas3_19.Class
{
    /// <summary>
    /// Represents a shop and its product information.
    /// </summary>
    public class Shop : IComparable<Shop>, IEquatable<Shop>, IFormattable, ITableGeneric
    {
        /// <summary>
        /// Gets or sets the name of the shop.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the total stock of all products in the shop.
        /// </summary>
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

        /// <summary>
        /// Gets the total value of all products in the shop.
        /// </summary>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Shop"/> class.
        /// </summary>
        public Shop() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shop"/> class with a specified name.
        /// </summary>
        /// <param name="name">The name of the shop.</param>
        public Shop(string name)
        {
            Name = name;
            shopProductInfos = new LinkList<ShopProductInfo>();
        }

        /// <summary>
        /// Gets the favorite product in the shop based on its sales-to-days ratio.
        /// </summary>
        /// <returns>The <see cref="ShopProductInfo"/> of the favorite product.</returns>
        public ShopProductInfo FavoriteProduct()
        {
            ShopProductInfo favorite = new ShopProductInfo();
            float ratio = -1f;

            foreach (ShopProductInfo info in shopProductInfos)
            {
                float selectedRatio = info.Sold / ((DateTime.Now - info.Arrived).Days + 1);
                if (selectedRatio > ratio)
                {
                    favorite = info;
                    ratio = selectedRatio;
                }
            }
            return favorite;
        }

        /// <summary>
        /// Finds all products in the shop that will expire within a certain number of days.
        /// </summary>
        /// <param name="Expires">The <see cref="LinkList{ShopProductInfo}"/> to store the expired products.</param>
        /// <param name="days">The number of days until expiration.</param>
        public void FindExpires(LinkList<ShopProductInfo> Expires, int days)
        {
            foreach (ShopProductInfo info in shopProductInfos)
            {
                if (info.Expire >= DateTime.Now && info.Expire <= DateTime.Now.AddDays(days))
                    Expires.Add(info);
            }
        }

        /// <summary>
        /// Returns an <see cref="IEnumerator"/> for the shop's <see cref="ShopProductInfo"/>s.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> for the shop's <see cref="ShopProductInfo"/>s.</returns>
        public IEnumerator GetEnumerator()
        {
            foreach (ShopProductInfo info in shopProductInfos)
            {
                yield return info;
            }
        }

        /// <summary>
        /// Gets the number of products in the shop.
        /// </summary>
        /// <returns>The number of products in the shop.</returns>
        public int ProductsCount() => shopProductInfos.Count;

        /// <summary>
        /// Adds a product to the shop.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public void ProductsAdd(ShopProductInfo product) => shopProductInfos.Add(product);

        /// <summary>
        /// Sorts the products in the shop.
        /// </summary>
        public void ProductsSort() => shopProductInfos.Sort();

        /// <summary>
        /// Compares this shop to another shop.
        /// </summary>
        /// <param name="other">The other shop to compare to.</param>
        /// <returns>An integer indicating the relative order of the two shops.</returns>
        public int CompareTo(Shop other)
        {
            if (other is null) return 1;

            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Determines whether this shop is equal to another shop.
        /// </summary>
        /// <param name="other">The other shop to compare to.</param>
        /// <returns>True if the shops are equal, otherwise false.</returns>
        public bool Equals(Shop other)
        {
            if (other is null) return false;

            return Name.Equals(other.Name);
        }

        /// <summary>
        /// Determines whether this shop is equal to another object.
        /// </summary>
        /// <param name="obj">The object to compare to.</param>
        /// <returns>True if the object is equal to this shop, otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is null) return false;

            Shop shopObj = obj as Shop;
            if (shopObj is null) return false;

            return this.Equals(shopObj);
        }

        /// <summary>
        /// Gets a hash code for this shop.
        /// </summary>
        /// <returns>An integer hash code.</returns>
        public override int GetHashCode()
        {
            return 539060726 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        /// <summary>
        /// Returns a string representation of this shop.
        /// </summary>
        /// <param name="format">The format of the string.</param>
        /// <param name="provider">The format provider to use.</param>
        /// <returns>A string representation of the shop.</returns>
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

        /// <summary>
        /// Returns a string representation of this shop.
        /// </summary>
        /// <returns>A string representation of the shop.</returns>
        public override string ToString()
        {
            return this.ToString("DEFAULT");
        }

        /// <summary>
        /// Generates the header string for the table with the specified format.
        /// </summary>
        /// <param name="format">The format of the header string.</param>
        /// <returns>The header string for the table with the specified format.</returns>
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

        /// <summary>
        /// Determines whether two Shop objects are equal.
        /// </summary>
        /// <param name="lhs">The first Shop object to compare.</param>
        /// <param name="rhs">The second Shop object to compare.</param>
        /// <returns>true if the two objects are equal; otherwise, false.</returns>
        public static bool operator ==(Shop lhs, Shop rhs)
        {
            if (lhs as object is null || rhs as object is null)
                return Object.Equals(lhs, rhs);

            return lhs.Equals(rhs);
        }

        /// <summary>
        /// Determines whether two Shop objects are not equal.
        /// </summary>
        /// <param name="lhs">The first Shop object to compare.</param>
        /// <param name="rhs">The second Shop object to compare.</param>
        /// <returns>true if the two objects are not equal; otherwise, false.</returns>
        public static bool operator !=(Shop lhs, Shop rhs) => !(lhs == rhs);
    }
}