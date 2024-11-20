namespace NPO_System_Testovoe
{
    public class TenderCost
    {
        public double? cost { get; set; }
        public TenderCostCurrency? currency { get; set; }
    }

    public class TenderCostCurrency
    {
        public string? alpha { get; set; }
    }

    public class TenderCustomer
    {
        public string? title { get; set; }
        public string? inn { get; set; }
    }

    /// <summary>
    /// Класс для представления ответа от API tenmon.ru
    /// </summary>
    internal class TenderJson
    {
        public string? govRuId { get; set; }
        public string? tenderTitle { get; set; }
        public string? datePublic { get; set; }
        public IList<TenderCustomer>? customers { get; set; }
        public Dictionary<string, TenderCost>? cost { get; set; }
    }
}
