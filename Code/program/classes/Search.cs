namespace Final_project;

public class Search
{
    public static int SearchByID(ref List<Friend> friends , int id)
    {
        int idx = 0;
        foreach (var friend in friends)
        {
            if (friend.id == id)
                return idx;
            idx++;
        }
        return -1;
    }
    
    public static List <int>  SearchByName(ref List<Friend> friends , string name)
    {
        List<int> FriendIndex = new List<int>();
        int idx = 0;
        foreach (var friend in friends)
        {
            if (friend.name == name)
                FriendIndex.Add(idx);
            idx++;
        }
        return FriendIndex;
    }
}