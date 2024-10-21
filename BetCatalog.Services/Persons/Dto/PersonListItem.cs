namespace BetCatalog.Services.Persons.Dto
{
    public class PersonListItem
    {
        public int Id { get; set; }
        public string FIO { get; set; } = default!;
        public string TelegramId { get; set; } = default!;
    }
}
