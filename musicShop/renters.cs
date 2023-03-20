using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicShop
{
    //class that sets up variables of renters
    class renter
    {
        protected string renterFname;
        protected string renterLname;
        protected int insNum;
        
        public renter(string fn, string ln, int n)
        {
            renterFname = fn;
            renterLname = ln;
            insNum = n;
        }

        public string RenterFname
        {
            get { return renterFname; }
            set { renterFname = value; }
        }

        public string RenterLname
        {
            get { return renterLname; }
            set { renterFname = value; }
        }

        public int InsNum
        {
            get { return insNum; }
            set { insNum = value; }
        }
        
    }

    class renters
    {
        List<renter> rent = new List<renter>();
        private string fileName = "renterList.txt";
        string fn, ln;
        public renters()
        {
            if (File.Exists(fileName))
            {
                string[] subs = File.ReadAllLines(fileName);
                for (int i = 0; i < subs.Length; i++)
                {
                    string[] ns = subs[i].Split(',');
                    renter temp = new renter(ns[0], ns[1], Convert.ToInt32(ns[2]) /*Convert.ToInt32(ns[3])*/);
                    rent.Add(temp);
                }
            }
            else
            {
                Console.WriteLine("An error occured!");
            }
        }
        public void rentOut(int n)
        {
            //gets info from user to add it to renter file
            Console.WriteLine("What is your first name?");
            fn = Console.ReadLine();
            Console.WriteLine("What is your last name?");
            ln = Console.ReadLine();
            renter temp = new renter(fn, ln, n);
            //add renter to renter list
            rent.Add(temp);
            saveFile();
        }

        public void returnIns(int i)
        {
            bool checker = false;
            //ensure that renter is the exact one that rented instrument
            while (true)
            {
                Console.WriteLine("What is your first name?");
                fn = Console.ReadLine();
                Console.WriteLine("What is your last name?");
                ln = Console.ReadLine();
                int counter = 0;
                foreach (renter r in rent)
                {
                    counter++;
                    //check that each piece of information given by user exists in files
                    if (r.RenterFname == fn && r.RenterLname == ln && r.InsNum == i)
                    {
                        //removes renter from renter list
                        rent.Remove(r);
                        checker = true;
                        break;
                    }
                    if (counter == rent.Count - 1)
                    {
                        Console.WriteLine("This individual is not currently renting any instrument.\nPlease try again.");
                    }
                }
                if (checker == true)
                {
                    break;
                }
            }
            saveFile();
        }

        private void saveFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < rent.Count; i++)
                {
                    string temp = rent[i].RenterFname + "," + rent[i].RenterLname /*+ "," + Convert.ToInt32(rent[i].RenterLength)*/ + "," + Convert.ToInt32(rent[i].InsNum);
                    writer.WriteLine(temp);
                }
            }
        }
    }


}
