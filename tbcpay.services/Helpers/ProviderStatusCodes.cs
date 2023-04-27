namespace tbcpay.services.Helpers;

public enum ProviderStatusCodes
{
    [XmlEnum("0")] Success = 0,
    [XmlEnum("1")] ServerTimeOut = 1,
    [XmlEnum("4")] InvalidAccountIdFormat = 4,
    [XmlEnum("5")] AccountNotFound = 5,
    [XmlEnum("7")] PaymentDeclined = 7,
    [XmlEnum("215")] DuplicatedTransaction = 215,
    [XmlEnum("275")] InvalidAmount = 275,
    [XmlEnum("300")] GenericError = 300
}