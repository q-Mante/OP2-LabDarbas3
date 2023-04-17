using LabDarbas3_19.Class;
using System;
using System.IO;

namespace LabDarbas3_19
{
    public partial class Form : System.Web.UI.Page
    {
        // Declaring constants
        const string CDataStorageName = "App_Data";
        const string CFr = "Results.txt";

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["Table3"] = null;
            Session["Table4"] = null;
            Session["Table5"] = null;
            Session["Shop"] = null;
            Label3.Text = string.Empty;
            Label4.Text = string.Empty;
            Label5.Text = string.Empty;
            Label6.Text = string.Empty;
            SessionLoad();

            // Checks if there are uploaded files
            if (FileUpload1.HasFile && FileUpload2.HasFile)
            {
                // Encodes file names from HTML
                string fileFirstName = Server.HtmlEncode(FileUpload1.FileName);
                string fileSecondName = Server.HtmlEncode(FileUpload2.FileName);

                // Gets file extentions
                string fileFirstExtention = Path.GetExtension(fileFirstName);
                string fileSecodExtention = Path.GetExtension(fileSecondName);

                // Checks if file extentions are correct
                if (fileFirstExtention.Equals(".txt") && fileSecodExtention.Equals(".txt"))
                {
                    // Sets file paths
                    string fileFirstPath = Server.MapPath(CDataStorageName + '/' + fileFirstName);
                    string fileSecondPath = Server.MapPath(CDataStorageName + '/' + fileSecondName);
                    string fileResultPath = Server.MapPath(CDataStorageName + '/' + CFr);

                    // Saves uploaded files
                    FileUpload1.SaveAs(fileFirstPath);
                    FileUpload2.SaveAs(fileSecondPath);

                    // Ensuring Tables integrity
                    Table1.Rows.Clear();
                    Table2.Rows.Clear();

                    // Deleting existing results
                    if (File.Exists(fileResultPath))
                        File.Delete(fileResultPath);

                    // Reading, processing and storing data from source files
                    LinkList<GeneralProductInfo> AllInformations = InOutUtils.ReadInformations(fileSecondPath);
                    LinkList<Shop> AllShops = InOutUtils.ReadShops(fileFirstPath, AllInformations);

                    Session["Table1"] = AllShops;
                    Session["Table2"] = AllInformations;

                    Label1.Text = string.Format("Failo {0} duomenys", fileFirstName);
                    Label2.Text = string.Format("Failo {0} duomenys", fileSecondName);

                    if (AllShops.Count != 0)
                    {
                        // Populating Table1 and showing results at WebInterface
                        AddTableHeaderRow(Table1, "Parduotuvės pavadinimas", "Prekės pavadinimas", "Atvykimo data", "Parduota", "Likutis");
                        foreach (Shop shop in AllShops)
                        {
                            foreach (ShopProductInfo info in shop)
                            {
                                AddTableRow(Table1, shop.Name, info.Name, info.Arrived.ToString("yyyy-MM-dd"), info.Sold.ToString(), info.Stock.ToString());
                            }
                        }

                        // Writting results to file
                        InOutUtils.Print(fileResultPath, "Duomenys: " + "\"" + fileFirstName + "\"", AllShops, "INPUT");
                    }
                    else
                    {
                        // Showing results at WebInterface
                        AddTableHeaderRow(Table1, "Sąrašo nėra");

                        // Writting results to file
                        File.AppendAllText(fileResultPath, "Duomenų nėra.\n\n");
                    }

                    if (AllInformations.Count != 0)
                    {
                        // Populating Table2 and showing results at WebInterface
                        AddTableHeaderRow(Table2, "Prekės pavadinimas", "Galiojimo laikotarpis (d)", "Kaina \u20AC");
                        foreach (GeneralProductInfo info in AllInformations)
                        {
                            AddTableRow(Table2, info.Name, info.Validity.ToString(), info.Price.ToString());
                        }

                        // Writting results to file
                        InOutUtils.Print(fileResultPath, "Duomenys: " + "\"" + fileSecondName + "\"", AllInformations);
                    }
                    else
                    {
                        // Showing results at WebInterface
                        AddTableHeaderRow(Table2, "Sąrašo nėra");

                        // Writting results to file
                        File.AppendAllText(fileResultPath, "Duomenų nėra.\n\n");
                    }
                }
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SessionLoad();

            // Initializing variables
            float MaximumValue = float.Parse(TextBox1.Text);
            string fileResultPath = Server.MapPath(CDataStorageName + '/' + CFr);
            LinkList<Shop> AllShops = (LinkList<Shop>)Session["Table1"];

            // Ensuring Tables integrity
            Table3.Rows.Clear();
            Table4.Rows.Clear();
            Table5.Rows.Clear();

            // Finding favorite products at each shop
            LinkList<GeneralProductInfo> Favorites = TaskUtils.FindFavorites(AllShops);
            Favorites.Sort();

            // Finding products which will expire in the next 30 days at each shop
            LinkList<ShopProductInfo> Expires = TaskUtils.FindProductsThatExpireIn(AllShops, 30);
            Expires.Sort();

            // Finding shop which has most variety of products
            Shop Biggest = TaskUtils.FindShopWithBiggestAssortment(AllShops);

            // Finding shops which values are below specified value
            LinkList<Shop> Shops = TaskUtils.FindShopsThatAreBelowSpecifiedValue(AllShops, MaximumValue);

            Session["Table3"] = Favorites;
            Session["Table4"] = Expires;
            Session["Table5"] = Shops;
            Session["Shop"] = Biggest;

            Label3.Text = "Perkamiausių prekių sąrašas";
            Label4.Text = "Prekių sąrašas, kurių galiojimas netrukus pasibaigs";
            Label5.Text = string.Format("Parduotuvių sąrašas, kurių vertė neviršija {0} \u20AC", MaximumValue);
            Label6.Text = string.Format("Parduotuvė, kuri turi didžiausią prekių asortimentą: {0}", Biggest.Name);

            File.AppendAllText(fileResultPath, string.Format("Pinigų suma: {0}\n\n", MaximumValue));

            if (Favorites.Count != 0)
            {
                // Populating Table3 and showing results at WebInterface
                AddTableHeaderRow(Table3, "Prekės pavadinimas", "Galiojimo laikotarpis (d)", "Kaina \u20AC");
                foreach (GeneralProductInfo info in Favorites)
                {
                    AddTableRow(Table3, info.Name, info.Validity.ToString(), info.Price.ToString());
                }

                // Writting results to file
                InOutUtils.Print(fileResultPath, "Rezultatai: Perkamiausių prekių sąrašas", Favorites);
            }
            else
            {
                // Showing results at WebInterface
                AddTableHeaderRow(Table3, "Sąrašo nėra");

                // Writting results to file
                File.AppendAllText(fileResultPath, "Perkamiausių prekių sąraše nėra.\n\n");
            }

            if (Expires.Count != 0)
            {
                // Populating Table4 and showing results at WebInterface
                AddTableHeaderRow(Table4, "Prekės pavadinimas", "Galiojimo pabaiga", "Kaina \u20AC");
                foreach (ShopProductInfo info in Expires)
                {
                    AddTableRow(Table4, info.Name, info.Expire.ToString("yyyy-MM-dd"), info.ProductInfo.Price.ToString());
                }

                // Writting results to file
                InOutUtils.Print(fileResultPath, "Rezultatai: Baigiantis galioti prekių sąrašas", Expires);
            }
            else
            {
                // Showing results at WebInterface
                AddTableHeaderRow(Table4, "Sąrašo nėra");

                // Writting results to file
                File.AppendAllText(fileResultPath, "Nėra prekių, kurių galiojimo laikotarpis pasibaigs.\n\n");
            }

            if (Biggest.Name != null)
            {
                // Setting Label7 text value and showing results at WebInterface
                //Label7.Text = Biggest.Name;

                // Writting results to file
                File.AppendAllText(fileResultPath, string.Format("Parduotuvė, kuri turi didžiausią prekių asortimentą: {0}.\n\n", Biggest.Name));
            }
            else
            {
                // Showing results at WebInterface
                //Label7.Text = "nėra";

                // Writting results to file
                File.AppendAllText(fileResultPath, "Nėra parduotuvės su didžiausiu prekių asortimentu.\n\n");
            }

            if (Shops.Count != 0)
            {
                // Populating Table5 and showing results at WebInterface
                AddTableHeaderRow(Table5, "Parduotuvės pavadinimas", "Likutis", "Vertė \u20AC");
                foreach (Shop shop in Shops)
                {
                    AddTableRow(Table5, shop.Name, shop.AllStock.ToString(), shop.Value.ToString());
                }

                // Writting results to file
                InOutUtils.Print(fileResultPath, "Rezultatai: Biudžeto parduotuvių sąrašas", Shops);
            }
            else
            {
                // Showing results at WebInterface
                AddTableHeaderRow(Table5, "Sąrašo nėra");

                // Writting results to file
                File.AppendAllText(fileResultPath, string.Format("Nėra parduotuvių, kurių vertė neviršija {0} \u20AC\n\n", MaximumValue));
            }
        }
    }
}