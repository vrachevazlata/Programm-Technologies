using System.Text;

namespace _11._09_практика;

public enum AuditType
{
    Deposit,
    Withdrawal,
    Interest
}

public record AuditEntry
{
    public string AccountNumber { get; }
    public AuditType Type { get; }
    public decimal Amount { get; }
    public DateTime Date { get; }

    public AuditEntry(string accountNumber, AuditType type, decimal amount, DateTime date)
    {
        AccountNumber = accountNumber;
        Type = type;
        Amount = amount;
        Date = date;
    }
}

public static class AuditLog
{
    private static readonly List<AuditEntry> _entries = new List<AuditEntry>();

    public static void Add(string accountNumber, AuditType type, decimal amount, DateTime date)
    {
        AuditEntry audit = new AuditEntry(accountNumber, type, amount, date);
        _entries.Add(audit);
    }

    public static string GetHistory()
    {
        StringBuilder report = new StringBuilder();
        report.AppendLine("AccountNumber\tType\t\tAmount\tDate");
        foreach (var entry in _entries)
        {
            report.AppendLine($"{entry.AccountNumber}\t" + $"{entry.Type}\t\t" + $"{entry.Amount}\t" + $"{entry.Date.ToShortDateString()}");
        }
        return report.ToString();
    }
}