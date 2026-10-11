namespace _11._09_практика;


/// <summary>
/// Представляет кредитный счёт с возможностью ухода баланса в отрицательную область в пределах установленного кредитного лимита.
/// </summary>
public class LineOfCreditAccount:BankAccount
{

    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="LineOfCreditAccount"/>
    /// с заданным кредитным лимитом.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="creditLimit">Максимально допустимая сумма кредита. </param>
    public LineOfCreditAccount(string name, decimal initialBalance,decimal creditLimit, BankAccountStatus status = BankAccountStatus.Regular) : base(name, initialBalance,-creditLimit,status)
    {
    }

    /// <summary>
    /// Выполняет ежемесячные операции по счёту: если есть задолженность, начисляет проценты по кредиту и списывает их со счёта.
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        base.PerformMonthAndTransactions();
        if(Balance < 0)
        {
            decimal interest = -Balance * 0.07m;
            MakeWithdrawal(interest, DateTime.UtcNow, "Charge monthly interest", AuditType.Interest);
        }
    }

    /// <summary>
    /// Проверяет, превышен ли кредитный лимит, и при необходимости возвращает транзакцию с комиссией за овердрафт.
    /// </summary>
    /// <param name="isOverdrawn">
    /// <c>true</c>, если при снятии баланс выходит за пределы кредитного лимита; иначе <c>false</c>. </param>
    protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) => isOverdrawn ? new Transaction(-20, DateTime.UtcNow,"apply overdraft"):default;
    
}
