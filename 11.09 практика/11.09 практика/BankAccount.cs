
using System.Text;

namespace _11._09_практика;

/// <summary>
/// Статус клиента
/// </summary>
public enum BankAccountStatus 
{
    /// <summary>
    /// обычный статус с ежемесячной комиссией
    /// </summary>
    Regular,
    /// <summary>
    /// премиальный статус с начислением 1% на остаток свыше 10 000
    /// </summary>
    Premium,
    /// <summary>
    /// VIP-статус с начислением 3% и кэшбэком 5%
    /// </summary>
    VIP
}

/// <summary>
/// Представляет банковский счёт с возможностью пополнения, снятия средств
/// И ведения истории транзакций.
/// </summary>
/// <remarks>
/// Класс наследуется от object и позволяет переопределять виртуальные методы
/// </remarks>

public class BankAccount
{
    /// <summary>
    /// Минимальный баланс счёта
    /// </summary>
    private readonly decimal _minimumBalance;

    /// <summary>
    /// Начальное значение генератора номеров счетов.
    /// </summary>
    static private int s_accountNumberSeed = 1000000000;

    /// <summary>
    /// Уникальный номер банковского счёта.
    /// </summary>
    public string Number { get; }

    /// <summary>
    /// Владелец банковского счёта.
    /// </summary>
    public string Owner { get; private set; }

    /// <summary>
    /// Статус владельца (Regular, Premium или VIP). Начальное значение - Regular
    /// </summary>
    public BankAccountStatus Status { get; private set; } = BankAccountStatus.Regular;

    /// <summary>
    /// Текущий баланс счёта.
    /// </summary>
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

    /// <summary>
    /// Список всех транзакций по счёту.
    /// </summary>
    private List<Transaction> _allTransactions = new List<Transaction>();

    private int _cashbackStartIndex = 0;

    /// <summary>
    /// Инициализирует новый экземпляр класса с нулевым минимальным балансом.
    /// </summary>
    /// <param name="name">Имя владельца счёта.</param>
    /// <param name="initialBalance">Начальный баланс счёта.</param>
    public BankAccount(string name, decimal initialBalance) : this(name, initialBalance, 0) { }

    /// <summary>
    /// Инициализирует новый экземпляр класса с заданным минимальным балансом.
    /// </summary>
    /// <param name="name"> Имя владельца счёта.</param>
    /// <param name="initialBalance"> Начальный баланс счёта.</param>
    /// <param name="minimumBalance"> Минимально допустимый остаток на счёте.</param>
    public BankAccount(string name, decimal initialBalance,decimal minimumBalance,BankAccountStatus status = BankAccountStatus.Regular)
    {

        Owner = name; //this.Owner = name  если одинаковые имена
        Status = status;
        _minimumBalance = minimumBalance;
        Number = s_accountNumberSeed.ToString();
        s_accountNumberSeed++;

        if (initialBalance > 0) 
        {
            MakeDeposit(initialBalance, DateTime.UtcNow, "Initial balance");
        }
    }

    /// <summary>
    /// Пополняет счёт на указанную сумму.
    /// </summary>
    /// <param name="amount">Сумма пополнения. Должна быть положительной.</param>
    /// <param name="date">Дата операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException"> Бросается, если <paramref name="amount"/> меньше или равен нулю.</exception>
    public void MakeDeposit(decimal amount, DateTime date, string note,
        AuditType auditType = AuditType.Deposit, Guid? transferId = null) 
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount of deposit must be posisive");
            
        }
        var deposit = new Transaction(amount, date, note, transferId);
        _allTransactions.Add(deposit);
        AuditLog.Add(Number, auditType, amount, date);
    }
    /// <summary>
    /// Снимает денежные средства со счёта.
    /// </summary>
    /// <param name="amount">Сумма снятия. Должна быть положительной.</param>
    /// <param name="date">Дата и время операции.</param>
    /// <param name="note">Комментарий к операции.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Выбрасывается, если <paramref name="amount"/> меньше или равно нулю.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается, если на счёте недостаточно средств
    /// </exception>
    public void MakeWithdrawal(decimal amount, DateTime date, string note, AuditType auditType = AuditType.Withdrawal, Guid? transferId = null) //снятие
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);
        Transaction? overdraftTransaction = CheckWithdrawalLimit(Balance - amount < _minimumBalance);
        Transaction? withdrawal = new(-amount, date, note,transferId);
        _allTransactions.Add(withdrawal);
        AuditLog.Add(Number, auditType, amount, date);

        if (overdraftTransaction is not null)
        {
            _allTransactions.Add(overdraftTransaction);
            AuditLog.Add(Number, AuditType.Withdrawal, Math.Abs(overdraftTransaction.Amount), overdraftTransaction.Date);
        }
    }


    /// <summary>
    /// Проверяет, уходит ли баланс ниже минимального.
    /// </summary>
    /// <param name="v">
    /// <c>true</c>, если баланс после снятия станет ниже минимально допустимого;
    /// иначе <c>false</c>.
    /// </param>
    /// <exception cref="NotImplementedException">
    /// Бросается при недостатке средств.
    /// </exception>
    protected virtual Transaction? CheckWithdrawalLimit(bool v)
    {
        if (v)
        {
            throw new NotImplementedException("Not sufficient rubls for this withdrawal");
        }
        return default;
    }

    /// <summary>
    /// Формирует историю всех транзакций по счёту.
    /// </summary>
    /// <returns>
    /// Строка с таблицей транзакций, содержащая дату, сумму, баланс и примечание.
    /// </returns>
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

    /// <summary>
    /// Выполняет операции, которые должны происходить в конце каждого месяца(начисляет комиссию/проценты)
    /// </summary>
    public virtual void PerformMonthAndTransactions()
    {
        switch (Status)
        {
            case BankAccountStatus.Regular:

                MakeWithdrawal(50, DateTime.UtcNow, "Monthly maintenance fee");
                break;

            case BankAccountStatus.Premium:
                if(Balance > 10000)
                {
                    MakeDeposit((Balance - 10000) * 0.01m, DateTime.UtcNow, "1% on the balance exceeding 10,000", AuditType.Interest);
                }
                break;

            case BankAccountStatus.VIP:
                if (Balance > 10000m)
                {
                     decimal interest = (Balance - 10000m) * 0.03m;

                     MakeDeposit(interest, DateTime.UtcNow, "VIP monthly interest", AuditType.Interest);
                }

                decimal totalWithdrawals = 0m;

                for (int i = _cashbackStartIndex; i < _allTransactions.Count; i++)
                {
                    if (_allTransactions[i].Amount < 0)
                    {
                        totalWithdrawals += -_allTransactions[i].Amount;
                    }
                }

                decimal cashback = totalWithdrawals * 0.05m;

                if (cashback > 0)
                {
                    MakeDeposit(cashback, DateTime.UtcNow, "VIP cashback");
                }

                _cashbackStartIndex = _allTransactions.Count;

                break;

        }

    }
    /// <summary>
    /// Возвращает строковое представление банковского счёта.
    /// </summary>
    public override string ToString() => $"Type: {GetType().Name}\tOwner: {Owner} \tNumber of account : {Number}";
}

