using Dapper;
using System.Data;

namespace Infrastructure.Dapper;

public class DapperCommand
{
    public DapperCommand(string command, CommandType commandType, int? commandTimeout = null)
    {
        Command = command;
        CommandType = commandType;
        CommandTimeout = commandTimeout;
    }

    public string Command { get; }

    public CommandType CommandType { get; }

    public int? CommandTimeout { get; }

    public CommandDefinition Definition()
    {
        return new CommandDefinition(Command, commandTimeout: CommandTimeout, commandType: CommandType);
    }

    public CommandDefinition Definition(object parameters)
    {
        return new CommandDefinition(Command, parameters: parameters, commandTimeout: CommandTimeout,
            commandType: CommandType);
    }

    public CommandDefinition Definition(CancellationToken cancellationToken)
    {
        return new CommandDefinition(Command, commandTimeout: CommandTimeout, commandType: CommandType,
            cancellationToken: cancellationToken);
    }

    public CommandDefinition Definition(object parameters, CancellationToken cancellationToken)
    {
        return new CommandDefinition(Command, parameters: parameters, commandTimeout: CommandTimeout,
            commandType: CommandType, cancellationToken: cancellationToken);
    }
}