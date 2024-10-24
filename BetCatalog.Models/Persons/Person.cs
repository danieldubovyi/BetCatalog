namespace BetCatalog.Models.Persons
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public string FIO { get; set; } = default!;
        public string TelegramId { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public DateOnly BirthDate { get; set; }
        public DateOnly PassportDate { get; set; }
        public PersonType PersonType { get; set; }
    }
}
