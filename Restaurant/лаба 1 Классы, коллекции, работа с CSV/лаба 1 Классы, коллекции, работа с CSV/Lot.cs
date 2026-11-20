namespace лаба_1_Классы__коллекции__работа_с_CSV;

public class Lot
{
    public Guid Id { get; } //уникальный код
    public string Title { get; } // название
    public decimal StartPrice { get; } // начальная цена
    public decimal BidStep { get; } // шаг ставки
    public decimal? CurrentBid { get; private set; } = default; //последняя ставка(null не было ставки)
    public string? CurrentLeader { get; private set; } = default; //Имя лидера(сделал последнюю ставку)

    public decimal? BuyNowPrice { get; } //цена мгновенного выкупа
    public DateTime EndsAt { get; } // время окончания торгов

    //public Action<string, decimal>? BidPlaced;  //делегат для оповещения, что ставка произошла

    //public Action<string, decimal>? Repurchase; //делегат для оповещения, что произошел мгновенный выкуп

    public EventHandler<BidPlacedEventsArgs>? BidPlaced; //Событие для оповещения, что ставка произошла

    public EventHandler<ByNowTriggerEventArgs>? Repurchase; //Cобытие для оповещения, что произошел мгновенный выкуп



    public Lot(string title, decimal startPrice, decimal bidStep,
        DateTime endsAt, decimal? buyNowPrice = default)
    {
        Id = Guid.NewGuid();
        Title = title;
        StartPrice = startPrice;
        BidStep = bidStep;
        EndsAt = endsAt;
        BuyNowPrice = buyNowPrice;
    }

    public void BuyNow(string buyer)
    {
        //	фиксирует продажу(можно просто вывести в консоль «лот выкуплен» и запомнить победителя);
        //	оповещает подписчиков — но вот вопрос: через BidPlaced? Или нужен новый делегат? Реши сам и напиши, почему выбрал так.
        if (BuyNowPrice is null || EndsAt < DateTime.UtcNow)
            throw new InvalidOperationException("Вы не можете выкупить этот лот");

        if (CurrentBid is null || CurrentBid < BuyNowPrice)
        {
            CurrentBid = BuyNowPrice;
            CurrentLeader = buyer;
            //Оповещаем о мгновенном выкупе
            // Repurchase(buyer, BuyNowPrice!.Value); //ПРОБЛЕМА
            OnBuyNowTrigger(new ByNowTriggerEventArgs(buyer, CurrentBid.Value!));
        }

        //если мгновенный выкуп меньше текущей ставки
        throw new InvalidOperationException("Вы не можете выкупить этот лот");

    }

    public void PlaceBid(string bidder, decimal amount)
    {
        if (DateTime.UtcNow >= EndsAt)
            throw new InvalidOperationException("Торги уже завершены");

        if (CurrentBid is null && amount < StartPrice)
            throw new InvalidOperationException("Ставка слишком мала");

        if (CurrentBid is not null && amount < CurrentBid + BidStep)
            throw new InvalidOperationException("Ставка слишком мала");

        var previosBid = CurrentBid;
        var previosLeader = CurrentLeader;

        CurrentBid = amount;
        CurrentLeader = bidder;

        //уведомление подписчикам о событии "Ставка произошла"
        OnBidPlaced(new BidPlacedEventsArgs(Title,bidder,previosBid,previosLeader,amount));


    }

    protected virtual void OnBidPlaced(BidPlacedEventsArgs e)
        => BidPlaced?.Invoke(this, e);


    protected virtual void OnBuyNowTrigger(ByNowTriggerEventArgs e)
        => Repurchase?.Invoke(this, e);
}
