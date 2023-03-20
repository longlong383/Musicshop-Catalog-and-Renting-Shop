using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicShop
{
    class usefulFunction
    {
        public int getInt(string message)
        {
            //get an integer with checks
            int n;
            Console.WriteLine(message);
            while (true)
            {
                try
                {
                    n = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("You need to provide a valid value");
                }
            }
            return (n);
        }
        public int menu(string message, int numOptions)
        {
            int n;
            while (true)
            {
                n = getInt(message);
                if (n < 0 || n > numOptions)
                {
                    Console.WriteLine("This value is not a valid menu option.");
                    continue;
                }
                else
                {
                    break;
                }
            }
            return n;
        }
        public string capitalize(string w)
        {

            string dw = "";
            string[] temp = w.Split(' ');

            foreach (string t in temp)
            {
                dw = dw + " " + char.ToUpper(t.First()) + t.Substring(1).ToLower();
            }
            dw = dw.Trim();

            return dw;
        }

        public double getDouble(string message)
        {
            //get an integer with checks
            double n;
            Console.WriteLine(message);
            while (true)
            {
                try
                {
                    n = Convert.ToDouble(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("You need to provide a valid number");
                }
            }
            return (n);
        }
    }
    //Plan
    /*
    1. Get instruments (objects such as violins, piano and guitar) and lists of instruments
    2.set up first menu possibilities
    3.set up menu options wtihin each list (add, print, delete, etc.)
    4. Set up rent options
    5. Set up return options

    */
}
