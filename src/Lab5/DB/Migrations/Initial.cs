using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace Itmo.ObjectOrientedProgramming.Lab5.DB.Migrations;

[Migration(1, "Initial")]
internal class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
    """
    CREATE TYPE transactionType
        (
            'withdrawal' ,
            'deposit' ,
        );
        
        CREATE TABLE IF NOT EXISTS accounts
        (
            account_number VARCHAR(44) PRIMARY KEY ,
            pin_code VARCHAR(44) NOT NULL ,
            balance DECIMAL(20,2) NOT NULL DEFAULT 0.00
        );
        
        CREATE TABLE IF NOT EXISTS transactions
        (
            id SERIAL PRIMARY KEY ,
            timestamp TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP ,
            account_number VARCHAR(44) NOT NULL REFERENCES accounts(number) ,
            balance_delta DECIMAL(20,2) NOT NULL CHECK (balance_delta >= 0) ,
            type transactionType ,
        );
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
    """
    drop table accounts;
    drop table transactions;
    
    drop type transactionType;
    """;
}