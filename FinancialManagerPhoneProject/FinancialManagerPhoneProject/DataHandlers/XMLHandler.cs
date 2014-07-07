using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;
using System.IO.IsolatedStorage;
using System.Windows;
using Microsoft.Phone.Shell;

#if DEBUG
    using MockIAPLib;
using System.Globalization;
#else
    using Windows.ApplicationModel.Store;
#endif



namespace FinancialManagerPhoneProject.DataHandlers
{
    public class XMLHandler
    {
        public static double DEIVCE_WIDTH;
        public static double DEIVCE_HEIGHT;
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
                                                           new XAttribute("CurrentMonth", DateTime.Today.Month),
                                                           new XAttribute("CurrentYear", DateTime.Today.Year),
                                                           new XAttribute("Version", StaticValues.CurrentVersion),
                                                           new XAttribute("IsDefaultData", "1")),
                              new XElement("Incomes", new XElement("Income", new XAttribute("ID", 1),
                                                                           new XAttribute("Description", "Monthly Income"),
                                                                           new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                           3500),
                                                    new XElement("Income", new XAttribute("ID", 22),
                                                                           new XAttribute("Description", "Painting Project"),
                                                                           new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                           850),
                                                    new XElement("Income", new XAttribute("ID", 23),
                                                                           new XAttribute("Description", "Garage Sale"),
                                                                           new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                           120)),
                              new XElement("Expenses", new XElement("Expense", new XAttribute("ID", 1),
                                                                              new XAttribute("Category", "Mortgage"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Monthly Mortgage"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              1350),
                                                       new XElement("Expense", new XAttribute("ID", 22),
                                                                              new XAttribute("Category", "Bills"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Electricity Bill"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              180),
                                                       new XElement("Expense", new XAttribute("ID", 23),
                                                                              new XAttribute("Category", "Bills"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Gas Bill"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              100),
                                                       new XElement("Expense", new XAttribute("ID", 24),
                                                                              new XAttribute("Category", "Bills"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Water Bill"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              20),
                                                       new XElement("Expense", new XAttribute("ID", 2),
                                                                              new XAttribute("Category", "Car"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Changing Motor Oil"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              90),
                                                       new XElement("Expense", new XAttribute("ID", 20),
                                                                              new XAttribute("Category", "Groceries"),
                                                                              new XAttribute("Description", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              89),
                                                       new XElement("Expense", new XAttribute("ID", 21),
                                                                              new XAttribute("Category", "Groceries"),
                                                                              new XAttribute("Description", ""),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              35),
                                                       new XElement("Expense", new XAttribute("ID", 4),
                                                                              new XAttribute("Category", "Groceries"),
                                                                              new XAttribute("Description", ""),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              43),
                                                       new XElement("Expense", new XAttribute("ID", 6),
                                                                              new XAttribute("Category", "Insurances"),
                                                                              new XAttribute("Description", "Car Insurance"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              120),
                                                       new XElement("Expense", new XAttribute("ID", 7),
                                                                              new XAttribute("Category", "Commute"),
                                                                              new XAttribute("Description", "Monthly train ticket top up"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              128),
                                                       new XElement("Expense", new XAttribute("ID", 8),
                                                                              new XAttribute("Category", "Children"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Alex monthly pocket money"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              70),
                                                       new XElement("Expense", new XAttribute("ID", 9),
                                                                              new XAttribute("Category", "Accessories"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "A headphone at ebay"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              160),
                                                       new XElement("Expense", new XAttribute("ID", 10),
                                                                              new XAttribute("Category", "Car"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Gas"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              50),
                                                       new XElement("Expense", new XAttribute("ID", 11),
                                                                              new XAttribute("Category", "Clothes"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "New Red Coat"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              38),
                                                       new XElement("Expense", new XAttribute("ID", 12),
                                                                              new XAttribute("Category", "Clothes"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "New Bag"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              30),
                                                       new XElement("Expense", new XAttribute("ID", 13),
                                                                              new XAttribute("Category", "Clothes"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "New Sweater"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              35),
                                                       new XElement("Expense", new XAttribute("ID", 14),
                                                                              new XAttribute("Category", "Game"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Buying a pack on hearthstone"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              50),
                                                       new XElement("Expense", new XAttribute("ID", 15),
                                                                              new XAttribute("Category", "Game"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Diablo 3"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              39),
                                                       new XElement("Expense", new XAttribute("ID", 16),
                                                                              new XAttribute("Category", "Pets"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Dog food"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              25),
                                                       new XElement("Expense", new XAttribute("ID", 17),
                                                                              new XAttribute("Category", "Pets"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Dog Shampoo"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              15),
                                                       new XElement("Expense", new XAttribute("ID", 18),
                                                                              new XAttribute("Category", "Pets"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Dog food"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              25),
                                                       new XElement("Expense", new XAttribute("ID", 19),
                                                                              new XAttribute("Category", "Liquor"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Beer"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              30),
                                                       new XElement("Expense", new XAttribute("ID", 5),
                                                                              new XAttribute("Category", "Liquor"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Wine"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
                                                                              12),
                                                       new XElement("Expense", new XAttribute("ID", 3),
                                                                              new XAttribute("Category", "Childcare"),
                                                                              new XAttribute("RecieptName", ""),
                                                                              new XAttribute("Latitude", ""),
                                                                              new XAttribute("Longtitude", ""),
                                                                              new XAttribute("Description", "Weekly Payment"),
                                                                              new XAttribute("Date", StaticMethods.DefaultRandomDate()),
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
            string FirstTime = null;
            IsolatedStorageSettings.ApplicationSettings.TryGetValue("firsttime", out FirstTime);

            if (FirstTime == "1")
            {
                IsolatedStorageSettings.ApplicationSettings["firsttime"] = "0";
                IsolatedStorageSettings.ApplicationSettings.Save();
                await LoadDefaultXmlAsync();
                await SaveXmlToFileAsync();
            }
            else
            {
                StorageFile openedFile = await ApplicationData.Current.LocalFolder.GetFileAsync("FinancialManagerXML.xml");
                using (StreamReader reader = new StreamReader(await openedFile.OpenStreamForReadAsync()))
                {
                    XMLHandler.FINANCIALMANAGER_XML = XDocument.Load(reader);
                }
            }
        }

        // create the folder to hold the pictures of the receipts
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

        public string GetVersion()        
        {
            try
            {
                string oldVersion = FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Version").Value.ToString();
                string newVersion = StaticValues.CurrentVersion;
                if (oldVersion != newVersion)
                {
                    FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Version").SetValue(newVersion);
                    SaveXmlToFileAsync();
                }
                return oldVersion;
            }
            catch
            {
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("Version", StaticValues.CurrentVersion));
                SaveXmlToFileAsync();
                return "1.3";
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

        public int GetCurrentMonth()
        {
            try
            {
                return Convert.ToInt32(FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("CurrentMonth").Value.ToString());
            }
            catch 
            {
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("CurrentMonth", DateTime.Today.Month));
                UpdateCategoriesTotalExpense();                
                return DateTime.Today.Month;
            }
        }
        public int GetCurrentYear()
        {
            try
            {
                return Convert.ToInt32(FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("CurrentYear").Value.ToString());
            }
            catch            
            {
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("CurrentYear", DateTime.Today.Year));
                SaveXmlToFileAsync();
                return DateTime.Today.Year;
            }
        }

        public bool IsDefaultData()
        {
            string temp = FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("IsDefaultData").Value;
            return temp == "1" ? true : false;
        }

        public void UpdateSettings(string symbol, string month, string year) 
        {
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Currency").SetValue(symbol);
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("CurrentMonth").SetValue(month);
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("CurrentYear").SetValue(year);

            UpdateCategoriesTotalExpense();
            //Save xml file has been added in UpdateCategoriesTotalExpense because this method is also being 
            // called from other places. 
            
        }
        private void UpdateCategoriesTotalExpense()
        {
            // we need to update the total expenses attribute in each category node. 
            var categoriesXml = from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                select x;
            
            foreach (XElement category in categoriesXml)
            {
                List<Expense> expenses = GetAllExpenses(category.Attribute("Name").Value.ToString());
                double totalExpenses = expenses.Sum(e => StaticMethods.CleanNumber(e.Value.ToString()));
                category.Attribute("TotalExpenses").SetValue(totalExpenses);
            }
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
            try
            {
                Task saveImage = Task.Factory.StartNew(() =>
                {
                    if (imageAsByte != null)
                    {
                        using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                        {
                            using (var stream = store.CreateFile(System.IO.Path.Combine("Receipts/" + ImageName)))
                            {
                                stream.Write(imageAsByte, 0, imageAsByte.Length);
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
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

        public void DeleteAllExpenses()
        {
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("IsDefaultData").SetValue("0");
            FINANCIALMANAGER_XML.Root.Element("Expenses").DescendantNodes().Remove();

            var categoriesXml = from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                select x;

            foreach (var node in categoriesXml)
            {
                node.Attribute("TotalExpenses").SetValue("0");
            }
            DeleteAllStorage();
            UpdateTileValues();
        }
        public void DeleteAllIncomes()
        {
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("IsDefaultData").SetValue("0");
            FINANCIALMANAGER_XML.Root.Element("Incomes").DescendantNodes().Remove();
            UpdateTileValues();
            SaveXmlToFileAsync();
        }
        public void DeleteAll()
        {
            FINANCIALMANAGER_XML.Root.Element("Expenses").DescendantNodes().Remove();
            FINANCIALMANAGER_XML.Root.Element("Incomes").DescendantNodes().Remove();
            FINANCIALMANAGER_XML.Root.Element("Categories").DescendantNodes().Remove();
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("IsDefaultData").SetValue("0");
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Currency").SetValue("$");
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("CurrentMonth").SetValue(DateTime.Today.Month.ToString());
            FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("CurrentYear").SetValue(DateTime.Today.Year.ToString());            

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
            UpdateTileValues();
        }
        public void DeleteAllStorage()
        {
            Task deleteStorage = Task.Factory.StartNew(async () =>
            {
                await DeleteReceiptsFolderAsync();
                SaveXmlToFileAsync();
            });
            UpdateTileValues();
        }

        #region Password
        public bool IsUsePassword()
        {
            string userPassword = string.Empty;
            try
            {
                userPassword = FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("UsePassword").Value.ToString();                
            }
            catch
            {
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("Password", ""));
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("UsePassword", "0"));
                SaveXmlToFileAsync();
            }
            if (userPassword == "1")
                return true;
            else
                return false;
        }

        public bool CheckPassword(string password)        
        {
            if (password == FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Password").Value.ToString())
                return true;
            else
                return false;
        }

        public void SetPassword(string password)
        {
            try
            {
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Password").SetValue(password);
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("UsePassword").SetValue("1");
            }
            catch
            {
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("Password", password));
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("UsePassword", "1"));                
            }
            SaveXmlToFileAsync();
        }

        public void RemovePassword()
        {
            try
            {
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("UsePassword").SetValue("0");
            }
            catch
            {
                FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("UsePassword", "0"));
            }
            SaveXmlToFileAsync();
        }

        #endregion

        #region Store Manager

        public bool IsValidToSave()
        {
            string IsFullAccess = "0";
            try
            {
                IsFullAccess = FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("FullAccess").Value.ToString();
            }
            catch            
            {
                // Handling those databases without FullAccess attribute
                if (CurrentApp.LicenseInformation.ProductLicenses["11111"].IsActive)
                {
                    FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("FullAccess", "1"));
                    IsFullAccess = "1";
                }
                else
                {
                    FINANCIALMANAGER_XML.Root.Element("StaticValues").Add(new XAttribute("FullAccess", "0"));
                }
                SaveXmlToFileAsync();
            }

            if (StaticValues.DB.IsDefaultData() || IsFullAccess == "1")
            {
                return true;                
            }
            else
            {
                if (StaticValues.DB.IsUltimateUser())
                {
                    return true;
                }
                else
                {
                    int expenseCount = FINANCIALMANAGER_XML.Root.Element("Expenses").Descendants().Count();
                    if (expenseCount <= 8)
                        return true;
                    else
                        return false;
                }
            }
        }
        public bool IsUltimateUser()
        {
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
                    FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("FullAccess").SetValue("1");
                    return true;
                }
                else
                {
                    string receipt = await CurrentApp.RequestProductPurchaseAsync("11111", true);
                    FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("FullAccess").SetValue("1");
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Income
        public List<Income> GetAllIncomes()
        {
            List<Income> incomes = new List<Income>();

            int currentMonth = GetCurrentMonth();
            int currentYear = GetCurrentYear();

            var incomesXml = from x in FINANCIALMANAGER_XML.Root.Element("Incomes").Elements()
                             where Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Month == currentMonth &&
                                   Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Year == currentYear
                             select x;

            foreach (var node in incomesXml)
            {
                DateTime date = Convert.ToDateTime(node.Attribute("Date").Value.ToString());
                
                incomes.Add(new Income()
                {
                    ID = node.Attribute("ID").Value,
                    Date = date,
                    Description = node.Attribute("Description").Value,
                    Value = StaticMethods.CleanNumber(node.Value.ToString()),                   
                });
            }
            return incomes.OrderByDescending(e => e.Date).ToList();
        }

        public double GetIncome()
        {
            int currentMonth = GetCurrentMonth();
            int currentYear = GetCurrentYear();

            try
            {
                var incomesXml = from x in FINANCIALMANAGER_XML.Root.Element("Incomes").Elements()
                                 where Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Month == currentMonth &&
                                       Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Year == currentYear
                                 select x;

                if (incomesXml == null)
                    return 0;
                
                return (incomesXml).Sum(e => StaticMethods.CleanNumber(e.Value));
            }
            catch
            {
                // the income node doesn't exist

                FINANCIALMANAGER_XML.Root.Add(new XElement("Incomes"));
                SaveXmlToFileAsync();
                return 0;                
            }
        }

        public Income GetIncome(string ID)
        {
            var xmlIncome = (from x in FINANCIALMANAGER_XML.Root.Element("Incomes").Elements()
                             where x.Attribute("ID").Value == ID
                             select x).FirstOrDefault();
            
            return new Income()
            {
                Date = Convert.ToDateTime(xmlIncome.Attribute("Date").Value.ToString()),
                Description = xmlIncome.Attribute("Description").Value,
                ID = ID,
                Value = StaticMethods.CleanNumber(xmlIncome.Value.ToString())
            };
        }

        public void AddIncome(Income income)
        {
            XElement newIncome = new XElement("Income",
                                                new XAttribute("ID", StaticMethods.GenerateID()),
                                                new XAttribute("Description", income.Description),
                                                new XAttribute("Date", income.Date.ToString()),
                                                income.Value);
            FINANCIALMANAGER_XML.Root.Element("Incomes").Add(newIncome);
            
            UpdateTileValues();
            SaveXmlToFileAsync();
        }

        public void DeleteIncome(string ID)
        {
            var incomeXML = (from x in FINANCIALMANAGER_XML.Root.Element("Incomes").Elements()
                             where x.Attribute("ID").Value == ID
                             select x).FirstOrDefault();
            incomeXML.Remove();

            UpdateTileValues();
            SaveXmlToFileAsync();
        }

        public void UpdateIncome(Income income)
        {
            var xmlIncome = (from x in FINANCIALMANAGER_XML.Root.Element("Incomes").Elements()
                             where x.Attribute("ID").Value == income.ID
                             select x).FirstOrDefault();

            xmlIncome.Attribute("Description").SetValue(income.Description);
            xmlIncome.SetValue(income.Value);
            xmlIncome.Attribute("Date").SetValue(income.Date.ToString());

            UpdateTileValues();
            SaveXmlToFileAsync();
        }

        #endregion

        #region Expense

        public List<Expense> GetAllExpenses()
        {
            List<Expense> expenses = new List<Expense>();

            int currentMonth = GetCurrentMonth();
            int currentYear = GetCurrentYear();

            var expensesXml = from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                              where Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Month == currentMonth &&
                                    Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Year == currentYear
                              select x;

            var categoriesXml = from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                select x;

            foreach (var node in expensesXml)
            {
                DateTime date = Convert.ToDateTime(node.Attribute("Date").Value.ToString());
                CultureInfo culture = new CultureInfo("en-us");
                expenses.Add(new Expense()
                             {
                                 ID = node.Attribute("ID").Value,
                                 Category = node.Attribute("Category").Value,
                                 Date = date,
                                 RecieptName = node.Attribute("RecieptName").Value,
                                 Longtitude = node.Attribute("Longtitude").Value,
                                 Latitude = node.Attribute("Latitude").Value,
                                 Description = node.Attribute("Description").Value,
                                 Value = Convert.ToDouble(node.Value.ToString(),culture),
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

            int currentMonth = GetCurrentMonth();
            int currentYear = GetCurrentYear();

            var expensesXml = from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                              where x.Attribute("Category").Value == CategoryName &&
                                    Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Month == currentMonth &&
                                    Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Year == currentYear
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
                    Value = StaticMethods.CleanNumber(node.Value.ToString()),
                    Icon = categoryXml.Attribute("Icon").Value
                });
            }
            return expenses.OrderBy(e => e.Date).ToList();
        }

        public double GetTotalExpenses()
        {
            int currentMonth = GetCurrentMonth();
            int currentYear = GetCurrentYear();

            var expensesXml = from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                              where Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Month == currentMonth &&
                                    Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Year == currentYear
                              select x;

            return (expensesXml).Sum(e => StaticMethods.CleanNumber(e.Value));
        }

        public double GetTotalExpenses(string CategoryName)
        {

            int currentMonth = GetCurrentMonth();
            int currentYear = GetCurrentYear();
            
            return (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                    where x.Attribute("Category").Value == CategoryName &&
                          Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Month == currentMonth &&
                          Convert.ToDateTime(x.Attribute("Date").Value.ToString()).Year == currentYear
                    select x).Sum(e => StaticMethods.CleanNumber(e.Value));
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
            if (StaticValues.DB.GetCurrentMonth() == expense.Date.Month && StaticValues.DB.GetCurrentYear() == expense.Date.Year)
            {
                
                var attribute = FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                    .Where(c => c.Attribute("Name").Value == expense.Category)
                                    .FirstOrDefault().Attribute("TotalExpenses");
                attribute.Value = (StaticMethods.CleanNumber(attribute.Value) + expense.Value).ToString();

            }
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
            double totalExpense = StaticMethods.CleanNumber(xmlCategory.Attribute("TotalExpenses").Value);
            totalExpense -= StaticMethods.CleanNumber(expenseXML.Value);
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

            if (expense.Value.ToString() != xmlExpense.Value.ToString())
            {
                var xmlCategory = (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                   where x.Attribute("Name").Value == expense.Category
                                   select x).FirstOrDefault();

                
                double totalExpense = StaticMethods.CleanNumber(xmlCategory.Attribute("TotalExpenses").Value);
                double oldExpense = StaticMethods.CleanNumber(xmlExpense.Value);
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
                Value = StaticMethods.CleanNumber(xmlExpense.Value.ToString())
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
                double plan = 0;
                double totalExpenses = 0;
                plan = StaticMethods.CleanNumber(node.Attribute("Plan").Value.ToString());
                totalExpenses = StaticMethods.CleanNumber(node.Attribute("TotalExpenses").Value.ToString());

                categories.Add(new Category()
                {
                    Icon = node.Attribute("Icon").Value.ToString(),
                    Name = node.Attribute("Name").Value.ToString(),
                    Plan = plan,
                    TotalExpenses = totalExpenses
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
                double plan = 0;
                double totalExpenses = 0;
                plan = StaticMethods.CleanNumber(node.Attribute("Plan").Value.ToString());
                totalExpenses = StaticMethods.CleanNumber(node.Attribute("TotalExpenses").Value.ToString());

                categories.Add(new Category()
                {
                    Icon = node.Attribute("Icon").Value.ToString(),
                    Name = node.Attribute("Name").Value.ToString(),
                    Plan = plan,
                    TotalExpenses = totalExpenses
                });
            }
            return categories.OrderByDescending(c => c.TotalExpenses).Take(5).ToList();
        }

        public double GetTotalPlan()
        {
            
            return (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                    select x).Sum(c => StaticMethods.CleanNumber(c.Attribute("Plan").Value));
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

        public bool DeleteCategory(string category)
        {
            try
            {
                (from x in FINANCIALMANAGER_XML.Root.Element("Expenses").Elements()
                 where x.Attribute("Category").Value == category
                 select x).Remove();

                (from x in FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                 where x.Attribute("Name").Value == category
                 select x).FirstOrDefault().Remove();

                UpdateTileValues();
                SaveXmlToFileAsync();

                return true;
            }
            catch
            {
                return false;
            }
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

            if (category.Plan != StaticMethods.CleanNumber(xmlCategory.Attribute("Plan").Value.ToString()))
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
            
            return new Category()
            {
                Icon = xmlCategory.Attribute("Icon").Value.ToString(),
                Name = xmlCategory.Attribute("Name").Value.ToString(),
                Plan = StaticMethods.CleanNumber(xmlCategory.Attribute("Plan").Value.ToString()),
                TotalExpenses = StaticMethods.CleanNumber(xmlCategory.Attribute("TotalExpenses").Value.ToString())
            };
        }

        #endregion

    }
}
