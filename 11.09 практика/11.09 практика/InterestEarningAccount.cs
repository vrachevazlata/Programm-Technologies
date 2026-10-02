namespace _11._09_практика;


public class InterestEarningAccount: BankAccount
{
    public InterestEarningAccount(string name, decimal initialBalance): base(name, initialBalance)
        { }




    //override позволяет в дочернем классе определить новую реализацию
    //метода PerformMountAndTransactions
    public override void PerformMountAndTransactions()
    {
        if( Balance > 500m)
        {
            decimal interest = Balance + 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "apply month interest");
        }
    }
}
