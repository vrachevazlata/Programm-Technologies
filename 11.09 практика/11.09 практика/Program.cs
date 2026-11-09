using BankAccounts.Core.AccountNumbers;
using BankAccounts.Core.Factories;
using BankAccounts.Core.Statements;
using BankAccounts.Core.Time;
using BankAccounts.Core.Accounts;

namespace BankAccounts.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clock = new SystemClock();
            var factory = new AccountFactory(clock, new SequentialAccountNumberGenerator());
            var formatter = new TextStatementFormatter();

            var savings = factory.CreateInterestEarning("Yana", 10000m);
            savings.Deposit(100m, clock.Now, "asda");

            System.Console.WriteLine(formatter.Format(savings.Transactions));


            var deposit = factory.CreateDebitAccount("Zlata", 1000m);
            deposit.Withdraw(500m, clock.Now, ")");
            var credit = factory.CreateCreditAccount("Zlata", 500m,500m);
            deposit.Withdraw(300m, clock.Now, "(");

            List<BankAccount> accounts = new List<BankAccount>()
            {
                savings, credit, deposit
            };

            foreach (var account in accounts)
            {
                System.Console.WriteLine(formatter.Format(account.Transactions));
                account.PerforMonthEndTransactions();

            }
            credit.Withdraw(300m,clock.Now, "---");
            formatter.Format(credit.Transactions);

        }
    }
}