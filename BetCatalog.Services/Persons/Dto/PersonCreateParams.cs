using BetCatalog.Models.Persons;

namespace BetCatalog.Services.Persons.Dto
{
    public class PersonCreateParams
    {
        public string FIO { get; set; } = default!;
        public string TelegramId { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateOnly BirthDate { get; set; }
        public DateOnly PassportDate { get; set; }
        public PersonType PersonType { get; set; }
    }
}
