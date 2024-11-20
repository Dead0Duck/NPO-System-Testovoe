using System.Xml.Serialization;

namespace NPO_System_Testovoe
{
    [XmlRoot(ElementName = "export", Namespace = "http://zakupki.gov.ru/oos/export/1")]
    public class TenderXml
    {
        [XmlElement("fcsNotificationEF")]
        public TenderXmlBody body { get; set; }

        [XmlRoot(Namespace = "http://zakupki.gov.ru/oos/types/1", IsNullable = false)]
        public class TenderXmlBody
        {
            [XmlElement("purchaseNumber")]
            public string govRuId { get; set; }
            [XmlElement("purchaseObjectInfo")]
            public string tenderTitle { get; set; }
            [XmlElement("docPublishDate")]
            public string datePublic { get; set; }
            [XmlElement("lot")]
            public TenderXmlLot lot { get; set; }
        }

        [XmlRoot(Namespace = "http://zakupki.gov.ru/oos/types/1", IsNullable = false)]
        public class TenderXmlLot
        {
            [XmlElement("maxPrice")]
            public double price { get; set; }
            [XmlElement("currency")]
            public TenderXmlLotCurrency currency { get; set; }

            [XmlElement("customerRequirements")]
            public TenderXmlCustomers customers { get; set; }
        }

        [XmlRoot(Namespace = "http://zakupki.gov.ru/oos/types/1", IsNullable = false)]
        public class TenderXmlLotCurrency
        {
            [XmlElement("code")]
            public string code { get; set; }
        }

        [XmlRoot(Namespace = "http://zakupki.gov.ru/oos/types/1", IsNullable = false)]
        public class TenderXmlCustomers
        {
            [XmlElement("customerRequirement")]
            public List<TenderXmlCustomerRequirements> list { get; set; }
        }

        [XmlRoot(Namespace = "http://zakupki.gov.ru/oos/types/1", IsNullable = false)]
        public class TenderXmlCustomerRequirements
        {
            [XmlElement("customer")]
            public TenderXmlCustomerRequirementsInfo info { get; set; }
        }

        [XmlRoot(Namespace = "http://zakupki.gov.ru/oos/types/1", IsNullable = false)]
        public class TenderXmlCustomerRequirementsInfo
        {
            [XmlElement("fullName")]
            public string title { get; set; }
        }
    }
}
