using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NPO_System_Testovoe
{
    internal class DataBase
    {
        private string connString = "Host=localhost;Username=postgres;Password=example;Database=postgres";
        private NpgsqlDataSource? dataSource;

        async public Task SaveTender(Tender tender)
        {
            await using var dataSource = NpgsqlDataSource.Create(connString);
            await using var conn = dataSource.OpenConnection();
            Console.WriteLine("Подключено к БД!");

            await using var cmd = dataSource.CreateCommand(
                "CREATE TABLE IF NOT EXISTS tenders (" +
                    "govRuId varchar(32) NOT NULL," +
                    "tenderName varchar(128) NOT NULL," +
                    "cost real NOT NULL," +
                    "costCurrency varchar(5) NOT NULL," +
                    "datePublic TIMESTAMP NOT NULL," +
                    "customerName varchar(128) NOT NULL," +
                    "customerInn varchar(32)," +
                    "PRIMARY KEY (govRuId))");

            await cmd.ExecuteNonQueryAsync();
            Console.WriteLine("Таблица тендеров обнаружена, добавляем тендер...");

            await using var cmd2 = dataSource.CreateCommand(
                "INSERT INTO tenders(govRuId, tenderName, cost, costCurrency, datePublic, customerName, customerInn)" +
                "VALUES ($1, $2, $3, $4, $5, $6, $7)" +
                "ON CONFLICT(govRuId)" +
                "DO UPDATE SET " +
                "tenderName = EXCLUDED.tenderName," +
                "cost = EXCLUDED.cost," +
                "costCurrency = EXCLUDED.costCurrency," +
                "datePublic = EXCLUDED.datePublic," +
                "customerName = EXCLUDED.customerName," +
                "customerInn = EXCLUDED.customerInn;");

            cmd2.Parameters.AddWithValue(tender.govRuId);
            cmd2.Parameters.AddWithValue(tender.tenderName);
            cmd2.Parameters.AddWithValue(tender.cost);
            cmd2.Parameters.AddWithValue(tender.costCurrency);
            cmd2.Parameters.AddWithValue(tender.datePublic);
            cmd2.Parameters.AddWithValue(tender.customerName);
            cmd2.Parameters.AddWithValue(tender.customerInn ?? "NULL");

            await cmd2.ExecuteNonQueryAsync();
            Console.WriteLine("Выполнено!...");
        }
    }
}
