namespace _11._09_практика;


/// <summary>
/// Представляет банковский счёт, по которому ежемесячно начисляются проценты.
/// </summary>
public class InterestEarningAccount: BankAccount
{
    /// <summary>
    /// Инициализирует новый экземпляр класса InterestEarningAccount.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="status">Статус владельца счёта.</param>
    
    public InterestEarningAccount(string name, decimal initialBalance, BankAccountStatus status = BankAccountStatus.Regular)
        :base(name, initialBalance,0,status)
        { }



    /// <summary>
    /// Выполняет ежемесячные операции по счёту: если текущий баланс превышает 500, начисляет процентную ставку и вносит её на счёт.
    /// </summary>
    /// 
    //override позволяет в дочернем классе определить новую реализацию
    //метода PerformMountAndTransactions

    public override void PerformMonthAndTransactions()
    {
        base.PerformMonthAndTransactions();
        if ( Balance > 500m)
        {
            decimal interest = Balance * 0.02m;
            MakeDeposit(interest, DateTime.UtcNow, "apply month interest", AuditType.Interest);
        }
    }
}
