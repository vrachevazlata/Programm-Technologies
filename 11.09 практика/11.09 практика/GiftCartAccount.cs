using System.Data;

namespace _11._09_практика;

public class GiftCartAccount : BankAccount
{
    private readonly decimal _monthlyDeposit = 0m;

    //monthlyDeposit - параметр по умолчанию (принимает 0)
    //при создании new GiftCartAccount("Zlata",1000); => monthlyDeposit = 0
    //GiftCartAccount("Zlata",1000,5000); => monthlyDeposit = 5000

    public GiftCartAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0) : base(name, initialBalance) => _monthlyDeposit = monthlyDeposit;

    public override void PerformMonthAndTransactions()
    {
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
        }
    }


    //base.ToString() - вызов базовой реализации => peaлизации из класса BankAccount
    public override string ToString()
    {
        return base.ToString() + $"montly deposit: {_monthlyDeposit}";
    }
}
