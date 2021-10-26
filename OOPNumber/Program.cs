using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPNumber
{
    class Program
    {
        static void Main(string[] args)
        {
            Numbers n = new Numbers();
            n.Add();
            n.Display();
            Console.ReadLine();
        }
    }

    class Numbers
    {
        private decimal num;

        public decimal Number
        {
            get { return num; }
            set { num = value; }
        }

        List<Numbers> list = new List<Numbers>();

        public Numbers()
        {

        }

        public Numbers(int num)
        {
            Number = num;
        }

        public Numbers(bool encode)
        {
            Console.Write("{0}) Input a number: ", list.Count + 1);
            Number = Convert.ToInt32(Console.ReadLine());
        }

        public void Add()
        {
            list.Clear();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Numbers(true));
            }
        }

        public void Display()
        {
            Console.WriteLine("Number of positive numbers: {0}", list.Count(f => f.Number > 0));
            Console.WriteLine("Number of negative numbers: {0}", list.Count(f => f.Number < 0));
            Console.WriteLine("Sum of all numbers: {0}", list.Sum(f => f.Number));
            Console.WriteLine("Product of all numbers: {0}", Product()); 
        }

        public decimal Product()
        {
            decimal total = 1;
            list.ForEach(r =>
            {
                total *= r.Number;
            });
            return total;
        }

    }
}
