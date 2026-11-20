namespace лаба_1_Классы__коллекции__работа_с_CSV;

public sealed class BidPlacedEventsArgs : EventArgs
{
    public string Title {  get; }
    
    public string Bidder { get; }

    public decimal? PreviosAmount { get; }

    public string PreviosLeader { get; }

    public decimal Amount { get; }

    public BidPlacedEventsArgs(string title, string bidDer, decimal? previosAmount, string previosLeader, decimal amount)
    {
        Title = title;
        Bidder = bidDer;
        PreviosAmount = previosAmount;
        PreviosLeader = previosLeader;
        Amount = amount;
    }
}