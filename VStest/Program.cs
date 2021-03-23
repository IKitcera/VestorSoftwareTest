using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VStest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=============================Task 1==========================\n");
            AlgTask();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=============================Task 2==========================\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            OOPTask();

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=============================Task 3==========================\n");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            DbTask();

            Console.ForegroundColor = ConsoleColor.Gray;
        }
        static void AlgTask()
        {
            var testStr = "WEAREDISCOVEREDFLEEATONCE";

            var encrTestStr = Encryption.Encode(testStr, 3);
            var decrTestStr = Encryption.Decode(encrTestStr, 3);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\t Encrypted string");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(encrTestStr);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("\t Decrypted string");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(decrTestStr);
        }
        static void OOPTask()
        {
            var side = 1.1234D;
            var radius = 1.1234D;
            var _base = 5D;
            var height = 2D;

            var shapes = new List<Shape>{ new Square(side),
                new Circle(radius),
                new Triangle(_base, height) };

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\t Before sort");
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var s in shapes)
            {
                s.Info();
            }

            shapes.Sort();

            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("\t After sort");
            Console.ForegroundColor = ConsoleColor.Blue;
            foreach (var s in shapes)
            {
                s.Info();
            }
        }
        static void DbTask()
        {
            using (AppContext db = new AppContext())
            {
                //db.Database.ExecuteSqlRaw("SET IDENTITY_INSERT Products ON");
                db.Database.ExecuteSqlRaw("INSERT INTO Suppliers VALUES " +
                   "('Exotic Liquid','London','UK'),('New Orleans Cajun Delights','New Orleans', 'USA')," +
                   "('Grandma Kelly’s Homestead','Ann Arbor','USA'),('Tokyo Traders','Tokyo','Japan')," +
                   "('Cooperativa de Quesos ‘Las Cabras‘','Oviedo','Spain')");
                db.Database.ExecuteSqlRaw("INSERT INTO Categories VALUES " +
                    "('Beverages','Soft drinks, coffees, teas, beers, and ales'), " +
                    "('Condiments', 'Sweet and savory sauces, relishes, spreads, and seasonings')," +
                    "('Confections', 'Desserts, candies, and sweet breads')," +
                    "('Dairy Products', 'Cheeses')," +
                    "('Grains / Cereals', 'Breads, crackers, pasta, and cereal')");
                db.Database.ExecuteSqlRaw("INSERT INTO Products(ProductName, SupplierID, CategoryID, Price) VALUES " +
                    "('Chais',1,1,18.00),('Chang', 1, 1, 19.00),('Aniseed Syrup', 1, 2, 10.00)," +
                    "('Chef Anton’s Cajun Seasoning', 2, 2, 22.00),('Chef Anton’s Gumbo Mix', 2, 2, 21.35)");

                db.SaveChanges();
                db.Products.Load();
                var prodNameBeginsWith = db.Products.FromSqlRaw("SELECT * FROM Products WHERE ProductName LIKE 'C%'").ToList();
                var prodWithSmallestPrice = db.Products.FromSqlRaw("SELECT * FROM Products WHERE Price = (SELECT MIN(Price) FROM Products)").FirstOrDefault();
                //SELECT Price FROM Products ...
                var prodFromUsa = db.Products.FromSqlRaw("SELECT * FROM Products WHERE SupplierID IN " +
                    "(SELECT SupplierID FROM Suppliers WHERE Suppliers.Country LIKE 'USA')").ToList();
                //"SELECT * FROM Suppliers s " +
                //    "JOIN Products p ON p.SupplierID = s.SupplierID " +
                //    "JOIN Categories c ON p.CategoryID = c.CategoryID " +
                //    "WHERE CategoryName LIKE 'Condiments'").ToList();
                var supplCondiments = db.Suppliers.FromSqlRaw("SELECT * FROM Suppliers WHERE SupplierID IN" +
                     "(SELECT SupplierID FROM Products WHERE CategoryID IN " +
                     "(SELECT CategoryID FROM Categories WHERE CategoryName LIKE 'Condiments'))").ToList();


                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\t\t" + "Selected product with product name that\n begins with ‘C’");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var p in prodNameBeginsWith)
                {
                    Console.WriteLine(p.ProductID + "\t" + p.ProductName + "\t" + p.SupplierID +
                        "\t" + p.CategoryID + "\t" + p.Price);
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\t\t" + "Selected product with the smallest price");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(prodWithSmallestPrice.ProductID + "\t" + prodWithSmallestPrice.ProductName +
                    "\t" + prodWithSmallestPrice.SupplierID + "\t" + prodWithSmallestPrice.CategoryID + "\t" + prodWithSmallestPrice.Price);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\t\t" + "Selected cost of all products from the USA");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var p in prodFromUsa.Select(p=>p.Price))
                    Console.WriteLine(p);

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\t\t" + "Selected suppliers that supply Condiments");
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (var s in supplCondiments)
                {
                    Console.WriteLine(s.SupplierID + "\t" + s.SupplierName + "\t" + s.City + "\t" + s.Country);
                }

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\t\t" + "Inserting into database new supplier");
                Console.ForegroundColor = ConsoleColor.Green;
                db.Database.ExecuteSqlRaw("INSERT INTO Suppliers VALUES " +
                   "('Norske Meierier','Lviv','Ukraine')");
                var catId = db.Categories.FromSqlRaw("SELECT * FROM Categories WHERE CategoryName LIKE 'Beverages'").FirstOrDefault().CategoryID;
                var supplId = db.Suppliers.FromSqlRaw("SELECT * FROM Suppliers WHERE SupplierName LIKE 'Norske Meierier'").FirstOrDefault().SupplierID;
                db.Database.ExecuteSqlRaw("INSERT INTO Products VALUES " +
                    $"('Green tea',{supplId},{catId},10)");

                var newSuppl = from s in db.Suppliers where s.SupplierName == "Norske Meierier" select s;
                var sp = newSuppl.FirstOrDefault();

                var newProd = from p in db.Products where p.SupplierID == sp.SupplierID select p;

                var pr = newProd.FirstOrDefault();
                Console.WriteLine(pr.ProductID + "\t" + pr.ProductName + "\t" + pr.SupplierID +
                       "\t" + pr.CategoryID + "\t" + pr.Price);
                Console.WriteLine(sp.SupplierID + "\t" + sp.SupplierName + "\t" + sp.City + "\t" + sp.Country);
            }

        }
    }
}
