namespace NPO_System_Testovoe
{
    internal class Tender
    {
        public readonly string govRuId;
        public readonly string tenderName;
        public readonly double cost;
        public readonly string costCurrency;
        public readonly DateTime datePublic;
        public readonly string customerName;
        public readonly string? customerInn;

        public Tender(string govRuId, string tenderName, double cost, string costCurrency, DateTime datePublic, string customerName, string? customerInn)
        {
            this.govRuId=govRuId;
            this.tenderName=tenderName;
            this.cost=cost;
            this.costCurrency=costCurrency;
            this.datePublic=datePublic;
            this.customerName=customerName;
            this.customerInn=customerInn;
        }
    }
}
