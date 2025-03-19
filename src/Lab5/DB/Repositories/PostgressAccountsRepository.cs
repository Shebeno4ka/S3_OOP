using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Interfaces.Accounts;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Domain.Models.Accounts;
using Npgsql;

namespace Itmo.ObjectOrientedProgramming.Lab5.DB.Repositories;

public class PostgressAccountsRepository(IPostgresConnectionProvider connectionProvider) : IAccountRepository
{
    public async Task UpdateAccountAsync(Account account)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        const string sql = """
                           UPDATE accounts SET balance = @balance WHERE id = @id
                           """;

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("account_number", account.AccountNumber);

        int rowsAffected = await command.ExecuteNonQueryAsync().ConfigureAwait(false);
    }

    public async Task<Account?> GetByAccountNumberAsync(AccountNumber accountNumber)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        const string sql = """
                            SELECT account_number, pin_code, balance FROM accounts WHERE account_number = @accountNumber
                            """;

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("account_number", accountNumber);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        if (await reader.ReadAsync().ConfigureAwait(false) is false)
            return null;

        return new Account(accountNumber, new PinCode(reader.GetString(1)));
    }

    public async Task<Account> CreateAccountAsync(AccountNumber accountNumber, PinCode pin)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        const string sql = """
                            INSERT INTO accounts (account_number, pin_code, balance) VALUES (@account_number, @pin_code, 0.00)
                            """;

        using NpgsqlCommand command = new NpgsqlCommand(sql, connection)
            .AddParameter("account_number", accountNumber)
            .AddParameter("pin_code", pin);

        await command.ExecuteNonQueryAsync().ConfigureAwait(false);

        return new Account(accountNumber, pin);
    }

    public async Task<IEnumerable<Transaction>> GetTransactionHistoryAsync(AccountNumber accountId)
    {
        NpgsqlConnection connection = await connectionProvider.GetConnectionAsync(default).ConfigureAwait(false);

        const string sql = """
                            SELECT timestamp, type, account_number, balance_delta
                            FROM transactions
                            """;

        using var command = new NpgsqlCommand(sql, connection);
        using NpgsqlDataReader reader = await command.ExecuteReaderAsync().ConfigureAwait(false);

        var transactions = new List<Transaction>();

        while (await reader.ReadAsync().ConfigureAwait(false))
        {
            transactions.Add(new Transaction(
                reader.GetDateTime(0),
                await reader.GetFieldValueAsync<TransactionTypes>(1).ConfigureAwait(false),
                new AccountNumber(reader.GetString(2)),
                new Money(reader.GetDecimal(3))));
        }

        return transactions;
    }
}