using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using System.IO.IsolatedStorage;
using System.Windows;
using Microsoft.Phone.Shell;
using System.Runtime.CompilerServices;

using Windows.ApplicationModel.Store;
//#if DEBUG
//using MockIAPLib;
//#else
//using Windows.ApplicationModel.Store;
//#endif


namespace FinancialManagerPhoneProject.DataHandlers
{
    public class XMLHandler
    {
        public static double DEIVCE_WIDTH;
        public static XDocument FINANCIALMANAGER_XML;

        public async Task LoadDefaultXmlAsync()
        {
            Task loadDefaultXml =
                  Task.Factory.StartNew(() =>
                  {
                      XDocument xml = new XDocument
                      (
                          new XElement("FinancialManager",

                              new XElement("StaticValues", new XAttribute("Income", "3600"),
                                                           new XAttribute("Currency", "$"),
                                                           new XAttribute("IsDefaultData", "1")),
                              new XElement("Expenses", new XElement("Expense", new XAttribute("ID", 1),
                                                                              new XAttribute("Category", "Mortgage"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Monthly Mortgage"),
                                                                              new XAttribute("Date", "01/05/2013"),
                                                                              1350),
                                                       new XElement("Expense", new XAttribute("ID", 22),
                                                                              new XAttribute("Category", "Bills"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Electricity Bill"),
                                                                              new XAttribute("Date", "03/05/2013"),
                                                                              180),
                                                       new XElement("Expense", new XAttribute("ID", 23),
                                                                              new XAttribute("Category", "Bills"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Gas Bill"),
                                                                              new XAttribute("Date", "04/05/2013"),
                                                                              100),
                                                       new XElement("Expense", new XAttribute("ID", 24),
                                                                              new XAttribute("Category", "Bills"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Water Bill"),
                                                                              new XAttribute("Date", "06/05/2013"),
                                                                              20),
                                                       new XElement("Expense", new XAttribute("ID", 2),
                                                                              new XAttribute("Category", "Car"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Changing Motor Oil"),
                                                                              new XAttribute("Date", "07/05/2013"),
                                                                              90),
                                                       new XElement("Expense", new XAttribute("ID", 20),
                                                                              new XAttribute("Category", "Groceries"),
                                                                              new XAttribute("Description", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Date", "07/05/2013"),
                                                                              89),
                                                       new XElement("Expense", new XAttribute("ID", 21),
                                                                              new XAttribute("Category", "Groceries"),
                                                                              new XAttribute("Description", ""),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Date", "09/05/2013"),
                                                                              35),
                                                       new XElement("Expense", new XAttribute("ID", 4),
                                                                              new XAttribute("Category", "Groceries"),
                                                                              new XAttribute("Description", ""),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Date", "12/05/2013"),
                                                                              43),
                                                       new XElement("Expense", new XAttribute("ID", 6),
                                                                              new XAttribute("Category", "Insurances"),
                                                                              new XAttribute("Description", "Car Insurance"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Date", "12/05/2013"),
                                                                              120),
                                                       new XElement("Expense", new XAttribute("ID", 7),
                                                                              new XAttribute("Category", "Commute"),
                                                                              new XAttribute("Description", "Monthly train ticket top up"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Date", "15/05/2013"),
                                                                              128),
                                                       new XElement("Expense", new XAttribute("ID", 8),
                                                                              new XAttribute("Category", "Children"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Alex monthly pocket money"),
                                                                              new XAttribute("Date", "18/05/2013"),
                                                                              70),
                                                       new XElement("Expense", new XAttribute("ID", 9),
                                                                              new XAttribute("Category", "Accessories"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "A headphone at ebay"),
                                                                              new XAttribute("Date", "20/05/2013"),
                                                                              160),
                                                       new XElement("Expense", new XAttribute("ID", 10),
                                                                              new XAttribute("Category", "Car"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Gas"),
                                                                              new XAttribute("Date", "22/05/2013"),
                                                                              50),
                                                       new XElement("Expense", new XAttribute("ID", 11),
                                                                              new XAttribute("Category", "Clothes"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "New Red Coat"),
                                                                              new XAttribute("Date", "06/05/2013"),
                                                                              38),
                                                       new XElement("Expense", new XAttribute("ID", 12),
                                                                              new XAttribute("Category", "Clothes"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "New Bag"),
                                                                              new XAttribute("Date", "18/05/2013"),
                                                                              30),
                                                       new XElement("Expense", new XAttribute("ID", 13),
                                                                              new XAttribute("Category", "Clothes"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "New Sweater"),
                                                                              new XAttribute("Date", "25/05/2013"),
                                                                              35),
                                                       new XElement("Expense", new XAttribute("ID", 14),
                                                                              new XAttribute("Category", "Game"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Buying a pack on hearthstone"),
                                                                              new XAttribute("Date", "12/05/2013"),
                                                                              50),
                                                       new XElement("Expense", new XAttribute("ID", 15),
                                                                              new XAttribute("Category", "Game"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Diablo 3"),
                                                                              new XAttribute("Date", "28/05/2013"),
                                                                              39),
                                                       new XElement("Expense", new XAttribute("ID", 16),
                                                                              new XAttribute("Category", "Pets"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Dog food"),
                                                                              new XAttribute("Date", "02/05/2013"),
                                                                              25),
                                                       new XElement("Expense", new XAttribute("ID", 17),
                                                                              new XAttribute("Category", "Pets"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Dog Shampoo"),
                                                                              new XAttribute("Date", "22/05/2013"),
                                                                              15),
                                                       new XElement("Expense", new XAttribute("ID", 18),
                                                                              new XAttribute("Category", "Pets"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Dog food"),
                                                                              new XAttribute("Date", "29/05/2013"),
                                                                              25),
                                                       new XElement("Expense", new XAttribute("ID", 19),
                                                                              new XAttribute("Category", "Liquor"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Beer"),
                                                                              new XAttribute("Date", "09/05/2013"),
                                                                              30),
                                                       new XElement("Expense", new XAttribute("ID", 5),
                                                                              new XAttribute("Category", "Liquor"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Wine"),
                                                                              new XAttribute("Date", "19/05/2013"),
                                                                              12),
                                                       new XElement("Expense", new XAttribute("ID", 3),
                                                                              new XAttribute("Category", "Childcare"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Weekly Payment"),
                                                                              new XAttribute("Date", "15/05/2013"),
                                                                              150)),
                              new XElement("Categories", new XElement("Category", new XAttribute("Name", "Mortgage"),
                                                                              new XAttribute("Plan", 1350),
                                                                              new XAttribute("Icon", "Mortgage"),
                                                                              new XAttribute("TotalExpenses", 1350)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Car"),
                                                                              new XAttribute("Plan", 200),
                                                                              new XAttribute("Icon", "auto"),
                                                                              new XAttribute("TotalExpenses", 140)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Childcare"),
                                                                              new XAttribute("Plan", 200),
                                                                              new XAttribute("Icon", "other_31"),
                                                                              new XAttribute("TotalExpenses", 150)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Groceries"),
                                                                              new XAttribute("Plan", 150),
                                                                              new XAttribute("Icon", "groceris"),
                                                                              new XAttribute("TotalExpenses", 167)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Insurances"),
                                                                              new XAttribute("Plan", 150),
                                                                              new XAttribute("Icon", "installments"),
                                                                              new XAttribute("TotalExpenses", 120)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Commute"),
                                                                              new XAttribute("Plan", 128),
                                                                              new XAttribute("Icon", "travel_bus"),
                                                                              new XAttribute("TotalExpenses", 128)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Children"),
                                                                              new XAttribute("Plan", 80),
                                                                              new XAttribute("Icon", "other_39"),
                                                                              new XAttribute("TotalExpenses", 70)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Accessories"),
                                                                              new XAttribute("Plan", 100),
                                                                              new XAttribute("Icon", "accessories"),
                                                                              new XAttribute("TotalExpenses", 160)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Clothes"),
                                                                              new XAttribute("Plan", 180),
                                                                              new XAttribute("Icon", "clothing"),
                                                                              new XAttribute("TotalExpenses", 103)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Gift"),
                                                                              new XAttribute("Plan", 50),
                                                                              new XAttribute("Icon", "gifts"),
                                                                              new XAttribute("TotalExpenses", 0)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Game"),
                                                                              new XAttribute("Plan", 100),
                                                                              new XAttribute("Icon", "input_gaming"),
                                                                              new XAttribute("TotalExpenses", 89)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Pets"),
                                                                              new XAttribute("Plan", 100),
                                                                              new XAttribute("Icon", "pets"),
                                                                              new XAttribute("TotalExpenses", 65)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Liquor"),
                                                                              new XAttribute("Plan", 50),
                                                                              new XAttribute("Icon", "wine"),
                                                                              new XAttribute("TotalExpenses", 42)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Bills"),
                                                                              new XAttribute("Plan", 250),
                                                                              new XAttribute("Icon", "invoice2"),
                                                                              new XAttribute("TotalExpenses", 300)
                                                                              ))

                          )
                      );

                      FINANCIALMANAGER_XML = xml;
                  });
            await CreateReceiptsFolderAsync();
            await loadDefaultXml;
        }
        public async Task SaveXmlToFileAsync()
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var xmlFile = await localFolder.CreateFileAsync("FinancialManagerXML.xml", CreationCollisionOption.ReplaceExisting);

            var ReceiptFolder = await localFolder.GetFolderAsync("Receipts");
            if (ReceiptFolder == null)
            {
                await localFolder.CreateFolderAsync("Receipts");
            }

            using (var stream = await xmlFile.OpenStreamForWriteAsync())
            {
                using (var writer = new StreamWriter(stream))
                {
                    FINANCIALMANAGER_XML.Save(writer);
                }
            }
        }
        public async Task LoadXmlFromFileAsync()
        {
            bool IsXmlExist = true;
            try
            {

                Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                var file = await localFolder.GetFileAsync("FinancialManagerXML.xml");
                var stream = await file.OpenStreamForReadAsync();
                FINANCIALMANAGER_XML = XDocument.Load(stream);
            }
            catch
            {
                IsXmlExist = false;
            }

            if (!IsXmlExist)
            {
                await LoadDefaultXmlAsync();
                await SaveXmlToFileAsync();
            }
        }
        
        public async Task CreateReceiptsFolderAsync()
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            await localFolder.CreateFolderAsync("Receipts");           
        }
        public async Task DeleteReceiptsFolderAsync()
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

            var ReceiptFolder = await localFolder.GetFolderAsync("Receipts");
            if (ReceiptFolder != null)
            {
                await ReceiptFolder.DeleteAsync();
                await CreateReceiptsFolderAsync();
            }
            
        }        

        public string GetCurrencySymbol()
        {
            string currency;
            try
            {
                currency = FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Currency").Value;
            }
            catch { currency = "$"; }

            if (currency == "&#x20b9;")
                return "₹";
            return currency;
        }
        public double GetIncome()
        {
            return Convert.ToDouble(FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Income").Value);
        }
        public bool IsDefaultData()
        {
            string temp = FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("IsDefaultData").Value;
            return temp == "1" ? true : false;
        }
        

        public void UpdateSettings(string income, string symbol) 
        {
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Currency").SetValue(symbol);
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Income").SetValue(income);
            SaveXmlToFileAsync();
        }

        public List<string> GetIcons(string initPath)
        {
            return new List<string>(){
                                        initPath+"60100_train"+".png",
                                        initPath+"accessories"+".png",
                                        initPath+"auto"+".png",
                                        initPath+"beer_corona"+".png",
                                        initPath+"borsalino_blanc"+".png",
                                        initPath+"bra"+".png",
                                        initPath+"cash_register"+".png",
                                        initPath+"chips"+".png",
                                        initPath+"christmas_tree"+".png",
                                        initPath+"clothing"+".png",
                                        initPath+"condom"+".png",
                                        initPath+"credit_cards"+".png",
                                        initPath+"dice"+".png",
                                        initPath+"eating_out"+".png",
                                        initPath+"education"+".png",
                                        initPath+"engineer_avatar"+".png",
                                        initPath+"family"+".png",
                                        initPath+"game"+".png",
                                        initPath+"garden_shears"+".png",
                                        initPath+"gas"+".png",
                                        initPath+"gas_station"+".png",
                                        initPath+"gas2"+".png",
                                        initPath+"gifts"+".png",
                                        initPath+"groceris"+".png",
                                        initPath+"gucci_glasses"+".png",
                                        initPath+"hair_dryer"+".png",
                                        initPath+"home"+".png",
                                        initPath+"icontexto_green_01"+".png",
                                        initPath+"input_gaming"+".png",
                                        initPath+"installments"+".png",
                                        initPath+"invoice"+".png",
                                        initPath+"invoice2"+".png",
                                        initPath+"iphone_off"+".png",
                                        initPath+"laptop"+".png",
                                        initPath+"lip_stick_icon"+".png",
                                        initPath+"medical"+".png",
                                        initPath+"modem2"+".png",
                                        initPath+"Mortgage"+".png",
                                        initPath+"other_1"+".png",
                                        initPath+"other_10"+".png",
                                        initPath+"other_11"+".png",
                                        initPath+"other_12"+".png",
                                        initPath+"other_13"+".png",
                                        initPath+"other_14"+".png",
                                        initPath+"other_15"+".png",
                                        initPath+"other_16"+".png",
                                        initPath+"other_17"+".png",
                                        initPath+"other_18"+".png",
                                        initPath+"other_19"+".png",
                                        initPath+"other_2"+".png",
                                        initPath+"other_20"+".png",
                                        initPath+"other_21"+".png",
                                        initPath+"other_22"+".png",
                                        initPath+"other_23"+".png",
                                        initPath+"other_24"+".png",
                                        initPath+"other_25"+".png",
                                        initPath+"other_26"+".png",
                                        initPath+"other_27"+".png",
                                        initPath+"other_28"+".png",
                                        initPath+"other_29"+".png",
                                        initPath+"other_3"+".png",
                                        initPath+"other_30"+".png",
                                        initPath+"other_31"+".png",
                                        initPath+"other_32"+".png",
                                        initPath+"other_33"+".png",
                                        initPath+"other_34"+".png",
                                        initPath+"other_35"+".png",
                                        initPath+"other_36"+".png",
                                        initPath+"other_37"+".png",
                                        initPath+"other_38"+".png",
                                        initPath+"other_39"+".png",
                                        initPath+"other_40"+".png",
                                        initPath+"other_5"+".png",
                                        initPath+"other_6"+".png",
                                        initPath+"other_7"+".png",
                                        initPath+"other_8"+".png",
                                        initPath+"other_9"+".png",
                                        initPath+"package_games"+".png",
                                        initPath+"pets"+".png",
                                        initPath+"phone_bill"+".png",
                                        initPath+"plasma_tv"+".png",
                                        initPath+"plug_electricity"+".png",
                                        initPath+"santa_hat"+".png",
                                        initPath+"saving"+".png",
                                        initPath+"tap_water"+".png",
                                        initPath+"taxi"+".png",
                                        initPath+"toys"+".png",
                                        initPath+"tram2"+".png",
                                        initPath+"travel_bus"+".png",
                                        initPath+"utilities"+".png",
                                        initPath+"vacation"+".png",
                                        initPath+"wine"+".png"
                                     };
        }

        public void SaveImageAsync(byte[] imageAsByte, string ImageName)
        {
            Task saveImage = Task.Factory.StartNew(() =>
            {
                if (imageAsByte != null)
                {                    
                    using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        using (var stream = store.CreateFile(System.IO.Path.Combine("Receipts/"+ImageName)))
                        {
                            stream.Write(imageAsByte, 0, imageAsByte.Length);
                        }
                    }
                }
            });
        }

        public void UpdateTileValues()
        {
            ShellTile TileToFind = ShellTile.ActiveTiles.FirstOrDefault(x => x.NavigationUri.ToString().Contains("DefaultTitle=FinancialManagerFromTile"));

            if (TileToFind != null)
            {
                double expenses = StaticValues.DB.GetTotalExpenses();
                double remaining = StaticValues.DB.GetIncome() - expenses;
                string symbol = StaticValues.DB.GetCurrencySymbol();               

                StandardTileData NewTileData = new StandardTileData
                {
                    BackgroundImage = new Uri("Assets/150_150_Logo.png", UriKind.Relative),
                    Title = "Financial Manager",
                    BackContent = "Expenses:\n" + expenses.ToString("n0") + " " + symbol + "\nRemaining:\n" +
                                  remaining.ToString("n0") + " " + symbol,
                    BackBackgroundImage = new Uri("Black.jpg", UriKind.Relative)
                };

                TileToFind.Update(NewTileData);
            }
        }

        public void DeleteAll()
        {
            FINANCIALMANAGER_XML.Root.Element("Expenses").DescendantNodes().Remove();
            FINANCIALMANAGER_XML.Root.Element("Categories").DescendantNodes().Remove();
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("IsDefaultData").SetValue("0");
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Currency").SetValue("$");
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Income").SetValue("3000");
            FINANCIALMANAGER_XML.Root.Element("Categories").Add(new XElement("Category", new XAttribute("Name", "Rent"),
                                                                                         new XAttribute("Plan", 1260),
                                                                                         new XAttribute("Icon", "Mortgage"),
                                                                                         new XAttribute("TotalExpenses", 0)
                                                                              ));
            FINANCIALMANAGER_XML.Root.Element("Categories").Add(new XElement("Category", new XAttribute("Name", "Car"),
                                                                                         new XAttribute("Plan", 100),
                                                                                         new XAttribute("Icon", "auto"),
                                                                                         new XAttribute("TotalExpenses", 0)
                                                                                         ));
            FINANCIALMANAGER_XML.Root.Element("Categories").Add(new XElement("Category", new XAttribute("Name", "Phone Bills"),
                                                                                         new XAttribute("Plan", 80),
                                                                                         new XAttribute("Icon", "iphone_off"),
                                                                                         new XAttribute("TotalExpenses", 0)
                                                                                         ));
            FINANCIALMANAGER_XML.Root.Element("Categories").Add(new XElement("Category", new XAttribute("Name", "Bills"),
                                                                                         new XAttribute("Plan", 600),
                                                                                         new XAttribute("Icon", "invoice2"),
                                                                                         new XAttribute("TotalExpenses", 0)
                                                                                         ));
            FINANCIALMANAGER_XML.Root.Element("Categories").Add(new XElement("Category", new XAttribute("Name", "Groceries"),
                                                                                         new XAttribute("Plan", 150),
                                                                                         new XAttribute("Icon", "groceris"),
                                                                                         new XAttribute("TotalExpenses", 0)
                                                                                         ));

            DeleteAllStorage();                                                                   
        }
        public void DeleteAllStorage()
        {
            Task deleteStorage = Task.Factory.StartNew(async() =>
            {
                await DeleteReceiptsFolderAsync();
                SaveXmlToFileAsync();
            });             
        }

        #region Store Manager
        public bool IsValidToSave()
        {
            if (!StaticValues.DB.IsDefaultData())
            {
                if (StaticValues.DB.IsUltimateUser())
                {
                    return true;
                }
                else
                {
                    int expenseCount = FINANCIALMANAGER_XML.Root.Element("Expenses").Descendants().Count();
                    if (expenseCount <= 1)
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                return true;
            }
        }
        public bool IsUltimateUser()
        {
            //if (FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("FullAccess").Value == "1")
            if (CurrentApp.LicenseInformation.ProductLicenses["11111"].IsActive)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> BuyUltimateUser()
        {
            var listing = await CurrentApp.LoadListingInformationAsync();
            try
            {
                if (CurrentApp.LicenseInformation.ProductLicenses["11111"].IsActive)
                {
                    //FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("FullAccess").SetValue("1");
                    return true;
                }
                else
                {
                    string receipt = await CurrentApp.RequestProductPurchaseAsync("11111", true);
                    //FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("FullAccess").SetValue("1");
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        #endregion

        #region Expense

        public List<Expense> GetAllExpenses()
        {
            List<Expense> expenses = new List<Expense>();

            var expensesXml = from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                              select x;

            var categoriesXml = from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                select x;

            foreach (var node in expensesXml)
            {
                DateTime date = Convert.ToDateTime(node.Attribute("Date").Value.ToString());

                expenses.Add(new Expense()
                             {
                                 ID = node.Attribute("ID").Value,
                                 Category = node.Attribute("Category").Value,
                                 Date = date,
                                 RecieptName = node.Attribute("RecieptName").Value,
                                 Longtitude = node.Attribute("Longtitude").Value,
                                 Latitude = node.Attribute("Latitude").Value,
                                 Description = node.Attribute("Description").Value,
                                 Value = Convert.ToDouble(node.Value.ToString()),
                                 Icon = categoriesXml
                                            .Where(c => c.Attribute("Name").Value == node.Attribute("Category").Value)
                                            .FirstOrDefault()
                                            .Attribute("Icon")
                                            .Value
                             });
            }
            return expenses.OrderByDescending(e => e.Date).ToList();
        }

        public List<Expense> GetAllExpenses(string CategoryName)
        {
            List<Expense> expenses = new List<Expense>();
            var expensesXml = from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                              where x.Attribute("Category").Value == CategoryName
                              select x;

            var categoryXml = (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                               where x.Attribute("Name").Value == CategoryName
                               select x).FirstOrDefault();

            foreach (var node in expensesXml)
            {
                DateTime date = Convert.ToDateTime(node.Attribute("Date").Value.ToString());

                expenses.Add(new Expense()
                {
                    ID = node.Attribute("ID").Value,
                    Category = node.Attribute("Category").Value,
                    Date = date,
                    RecieptName = node.Attribute("RecieptName").Value,
                    Longtitude = node.Attribute("Longtitude").Value,
                    Latitude = node.Attribute("Latitude").Value,
                    Description = node.Attribute("Description").Value,
                    Value = Convert.ToDouble(node.Value.ToString()),
                    Icon = categoryXml.Attribute("Icon").Value
                });
            }
            return expenses.OrderBy(e => e.Date).ToList();
        }

        public double GetTotalExpenses()
        {
            return (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                    select x).Sum(e => Convert.ToDouble(e.Value));
        }

        public double GetTotalExpenses(string CategoryName)
        {
            return (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                    where x.Attribute("Category").Value == CategoryName
                    select x).Sum(e => Convert.ToDouble(e.Value));
        }

        public void AddExpense(Expense expense)
        {
            XElement newExpense = new XElement("Expense",
                                                new XAttribute("ID", StaticMethods.GenerateID()),
                                                new XAttribute("Category", expense.Category),
                                                new XAttribute("RecieptName", expense.RecieptName),
                                                new XAttribute("Longtitude", expense.Longtitude),
                                                new XAttribute("Latitude", expense.Latitude),
                                                new XAttribute("Description", expense.Description),
                                                new XAttribute("Date", expense.Date.ToString()),
                                                expense.Value);
            FINANCIALMANAGER_XML.Root.Element("Expenses").Add(newExpense);

            //updating TotalExpenses on the related category
            var attribute = FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                .Where(c => c.Attribute("Name").Value == expense.Category)
                                .FirstOrDefault().Attribute("TotalExpenses");
            attribute.Value = (Convert.ToDouble(attribute.Value) + expense.Value).ToString();

            UpdateTileValues();

            SaveXmlToFileAsync();
                      
        }
        

        public void DeleteExpense(string ID)
        {
            var expenseXML = (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                              where x.Attribute("ID").Value == ID
                              select x).FirstOrDefault();
            expenseXML.Remove();

            var xmlCategory = (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                               where x.Attribute("Name").Value == expenseXML.Attribute("Category").Value
                               select x).FirstOrDefault();
            double totalExpense = Convert.ToDouble(xmlCategory.Attribute("TotalExpenses").Value);
            totalExpense -= Convert.ToDouble(expenseXML.Value);
            xmlCategory.Attribute("TotalExpenses").SetValue(totalExpense);

            // deleting reciept if exists
            try
            {
                string recieptImage = expenseXML.Attribute("RecieptName").Value;
                if (!string.IsNullOrEmpty(recieptImage))
                {                    
                    using (IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication())
                    {
                        iso.DeleteFile(recieptImage);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
                /* It means there is no image. ignore it */
            }

            UpdateTileValues();

            SaveXmlToFileAsync();
        }

        public void UpdateExpense(Expense expense)
        {
            var xmlExpense = (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                              where x.Attribute("ID").Value == expense.ID
                              select x).FirstOrDefault();

            if (expense.Value.ToString() != xmlExpense.Value)
            {
                var xmlCategory = (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                   where x.Attribute("Name").Value == expense.Category
                                   select x).FirstOrDefault();

                double totalExpense = Convert.ToDouble(xmlCategory.Attribute("TotalExpenses").Value);
                double oldExpense = Convert.ToDouble(xmlExpense.Value);
                double newExpense = expense.Value;
                totalExpense -= oldExpense;
                totalExpense += newExpense;

                xmlCategory.Attribute("TotalExpenses").SetValue(totalExpense);
            }

            xmlExpense.Attribute("Description").SetValue(expense.Description);
            xmlExpense.Attribute("RecieptName").SetValue(expense.RecieptName);
            xmlExpense.SetValue(expense.Value);
            //xmlExpense.Attribute("Date").SetValue(expense.Date.Day + "/" + expense.Date.Month + "/" + expense.Date.Year);
            xmlExpense.Attribute("Date").SetValue(expense.Date.ToString());

            UpdateTileValues();

            SaveXmlToFileAsync();
        }

        public Expense GetExpense(string ID)
        {
            var xmlExpense = (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                              where x.Attribute("ID").Value == ID
                              select x).FirstOrDefault();
            return new Expense()
            {
                Category = xmlExpense.Attribute("Category").Value,
                Date = Convert.ToDateTime(xmlExpense.Attribute("Date").Value.ToString()),
                Description = xmlExpense.Attribute("Description").Value,
                RecieptName = xmlExpense.Attribute("RecieptName").Value,
                Latitude = xmlExpense.Attribute("Latitude").Value,
                Longtitude = xmlExpense.Attribute("Longtitude").Value,
                ID = ID,
                Value = Convert.ToDouble(xmlExpense.Value.ToString())
            };
        }

        public string GetCategoryName(string ExpenseID)
        {
            string categoryName = (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                                   where x.Attribute("ID").Value == ExpenseID
                                   select x).FirstOrDefault().Attribute("Category").Value;
            return categoryName;
        }

        #endregion

        #region Category

        public List<Category> GetAllCategories()
        {
            List<Category> categories = new List<Category>();
            var categoriesXml = from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                select x;

            foreach (var node in categoriesXml)
            {
                categories.Add(new Category()
                {
                    Icon = node.Attribute("Icon").Value.ToString(),
                    Name = node.Attribute("Name").Value.ToString(),
                    Plan = Convert.ToDouble(node.Attribute("Plan").Value.ToString()),
                    TotalExpenses = Convert.ToDouble(node.Attribute("TotalExpenses").Value.ToString())
                });
            }
            return categories;
        }

        public List<Category> GetTopCategories()
        {
            List<Category> categories = new List<Category>();
            var categoriesXml = from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                select x;

            foreach (var node in categoriesXml)
            {
                categories.Add(new Category()
                {
                    Icon = node.Attribute("Icon").Value.ToString(),
                    Name = node.Attribute("Name").Value.ToString(),
                    Plan = Convert.ToDouble(node.Attribute("Plan").Value.ToString()),
                    TotalExpenses = Convert.ToDouble(node.Attribute("TotalExpenses").Value.ToString())
                });
            }
            return categories.OrderByDescending(c=>c.TotalExpenses).Take(5).ToList();
        }

        public double GetTotalPlan()
        {
            return (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                    select x).Sum(c => Convert.ToDouble(c.Attribute("Plan").Value));
        }

        public bool AddCategory(Category category)
        {
            var xmlCategories = FINANCIALMANAGER_XML.Root.Element("Categories").Elements();

            bool IsExisted = false;
            foreach (var xmlCategory in xmlCategories)
            {
                if (xmlCategory.Attribute("Name").Value.ToString() == category.Name)
                {
                    IsExisted = true;
                }
            }
            if (IsExisted)
                return false;

            XElement newCategory = new XElement("Category",
                                                new XAttribute("Name", category.Name),
                                                new XAttribute("Plan", category.Plan),
                                                new XAttribute("Icon", category.Icon),
                                                new XAttribute("TotalExpenses", 0));

            FINANCIALMANAGER_XML.Root.Element("Categories").Add(newCategory);

            SaveXmlToFileAsync();
            return true;
        }

        public void DeleteCategory(Category category)
        {
            (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
             where x.Attribute("Category").Value == category.Name
             select x).Remove();

            (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
             where x.Attribute("Name").Value == category.Name
             select x).FirstOrDefault().Remove();

            UpdateTileValues();
            SaveXmlToFileAsync();
        }

        public void DeleteCategory(string category)
        {
            (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
             where x.Attribute("Category").Value == category
             select x).Remove();

            (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
             where x.Attribute("Name").Value == category
             select x).FirstOrDefault().Remove();
            
            UpdateTileValues();
            SaveXmlToFileAsync();
        }

        public void UpdateCategory(Category category)
        {
            var xmlCategory = (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                               where x.Attribute("Name").Value == category.Name
                               select x).FirstOrDefault();

            if (category.NewName != xmlCategory.Attribute("Name").Value.ToString())
            {
                var expensesXml = from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                                  where x.Attribute("Category").Value == category.Name
                                  select x;
                foreach (var node in expensesXml)
                {
                    node.SetAttributeValue("Category", category.NewName);
                }

                xmlCategory.SetAttributeValue("Name", category.NewName);
            }

            if (category.Icon != xmlCategory.Attribute("Icon").Value.ToString())
            {
                xmlCategory.SetAttributeValue("Icon", category.Icon);
            }

            if (category.Plan != Convert.ToDouble(xmlCategory.Attribute("Plan").Value.ToString()))
            {
                xmlCategory.SetAttributeValue("Plan", category.Plan);
            }

            SaveXmlToFileAsync();
        }

        public Category GetCategoryObject(string CategoryName)
        {
            var xmlCategory = (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                               where x.Attribute("Name").Value == CategoryName
                               select x).FirstOrDefault();
            return new Category() { 
                                    Icon = xmlCategory.Attribute("Icon").Value.ToString(), 
                                    Name = xmlCategory.Attribute("Name").Value.ToString(),
                                    Plan = Convert.ToDouble(xmlCategory.Attribute("Plan").Value.ToString()),
                                    TotalExpenses = Convert.ToDouble(xmlCategory.Attribute("TotalExpenses").Value.ToString())
                                  };
        }

        #endregion

    }
}
