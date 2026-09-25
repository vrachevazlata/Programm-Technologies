
namespace _11._09_практика;

internal class BankAccount
{

    static private int s_accountNumberSeed = 1000000000;
    //данные конкретного обьекта
    public string Number { get; }//номер счета там
    public string Owner { get; private set; }//владелец
    public decimal Balance
    {
        get
        {
            decimal balance = 0;
            foreach (var item in _allTransactions)
            {
                balance += item.Amount;
            }
            return balance;


        }
    }


    private List<Transaction> _allTransactions = new List<Transaction>();
    public BankAccount(string name, decimal initialBalance)
    {

        Owner = name; //this.Owner = name  если одинаковые имена
        MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");

        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++; //
    }

    public void MakeDeposit(decimal amout, DateTime date, string note) //пополнение
    {
        if (amout <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amout), "Amount of deposit must be posisive");
            var deposit = new Transaction(amout, date, note);
            _allTransactions.Add(deposit);
        }



    }
    public void MakeWithdrawal(decimal amout, DateTime date, string note) //снятие
    {
        if (amout <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amout), "Amount of deposit must be posisive");
        }
        if (Balance < amout)
        {
            throw new InvalidOperationException("Not s...");
        }
        var withdrawal = new Transaction(-amout, date, note);
        _allTransactions.Add(withdrawal);

    }


}
