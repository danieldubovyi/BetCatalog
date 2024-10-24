using BetCatalog.Models.BankAccounts;

namespace BetCatalog.Services.BankAccounts.Dto
{
    public class BankAccountListItem
    {
        public int Id { get; set; }
        public string BankName { get; set; } = default!;
        public PaymentType PaymentType { get; set; }
        public string CardNumber { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public DateOnly ValidationEndDate { get; set; }
    }
}
