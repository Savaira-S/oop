namespace PersonalFinanceTracker_Layered.Models
{
    public class User
    {
        public string Name;
        public string Password;
        public float Budget;
        public Transaction[] Transactions = new Transaction[100];
        public int Count = 0;

        public User(string name, string password)
        {
            Name = name;
            Password = password;
            Budget = 0;
        }
    }
}
