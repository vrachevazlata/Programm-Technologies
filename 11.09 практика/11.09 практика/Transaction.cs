namespace _11._09_практика;
//  record -состояние обьектов класса нельзя изменить

/// <summary>
/// Представляет одну банковскую транзакцию: сумму, дату и примечание.
/// </summary>
/// <param name="Amount">Сумма транзакции. Положительное значение — пополнение,отрицательное — списание.</param>
/// <param name="Date">Дата и время совершения транзакции.</param>
/// <param name="Note">Текстовое примечание к транзакции.</param>
/// <param name="TransferId">идентификатор перевода, если это не перевод он равен null </param>
public record Transaction(decimal Amount, DateTime Date, string Note, Guid? TransferId = null); //конструктор кратко
