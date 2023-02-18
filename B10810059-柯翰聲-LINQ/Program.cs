using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace B10810059_柯翰聲_LINQ
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = ProductCSV("product.csv");
            int pages ;
            do
            {   
                Console.Write("請輸入想要查看的頁碼(1-4)(其他則離開視窗): ");
                string page = Console.ReadLine();
                pages=0;
                if (page == "1")
                {
                    var tota_price = list.Sum((x) => x.Price);
                    Console.WriteLine($"所有商品的總價格為: {tota_price}");//1
                    Console.WriteLine("-------------------");
                    var average_price = list.Average(x => x.Price);
                    Console.WriteLine($"所有商品的平均價格為: {average_price:N2}");//2
                    Console.WriteLine("-------------------");
                    var total_Quantity = list.Sum(x => x.Quantity);
                    Console.WriteLine($"所有商品的總數量為: {tota_price}");//3
                    Console.WriteLine("-------------------");
                    var average_Quantity = list.Average(x => x.Quantity);
                    Console.WriteLine($"所有商品的平均數量為: {average_Quantity:N2}");//4
                    Console.WriteLine("-------------------");
                }
                else if (page == "2")
                {
                    var max_price = list.Max(x => x.Price);
                    var max_name = list.Where(x => x.Price == max_price);
                    foreach (var item in max_name)
                    {
                        Console.WriteLine($"價格最高的商品為: {item.Name}");//5
                    }
                    Console.WriteLine("-------------------");
                    var min_price = list.Min(x => x.Price);
                    var min_name = list.Where(x => x.Price == min_price);
                    foreach (var item in min_name)
                    {
                        Console.WriteLine($"價格最低的商品為: {item.Name}");//6
                    }
                    Console.WriteLine("-------------------");
                    var total_3c = list.Where((x) => x.Category == "3C").Sum((x) => x.Price);
                    Console.WriteLine($"3C商品的總價為: {total_3c}");//7
                    Console.WriteLine("-------------------");
                    var total_drink = list.Where((x) => x.Category == "飲料").Sum((x) => x.Price);
                    var total_food = list.Where((x) => x.Category == "食品").Sum((x) => x.Price);
                    Console.WriteLine($"飲料及食品的總價格為: {total_drink + total_food}");//8
                    Console.WriteLine("-------------------");
                }
                else if (page == "3")
                {
                    var food_100up = list.Where((x) => x.Category == "食品").Where((x) => x.Quantity > 100);

                    Console.Write("食品數量大於100的商品為: ");
                    foreach (var item in food_100up)
                    {
                        Console.Write(item.Name + ", ");//9   
                    }
                    Console.WriteLine();
                    Console.WriteLine("-------------------");
                    var price_1000up = list.Where((x) => x.Price > 1000).GroupBy((x) => x.Category);
                    Console.WriteLine("商品價格大於1000的商品為: ");
                    foreach (var item in price_1000up)
                    {
                        Console.WriteLine(item.Key);
                        foreach (var p in item)
                        {
                            Console.WriteLine(p.Name);//10
                        }
                    }
                    Console.WriteLine("-------------------");
                    var price_1000up_avg =
                        from type in list
                        where type.Price > 1000
                        group type by type.Category into grouping
                        select grouping.Average(x => x.Price);
                    foreach (var item in price_1000up)
                    {
                        Console.WriteLine(item.Key);
                        foreach (var p in price_1000up_avg)
                        {
                            Console.WriteLine($"的平均價格為:{p}");//11

                        }
                    }      
                    Console.WriteLine("-------------------");
                    var high = list.OrderByDescending((x) => x.Price);
                    Console.WriteLine("依照商品價格由高到低的排序為: ");
                    foreach (var p in high)
                    {
                        Console.WriteLine($"{p.Id} {p.Name} {p.Quantity} {p.Price} {p.Category}");//12
                    }
                    Console.WriteLine("-------------------");
                    
                }
                else if(page == "4")
                {
                    var low = list.OrderBy((x) => x.Price);
                    Console.WriteLine("依照商品價格由低到高的排序為: ");
                    foreach (var p in low)
                    {
                        Console.WriteLine($"{p.Id} {p.Name} {p.Quantity} {p.Price} {p.Category}");//13
                    }
                    Console.WriteLine("-------------------");
                    var maxtype =
                                from type in list
                                group type by type.Category into grouping
                                select new
                                {
                                    grouping.Key,
                                    MostExpensiveProducts =
                                  from type2 in grouping
                                  where type2.Price == grouping.Max(type3 =>
                                  type3.Price)
                                  select type2
                                };
                    Console.WriteLine("各類別中最貴的商品為: ");

                    foreach (var item in maxtype)
                    {
                        Console.WriteLine(item.Key + ":");
                        foreach (var p in item.MostExpensiveProducts)
                        {
                            Console.WriteLine(p.Name);//14
                        }
                    }
                    Console.WriteLine("-------------------");
                    var mintype =
                                from type in list
                                group type by type.Category into grouping
                                select new
                                {
                                    grouping.Key,
                                    MostCheapProducts =
                                  from type2 in grouping
                                  where type2.Price == grouping.Min(type3 =>
                                  type3.Price)

                                  select type2
                                };
                    Console.WriteLine("各類別中最便宜的商品為: ");
                    foreach (var item in mintype)
                    {
                        Console.WriteLine(item.Key + ":");
                        foreach (var p in item.MostCheapProducts)
                        {
                            Console.WriteLine(p.Name);//15
                        }
                    }
                    Console.WriteLine("-------------------");
                    var price_10000low = list.Where((x) => x.Price <= 10000);
                    Console.WriteLine("價格小於等於10000的商品為: ");//16
                    foreach (var item in price_10000low)
                    {
                        Console.WriteLine($"{item.Name}");
                    }
                }
                else
                {
                    pages++; 
                }

            }
            while (pages == 0);
            Console.WriteLine("離開視窗");
            Console.ReadLine();
        }   
        
        private static List<Product>ProductCSV(string path)
        {
            return File.ReadAllLines(path)
                .Skip(1)
                .Where(row => row.Length > 0)
                .Select(Product.ParseRow)
                .ToList();
                


        }
        
        





    }
}
