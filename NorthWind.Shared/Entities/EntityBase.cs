using NorthWind.Shared.Notifications;

namespace NorthWind.Shared.Entities
{
    public abstract class EntityBase : Notifiable
    {
        public int Id { get; private set; }
    }
}
