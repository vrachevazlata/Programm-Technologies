namespace лаба_1_Классы__коллекции__работа_с_CSV;

internal class Program
{
    static void Main()
    {
        Lot lot = new Lot("Pencel", 100m, 50m, DateTime.UtcNow.AddDays(1));

        lot.BidPlaced += (obj, e) => Console.WriteLine($"[Lot] Ставка: {e.Bidder} - {e.Amount}");


        lot.PlaceBid("Zlata", 10_000m);
    }
}


