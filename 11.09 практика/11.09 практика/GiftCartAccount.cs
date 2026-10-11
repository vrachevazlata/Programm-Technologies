using System.Data;

namespace _11._09_практика;


/// <summary>
/// Представляет подарочный банковский счёт, по которому ежемесячно может автоматически вноситься фиксированная сумма.
/// </summary>
public class GiftCartAccount : BankAccount
{
    /// <summary>
    /// Размер ежемесячного автоматического пополнения счёта.
    /// Значение 0 означает, что пополнение не выполняется.
    /// </summary>
    private readonly decimal _monthlyDeposit = 0m;

    //monthlyDeposit - параметр по умолчанию (принимает 0)
    //при создании new GiftCartAccount("Zlata",1000); => monthlyDeposit = 0
    //GiftCartAccount("Zlata",1000,5000); => monthlyDeposit = 5000


    /// <summary>
    /// Инициализирует новый экземпляр класса .
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    /// <param name="monthlyDeposit"> Сумма ежемесячного автоматического пополнения. Значение по умолчанию — 0 (пополнение не выполняется). </param>
    /// <param name="status">Статус владельца счета</param>
    public GiftCartAccount(string name, decimal initialBalance, decimal monthlyDeposit = 0m, BankAccountStatus status = BankAccountStatus.Regular) : base(name, initialBalance,0,status) => _monthlyDeposit = monthlyDeposit;


    /// <summary>
    /// Выполняет ежемесячные операции по счёту: если задана ненулевая сумма, вносит её на счёт.
    /// </summary>
    public override void PerformMonthAndTransactions()
    {
        base.PerformMonthAndTransactions();
        if (_monthlyDeposit != 0)
        {
            MakeDeposit(_monthlyDeposit, DateTime.UtcNow, "Add monthly deposit");
        }
    }

//base.ToString() - вызов базовой реализации => peaлизации из класса BankAccount

    /// <summary>
    /// Возвращает строку с инфой подарочного счёта, включая размер ежемесячного пополнения.
    /// </summary>
    public override string ToString()
    {
        return base.ToString() + $"montly deposit: {_monthlyDeposit}";
    }
}
