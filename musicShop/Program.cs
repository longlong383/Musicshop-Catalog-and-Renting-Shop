using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicShop
{
    internal class Program
    {
        //violin inventory menu selection options
        static void violin(Violins V)
        {
            usefulFunction uf = new usefulFunction();
            //boolean that's activated when user wants to get out of violin inventory
            bool violin = false;
            while (true)
            {
                Console.WriteLine("You are in the violin inventory");
                Console.WriteLine(); 
                string menuMessage01 = "0. Exit Violin Inventory\n1. Add Violin\n2. Delete Violin\n3. Edit Violin";
                int menuOptions01 = uf.menu(menuMessage01, 3);
                Console.Clear();
                switch (menuOptions01)
                {
                    case 0:
                        //gets user out of violin inventory
                        violin = true;
                        break;
                    case 1:
                        //violin is added
                        V.addViolin();
                        break;
                    case 2:
                        //violin is removed
                        V.removeViolin();
                        break;
                    case 3:
                        //violin is edited
                        V.editViolin();
                        break;
                }
                if (violin == true)
                {
                    break;
                }
            }

        }
        static void guitar(Guitars G)
        {
            //similar code to violin inventory, only with guitars
            usefulFunction uf = new usefulFunction();
            bool guitar = false;
            while (true)
            {
                Console.WriteLine("You are in the Guitar inventory");
                Console.WriteLine();
                string menuMessage01 = "0. Exit Guitar Inventory\n1. Add Guitar\n2. Delete Guitar\n3. Edit Guitar";
                int menuOptions01 = uf.menu(menuMessage01, 3);
                Console.Clear();
                switch (menuOptions01)
                {
                    case 0:
                        guitar = true;
                        break;
                    case 1:
                        G.addGuitar();
                        break;
                    case 2:
                        G.removeGuitar();
                        break;
                    case 3:
                        G.editGuitar();
                        break;
                }
                if (guitar == true)
                {
                    break;
                }
            }

        }
        static void Main(string[] args)
        {
            usefulFunction uf = new usefulFunction();
            Violins V = new Violins();
            Guitars G = new Guitars();

            while (true)
            {
                Console.WriteLine("Welcome to Bob's Badgers Violin and Guitar Rental Shop!");
                Console.WriteLine();
                string menu01 = "0. Quit\n1. Rent Instrument\n2. Return Instrument\n3. Show inventory\n4. Edit Inventory";
                int options01 = uf.menu(menu01, 5);
                Console.Clear();
                if (options01 == 0)
                {
                    //quit out of shop entirely
                    break;
                } 
                else if (options01 == 1)
                {
                    //rental shop section 
                    while (true)
                    {
                        string menu11 = "What Instrument would you like to rent?\n\n0. Exit Rental\n1. Violin\n2. Guitar";
                        int options11 = uf.menu(menu11, 2);
                        Console.Clear();
                        if (options11 == 0)
                        {
                            break;
                        }
                        else if (options11 == 1)
                        {
                            V.rentViolin();
                        }
                        else if (options11 == 2)
                        {
                            G.rentGuitar();
                        }
                    }
                    Console.Clear();

                }
                else if (options01 == 2)
                {
                    //return section of shop
                    while (true)
                    {
                        string menu21 = "What Instrument would you like to return?\n\n0. Exit Return \n1. Violin \n2. Guitar";
                        int options21 = uf.menu(menu21, 2);
                        Console.Clear();
                        if (options21 == 0)
                        {
                            break;
                        }
                        else if (options21 == 1)
                        {
                            V.returnViolin();
                        }
                        else if (options21==2)
                        {
                            G.returnGuitar();
                        }
                    }
                    Console.Clear();
                } 
                else if (options01 == 3)
                {
                    //inventory display of all instruments available and unavailable
                    Console.Clear();
                    Console.WriteLine("Here's all instruments in stock");
                    V.printInfo();
                    G.printInfo();
                    Console.WriteLine();
                }
                else if (options01 == 4)
                {
                    //editing of inventory of both violin and guitar
                    while (true)
                    {
                        string menu41 = "Please choose which instrument's inventory " +
                            "you would like to enter\n\n0. Exit \n1. Violins\n2. Guitars";
                        int options41 = uf.menu(menu41, 2);
                        Console.Clear();
                        if (options41 == 0)
                        {
                            break;
                        }
                        else if (options41 == 1)
                        {
                            violin(V);
                        }
                        else if (options41 == 2)
                        {
                            guitar(G);
                        }
                    }
                    Console.Clear();
                }
               
            }

            Console.Clear();
            Console.WriteLine("Have a nice day");
            Console.ReadKey();
        }
    }
}
