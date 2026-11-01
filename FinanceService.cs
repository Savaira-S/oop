using PersonalFinanceTracker_Layered.DataAccess;
using PersonalFinanceTracker_Layered.Models;

namespace PersonalFinanceTracker_Layered.Services
{
    public class FinanceService
    {
        private readonly FileManager fileManager;

        public FinanceService(FileManager manager)
        {
            fileManager = manager;
        }

        public void LoadUser(User user)
        {
            fileManager.LoadUser(user);
        }

        public void SaveUser(User user)
        {
            fileManager.SaveUser(user);
        }

        public bool AddTransaction(User user, string date, string category,
                                   float amount, bool isExpense)
        {
            if (user.Count >= user.Transactions.Length)
                return false;

            user.Transactions[user.Count] =
                new Transaction(date, category, amount, isExpense);
            user.Count++;
            return true;
        }

        public bool UpdateTransaction(User user, int index, string date,
                                      string category, float amount, bool isExpense)
        {
            if (index < 0 || index >= user.Count)
                return false;

            user.Transactions[index].Date = date;
            user.Transactions[index].Category = category;
            user.Transactions[index].Amount = amount;
            user.Transactions[index].IsExpense = isExpense;
            return true;
        }

        public bool DeleteTransaction(User user, int index)
        {
            if (index < 0 || index >= user.Count)
                return false;

            for (int i = index; i < user.Count - 1; i++)
                user.Transactions[i] = user.Transactions[i + 1];

            user.Count--;
            user.Transactions[user.Count] = null;
            return true;
        }

        public void ClearTransactions(User user)
        {
            for (int i = 0; i < user.Count; i++)
                user.Transactions[i] = null;

            user.Count = 0;
        }

        public float GetTotalIncome(User user)
        {
            float total = 0;

            for (int i = 0; i < user.Count; i++)
            {
                if (!user.Transactions[i].IsExpense)
                    total += user.Transactions[i].Amount;
            }

            return total;
        }

        public float GetTotalExpenses(User user)
        {
            float total = 0;

            for (int i = 0; i < user.Count; i++)
            {
                if (user.Transactions[i].IsExpense)
                    total += user.Transactions[i].Amount;
            }

            return total;
        }

        public float GetRemainingBalance(User user)
        {
            return user.Budget + GetTotalIncome(user) - GetTotalExpenses(user);
        }
    }
}
