using Core_Layer.Enums;

public class PaymentAccountDTO
{
    public string AccountOwnerName { get; set; } = string.Empty;
    public int CurrencyID { get; set; }
    public int ServiceProviderID { get; set; }
    public EnPaymentAccountStatus PaymentStatus { get; set; } = EnPaymentAccountStatus.Active;
}
