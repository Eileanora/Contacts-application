namespace Final_project 
{
    internal class Program
    {

        static FileManager file = new FileManager("friends.json");
        public static List<Friend> friends = new List<Friend>();

        static void Main(string[] args)
        {
            file.LoadFriends(ref friends);
            Display_Operations();
        }
        static void Display_Operations()
        {
            Console.Clear();
            Console.WriteLine("Select operation:");
            Console.WriteLine("1. Add friend");
            Console.WriteLine("2. Edit friend phone number");
            Console.WriteLine("3. Print friend(s)");
            Console.WriteLine("4. Delete friend(s)");
            Console.WriteLine("5. Read from file");
            Console.WriteLine("6. Save to file");
            Console.WriteLine("7. Save and Exit");
            Select_Operation();
        }

        static void Select_Operation()
        {
            string operation = Console.ReadLine();
            switch (operation)
            {
                case "1":
                    Add_Friend();
                    break;
                case "2":
                    Edit_Friend_PhoneNumber();  // by id or name
                    break;
                case "3":
                    Print_Friends();
                    break;
                case "4":
                    Delete_Friends();
                    break;
                case "5":
                    Read_From_File();
                    break;
                case "6":
                    Save_To_File();
                    break;
                case "7":
                Save_And_Exit();
                break;
                default:
                    Console.WriteLine("Invalid operation");
                    Thread.Sleep(1000);
                    Display_Operations();
                    break;
            }

        }
        
        static void ReConfermation(Action func)
        {
            string ans = Console.ReadLine();
            if (ans == "y")
                func();
            else
                Display_Operations();
        }
        
        static void CanDoOperation(ref List<Friend> mylist,  string operation)
        {
            if (mylist.Count == 0)
            {
                Console.WriteLine($"No friends to {operation}\n");
                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Display_Operations();
            }
        }
        static void Add_Friend()
        {
            Console.Clear();
            string name, phoneNumber;
            int age = 140;

            Console.WriteLine("Enter friend name");
            name = Console.ReadLine();

            Console.WriteLine("Enter friend age");
            int.TryParse(Console.ReadLine() , out age);

            Console.WriteLine("Enter friend phone number");
            Console.WriteLine("Phone number must be at least 5 digits");
            phoneNumber = Console.ReadLine();

            bool isSaved = Friend.AddFriend(new Friend(name, age, phoneNumber));
            // friend data are in invalid format
            if (!isSaved)
            {
                Console.WriteLine("Press y if you want to try again or any other key to return to main menu");
                Friend.Max_ID--;
                ReConfermation(Add_Friend);
            }
            
            // if successfull
            Console.WriteLine("Saved!");
            Thread.Sleep(1000);
            Console.Clear();
                
               Console.WriteLine("Press y if you want to add another friend");
                ReConfermation(Add_Friend);
        }

        static void Print_Friends()
        {
            Console.Clear();
            CanDoOperation(ref friends , "print");
            
            Console.WriteLine("1. Print all friends");
            Console.WriteLine("2. Print friends by ID");
            Console.WriteLine("3. Print friends by name");
            
            string operation = Console.ReadLine();
            switch (operation)
            {
                case "1":
                    Print_All_Friends();
                    break;
                case "2":
                    Print_Friend_By_Id();
                    break;
                case "3":
                    Print_Friend_By_Name();
                    break;
                default:
                    Console.WriteLine("Invalid operation");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Press y if you want to try again or any other key to return to main menu");
                    ReConfermation(Print_Friends);
                    break;
            }
        }
        
        static void Print_All_Friends()
        {
            Console.Clear();
            foreach (var friend in friends)
            {
                friend.print_friend();
            }
            Console.WriteLine("Press y to print another friend otherwise press any key to return to main menu");
            ReConfermation(Print_Friends);
        }
        

        static void Print_Friend_By_Id()
        {
            int id = -1;
            Console.Clear();
            Console.WriteLine("Enter friend id");
            int.TryParse(Console.ReadLine() , out id);

            if (!Validiation.ValidiateID( id))
            {
                string ans;
                Console.WriteLine("Invalid ID");
                Console.WriteLine("Press y if you wish to re-enter ID");
                ReConfermation(Print_Friend_By_Id);
            }

            int FriendIDX = Search.SearchByID(ref friends, id);
            if(FriendIDX == -1)
                Console.WriteLine("Friend not found");
            else
                friends[FriendIDX].print_friend();
                
            Console.WriteLine("Press y if you wish to print another friend");
            ReConfermation(Print_Friends);
        }

        static void Print_Friend_By_Name()
        {
            Console.Clear();
            Console.WriteLine("Enter friend name");
            string name = Console.ReadLine();
            if(!Validiation.Validiate_Name(name))
            {
                Console.WriteLine("Invalid name");
                Console.WriteLine("Press y if you wish to re-enter name");
                ReConfermation(Print_Friend_By_Name);
            }
            
            List<int> FriendIndex = Search.SearchByName(ref friends, name);
            if(FriendIndex.Count == 0)
                    Console.WriteLine("Friend not found");
            else {
                Console.WriteLine($"Found {FriendIndex.Count} friend(s)!");
                foreach (var idx in FriendIndex)
                    friends[idx].print_friend();
            }
            
            Console.WriteLine("Press y if you wish to print another friend");
            ReConfermation(Print_Friends);
        }

        static void Delete_Friends()
        {
            Console.Clear();
            
            CanDoOperation(ref friends , "delete");
            
            Console.WriteLine("1. Delete all friends");
            Console.WriteLine("2. Delete friend by ID");
            Console.WriteLine("3. Delete friend by name");
            
            string operation = Console.ReadLine();
            switch (operation)
            {
                case "1":
                    Delete_All_Friends();
                    break;
                case "2":
                    Delete_Friend_By_Id();
                    break;
                case "3":
                    Delete_Friend_By_Name();
                    break;
                default:
                    Console.WriteLine("Invalid operation");
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Press y if you want to try again or any other key to return to main menu");
                    ReConfermation(Delete_Friends);
                    break;
            }
        }

        static void Delete_All_Friends()
        {
            Console.Clear();
            Console.WriteLine("Are you sure you want to delete all friends?");
            Console.WriteLine("To proceed press y otherwise press any key");

            string ans;
            ans = Console.ReadLine();
            if (ans == "y")
            {
                friends.Clear();
                Console.WriteLine("All friends deleted Successfully");
                Thread.Sleep(1000);
                Display_Operations();
            }                
        }
        
        static void Delete_Friend_By_Id()
        {
            int id = -1;
            Console.Clear();

            Console.WriteLine("Enter friend id");
            int.TryParse(Console.ReadLine() , out id);

            if (!Validiation.ValidiateID( id))
            {
                string ans;
                Console.WriteLine("Invalid ID");
                Console.WriteLine("Press y if you wish to re-enter ID");
                ReConfermation(Delete_Friend_By_Id);
                
            }
            
            int FriendIDX = Search.SearchByID(ref friends, id);
            if (FriendIDX == -1)
                Console.WriteLine("Friend not found");
            else
            {
                friends[FriendIDX].print_friend();
                // confirmation
                Console.WriteLine("Are you sure you want to delete this friend? to delete press y");
                string ans = Console.ReadLine();
                if (ans != "y")
                    Delete_Friends();
                
                // delete friend
                friends.RemoveAt(FriendIDX);
                Console.WriteLine("Friend deleted successfully");
                Thread.Sleep(1000);
                Console.Clear();
            }
            
            Console.WriteLine("Press y if you wish to delete another friend");
            ReConfermation(Delete_Friends);
        }

        static void Delete_Friend_By_Name()
        {
            Console.Clear();

            Console.WriteLine("Enter friend name");
            string name = Console.ReadLine();
            if(!Validiation.Validiate_Name(name))
            {
                Console.WriteLine("Invalid name");
                Console.WriteLine("Press y if you wish to re-enter name");
                ReConfermation(Delete_Friend_By_Name);
            }
            
            List<int> FriendIndex = Search.SearchByName(ref friends, name);
            if(FriendIndex.Count == 0)
                Console.WriteLine("Friend not found");
            else 
            {
                Console.WriteLine($"Found {FriendIndex.Count} friend(s)");

                for (int i = 0; i < FriendIndex.Count; i++)
                {
                    Console.Write(i + 1 + " - ");
                    friends[FriendIndex[i]].print_friend();
                }

                Console.WriteLine("Select friend to delete or if you want to delete all press 0");
                int idx = -1; 
                int.TryParse(Console.ReadLine(), out idx);
                if (idx == -1 || !Validiation.ValidiateID(idx))
                {
                    Console.WriteLine("Invalid ID");
                    Console.WriteLine("Press y if you wish to re-enter friend name");
                    ReConfermation(Delete_Friend_By_Name);
                }
                else if (idx == 0)
                {
                    Console.WriteLine("Are you sure you want to delete all friends displayed? to delete press y");
                    string ans = Console.ReadLine();
                    if (ans != "y")
                        Display_Operations();

                    // delete friends
                    friends.RemoveAll(f => f.name == name);

                    Console.WriteLine("Friends deleted successfully");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine("Are you sure you want to delete this friend? to delete press y");
                    string ans = Console.ReadLine();
                    if (ans != "y")
                        Display_Operations();

                    // delete friend
                    friends.RemoveAt(--idx);
                    Console.WriteLine("Friend deleted successfully");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
            }
            
            Console.WriteLine("Press y if you wish to delete another friend");
            ReConfermation(Delete_Friends);
        }

        static void Edit_Friend_PhoneNumber()
        {
            Console.Clear();
            CanDoOperation(ref friends , "edit");

            Console.WriteLine("1- Edit friend phone number by ID");
            Console.WriteLine("2- Edit friend phone number by name");
            int choice = -1;
            int.TryParse(Console.ReadLine(), out choice);
            if (choice == -1 || choice > 2 || choice < 1)
            {
                Console.WriteLine("Invalid choice");
                Console.WriteLine("Press y if you wish to re-enter choice");
                ReConfermation(Edit_Friend_PhoneNumber);
            }
            else if (choice == 1)
                Edit_Friend_PhoneNumber_By_ID();
            else
                Edit_Friend_PhoneNumber_By_Name();
        }

        static void Edit_Friend_PhoneNumber_By_ID()
        {
            Console.Clear();
            if(friends.Count == 0)
            {
                Console.WriteLine("No friends to edit");
                Thread.Sleep(1000);
                Console.Clear();
                Display_Operations();
            }
            
            Console.WriteLine("Enter friend id");
            int id = -1;
            int.TryParse(Console.ReadLine(), out id);
            if (!Validiation.ValidiateID(id))
            {
                Console.WriteLine("Invalid ID");
                Console.WriteLine("Press y if you wish to re-enter friend id");
                ReConfermation(Edit_Friend_PhoneNumber_By_ID);
            }
            
            int FriendIDX = Search.SearchByID(ref friends, id);
            if (FriendIDX == -1)
                Console.WriteLine("Friend not found");
            else
            {
                friends[FriendIDX].print_friend();
                Console.WriteLine("Enter new phone number");
                string phone = Console.ReadLine();
                if (!Validiation.Validiate_PhoneNumber(phone))
                {
                    Console.WriteLine("Invalid phone number");
                    Console.WriteLine("Press y if you wish to re-enter phone number");
                    ReConfermation(Edit_Friend_PhoneNumber_By_ID);
                }
                else
                {
                    friends[FriendIDX].phone_number = phone;
                    Console.WriteLine("Phone number edited successfully!");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                Console.WriteLine("Press y if you wish to edit another friend");
                ReConfermation(Edit_Friend_PhoneNumber_By_ID);
            }
        }

        static void Edit_Friend_PhoneNumber_By_Name()
        {
            Console.Clear();
            Console.WriteLine("Enter friend name");
            string name = Console.ReadLine();
            
            if(!Validiation.Validiate_Name(name))
            {
                Console.WriteLine("Invalid name");
                ReConfermation(Edit_Friend_PhoneNumber_By_Name);
            }
            
            // search for friend
            List<int> FriendIndex = Search.SearchByName(ref friends, name);
            if (FriendIndex.Count == 0)
            {
                Console.WriteLine("Friend not found");
                ReConfermation(Edit_Friend_PhoneNumber_By_Name);
            }
            // Display friends
            for(int i = 0; i < FriendIndex.Count; i++)
            {
                Console.Write(i + 1 + " - ");
                friends[FriendIndex[i]].print_friend();
            }
            Console.WriteLine("Select friend to edit");
            int idx = -1;
            int.TryParse(Console.ReadLine(), out idx);
            if (idx == -1 || !Validiation.ValidiateID(idx))
            {
                Console.WriteLine("Invalid ID");
                Console.WriteLine("Press y if you wish to re-enter friend name");
                ReConfermation(Edit_Friend_PhoneNumber_By_Name);
            }
            else
            {
                Console.WriteLine("Enter new phone number");
                string phone = Console.ReadLine();
                if (!Validiation.Validiate_PhoneNumber(phone))
                {
                    Console.WriteLine("Invalid phone number");
                    Console.WriteLine("Press y if you wish to re-enter phone number");
                    ReConfermation(Edit_Friend_PhoneNumber_By_Name);
                }
                else
                {
                    friends[FriendIndex[--idx]].phone_number = phone;
                    Console.WriteLine("Phone number edited successfully!");
                    Thread.Sleep(1000);
                    Console.Clear();
                }
                Console.WriteLine("Press y if you wish to edit another friend");
                ReConfermation(Edit_Friend_PhoneNumber_By_Name);
            }

        }

        static void Read_From_File()
        {
            Console.Clear();
            Console.WriteLine("Loading friends from file...");
            Thread.Sleep(1000);
            Console.Clear();

            List<Friend> temp = new List<Friend>();
            
            file.LoadFriends(ref temp);
            CanDoOperation(ref temp, "read");
            foreach (var friend  in temp)
            {
                friend.print_friend();
            }
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
            Display_Operations();
        }
        
        static void Save_To_File()
        {
            Console.Clear();
            Console.WriteLine("Saving friends to file...");
            Thread.Sleep(1000);
            file.SaveFriend();
            Console.WriteLine("Friends saved successfully");
            Thread.Sleep(1000);
            Console.Clear();
            Display_Operations();
        }
        static void Save_And_Exit()
        {
            Console.Clear();
            Console.WriteLine("Saving friends to file...");
            Thread.Sleep(1000);
            file.SaveFriend();
            Console.WriteLine("Friends saved successfully");
            Thread.Sleep(1000);
            Console.Clear();
            Environment.Exit(0);
        }
    }
}