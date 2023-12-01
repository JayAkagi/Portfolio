using System;
using System.Collections.Generic;

namespace Assignment
{
    class Program
    {
        private const int ADD_ITEM_TO_STOCK = 1;
        private const int ADD_QUANTITY_TO_ITEM = 2;
        private const int TAKE_QUANTITY_FROM_ITEM = 3;
        private const int VIEW_INVENTORY_REPORT = 4;
        private const int VIEW_FINANCIAL_REPORT = 5;
        private const int VIEW_TRANSACTION_LOG = 6;
        private const int VIEW_PERSONAL_USAGE_REPORT = 7;
        private const int EXIT = 8;

        private static readonly EmployeeManager employeeManager = new EmployeeManager();
        private static readonly StockManager stockManager = new StockManager();
        private static readonly TransactionLogManager transactionLogManager = new TransactionLogManager();

        static void Main(string[] args)
        {
            InitialiseData();

            DisplayMenu();
            int option = GetUserChoice();

            while (option != EXIT)
            {
                switch (option)
                {
                    case ADD_ITEM_TO_STOCK:
                        AddItemToStock();
                        break;
                    case ADD_QUANTITY_TO_ITEM:
                        AddQuantityToItem();
                        break;
                    case TAKE_QUANTITY_FROM_ITEM:
                        TakeQuantityFromItem();
                        break;
                    case VIEW_INVENTORY_REPORT:
                        ViewInventoryReport();
                        break;
                    case VIEW_FINANCIAL_REPORT:
                        ViewFinancialReport();
                        break;
                    case VIEW_TRANSACTION_LOG:
                        ViewTransactionLog();
                        break;
                    case VIEW_PERSONAL_USAGE_REPORT:
                        ViewPersonalUsageReport();
                        break;
                    default:
                        Console.WriteLine("\nThis choice is not recognised.");
                        break;
                }

                DisplayMenu();
                option = GetUserChoice();
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("\n{0}. Add item to stock", ADD_ITEM_TO_STOCK);
            Console.WriteLine("{0}. Add quantity to item", ADD_QUANTITY_TO_ITEM);
            Console.WriteLine("{0}. Take quantity from item", TAKE_QUANTITY_FROM_ITEM);
            Console.WriteLine("{0}. View inventory report", VIEW_INVENTORY_REPORT);
            Console.WriteLine("{0}. View financial report", VIEW_FINANCIAL_REPORT);
            Console.WriteLine("{0}. View transaction log", VIEW_TRANSACTION_LOG);
            Console.WriteLine("{0}. View personal usage report", VIEW_PERSONAL_USAGE_REPORT);
            Console.WriteLine("{0}. Exit", EXIT);
        }

        private static int GetUserChoice()
        {
            int option = ConsoleReader.ReadInteger("\nOption");
            while (option < ADD_ITEM_TO_STOCK || option > EXIT)
            {
                Console.WriteLine("\nChoice not recognised, Please enter again");
                option = ConsoleReader.ReadInteger("\nOption");
            }
            return option;
        }

        private static void InitialiseData()
        {
            employeeManager.AddEmployee(new Employee("Graham"));
            employeeManager.AddEmployee(new Employee("Phil"));
            employeeManager.AddEmployee(new Employee("Jan"));

            Item i1 = stockManager.AddItem(1, "Pencil", 10);
            transactionLogManager.AddTransactionLog(
                new TransactionLogEntry("Item Added", i1.ID, i1.Name, 0.25f, i1.Quantity, "Graham", DateTime.Now));

            Item i2 = stockManager.AddItem(2, "Eraser", 20);
            transactionLogManager.AddTransactionLog(
                new TransactionLogEntry("Item Added", i2.ID, i2.Name, 0.15f, i2.Quantity, "Phil", DateTime.Now));

            i2.RemoveQuantity(4);
            transactionLogManager.AddTransactionLog(
                new TransactionLogEntry("Quantity Removed", i2.ID, i2.Name, -1, 4, "Graham", DateTime.Now));

            i2.AddQuantity(2);
            transactionLogManager.AddTransactionLog(
                new TransactionLogEntry("Quantity Added", i2.ID, i2.Name, 0.33, 2, "Phil", DateTime.Now));
        }

        public static void AddItemToStock()
        {
            try
            {
                string employeeName = ConsoleReader.ReadString("\nEmployee Name");
                if (employeeManager.FindEmployee(employeeName) == null)
                {
                    throw new Exception("ERROR: Employee not found");
                }

                int itemId = ConsoleReader.ReadInteger("Item ID");
                string itemName = ConsoleReader.ReadString("Item Name");
                int itemQuantity = ConsoleReader.ReadInteger("Item Quantity");
                double itemPrice = ConsoleReader.ReadDouble("Item Price");

                if (itemPrice < 0)
                {
                    throw new Exception("ERROR: Price below 0");
                }

                Item i = stockManager.AddItem(itemId, itemName, itemQuantity);
                transactionLogManager.AddTransactionLog(
                    new TransactionLogEntry("Item Added", i.ID, i.Name, itemPrice, i.Quantity, employeeName, DateTime.Now));

                Console.WriteLine("Item Added");
                ViewInventoryReport();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void AddQuantityToItem()
        {
            try
            {
                string employeeName = ConsoleReader.ReadString("\nEmployee Name");
                if (employeeManager.FindEmployee(employeeName) == null)
                {
                    throw new Exception("ERROR: Employee not found");
                }

                int itemId = ConsoleReader.ReadInteger("Item ID");
                Item item = stockManager.FindItem(itemId);
                if (item == null)
                {
                    throw new Exception("ERROR: Item not found");
                }

                int quantityToAdd = ConsoleReader.ReadInteger("How many items would you like to add?");
                double itemPrice = ConsoleReader.ReadDouble("Item Price");

                if (itemPrice < 0)
                {
                    throw new Exception("ERROR: Price below 0");
                }

                item.AddQuantity(quantityToAdd);
                Console.WriteLine(
                    "{0} items have been added to Item ID: {1} on {2}",
                    quantityToAdd,
                    itemId,
                    DateTime.Now.ToString("dd/MM/yyyy"));

                transactionLogManager.AddTransactionLog(
                    new TransactionLogEntry("Quantity Added", item.ID, item.Name, itemPrice, quantityToAdd, employeeName, DateTime.Now));
            }
            catch (Exception e)
            { 
                Console.WriteLine(e.Message); 
            }
        }

        public static void TakeQuantityFromItem()
        {
            try
            {
                string employeeName = ConsoleReader.ReadString("\nEmployee Name");
                if (employeeManager.FindEmployee(employeeName) == null)
                {
                    throw new Exception("ERROR: Employee not found");
                }

                int itemId = ConsoleReader.ReadInteger("Item ID");
                Item item = stockManager.FindItem(itemId);
                if (item == null)
                {
                    throw new Exception("ERROR: Item not found");
                }

                int quantityToRemove = ConsoleReader.ReadInteger("How many items would you like to remove?");

                item.RemoveQuantity(quantityToRemove);
                Console.WriteLine(
                    "{0} has removed {1} of Item ID: {2} on {3}",
                    employeeName,
                    quantityToRemove,
                    itemId,
                    DateTime.Now.ToString("dd/MM/yyyy"));

                transactionLogManager.AddTransactionLog(
                    new TransactionLogEntry("Quantity Removed", item.ID, item.Name, -1, quantityToRemove, employeeName, DateTime.Now));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ViewFinancialReport()
        {
            double total = 0;

            Console.WriteLine("\nFinancial Report:");

            foreach (TransactionLogEntry entry in transactionLogManager.GetTransactionLog())
            {
                if (entry.TypeOfTransaction.Equals("Item Added") 
                    || entry.TypeOfTransaction.Equals("Quantity Added"))
                {
                    double cost = entry.ItemPrice * entry.Quantity;
                    Console.WriteLine("{0}: Total price of item: {1:C}", entry.ItemName, cost);
                    total += cost;
                }
            }

            Console.WriteLine("{0}: {1:C}", "Total price of all items", total);
        }

        private static void ViewInventoryReport()
        {
            Console.WriteLine("\nAll items");
            Console.WriteLine(
                "\t{0, -4} {1, -20} {2, -20}",
                "ID",
                "Name",
                "Quantity");

            foreach (Item i in stockManager.GetItems())
            {
                Console.WriteLine(
                    "\t{0, -4} {1, -20} {2, -20}",
                    i.ID,
                    i.Name,
                    i.Quantity);
            }
        }

        public static void ViewPersonalUsageReport()
        {
            string employeeName = ConsoleReader.ReadString("Employee name");

            Console.WriteLine("\nPersonal Usage Report for {0}", employeeName);
            Console.WriteLine(
                "\t{0, -20} {1, -10} {2, -12} {3, -12}",
                "Date Taken",
                "ID",
                "Name",
                "Quantity Removed");

            foreach (TransactionLogEntry entry in transactionLogManager.GetTransactionLog())
            {
                if (entry.TypeOfTransaction.Equals("Quantity Removed") && entry.EmployeeName == employeeName)
                {
                    Console.WriteLine(
                        "\t{0, -20} {1, -10} {2, -12} {3, -12}",
                        entry.DateAdded,
                        entry.ItemID,
                        entry.ItemName,
                        entry.Quantity);
                }
            }
        }

        private static void ViewTransactionLog()
        {
            List<TransactionLogEntry> tls = transactionLogManager.GetTransactionLog();

            Console.WriteLine("\nTransaction Log:");
            Console.WriteLine(
                "\t{0, -20} {1, -16} {2, -6} {3, -12} {4, -10} {5, -12} {6, -12}",
                "Date",
                "Type",
                "ID",
                "Name",
                "Quantity",
                "Employee",
                "Price");

            foreach (TransactionLogEntry entry in tls)
            {
                Console.WriteLine(
                    "\t{0, -20} {1, -16} {2, -6} {3, -12} {4, -10} {5, -12} {6, -12}",
                    entry.DateAdded.ToString("dd/MM/yyyy"),
                    entry.TypeOfTransaction,
                    entry.ItemID,
                    entry.ItemName,
                    entry.Quantity,
                    entry.EmployeeName,
                    entry.TypeOfTransaction.Equals("Quantity Removed") ? "" : "" + string.Format("{0:C}", entry.ItemPrice));
            }
        }
    }
}
