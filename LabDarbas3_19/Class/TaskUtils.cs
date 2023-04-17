namespace LabDarbas3_19.Class
{
    /// <summary>
    /// Provides utility methods for working with <see cref="Shop"/> objects and collections of shops.
    /// </summary>
    public class TaskUtils
    {
        /// <summary>
        /// Finds the favorite product for each shop in the provided collection of <see cref="Shop"/> objects
        /// and returns a collection of <see cref="GeneralProductInfo"/> objects that represent those favorites.
        /// </summary>
        /// <param name="linkedShops">The collection of shops to search for favorites.</param>
        /// <returns>A collection of <see cref="GeneralProductInfo"/> objects that represent the favorite products from each shop.</returns>
        public static LinkList<GeneralProductInfo> FindFavorites(LinkList<Shop> linkedShops)
        {
            LinkList<GeneralProductInfo> Favorites = new LinkList<GeneralProductInfo>();
            foreach (Shop shop in linkedShops)
            {
                GeneralProductInfo favorite = shop.FavoriteProduct().ProductInfo;
                if (!Favorites.Contains(favorite))
                    Favorites.Add(favorite);
            }
            return Favorites;
        }

        /// <summary>
        /// Finds all products in the provided collection of <see cref="Shop"/> objects that expire within the specified
        /// number of days and returns a collection of <see cref="ShopProductInfo"/> objects that represent those products.
        /// </summary>
        /// <param name="linkedShops">The collection of shops to search for products that expire within the specified number of days.</param>
        /// <param name="days">The number of days within which products must expire in order to be included in the returned collection.</param>
        /// <returns>A collection of <see cref="ShopProductInfo"/> objects that represent the products that expire within the specified number of days.</returns>
        public static LinkList<ShopProductInfo> FindProductsThatExpireIn(LinkList<Shop> linkedShops, int days)
        {
            LinkList<ShopProductInfo> Expires = new LinkList<ShopProductInfo>();
            foreach(Shop shop in linkedShops)
            {
                shop.FindExpires(Expires, days);
            }
            return Expires;
        }

        /// <summary>
        /// Finds the shop in the provided collection of <see cref="Shop"/> objects that has the greatest number of products,
        /// and returns that shop.
        /// </summary>
        /// <param name="linkedShops">The collection of shops to search for the one with the most products.</param>
        /// <returns>The shop from the provided collection with the greatest number of products.</returns>
        public static Shop FindShopWithBiggestAssortment(LinkList<Shop> linkedShops)
        {
            Shop Biggest = new Shop();
            int count = -1;

            foreach (Shop shop in linkedShops)
            {
                if (shop.ProductsCount() > count)
                {
                    Biggest = shop;
                    count = shop.ProductsCount();
                }
            }
            return Biggest;
        }

        /// <summary>
        /// Finds all shops in the specified linked list whose value is less than or equal to the specified maximum value.
        /// </summary>
        /// <param name="linkedShops">The linked list of shops to search through.</param>
        /// <param name="maximumValue">The maximum value that a shop can have to be included in the result.</param>
        /// <returns>A new linked list containing all the shops with value less than or equal to the specified maximum value.</returns>
        public static LinkList<Shop> FindShopsThatAreBelowSpecifiedValue(LinkList<Shop> linkedShops, float maximumValue)
        {
            LinkList<Shop> Shops = new LinkList<Shop>();
            foreach (Shop shop in linkedShops)
            {
                if (shop.Value <= maximumValue)
                {
                    Shops.Add(shop);
                }
            }
            return Shops;
        }
    }
}