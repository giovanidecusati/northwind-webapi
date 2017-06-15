using NorthWind.Shared.Entities;

namespace NorthWind.Shared.Commands
{
    public class CreatedCommandResult : ICommandResult
    {
        private EntityBase _result;
        public int Id { get { return _result.Id; } }

        public CreatedCommandResult(EntityBase entity)
        {
            _result = entity;
        }
    }
}