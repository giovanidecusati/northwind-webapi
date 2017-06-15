namespace NorthWind.Shared.Commands
{
    public interface ICommandHandler<in TCommand, out TCommandResult>
        where TCommand : ICommand
        where TCommandResult : ICommandResult
    {
        TCommandResult Handle(TCommand command);
    }
}
