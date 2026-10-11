namespace _11._09_практика;

/// <summary>
/// Класс, выполняющий переводы между счетами
/// </summary>
public static class TransferService
{
    /// <summary>
    /// Метод переводит указанную сумму со счёта отправителя на счёт получателя
    /// </summary>
    /// <param name="fromAccount">счет с которого переводятся деньги</param>
    /// <param name="toAccount">счет на который переводятся деньги</param>
    /// <param name="amount">сумма перевода</param>
    /// <exception cref="InvalidOperationException">если отправителем и получателем является один счёт</exception>
    /// <exception cref="ArgumentNullException">если один из счетов равен null</exception>
    /// <exception cref="ArgumentOutOfRangeException">если сумма перевода отрицательная или равна нулю</exception>
    public static void Transfer(BankAccount fromAccount, BankAccount toAccount, decimal amount)
    {
        ArgumentNullException.ThrowIfNull(fromAccount);
        ArgumentNullException.ThrowIfNull(toAccount);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);


        if (fromAccount.Number == toAccount.Number)
        {
            throw new InvalidOperationException("Нельзя перевести деньги на тот же счёт.");
        }
        Guid transferId = Guid.NewGuid();
        DateTime date = DateTime.UtcNow;
        fromAccount.MakeWithdrawal(amount,date, $"Перевод на счет {toAccount.Number}", AuditType.Withdrawal,transferId);
        try
        {
            toAccount.MakeDeposit(amount, date, $"Перевод со счета {fromAccount.Number}", AuditType.Deposit, transferId);
        }
        catch
        {
            fromAccount.MakeDeposit(amount,date,"Возврат средств после ошибки перевода", AuditType.Deposit,transferId);
            throw;
        }
    }
}