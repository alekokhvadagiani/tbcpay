using System.Xml.Serialization;

namespace tbcpay.services.Helpers
{
    public enum ProviderStatusCodes
    {
        [XmlEnum("0")]
        Success,
        [XmlEnum("1")]
        ServerTimeOut,
        [XmlEnum("4")]
        InvalidAccountIdFormat,
        [XmlEnum("5")]
        AccountNotFound,
        [XmlEnum("7")]
        PaymentDeclined,
        [XmlEnum("215")]
        DuplicatedTransaction,
        [XmlEnum("275")]
        InvalidAmount,
        [XmlEnum("300")]
        GenericError
    }
}