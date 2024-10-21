using BetCatalog.Models.Interfaces.Repositories;
using BetCatalog.Models.Users;
using BetCatalog.Services.Users.Dto;
using MediatR;

namespace BetCatalog.Services.Users.Commands
{
    public record UpdateUserSettingsCommand(string UserId, IReadOnlyCollection<UserSettingItem> Settings) : IRequest;

    internal class UpdateUserSettingsCommandHandler(IEntityRepository<UserSetting> settingRepository)
        : IRequestHandler<UpdateUserSettingsCommand>
    {
        public async Task Handle(UpdateUserSettingsCommand request, CancellationToken cancellationToken)
        {
            var existSettings = await settingRepository.GetListAsync(s => s.UserId == request.UserId, cancellationToken);

            var newSettings = new List<UserSetting>();
            foreach (var setting in request.Settings)
            {
                var existSetting = existSettings.FirstOrDefault(s => s.Name.Equals(setting.Name, StringComparison.OrdinalIgnoreCase));
                if (existSetting != null)
                {
                    existSetting.Value = setting.Value;
                }
                else
                {
                    newSettings.Add(new UserSetting { UserId = request.UserId, Name = setting.Name, Value = setting.Value });
                }
            }
            await settingRepository.MergeAsync(newSettings, existSettings, Array.Empty<UserSetting>(), cancellationToken);
        }
    }
}
