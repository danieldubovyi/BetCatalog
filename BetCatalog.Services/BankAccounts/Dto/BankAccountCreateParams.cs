using BetCatalog.Models.BankAccounts;

namespace BetCatalog.Services.BankAccounts.Dto
{
    public class BankAccountCreateParams
    {
        public string BankName { get; set; } = default!;
        public string PinCode { get; set; } = default!;
        public string INN { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public PaymentType PaymentType { get; set; }
        public DateOnly ValidationEndDate { get; set; }

        public int PersonId { get; set; }
    }
}
