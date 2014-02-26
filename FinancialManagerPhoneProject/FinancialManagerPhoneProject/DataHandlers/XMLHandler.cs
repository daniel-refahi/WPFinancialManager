using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

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
                                                           new XAttribute("Currency", "$")),
                              new XElement("Expenses", new XElement("Expense", new XAttribute("ID", 1),
                                                                              new XAttribute("Category", "Mortgage"),
                                                                              new XAttribute("Description", "Monthly Mortgage"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              1200),
                                                       new XElement("Expense", new XAttribute("ID", 22),
                                                                              new XAttribute("Category", "Bills"),
                                                                              new XAttribute("Description", "Electricity Bill"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              180),
                                                       new XElement("Expense", new XAttribute("ID", 23),
                                                                              new XAttribute("Category", "Bills"),
                                                                              new XAttribute("Description", "Gas Bill"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              100),
                                                       new XElement("Expense", new XAttribute("ID", 24),
                                                                              new XAttribute("Category", "Bills"),
                                                                              new XAttribute("Description", "Water Bill"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              20),
                                                       new XElement("Expense", new XAttribute("ID", 2),
                                                                              new XAttribute("Category", "Car"),
                                                                              new XAttribute("Description", "Changing Motor Oil"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              90),
                                                       new XElement("Expense", new XAttribute("ID", 20),
                                                                              new XAttribute("Category", "Groceries"),
                                                                              new XAttribute("Description", ""),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              89),
                                                       new XElement("Expense", new XAttribute("ID", 21),
                                                                              new XAttribute("Category", "Groceries"),
                                                                              new XAttribute("Description", ""),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              35),
                                                       new XElement("Expense", new XAttribute("ID", 4),
                                                                              new XAttribute("Category", "Groceries"),
                                                                              new XAttribute("Description", ""),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              43),
                                                       new XElement("Expense", new XAttribute("ID", 6),
                                                                              new XAttribute("Category", "Insurances"),
                                                                              new XAttribute("Description", "Car Insurance"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              120),
                                                       new XElement("Expense", new XAttribute("ID", 7),
                                                                              new XAttribute("Category", "Commute"),
                                                                              new XAttribute("Description", "My Key Top up"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              128),
                                                       new XElement("Expense", new XAttribute("ID", 8),
                                                                              new XAttribute("Category", "Children"),
                                                                              new XAttribute("Description", "Monthly pocket money"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              70),
                                                       new XElement("Expense", new XAttribute("ID", 9),
                                                                              new XAttribute("Category", "Accessories"),
                                                                              new XAttribute("Description", "A headphone"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              160),
                                                       new XElement("Expense", new XAttribute("ID", 10),
                                                                              new XAttribute("Category", "Car"),
                                                                              new XAttribute("Description", "Gas"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              50),
                                                       new XElement("Expense", new XAttribute("ID", 11),
                                                                              new XAttribute("Category", "Clothes"),
                                                                              new XAttribute("Description", "New Coat"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              138),
                                                       new XElement("Expense", new XAttribute("ID", 12),
                                                                              new XAttribute("Category", "Clothes"),
                                                                              new XAttribute("Description", "New Bag"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              80),
                                                       new XElement("Expense", new XAttribute("ID", 13),
                                                                              new XAttribute("Category", "Clothes"),
                                                                              new XAttribute("Description", "New Sweater"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              35),
                                                       new XElement("Expense", new XAttribute("ID", 14),
                                                                              new XAttribute("Category", "Game"),
                                                                              new XAttribute("Description", "Call of duty"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              88),
                                                       new XElement("Expense", new XAttribute("ID", 15),
                                                                              new XAttribute("Category", "Game"),
                                                                              new XAttribute("Description", "Dishonored"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              49),
                                                       new XElement("Expense", new XAttribute("ID", 16),
                                                                              new XAttribute("Category", "Pets"),
                                                                              new XAttribute("Description", "Dog food"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              25),
                                                       new XElement("Expense", new XAttribute("ID", 17),
                                                                              new XAttribute("Category", "Pets"),
                                                                              new XAttribute("Description", "Dog Shampoo"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              15),
                                                       new XElement("Expense", new XAttribute("ID", 18),
                                                                              new XAttribute("Category", "Pets"),
                                                                              new XAttribute("Description", "Dog food"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              25),
                                                       new XElement("Expense", new XAttribute("ID", 19),
                                                                              new XAttribute("Category", "Liquor"),
                                                                              new XAttribute("Description", "Beer"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              30),
                                                       new XElement("Expense", new XAttribute("ID", 5),
                                                                              new XAttribute("Category", "Liquor"),
                                                                              new XAttribute("Description", "Wine"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              12),
                                                       new XElement("Expense", new XAttribute("ID", 3),
                                                                              new XAttribute("Category", "Childcare"),
                                                                              new XAttribute("Description", "Weekly Payment"),
                                                                              new XAttribute("Date", "05/12/2013"),
                                                                              150)),
                              new XElement("Categories", new XElement("Category", new XAttribute("Name", "Mortgage"),
                                                                              new XAttribute("Plan", 1200),
                                                                              new XAttribute("Icon", "Mortgage"),
                                                                              new XAttribute("TotalExpenses", 1200)
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
                                                                              new XAttribute("TotalExpenses", 253)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Gift"),
                                                                              new XAttribute("Plan", 50),
                                                                              new XAttribute("Icon", "gifts"),
                                                                              new XAttribute("TotalExpenses", 0)
                                                                              ),
                                                         new XElement("Category", new XAttribute("Name", "Game"),
                                                                              new XAttribute("Plan", 100),
                                                                              new XAttribute("Icon", "input_gaming"),
                                                                              new XAttribute("TotalExpenses", 137)
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
            await loadDefaultXml;
        }

        public async Task SaveXmlToFileAsync()
        {
            Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var xmlFile = await localFolder.CreateFileAsync("FinancialManagerXML.xml", CreationCollisionOption.ReplaceExisting);

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

        public string GetCurrencySymbol()
        {
            string currency;
            try
            {
                currency = FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Currency").Value;
            }
            catch { currency = "$"; }
            return currency;
        }

        public double GetIncome()
        {
            return Convert.ToDouble(FINANCIALMANAGER_XML.Root.Element("StaticValues").Attribute("Income").Value);
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
                                                new XAttribute("Description", expense.Description),
                                                new XAttribute("Date", expense.Date.ToString()),
                                                expense.Value);
            FINANCIALMANAGER_XML.Root.Element("Expenses").Add(newExpense);

            //updating TotalExpenses on the related category
            var attribute = FINANCIALMANAGER_XML.Root.Element("Categories").Elements()
                                .Where(c => c.Attribute("Name").Value == expense.Category)
                                .FirstOrDefault().Attribute("TotalExpenses");
            attribute.Value = (Convert.ToDouble(attribute.Value) + expense.Value).ToString();

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
            xmlExpense.SetValue(expense.Value);
            //xmlExpense.Attribute("Date").SetValue(expense.Date.Day + "/" + expense.Date.Month + "/" + expense.Date.Year);
            xmlExpense.Attribute("Date").SetValue(expense.Date.ToString());

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
