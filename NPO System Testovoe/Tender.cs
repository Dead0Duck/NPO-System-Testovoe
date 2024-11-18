using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPO_System_Testovoe
{
    internal class Tender
    {
        public readonly uint govRuId;
        public readonly string tenderName;
        public readonly double cost;
        public readonly string costCurrency;
        public readonly DateTime datePublic;
        public readonly string customerName;
        public readonly uint? customerInn;

        public Tender(uint govRuId, string tenderName, double cost, string costCurrency, DateTime datePublic, string customerName, uint? customerInn)
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
