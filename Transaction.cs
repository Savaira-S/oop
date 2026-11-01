namespace PersonalFinanceTracker_Layered.Models
{
    public class Transaction
    {
        public string Date;
        public string Category;
        public float Amount;
        public bool IsExpense;

        public Transaction(string date, string category, float amount, bool isExpense)
        {
            Date = date;
            Category = category;
            Amount = amount;
            IsExpense = isExpense;
        }
    }
}
