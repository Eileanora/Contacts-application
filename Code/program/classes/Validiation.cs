namespace Final_project;

public class Validiation
{
    public static bool Validiate_Name( string name)
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length <= 1)
            return false;
        foreach(var c in name)
            if (!char.IsLetter(c))
                return false;
        return true;
    }
    

    public static bool Validiate_Age( int age) {
        return age is >= 1 and <= 120;
    }

    public static bool Validiate_PhoneNumber( string phone_number)
    {
        if (string.IsNullOrWhiteSpace(phone_number) || phone_number.Length < 5)
            return false;
        foreach(var c in phone_number)
            if (!char.IsDigit(c))
                return false;
        return true;
    }

    static public bool ValidiateID( int id)
    {
        return id >= 0 && id < Friend.Max_ID;
    }
    
    
    public static bool CheckDuplicate(ref List<Friend> friends, string name , string phone_number)
    {
        foreach (var friend in friends)
        {
            if (friend.name == name && friend.phone_number == phone_number)
                return true;
        }
        return false;
    }
}