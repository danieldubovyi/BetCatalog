namespace BetCatalog.Services.Persons.Dto
{
    public class PersonDetails
    {
        public int Id { get; set; }
        public string FIO { get; set; } = default!;
        public string TelegramId { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string Bank { get; set; } = default!;
        public DateOnly BirthDate { get; set; }
        public DateOnly PassportDate { get; set; }
    }
}
