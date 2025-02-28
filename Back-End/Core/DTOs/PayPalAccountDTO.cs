using System.ComponentModel.DataAnnotations;

public class PayPalAccountDTO
{
    public string AccountEmail { get; set; } = string.Empty;
    public PaymentAccountDTO PaymentAccount { get; set; } = new PaymentAccountDTO();
}
