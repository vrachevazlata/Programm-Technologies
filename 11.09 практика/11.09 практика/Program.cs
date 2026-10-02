namespace _11._09_практика

    //Классы банковских считов

{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankAccount account = new BankAccount("nasy ", 5000);

            BankAccount account2 = new BankAccount("nasy", 7000);
            Console.WriteLine($"account {account.Balance} {account.Number} {account.Owner}");
            Console.WriteLine($"account {account2.Balance} {account2.Number} {account2.Owner}");

            account.MakeDeposit(100, DateTime.UtcNow, ":)");
            Console.WriteLine(account.Balance);
            account2.MakeDeposit(10, DateTime.UtcNow, ":)");
            Console.WriteLine(account2.Balance);
            Console.WriteLine(account2.GetAccountHistory());

            try
            {
                account2.MakeWithdrawal(100000000, DateTime.UtcNow, ":(");
            }
            catch (InvalidOperationException e)
            { 
                Console.WriteLine(e.Message );
            }
            InterestEarningAccount interestEarning = new("Zlata", 10000m); //точка остановы
            interestEarning.MakeDeposit(100m, DateTime.UtcNow, ":)");
            interestEarning.MakeWithdrawal(10m, DateTime.UtcNow, ":(");
            interestEarning.PerformMountAndTransactions();
            Console.WriteLine(interestEarning.GetAccountHistory());

            Console.WriteLine(interestEarning);
            Console.WriteLine(interestEarning.GetAccountHistory());

        }

    }
}
