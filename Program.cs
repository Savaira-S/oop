using System;
using System.IO;
class Transaction
{
    public string date;
    public string category;
    public float amount;
    public bool isExpense;
    public Transaction(string d, string c, float a, bool expense)
    {
        date = d;
        category = c;
        amount = a;
        isExpense = expense;
    }
}
class User
{
    public string name;
    public string password;
    public float budget;
    public Transaction[] transactions = new Transaction[100];
    public int count = 0;
    public User(string n, string p)
    {
        name = n;
        password = p;
        budget = 0;
    }
    public void AddTransaction()
    {
        if (count >= 100)
        {
            Console.WriteLine("Transaction limit reached!");
            return;
        }
        Console.Write("Enter date (DD/MM/YYYY): ");
        string date = Console.ReadLine();
        Console.Write("Enter category: ");
        string category = Console.ReadLine();
        Console.Write("Enter amount: ");
        float amount = float.Parse(Console.ReadLine());
        Console.Write("Type (1 for expense, 0 for income): ");
        int type = int.Parse(Console.ReadLine());

        bool expense;
        if (type == 1)
            expense = true;
        else
            expense = false;
        transactions[count] =
            new Transaction(date, category, amount, expense);
        count++;
        Console.WriteLine("Data Saved Successfully!");
    }
    public void ViewSummary()
    {
        if (count == 0)
        {
            Console.WriteLine("No transactions available.");
            return;
        }
        Console.WriteLine("Date\tCategory\tAmount\tType");
        for (int i = 0; i < count; i++)
        {
            Console.Write(transactions[i].date + "\t");
            Console.Write(transactions[i].category + "\t");
            Console.Write(transactions[i].amount + "\t");
            if (transactions[i].isExpense == true)
                Console.WriteLine("Expense");
            else
                Console.WriteLine("Income");
        }
    }
    public void TotalBalance()
    {
        float totalIncome = 0;
        float totalExpenses = 0;
        for (int i = 0; i < count; i++)
        {
            if (transactions[i].isExpense == true)
                totalExpenses += transactions[i].amount;
            else
                totalIncome += transactions[i].amount;
        }
        float remaining = budget + totalIncome - totalExpenses;
        Console.WriteLine("Initial Budget: " + budget);
        Console.WriteLine("Total Income: " + totalIncome);
        Console.WriteLine("Total Expenses: " + totalExpenses);
        Console.WriteLine("Remaining Budget: " + remaining);
        if (remaining < 0)
            Console.WriteLine("Warning: You are over Budget!");
    }
    public void SetBudget()
    {
        Console.Write("Enter your total initial budget: ");
        budget = float.Parse(Console.ReadLine());
        Console.WriteLine("Budget set successfully!");
    }
    public void UpdateTransaction()
    {
        if (count == 0)
        {
            Console.WriteLine("No transactions to update!");
            return;
        }
        Console.Write("Enter index to update: ");
        int index = int.Parse(Console.ReadLine());
        if (index >= 0 && index < count)
        {
            Console.Write("Enter new date: ");
            transactions[index].date = Console.ReadLine();
            Console.Write("Enter new category: ");
            transactions[index].category = Console.ReadLine();
            Console.Write("Enter new amount: ");
            transactions[index].amount = float.Parse(Console.ReadLine());
            Console.Write("Type (1 for expense, 0 for income): ");
            int type = int.Parse(Console.ReadLine());
            if (type == 1)
                transactions[index].isExpense = true;
            else
                transactions[index].isExpense = false;
            Console.WriteLine("Updated!");
        }
        else
        {
            Console.WriteLine("Invalid index!");
        }
    }
    public void DeleteTransaction()
    {
        if (count == 0)
        {
            Console.WriteLine("No data!");
            return;
        }
        Console.Write("Enter index to delete: ");
        int index = int.Parse(Console.ReadLine());
        if (index >= 0 && index < count)
        {
            for (int i = index; i < count - 1; i++)
                transactions[i] = transactions[i + 1];
            count--;
            Console.WriteLine("Deleted!");
        }
        else
        {
            Console.WriteLine("Invalid index!");
        }
    }
    public void ShowTotalIncome()
    {
        float totalIncome = 0;
        for (int i = 0; i < count; i++)
        {
            if (transactions[i].isExpense == false)
                totalIncome += transactions[i].amount;
        }
        Console.WriteLine("Total Income for " + name + ": " + totalIncome);
    }
    public void ShowTotalExpenses()
    {
        float totalExpense = 0;
        for (int i = 0; i < count; i++)
        {
            if (transactions[i].isExpense == true)
                totalExpense += transactions[i].amount;
        }
        Console.WriteLine("Total Expenses for " + name + ": " + totalExpense);
    }
    public void ClearTransactions()
    {
        Console.Write("Are you sure you want to delete ALL transactions? ");
        Console.Write("(1 for YES, 0 to cancel): ");
        int confirm = int.Parse(Console.ReadLine());
        if (confirm == 1)
        {
            count = 0;
            Console.WriteLine("All transactions cleared for " + name + "!");
        }
        else
        {
            Console.WriteLine("Operation cancelled.");
        }
    }
    public void ResetBudget()
    {
        budget = 0;
        Console.WriteLine("Budget for " + name + " has been reset to 0.");
    }
    public void SaveToFile()
    {
        StreamWriter file = new StreamWriter(name + ".txt");
        file.WriteLine(budget);
        for (int i = 0; i < count; i++)
        {
            file.WriteLine(
                transactions[i].date + "," +
                transactions[i].category + "," +
                transactions[i].amount + "," +
                transactions[i].isExpense);
        }
        file.Close();
        Console.WriteLine("Data saved successfully!");
    }
    public void LoadFromFile()
    {
        if (!File.Exists(name + ".txt"))
            return;
        StreamReader file = new StreamReader(name + ".txt");
        string line = file.ReadLine();
        if (line != null)
            budget = float.Parse(line);
        while ((line = file.ReadLine()) != null)
        {
            string[] data = line.Split(',');
            if (data.Length == 4)
            {
                string date = data[0];
                string category = data[1];
                float amount = float.Parse(data[2]);
                bool expense = bool.Parse(data[3]);
                transactions[count] =
                    new Transaction(date, category, amount, expense);
                count++;
            }
        }
        file.Close();
        Console.WriteLine("Data loaded successfully!");
    }
}
class Program
{
    static void Main()
    {
        User user1 = new User("User1", "pss14");
        User user2 = new User("User2", "ibo12");

        user1.LoadFromFile();
        user2.LoadFromFile();
        User[] users = new User[2];
        users[0] = user1;
        users[1] = user2;
        int currentUser = 0;
        bool running = true;
        while (running)
        {
            bool loginSuccess = false;
            while (loginSuccess == false)
            {
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("        PERSONAL FINANCE TRACKER");
                Console.WriteLine("============================================");
                Console.WriteLine("1. Login as User1");
                Console.WriteLine("2. Login as User2");
                Console.WriteLine("3. Exit completely");
                Console.Write("Select option: ");
                int loginChoice = int.Parse(Console.ReadLine());
                if (loginChoice == 1 || loginChoice == 2)
                {
                    int selectedUser = loginChoice - 1;
                    Console.Write("Enter password for " +
                                  users[selectedUser].name + ": ");
                    string enteredPassword = Console.ReadLine();
                    if (enteredPassword ==
                        users[selectedUser].password)
                    {
                        currentUser = selectedUser;
                        loginSuccess = true;
                        Console.WriteLine(
                            "Login Successful! Welcome, " +
                            users[currentUser].name);
                    }
                    else
                    {
                        Console.WriteLine(
                            "Incorrect password! Access Denied.");
                    }
                }
                else if (loginChoice == 3)
                {
                    users[0].SaveToFile();
                    users[1].SaveToFile();
                    Console.WriteLine("Exiting system...");
                    return;
                }
                else
                {
                    Console.WriteLine("Invalid choice!");
                }
                Console.WriteLine("Press Enter...");
                Console.ReadLine();
            }
            while (loginSuccess)
            {
                Console.Clear();
                Console.WriteLine("============================================");
                Console.WriteLine("        PERSONAL FINANCE TRACKER");
                Console.WriteLine("============================================");
                Console.WriteLine("Logged in as: " +
                                  users[currentUser].name);
                Console.WriteLine();
                Console.WriteLine("1. Add Transaction");
                Console.WriteLine("2. View Summary");
                Console.WriteLine("3. Calculate Total Balance");
                Console.WriteLine("4. Set Initial Budget");
                Console.WriteLine("5. Update Transaction");
                Console.WriteLine("6. Delete Transaction");
                Console.WriteLine("7. View Total Income");
                Console.WriteLine("8. View Total Expenses");
                Console.WriteLine("9. Clear All Transactions");
                Console.WriteLine("10. Change User");
                Console.WriteLine("11. Reset Budget to Zero");
                Console.WriteLine("12. Exit / Sign Out");
                Console.Write("Enter choice: ");
                int choice = int.Parse(Console.ReadLine());
                User current = users[currentUser];
                if (choice == 1)
                    current.AddTransaction();
                else if (choice == 2)
                    current.ViewSummary();
                else if (choice == 3)
                    current.TotalBalance();
                else if (choice == 4)
                    current.SetBudget();
                else if (choice == 5)
                    current.UpdateTransaction();
                else if (choice == 6)
                    current.DeleteTransaction();
                else if (choice == 7)
                    current.ShowTotalIncome();
                else if (choice == 8)
                    current.ShowTotalExpenses();
                else if (choice == 9)
                    current.ClearTransactions();
                else if (choice == 10)
                {
                    Console.WriteLine("Available users:");
                    for (int i = 0; i < 2; i++)
                        Console.WriteLine((i + 1) + ". " + users[i].name);
                    Console.Write("Select user: ");
                    int userChoice = int.Parse(Console.ReadLine());
                    if (userChoice == 1 || userChoice == 2)
                    {
                        currentUser = userChoice - 1;
                        Console.WriteLine(
                            "Now using: " +
                            users[currentUser].name);
                    }
                    else
                    {
                        Console.WriteLine("Invalid user choice!");
                    }
                }
                else if (choice == 11)
                    current.ResetBudget();
                else if (choice == 12)
                {
                    current.SaveToFile();
                    loginSuccess = false;
                    Console.WriteLine("Signed out successfully!");
                }
                else
                    Console.WriteLine("Invalid choice!");
                Console.WriteLine();
                Console.WriteLine("Press Enter to continue...");
                Console.ReadLine();
            }
        }
    }
}
