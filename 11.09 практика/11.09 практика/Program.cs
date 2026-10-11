namespace _11._09_практика

    //Классы банковских считов

{
    public class Program
    {
        static void Main(string[] args)
        {
            BankAccount account1 = new BankAccount("nasy ", 5000);

            BankAccount account2 = new BankAccount("nasy", 7000);
            Console.WriteLine($"account {account1.Balance} {account1.Number} {account1.Owner}");
            Console.WriteLine($"account {account2.Balance} {account2.Number} {account2.Owner}");

            account1.MakeDeposit(100, DateTime.UtcNow, ":)");
            Console.WriteLine(account1.Balance);
            account2.MakeDeposit(10, DateTime.UtcNow, ":)");
            Console.WriteLine(account2.Balance);
            Console.WriteLine(account2.GetAccountHistory());

            try
            {
                account2.MakeWithdrawal(100, DateTime.UtcNow, ":(");
            }
            catch (InvalidOperationException e)
            {
                Console.WriteLine(e.Message);
            }
            InterestEarningAccount interest = new("Zlata", 10000m); 
            interest.MakeDeposit(100m, DateTime.UtcNow, ":)");
            interest.MakeWithdrawal(10m, DateTime.UtcNow, ":(");
            interest.PerformMonthAndTransactions();
            
            Console.WriteLine(interest.GetAccountHistory());

            LineOfCreditAccount lineOfCredit = new LineOfCreditAccount("Zlata", 0, 1000m);//1
            lineOfCredit.MakeWithdrawal(500m, DateTime.UtcNow, "credit");

            GiftCartAccount giftCart = new("Zlata", 1000m, 5000m);

            List<BankAccount> accounts = new List<BankAccount>();
            accounts.Add(account1);
            accounts.Add(interest);
            accounts.Add(lineOfCredit);
            accounts.Add(giftCart);

            foreach(BankAccount account in accounts)
            {
                Console.WriteLine(account);
                account.PerformMonthAndTransactions();
                Console.WriteLine(account.GetAccountHistory());
            }


            giftCart.MakeDeposit(100m, DateTime.UtcNow, ":)");
            giftCart.MakeWithdrawal(10m, DateTime.UtcNow, ":(");
            giftCart.PerformMonthAndTransactions();

            Console.WriteLine(giftCart);
            Console.WriteLine(giftCart.GetAccountHistory());

            BankAccount RegularClient = new BankAccount("Regular", 5000, 0);
            BankAccount PremiumClient = new BankAccount("Premium", 20000, 0,BankAccountStatus.Premium);
            BankAccount VIPClient = new BankAccount("VIP", 20000, 0,BankAccountStatus.VIP);


            //Аккаунты для проверки РАБОТЫ СТАТУСА
            RegularClient.PerformMonthAndTransactions();
            Console.WriteLine(RegularClient.GetAccountHistory());

            PremiumClient.PerformMonthAndTransactions();
            Console.WriteLine(PremiumClient.GetAccountHistory());

            VIPClient.PerformMonthAndTransactions();
            Console.WriteLine(VIPClient.GetAccountHistory());

            Console.WriteLine("\nПРОВЕРКА VIP-КЭШБЭКА");

            BankAccount vipCashbackTest =
                new("Проверка кэшбэка", 20000m, 0, BankAccountStatus.VIP);

            vipCashbackTest.MakeWithdrawal(
                1000m,
                DateTime.UtcNow,
                "Покупка");

            // Первый расчёт — кэшбэк должен начислиться.
            vipCashbackTest.PerformMonthAndTransactions();

            // Второй расчёт — за старую покупку кэшбэка уже быть не должно.
            vipCashbackTest.PerformMonthAndTransactions();

            Console.WriteLine(vipCashbackTest.GetAccountHistory());


            Console.WriteLine("ЖУРНАЛ АУДИТА");
            Console.WriteLine(AuditLog.GetHistory());

            Console.WriteLine("ПРОВЕРКИ ПЕРЕВОДА");
            Console.WriteLine("ДО ПЕРЕВОДА:");
            Console.WriteLine($"account1:{account1.Balance}");
            Console.WriteLine($"account1:{account2.Balance}");

            TransferService.Transfer(account1,account2,1000);
            
            Console.WriteLine("ПОСЛЕ ПЕРЕВОДА");
            Console.WriteLine($"account1:{account1.Balance}");
            Console.WriteLine($"account1:{account2.Balance}");

        }
    }
}
