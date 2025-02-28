using Core_Layer.Enums;

public class PayPalAccountDetailsDTO
{
    public int PayPalAccountID { get; set; }
    public string AccountEmail { get; set; }
    public int PaymentAccountID { get; set; }
    public string AccountOwnerName { get; set; }
    public EnPaymentAccountStatus PaymentStatus { get; set; }
    public string CurrencyName { get; set; }
}
