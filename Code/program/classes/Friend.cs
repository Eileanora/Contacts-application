using Newtonsoft.Json;

namespace Final_project;
[Serializable]
public class Friend
{
    public static int Max_ID = 1;
    public int id { get; set; }
    public string name { get; set; }
    public int age { get; set; }
    public string phone_number { get; set; }
    
    
    public Friend(string name, int age, string phone_number)
    {
        this.name = name;
        this.age = age;
        this.phone_number = phone_number;
        id = Max_ID;
        Max_ID++;
    }


    public override string ToString()
    {
        return $"ID: {id}\tName: {name}\tAge: {age}\t\tPhone number: {phone_number}";
    }

    public void print_friend()
    {
        Console.WriteLine(ToString());
    }

    public static bool AddFriend(Friend friend)
    {
        bool Phone = true , Name = true , Age = true;
        if (!Validiation.Validiate_Name(friend.name))
        {
            Console.WriteLine("Invalid Name");
            Name = false;
        }
        if (!Validiation.Validiate_Age( friend.age))
        {
            Console.WriteLine("Invalid age");
            Age = false;
        }
        if (!Validiation.Validiate_PhoneNumber( friend.phone_number))
        {
            Console.WriteLine("Invalid phone number");
            Phone = false;
        }
        if(!Name || !Age || !Phone)
            return false;
        
        if (Validiation.CheckDuplicate(ref Program.friends, friend.name, friend.phone_number))
        {
            Console.WriteLine("Friend already exists");
            return false;
        }
        
        Program.friends.Add(friend);
        // FileManager fileManager= new FileManager("friends.json");
        // fileManager.SaveFriend();
        return true;
    }
    
    public static void UpdateMaxID()
    {
        foreach (var friend in Program.friends)
        {
            if (friend.id >= Max_ID)
                Max_ID = friend.id + 2;
        }
    }
}
