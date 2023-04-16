namespace LabDarbas3_19.Class
{
    public class TaskUtils
    {
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

        public static LinkList<ShopProductInfo> FindProductsThatExpireIn(LinkList<Shop> linkedShops, int days)
        {
            LinkList<ShopProductInfo> Expires = new LinkList<ShopProductInfo>();
            foreach(Shop shop in linkedShops)
            {
                shop.FindExpires(Expires, days);
            }
            return Expires;
        }

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