
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _11._09_практика;
//  recoed -состояние обьектов класса нельзя изменить
public record Transaction(decimal Amount, DateTime Date, string Note); //конструктор кратко

//internal record Transaction
//{
//    public decimal Amount { get; }
//    public decimal DateTime Date { get; }
//    public decimal string Note { get; }

//    public Transaction(decimal Amount, DateTime Date, string Note) 
//    {
//        this.Amount = Amount;
//        this. Date =  Date;
//        this.Note = Note;


//    }
//}
