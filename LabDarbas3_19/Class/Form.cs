using LabDarbas3_19.Class;
using System.Web.UI.WebControls;

namespace LabDarbas3_19
{
    public partial class Form : System.Web.UI.Page
    {
        protected void AddTableHeaderRow(Table table, params string[] cellsHeaders)
        {
            TableHeaderRow hRow = new TableHeaderRow();
            for (int i = 0; i < cellsHeaders.Length; i++)
            {
                TableHeaderCell hCell = new TableHeaderCell { Text = cellsHeaders[i] };
                hRow.Cells.Add(hCell);
            }
            table.Rows.Add(hRow);
        }

        protected void AddTableRow(Table table, params string[] cells)
        {
            TableHeaderRow row = new TableHeaderRow();
            for (int i = 0; i < cells.Length; i++)
            {
                TableCell cell = new TableCell { Text = cells[i] };
                row.Cells.Add(cell);
            }
            table.Rows.Add(row);
        }

        protected void SessionLoad()
        {
            if (Session["Table1"] != null)
            {
                LinkList<Shop> sessionTable1 = (LinkList<Shop>)Session["Table1"];

                Table1.Rows.Clear();
                if (sessionTable1.Count != 0)
                {
                    AddTableHeaderRow(Table1, "Parduotuvės pavadinimas", "Prekės pavadinimas", "Atvykimo data", "Parduota", "Likutis");
                    foreach (Shop shop in sessionTable1)
                    {
                        foreach (ShopProductInfo info in shop)
                        {
                            AddTableRow(Table1, shop.Name, info.Name, info.Arrived.ToString("yyyy-MM-dd"), info.Sold.ToString(), info.Stock.ToString());
                        }
                    }
                }
                else
                {
                    AddTableHeaderRow(Table1, "Sąrašo nėra");
                }
            }

            if (Session["Table2"] != null)
            {
                LinkList<GeneralProductInfo> sessionTable2 = (LinkList<GeneralProductInfo>)Session["Table2"];

                Table2.Rows.Clear();
                if (sessionTable2.Count != 0)
                {
                    AddTableHeaderRow(Table2, "Prekės pavadinimas", "Galiojimo laikotarpis (d)", "Kaina \u20AC");
                    foreach (GeneralProductInfo info in sessionTable2)
                    {
                        AddTableRow(Table2, info.Name, info.Validity.ToString(), info.Price.ToString());
                    }
                }
                else
                {
                    AddTableHeaderRow(Table2, "Sąrašo nėra");
                }
            }

            if (Session["Table3"] != null)
            {
                LinkList<GeneralProductInfo> sessionTable3 = (LinkList<GeneralProductInfo>)Session["Table3"];

                Table3.Rows.Clear();
                if (sessionTable3.Count != 0)
                {
                    AddTableHeaderRow(Table3, "Prekės pavadinimas", "Galiojimo laikotarpis (d)", "Kaina \u20AC");
                    foreach (GeneralProductInfo info in sessionTable3)
                    {
                        AddTableRow(Table3, info.Name, info.Validity.ToString(), info.Price.ToString());
                    }
                }
                else
                {
                    AddTableHeaderRow(Table3, "Sąrašo nėra");
                }
            }

            if (Session["Table4"] != null)
            {
                LinkList<ShopProductInfo> sessionTable4 = (LinkList<ShopProductInfo>)Session["Table4"];

                Table4.Rows.Clear();
                if (sessionTable4.Count != 0)
                {
                    AddTableHeaderRow(Table4, "Prekės pavadinimas", "Galiojimo pabaiga", "Kaina \u20AC");
                    foreach (ShopProductInfo info in sessionTable4)
                    {
                        AddTableRow(Table4, info.Name, info.Expire.ToString("yyyy-MM-dd"), info.ProductInfo.Price.ToString());
                    }
                }
                else
                {
                    AddTableHeaderRow(Table4, "Sąrašo nėra");
                }
            }

            if (Session["Shop"] != null)
            {
                Shop shop = (Shop)Session["Shop"];

                if (shop.Name != null)
                {
                    //Label7.Text = shop.Name;
                }
                else
                {
                    //Label7.Text = "nėra";
                }
            }

            if (Session["Table5"] != null)
            {
                LinkList<Shop> sessionTable5 = (LinkList<Shop>)Session["Table5"];

                Table5.Rows.Clear();
                if (sessionTable5.Count != 0)
                {
                    AddTableHeaderRow(Table5, "Parduotuvės pavadinimas", "Likutis", "Vertė \u20AC");
                    foreach (Shop shop in sessionTable5)
                    {
                        AddTableRow(Table5, shop.Name, shop.AllStock.ToString(), shop.Value.ToString());
                    }
                }
                else
                {
                    AddTableHeaderRow(Table5, "Sąrašo nėra");
                }
            }
        }
    }
}