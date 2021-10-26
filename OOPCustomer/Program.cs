using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOPCustomer
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer c = new Customer();
            int option = 0;
            do
            {
                Console.Clear();
                Console.Write("1) Add Customer\n2) View all customer\n3) Exist\n\nSelected Option: ");
                option = Convert.ToInt32(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        try
                        {
                            c.Add(new Customer(true));
                        }
                        catch (Exception e)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine(e.Message);
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                        Console.ReadLine();
                        break;
                    case 2:
                        Console.BackgroundColor = ConsoleColor.DarkRed;
                        Console.WriteLine($"\n|{"Name",-20}|{"Address",-45}|{"Number",-11}|{"Qty",-5}|");
                        Console.BackgroundColor = ConsoleColor.Black;
                        c.List().ForEach(r =>
                        {
                            Console.WriteLine($"|{r.Name,-20}|{r.Address,-45}|{r.Number,-11}|{r.Qty,-5}|");
                        });
                        Console.WriteLine("\n\nPress any key to continue...");
                        Console.ReadLine();
                        break;
                    case 3:
                        option = 3;
                        break;
                    default:
                        break;
                }
            } while (option != 3);
        }
    }

    class Customer
    {
        private string name;

        public string Name
        {
            get { return name?.TrimStart().TrimEnd(); }
            set
            {
                if (value.Length <= 50)
                {
                    name = value;
                }
                else
                {
                    throw new Exception("Value must not exceed 50 character!");
                }
            }
        }

        private string address;

        public string Address
        {
            get { return address?.TrimStart().TrimEnd(); }
            set
            {
                if (value.Length <= 100)
                {
                    address = value;
                }
                else
                {
                    throw new Exception("Value must not exceed 100 character!");
                }
            }
        }

        private string number;

        public string Number
        {
            get { return number?.TrimStart().TrimEnd(); }
            set
            {
                if (value.Length == 11)
                {
                    number = value;
                }
                else
                {
                    throw new Exception("The customer telephone number must have a length of 11 digits!");
                }
            }
        }

        private int? qty;

        public int? Qty
        {
            get { return qty; }
            set
            {
                if (value > 1 && value <= 100)
                {
                    qty = value;
                }
                else
                {
                    throw new Exception("The quantity must be a positive number not more than 100 pcs.");
                }
            }
        }

        public Customer()
        {

        }

        public Customer(bool encode)
        {
            Console.Write("Customer Name: ");
            Name = Console.ReadLine();
            Console.Write("Customer Address: ");
            Address = Console.ReadLine();
            Console.Write("Customer Telephone Number: ");
            Number = Console.ReadLine();
            Console.Write("Customer Qty: ");
            Qty = (int)Convert.ChangeType(Console.ReadLine(), typeof(int));
        }

        List<Customer> InitializeData()
        {
            var output = new List<Customer>();
            var lines = File.ReadAllText("Customer.txt").Split('\n', '\r');
            foreach (var s in lines)
            {
                if (!string.IsNullOrEmpty(s))
                {
                    var item = s.Split(';');
                    output.Add(new Customer
                    {
                        Name = item[0],
                        Address = item[1],
                        Number = item[2],
                        Qty = string.IsNullOrWhiteSpace(item[3]) ? 0 : Convert.ToInt32(item[3])
                    });
                }
            }
            return output;
        }

        public List<Customer> List()
        {
            return InitializeData();
        }

        public void Add(Customer obj)
        {
            using (var fs = new FileStream("Customer.txt", FileMode.Append, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"{obj.Name,-50};{obj.Address,-100};{obj.Number,-11};{obj.Qty,-5};");
                }
            }
        }
    }
}
