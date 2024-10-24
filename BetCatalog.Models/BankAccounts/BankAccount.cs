using BetCatalog.Models.Persons;

namespace BetCatalog.Models.BankAccounts
{
    public class BankAccount : IEntity
    {
        public int Id { get; set; }
        public string BankName { get; set; } = default!;
        public string PinCode { get; set; } = default!;
        public string INN { get; set; } = default!;
        public string CardNumber { get; set; } = default!;
        public string CVV { get; set; } = default!;
        public PaymentType PaymentType { get; set; }
        public DateOnly ValidationEndDate { get; set; }

        public int? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
