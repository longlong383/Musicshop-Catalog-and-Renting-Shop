using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace musicShop
{
    //abstract class for violin and guitar
    abstract class instruments
    {
        //variables used to differentiate instruments
        protected int yearMade;
        protected string instrument;
        protected string condition;
        protected string manufacturer;
        protected int rentalCost;
        protected int iDNum;
        protected string status;


        public instruments (int y, string ins, string c, string m, int r, int ID, string s)
        {
            yearMade = y;
            instrument = ins;
            condition = c;
            manufacturer = m;
            rentalCost = r;
            iDNum = ID;
            status = s;
        }

        public int YearMade
        {
            get { return yearMade; }    
            set { yearMade = value; }
        }
        public string Instrument
        {
            get { return instrument; }
            set { instrument = value; }
        }
        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }
        public int RentalCost
        {
            get { return rentalCost; }
            set { rentalCost = value; }
        }
        public int IDNum
        {
            get { return iDNum; }
            set { iDNum = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
    }

    //violin class with its specific variables
    class Violin : instruments
    {
        private double size;
        private string violinType;

        public Violin(string ins, int y, string c, string m, int r, int ID, string s, double si, string v) : base(y, ins, c, m, r, ID, s)
        {
            size = si;
            violinType = v;
        }

        public double Size
        {
            get { return size; }
            set { size = value; }
        }

        public string ViolinType
        {
            get { return violinType; }
            set { violinType = value; }
        }

        public void printInfo()
        {                                                                                       //int y1, string ins2, string c3, string m4, double r5, int ID6, string s7, double si8, string v9
            Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", instrument, yearMade, condition, manufacturer, 
                rentalCost,iDNum, status, size, violinType );
        }
    }
    
    //guitar with its specific variables
    class Guitar : instruments
    {
        private string guitarType;
        private string color;
        public Guitar(string ins, int y, string c, string m, int r, int ID, string s, string g, string co) : base(y, ins, c, m, r, ID, s)
        {
            guitarType = g;
            color = co;
        }

        public string GuitarType
        {
            get { return guitarType; }
            set { guitarType = value; }
        }

        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        public void printInfo()
        {
            Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", instrument, yearMade, condition, manufacturer, 
                rentalCost, iDNum, status, color, guitarType);
        }
    }

    class Violins
    {
        List<Violin> vins = new List<Violin>();
        private string fileName = "violinList.txt";
        usefulFunction uf = new usefulFunction();
        renters R = new renters();
        public Violins() 
        {
            //obtaining info from the file for inventory of violins
            if (File.Exists(fileName))
            {
                string[] subs = File.ReadAllLines(fileName);
                for (int i = 0; i < subs.Length; i++)
                {
                    string[] ns = subs[i].Split(',');
                    Violin temp = new Violin(ns[0], Convert.ToInt32(ns[1]), ns[2], ns[3], 
                        Convert.ToInt32(ns[4]), Convert.ToInt32(ns[5]), ns[6], Convert.ToDouble(ns[7]), ns[8]);
                    vins.Add(temp);
                    
                }
            }
            else
            {
                Console.WriteLine("An error has occured!");
            }
        }
        public void printInfo()
        {
            //show inventory of violins
            Console.WriteLine("");
            Console.WriteLine("Violin Inventory Info:");
            Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "Instrument", "Year", "Condition", "Manufacturer", 
                "RentCost/Day","ID Number", "Status", "Size", "Violin Type");
            Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "---------------", "-------", "------------", "-----------", 
                "------------", "----------", "--------", "-------", "-------------");
            foreach (Violin v in vins)
            {
                v.printInfo();
            }
        }

        public void addViolin()
        {
            //adding violin
            //getting all info from user
            string ins, c, m, s, v;
            int y, ID, r;
            double si;
            bool IDchecker = true;
            bool yearChecker = true;
            Console.WriteLine("You are adding a violin to your inventory");
            ins = "Violin";
            //ensure that the year they input is valid with an appropriate length
            while (true)
            {
                y = uf.getInt("Year made:");
                if (y.ToString().Length != 4)
                {
                    Console.WriteLine("Please input a valid year");
                    yearChecker = false;
                }
                if (yearChecker == true)
                {
                    break;
                }
                yearChecker = true;
            }
            Console.WriteLine("Condition of violin:");
            c = Console.ReadLine();
            Console.WriteLine("Manufacturer of violin:");
            m = Console.ReadLine();
            r = uf.getInt("Cost of rent per day:");
            while (true)
            {
                //ensure the ID number is an appropriate length
                ID = uf.getInt("ID number for instrument (must be 4-digits long):");
                if (ID.ToString().Length != 4 )
                {
                    Console.WriteLine("The ID number must be 4-digits long");
                    IDchecker = false;
                }

                //ensure that the ID number hasn't already been taken
                foreach (Violin V in vins)
                {
                    if (ID == V.IDNum)
                    {
                        Console.WriteLine("This ID number has already been taken. Please enter another ID number");
                        IDchecker = false;
                        break;
                    }
                }
                if (IDchecker == true)
                {
                    break;
                }
                IDchecker = true;
            }
            s = "Available";
            si = uf.getInt("Size of violin"); ;
            Console.WriteLine("Type of violin:");
            v = Console.ReadLine();
            Violin newV = new Violin(ins, y, c, m, r, ID, s, si, v);
            vins.Add(newV);
            saveFile();
        }

        public void removeViolin()
        {
            //if there's no violins in inventory
            if (vins.Count == 0)
            {
                Console.WriteLine("There are no violins in inventory");
                return;
            }
            //remove violin
            printInfo();
            int ID = uf.getInt("Enter the ID number of the violin you would like to remove");
            Console.Clear();
            for (int i = 0; i < vins.Count; i++)
            {
                string[] subs = File.ReadAllLines(fileName);
                string[] ns = subs[i].Split(',');
                int idtemp = Convert.ToInt32(ns[5]);
                if (idtemp == ID)
                {
                    vins.Remove(vins[i]);
                }
                else if (i == vins.Count-1)
                {
                    //if the violin the user wants to remove doesn't exist
                    Console.WriteLine("This ID number does not exist");
                }
            }
            saveFile();
        }

        public void editViolin()
        {
            //if there's no violins in inventory
            if (vins.Count == 0)
            {
                Console.WriteLine("There are no violins in inventory");
                return;
            }
            int IDedit;
            bool IDchecker = false;
            int IDcounter = -1;
            //ensure violin that user wants to edit actually exists in inventory
            while (true)
            {
                printInfo();
                Console.WriteLine();
                IDedit = uf.getInt("You are editing a violin. Enter the ID number of the violin you would like to edit:");
                Console.Clear();
                foreach (Violin V in vins)
                {
                    IDcounter++;
                    if (IDedit == V.IDNum)
                    {
                        IDchecker = true;
                        break;
                    }
                }
                if (IDchecker == false)
                {
                    //if violin user want sto edit doesn't exist, this will happen
                    IDcounter = -1;
                    Console.WriteLine("This violin does not exist. Please enter a valid ID number.");
                }
                else
                {
                    break;
                }
            }
            string c, m, v;
            int y, ID, r;
            double si;
            IDchecker = true;
            //boolean to ensure year is an appropriate length
            bool yearChecker = true;
            //boolean to get user out of editing for their specified violin
            bool getout = false;
            Console.Clear();
           
            while (true)
            {
                //show info of selected violin
                Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "Instrument", "Year", "Condition", "Manufacturer",
                "RentCost/Day", "ID Number", "Status", "Size", "Violin Type");
                Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "---------------", "-------", "------------", "-----------",
                    "------------", "----------", "--------", "-------", "-------------");
                vins[IDcounter].printInfo();
                Console.WriteLine();
                string editMessage = "0. Exit Editing\n1. Year Made\n2. Condition\n3. Manufacturer\n4. Cost of Rent per day\n5. ID number\n6. Size\n7. Violin Type";
                int edit = uf.menu(editMessage, 7);
                Console.Clear();
                //editing of variables
                switch (edit)
                {
                    case 0:
                        //exit editing for specified violin
                        getout = true;
                        break;
                    case 1:
                        while (true)
                        {
                            y = uf.getInt("New year made:");
                            if (y.ToString().Length != 4)
                            {
                                Console.WriteLine("Please input a valid year");
                                yearChecker = false;
                            }
                            if (yearChecker == true)
                            {
                                break;
                            }
                            yearChecker = true;
                        }
                        vins[IDcounter].YearMade = y;
                        break;
                    case 2:
                        Console.WriteLine("Condition of violin:");
                        c = Console.ReadLine();
                        vins[IDcounter].Condition = c;
                        break;
                    case 3:
                        Console.WriteLine("Manufacturer of violin:");
                        m = Console.ReadLine();
                        vins[IDcounter].Manufacturer = m;
                        break;
                    case 4:
                        r = uf.getInt("Cost of rent per day:");
                        vins[IDcounter].RentalCost = r;
                        break;
                    case 5:
                        //if the ID number of a instrument is being changed, it mustn't be one of a rented instrument
                        if (vins[IDcounter].Status == "Unavailable")
                        {
                            Console.WriteLine("The instrument is being rented and its ID number cannot be changed");
                            break;
                        }
                        //ensure appropraite paramters for new ID number
                        while (true)
                        {
                            ID = uf.getInt("New ID number for instrument (must be 4-digits long):");
                            //ensure appropraite length
                            if (ID.ToString().Length != 4)
                            {
                                Console.WriteLine("The ID number must be 4-digits long");
                                IDchecker = false;
                            }
                            //ensure ID number hasn't been already been taken
                            foreach (Violin V in vins)
                            {
                                if (ID == V.IDNum)
                                {
                                    Console.WriteLine("This ID number has already been taken. Please enter another ID number");
                                    IDchecker = false;
                                    break;
                                }
                            }
                            if (IDchecker == true)
                            {
                                break;
                            }
                            IDchecker = true;
                        }
                        vins[IDcounter].IDNum = ID;
                        break;
                    case 6:
                        si = uf.getInt("Size of violin");
                        vins[IDcounter].Size = si;
                        break;
                    case 7:
                        Console.WriteLine("Type of violin:");
                        v = Console.ReadLine();
                        vins[IDcounter].ViolinType = v;
                        break;
                }
                if (getout == true)
                {
                    break;
                }
            }
            saveFile();
        }

        public void rentViolin()
        {
            //availability is to ensure that there's an available violin in inventory
            int availability = 0;
            //boolean is to ensure the instrument user wants to rent is valid
            bool valid = false;
            int vioType;
            int costperday = 0;
            while (true)
            {
                //write all available violin instruments
                Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "Instrument", "Year", "Condition", "Manufacturer",
                "RentCost/Day", "ID Number", "Status", "Size", "Violin Type");
                Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "---------------", "-------", "------------", "-----------",
                    "------------", "----------", "--------", "-------", "-------------");
                foreach (Violin v in vins)
                {
                    if (v.Status == "Unavailable")
                    {
                        continue;
                    }
                    else
                    {
                        availability++;
                        v.printInfo();
                    }
                }
                //in case there's no instruments available for rent
                if (availability == 0)
                {
                    Console.WriteLine("There are no violins available for rent");
                    return;
                }
                vioType = uf.getInt("Enter the ID number of the violin you would like to rent:");
                int counter = 0;

                foreach (Violin v in vins)
                {
                    counter++;
                    if (v.IDNum == vioType && v.Status != "Unavailable")
                    {
                        //sets the instrument so it's unavailable
                        v.Status = "Unavailable";
                        costperday = v.RentalCost;
                        //now can get out of loop
                        valid = true;
                        break;
                    }
                    if (counter == vins.Count)
                    {
                        Console.WriteLine("This ID Number does not exist or is not available");
                    }
                }
                if (valid == true)
                {
                    break;
                }
            }
            Console.WriteLine("Please remember the ID number for when you return the instrument"); 
            //determine cost user must pay for renting violin
            string lengthrequest = "How long will you be renting the violin for?";
            int length = uf.getInt(lengthrequest);
            int subtotal = costperday * length;
            double tax = Convert.ToDouble(subtotal) * 0.13;
            double total = Math.Round(tax + subtotal, 2);
            Console.WriteLine($"The total for renting this instrument is {total.ToString("0.00")}$");
            R.rentOut(vioType);
            saveFile();
            Console.Clear();
        }

        public void returnViolin()
        {
            int IDnum;
            //boolean to exit user out of return
            bool valid = false;
            while (true)
            {
                IDnum = uf.getInt("What is the ID number of the instrument you would like to return?");
                Console.Clear();
                //reset rented violin to available
                foreach (Violin v in vins)
                {
                    if (v.IDNum == IDnum && v.Status == "Unavailable")
                    {
                        v.Status = "Available";
                        valid = true;
                        break;
                    }
                }
                if (valid == true)
                {
                    break;
                }
                //if violin that's being returned doesn't exist, they have options to exit
                Console.WriteLine("There's no existence of this instrument in the inventory");
                int getout = uf.menu("Would you like to try again, or return to the menu?" +
                    "\n0. Yes\n1. No", 1);
                if (getout == 0)
                {
                    continue;
                }
                else
                {
                    return;
                }

            }
            R.returnIns(IDnum);
            saveFile();
            Console.Clear();
        }
        private void saveFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < vins.Count; i++)
                {
                    string temp = vins[i].Instrument + "," + vins[i].YearMade + "," + vins[i].Condition +
                        "," + vins[i].Manufacturer + "," + vins[i].RentalCost + "," + vins[i].IDNum + "," + vins[i].Status
                        + "," + vins[i].Size + "," + vins[i].ViolinType;
                    writer.WriteLine(temp);
                }
            }
        }

    }

    //exact same thing with the Violins class, only with guitars
    class Guitars
    {
        List<Guitar> gui = new List<Guitar>();
        private string fileName = "guitarList.txt";
        usefulFunction uf = new usefulFunction();
        renters R = new renters();
        public Guitars()
        {
            if (File.Exists(fileName))
            {
                string[] subs = File.ReadAllLines(fileName);
                for (int i = 0; i < subs.Length; i++)
                {
                    string[] ns = subs[i].Split(',');
                    Guitar temp = new Guitar(ns[0], Convert.ToInt32(ns[1]), ns[2], ns[3],
                        Convert.ToInt32(ns[4]), Convert.ToInt32(ns[5]), ns[6], ns[7], ns[8]);
                    gui.Add(temp);

                }
            }
            else
            {
                Console.WriteLine("An error has occured!");
            }
        }
        public void printInfo()
        {
            
            Console.WriteLine("");
            Console.WriteLine("Guitar Inventory Info:");
            Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "Instrument", "Year", "Condition", "Manufacturer", 
                "RentCost/Day", "ID Number", "Status", "Color", "Guitar Type");
            Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "---------------", "-------", "------------", "-----------", 
                "------------", "----------", "--------", "---------", "-------------");
            foreach (Guitar g in gui)
            {
                g.printInfo();
            }
        }

        public void addGuitar()
        {
            //get all info from user
            string ins, c, m, s, g, co;
            int y, ID, r;
            bool IDchecker = true;
            bool yearChecker = true;
            Console.WriteLine("You are adding a guitar to your inventory");
            ins = "Guitar";
            while (true)
            {
                y = uf.getInt("Year made:");
                if (y.ToString().Length != 4)
                {
                    Console.WriteLine("Please input a valid year");
                    yearChecker = false;
                }
                if (yearChecker == true)
                {
                    break;
                }
                yearChecker = true;
            }
            Console.WriteLine("Condition of guitar:");
            c = Console.ReadLine();
            Console.WriteLine("Manufacturer of guitar:");
            m = Console.ReadLine();
            r = uf.getInt("Cost of rent per day:");
            while (true)
            {
                ID = uf.getInt("ID number for instrument (must be 4-digits long):");
                if (ID.ToString().Length != 4)
                {
                    Console.WriteLine("The ID number must be 4-digits long");
                    IDchecker = false;
                }
                foreach (Guitar G in gui)
                {
                    if (ID == G.IDNum)
                    {
                        Console.WriteLine("This ID number has already been taken. Please enter another ID number");
                        IDchecker = false;
                        break;
                    }
                }
                if (IDchecker == true)
                {
                    break;
                }
                IDchecker = true;
            }
            s = "Available";
            Console.WriteLine("Type of guitar:");
            g = Console.ReadLine();
            Console.WriteLine("Color of guitar:");
            co = Console.ReadLine();
            Guitar newG = new Guitar(ins, y, c, m, r, ID, s, g, co);
            gui.Add(newG);
            saveFile();
        }

        public void removeGuitar()
        {
            //if there's no guitars in the inventory
            if (gui.Count == 0)
            {
                Console.WriteLine("There are no guitars in inventory");
                return;
            }
            printInfo();
            int ID = uf.getInt("Enter the ID number of the guitar you would like to remove");
            for (int i = 0; i < gui.Count; i++)
            {
                string[] subs = File.ReadAllLines(fileName);
                string[] ns = subs[i].Split(',');
                int idtemp = Convert.ToInt32(ns[5]);
                if (idtemp == ID)
                {
                    gui.Remove(gui[i]);
                }
                else if (i == gui.Count - 1)
                {
                    Console.WriteLine("This ID number does not exist");
                }
            }
            saveFile();
        }

        public void editGuitar()
        {
            //if there's no guitars in the inventory
            if (gui.Count == 0)
            {
                Console.WriteLine("There are no guitars in inventory");
                return;
            }
            int IDedit;
            bool IDchecker = false;
            int IDcounter = -1;
            while (true)
            {
                printInfo();
                Console.WriteLine();
                IDedit = uf.getInt("You are editing a guitar. Enter the ID number of the guitar you would like to edit:");
                Console.Clear();
                foreach (Guitar G in gui)
                {
                    IDcounter++;
                    if (IDedit == G.IDNum)
                    {
                        IDchecker = true;
                        break;
                    }
                }
                if (IDchecker == false)
                {
                    Console.Clear();
                    IDcounter = -1;
                    Console.WriteLine("This guitar does not exist. Please enter a valid ID number.");
                }
                else
                {
                    break;
                }
            }
            string c, m, g, co;
            int y, ID, r;
            IDchecker = true;
            bool yearChecker = true;
            bool getout = false;
            Console.Clear();

            while (true)
            {
                Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "Instrument", "Year", "Condition", "Manufacturer",
                "RentCost/Day", "ID Number", "Status", "Color", "Guitar Type");
                Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "---------------", "-------", "------------", "-----------",
                    "------------", "----------", "--------", "---------", "-------------");
                gui[IDcounter].printInfo();
                Console.WriteLine();
                string editMessage = "0. Exit Editing\n1. Year Made\n2. Condition\n3. Manufacturer\n4. Cost of Rent per day\n5. ID number\n6. Color\n7. Guitar Type";
                int edit = uf.menu(editMessage, 7);
                Console.Clear();
                switch (edit)
                {
                    case 0:
                        getout = true;
                        break;
                    case 1:
                        while (true)
                        {
                            y = uf.getInt("New year made:");
                            if (y.ToString().Length != 4)
                            {
                                Console.WriteLine("Please input a valid year");
                                yearChecker = false;
                            }
                            if (yearChecker == true)
                            {
                                break;
                            }
                            yearChecker = true;
                        }
                        gui[IDcounter].YearMade = y;
                        break;
                    case 2:
                        Console.WriteLine("Condition of guitar:");
                        c = Console.ReadLine();
                        gui[IDcounter].Condition = c;
                        break;
                    case 3:
                        Console.WriteLine("Manufacturer of guitar:");
                        m = Console.ReadLine();
                        gui[IDcounter].Manufacturer = m;
                        break;
                    case 4:
                        r = uf.getInt("Cost of rent per day:");
                        gui[IDcounter].RentalCost = r;
                        break;
                    case 5:
                        //if the ID number of a instrument is being changed, it mustn't be one of a rented instrument
                        if (gui[IDcounter].Status == "Unavailable")
                        {
                            Console.WriteLine("The instrument is being rented and its ID number cannot be changed");
                            break;
                        }
                        while (true)
                        {
                            ID = uf.getInt("New ID number for instrument (must be 4-digits long):");
                            if (ID.ToString().Length != 4)
                            {
                                Console.WriteLine("The ID number must be 4-digits long");
                                IDchecker = false;
                            }
                            foreach (Guitar G in gui)
                            {
                                if (ID == G.IDNum)
                                {
                                    Console.WriteLine("This ID number has already been taken. Please enter another ID number");
                                    IDchecker = false;
                                    break;
                                }
                            }
                            if (IDchecker == true)
                            {
                                break;
                            }
                            IDchecker = true;
                        }
                        gui[IDcounter].IDNum = ID;
                        break;
                    case 6:
                        Console.WriteLine("New color of guitar:");
                        co = Console.ReadLine();
                        gui[IDcounter].Color = co;
                        break;
                    case 7:
                        Console.WriteLine("Type of guitar:");
                        g = Console.ReadLine();
                        gui[IDcounter].GuitarType = g;
                        break;
                }
                if (getout == true)
                {
                    break;
                }
            }
            saveFile();
        }

        public void rentGuitar()
        {
            int availability = 0;
            bool valid = false;
            int guiType;
            int costperday = 0;
            while (true)
            {
                //write all available guitar instruments
                Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "Instrument", "Year", "Condition", "Manufacturer",
                "RentCost/Day", "ID Number", "Status", "Color", "Guitar Type");
                Console.WriteLine("{0, -20}{1, -10}{2,-15}{3, -15}{4, -15}{5, -15}{6, -15}{7, -10}{8, -10}", "---------------", "-------", "------------", "-----------",
                    "------------", "----------", "--------", "---------", "-------------");
                foreach (Guitar G in gui)
                {
                    if (G.Status == "Unavailable")
                    {
                        continue;
                    }
                    else
                    {
                        availability++;
                        G.printInfo();
                    }
                }
                //if there's no instruments available for rent
                if (availability == 0)
                {
                    Console.WriteLine("There are no guitars available for rent");
                    return;
                }
                guiType = uf.getInt("Enter the ID number of the violin you would like to rent:");
                int counter = 0;

                foreach (Guitar G in gui)
                {
                    counter++;
                    if (G.IDNum == guiType && G.Status != "Unavailable")
                    {
                        //sets the instrument so it's unavailable
                        G.Status = "Unavailable";
                        costperday = G.RentalCost;
                        valid = true;
                        break;
                    }
                    if (counter == gui.Count)
                    {
                        Console.WriteLine("This ID Number does not exist or is not available");
                    }
                }
                if (valid == true)
                {
                    break;
                }
            }
            Console.WriteLine("Please remember the ID number for when you return the instrument");
            string lengthrequest = "How long will you be renting the violin for?";
            int length = uf.getInt(lengthrequest);
            int subtotal = costperday * length;
            double tax = Convert.ToDouble(subtotal) * 0.13;
            double total = Math.Round(tax + subtotal, 2);
            Console.WriteLine($"The total for renting this instrument is {total.ToString("0.00")}$");
            R.rentOut(guiType);
            saveFile();
            Console.Clear();
        }

        public void returnGuitar()
        {
            int IDnum;
            bool valid = false;
            while (true)
            {
                IDnum = uf.getInt("What is the ID number of the instrument you would like to return?");
                Console.Clear();
                foreach (Guitar G in gui)
                {
                    if (G.IDNum == IDnum && G.Status == "Unavailable")
                    {
                        G.Status = "Available";
                        valid = true;
                        break;
                    }
                }
                if (valid == true)
                {
                    break;
                }
                //if guitar that's being returned doesn't exist, they have options to exit out
                Console.WriteLine("There's no existence of this instrument in the inventory");
                int getout = uf.menu("Would you like to try again, or return to the menu?" +
                    "\n0. Yes\n1. No", 1);
                if (getout == 0)
                {
                    continue;
                }
                else
                {
                    return;
                }
            }
            R.returnIns(IDnum);
            saveFile();
            Console.Clear();
        }
        private void saveFile()
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < gui.Count; i++)
                {
                    string temp = gui[i].Instrument + "," + gui[i].YearMade + "," + gui[i].Condition +
                        "," + gui[i].Manufacturer + "," + gui[i].RentalCost + "," + gui[i].IDNum + "," + gui[i].Status
                        + "," + gui[i].Color + "," + gui[i].GuitarType;
                    writer.WriteLine(temp);
                }
            }
        }

    }

}
