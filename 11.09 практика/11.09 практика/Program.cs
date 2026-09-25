namespace _11._09_практика

    //Классы банковских считов

{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("nasy", 5000);

            BankAccount account2 = new BankAccount("nasy", 7000);
            Console.WriteLine($"account {account.Balance} {account.Number} {account.Owner}");
            Console.WriteLine($"account {account2.Balance} {account2.Number} {account2.Owner}");

            account.MakeDeposit(100, DateTime.UtcNow, ":)");
            Console.WriteLine(account.Balance);
            account2.MakeDeposit(10, DateTime.UtcNow, ":)");
            Console.WriteLine(account2.Balance);

            try
            {
                account2.MakeWithdrawal(100000000, DateTime.UtcNow, ":(");
            }
            catch (InvalidOperationException e)
            { 
                Console.WriteLine(e.Message );
            }
        }

    }
}
