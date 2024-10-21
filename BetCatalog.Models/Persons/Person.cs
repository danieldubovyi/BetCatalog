namespace BetCatalog.Models.Persons
{
    public class Person : IEntity
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string TelegramId { get; set; }
        public string PhoneNumber { get; set; }
        public string Bank { get; set; }
        public DateOnly BirthDate { get; set; }
        public DateOnly PassportDate { get; set; }
    }
}
