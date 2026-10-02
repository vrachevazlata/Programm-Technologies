
using System.Text;

namespace _11._09_практика;

//BankAccount - потомок от object => можно переопределить
//виртуальные методы, находящиеся в object

public class BankAccount
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

    public string GetAccountHistory()
    {
        var repost = new StringBuilder();
        decimal balance = 0;
        repost.AppendLine("Data\t\tAmount\tBAlance\tNote");
        foreach (var item in _allTransactions)
        {
            balance += item.Amount;
            repost.AppendLine($"" +
                $"{item.Date.ToShortDateString()}\t" +
                $"{item.Amount}\t {balance}\t {item.Note}");
        }
        return repost.ToString();

    }
    //Ключевое слово  virtual позволяет в дочернем классе 
    //предоставить другую реализацию
    //метода PerformMountAndTransactions
    public virtual void PerformMountAndTransactions()
    {

    }


    //переопределяем метод, который унаследовали от object
    //этот метод должен возвращать строку с состоянием объекта
    //public override string ToString()
    //{
    //    return $'Type: {GetType().Name}\tOwner: {Owner} \tNumber of account : {Number}'
    //}

    public override string ToString() => return $'Type: {GetType().Name}\tOwner: {Owner} \tNumber of account : {Number}';
}

