using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1A2B
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] number_rbs = { 0, 0, 0, 0 };//設定亂數的陣列
            Random number_rb = new Random();//設定隨機亂數
            int A = 0;//設定幾A幾B中A的值
            int B = 0;//設定幾A幾B中B的值
            string ans = "";//設定回答的變數

            do //整個遊戲得開始
            {
                Console.WriteLine("歡迎來到1A2B猜數字的遊戲~");

                for (int i = 0; i < 4; i++)//設定4個亂數
                {
                    number_rbs[i] = number_rb.Next(0, 10);

                    for (int j = 0; j < i; j++)
                    {
                        while (number_rbs[j] == number_rbs[i])//判斷亂數是否重複
                        {
                            j = 0;
                            number_rbs[i] = number_rb.Next(0, 10);
                        }
                    }
                    Console.WriteLine(number_rbs[i]);

                }
                
                do
                {
                    int[] numbers = { 0, 0, 0, 0 };//設定輸入數字的陣列
                    Console.Write("請輸入4個數字: ");
                    int number = Convert.ToInt32(Console.ReadLine());//輸入數字
                    A = 0;
                    B = 0;
                    int z= 0;
                    numbers[0] = number / 1000;
                    numbers[1] = (number % 1000) / 100;
                    numbers[2] = (number % 100) / 10;
                    numbers[3] = number % 10;
                    List<int> list_ans = new List<int>(numbers);
                    List<int> list_rb = new List<int>(number_rbs);
                    var same = list_ans.Intersect(list_rb);
                    if (list_ans.ElementAt(0) == list_rb.ElementAt(0)) 
                    {
                        A++;

                    }
                    if (list_ans.ElementAt(1) == list_rb.ElementAt(1))
                    {
                        A++;

                    }
                    if (list_ans.ElementAt(2) == list_rb.ElementAt(2))
                    {
                        A++;

                    }
                    if (list_ans.ElementAt(3) == list_rb.ElementAt(3))
                    {
                        A++;

                    }
                    if (same.Count() > 0)
                    {
                        B=same.Count()-A;
                    }




                    
                    
                    
                    Console.WriteLine($"判斷結果為{A}A{B}B");
                    Console.WriteLine("--------");
                }
                while (A < 4);
                Console.WriteLine("恭喜你!猜對了!!");
                Console.Write("你要繼續玩嗎?(y/n): ");
                ans = Console.ReadLine();
                if (ans != "n")
                {
                    Console.WriteLine("請輸入(y/n): ");
                    ans = Console.ReadLine();
                }

            }
            while (ans == "y");
            Console.WriteLine("遊戲結束,下次再來玩喔~");

            Console.ReadLine();
        }
    }
    }


