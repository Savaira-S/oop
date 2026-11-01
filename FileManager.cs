using System;
using System.IO;
using PersonalFinanceTracker_Layered.Models;

namespace PersonalFinanceTracker_Layered.DataAccess
{
    public class FileManager
    {
        private string GetFileName(User user)
        {
            return user.Name + ".txt";
        }

        public void SaveUser(User user)
        {
            using (StreamWriter file = new StreamWriter(GetFileName(user)))
            {
                file.WriteLine(user.Budget);

                for (int i = 0; i < user.Count; i++)
                {
                    Transaction t = user.Transactions[i];
                    file.WriteLine(t.Date + "," + t.Category + "," +
                                   t.Amount + "," + t.IsExpense);
                }
            }
        }

        public void LoadUser(User user)
        {
            string fileName = GetFileName(user);

            if (!File.Exists(fileName))
                return;

            using (StreamReader file = new StreamReader(fileName))
            {
                string line = file.ReadLine();

                if (line != null)
                    user.Budget = float.Parse(line);

                while ((line = file.ReadLine()) != null && user.Count < user.Transactions.Length)
                {
                    string[] data = line.Split(',');

                    if (data.Length == 4)
                    {
                        string date = data[0];
                        string category = data[1];
                        float amount = float.Parse(data[2]);
                        bool isExpense = bool.Parse(data[3]);

                        user.Transactions[user.Count] =
                            new Transaction(date, category, amount, isExpense);
                        user.Count++;
                    }
                }
            }
        }
    }
}