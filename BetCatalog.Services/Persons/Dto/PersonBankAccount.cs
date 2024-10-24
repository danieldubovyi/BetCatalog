using BetCatalog.Models.BankAccounts;

namespace BetCatalog.Services.Persons.Dto
{
    public class PersonBankAccount
    {
        public int Id { get; set; }
        public string BankName { get; set; } = default!;
        public string PinCode { get; set; } = default!;
        public string INN { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public PaymentType PaymentType { get; set; }
        public DateOnly ValidationEndDate { get; set; }
    }
}
