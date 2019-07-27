using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonClasses.DTO;

namespace CoinCashierDAL
{
    public class CoinBalanceDAL
    {
        public static List<CoinBalanceDTO> GetCoinBalance(int idCashier, SQLiteConnection dbConnection)
        {
            List<CoinBalanceDTO> coinBalances = new List<CoinBalanceDTO>();

            using (var dbCommand = new System.Data.SQLite.SQLiteCommand(dbConnection))
            {
                dbCommand.CommandText = @"
                    SELECT ID_COIN_BALANCE, COIN_VALUE, QUANTITY
                        FROM COIN_BALANCE
                        WHERE ID_CASHIER = @ID_CASHIER
                        ORDER BY COIN_VALUE DESC";
                dbCommand.Parameters.AddWithValue("@ID_CASHIER", idCashier);

                using (var reader = dbCommand.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        coinBalances.Add(new CoinBalanceDTO()
                        {
                            idCoinBalance = Convert.ToInt32(reader["ID_COIN_BALANCE"]),
                            coinValue = Convert.ToInt32(reader["COIN_VALUE"]),
                            quantity = Convert.ToInt32(reader["QUANTITY"])
                        });
                    }
                }
            }

            return coinBalances;
        }

        public static void UpdateCashierBalance(int idCashier, List<CoinBalanceDTO> newCashierCoinBalance)
        {
            var dbConnection = SQLiteAccess.CreateConnection();

            dbConnection.Open();

            foreach (CoinBalanceDTO coinBalance in newCashierCoinBalance)
            {
                UpdateCashierBalance(idCashier, coinBalance, dbConnection);
            }

            dbConnection.Close();
            dbConnection.Dispose();
        }

        public static void UpdateCashierBalance(int idCashier, CoinBalanceDTO newCashierCoinBalance)
        {
            var dbConnection = SQLiteAccess.CreateConnection();

            dbConnection.Open();

            UpdateCashierBalance(idCashier, newCashierCoinBalance, dbConnection);

            dbConnection.Close();
            dbConnection.Dispose();
        }

        public static void UpdateCashierBalance(int idCashier, CoinBalanceDTO coinBalance, SQLiteConnection dbConnection)
        {
            if (coinBalance.idCoinBalance.HasValue == false)
            {
                InsertCoinBalance(idCashier, coinBalance, dbConnection);
            }
            else if (coinBalance.quantity > 0)
            {
                UpdateCoinBalance(idCashier, coinBalance, dbConnection);
            }
            else
            {
                DeleteCoinBalance(idCashier, coinBalance, dbConnection);
            }
        }

        public static void UpdateCoinBalance(int idCashier, CoinBalanceDTO coinBalance)
        {
            var dbConnection = SQLiteAccess.CreateConnection();

            dbConnection.Open();

            UpdateCoinBalance(idCashier, coinBalance, dbConnection);

            dbConnection.Close();
            dbConnection.Dispose();
        }

        public static void UpdateCoinBalance(int idCashier, CoinBalanceDTO coinBalance, SQLiteConnection dbConnection)
        {
            using (var dbCommand = new System.Data.SQLite.SQLiteCommand(dbConnection))
            {
                dbCommand.CommandText = @"
                        UPDATE COIN_BALANCE SET QUANTITY = @QUANTITY
                            WHERE ID_COIN_BALANCE = @ID_COIN_BALANCE";
                dbCommand.Parameters.AddWithValue("@ID_COIN_BALANCE", coinBalance.idCoinBalance.Value);
                dbCommand.Parameters.AddWithValue("@QUANTITY", coinBalance.quantity);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void DeleteCoinBalance(int idCashier, CoinBalanceDTO coinBalance, SQLiteConnection dbConnection)
        {
            using (var dbCommand = new System.Data.SQLite.SQLiteCommand(dbConnection))
            {
                dbCommand.CommandText = @"
                    DELETE
                        FROM COIN_BALANCE
                        WHERE ID_COIN_BALANCE = @ID_COIN_BALANCE";
                dbCommand.Parameters.AddWithValue("@ID_COIN_BALANCE", coinBalance.idCoinBalance.Value);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void InsertCoinBalance(int idCashier, CoinBalanceDTO coinBalance)
        {
            var dbConnection = SQLiteAccess.CreateConnection();

            dbConnection.Open();

            InsertCoinBalance(idCashier, coinBalance, dbConnection);

            dbConnection.Close();
            dbConnection.Dispose();
        }

        public static void InsertCoinBalance(int idCashier, CoinBalanceDTO coinBalance, SQLiteConnection dbConnection)
        {
            using (var dbCommand = new System.Data.SQLite.SQLiteCommand(dbConnection))
            {
                dbCommand.CommandText = @"
                    INSERT INTO COIN_BALANCE
                    (ID_CASHIER, COIN_VALUE, QUANTITY)
                    VALUES
                    (@ID_CASHIER, @COIN_VALUE, @QUANTITY);
                    SELECT LAST_INSERT_ROWID() AS ID_COIN_BALANCE";
                dbCommand.Parameters.AddWithValue("@ID_CASHIER", idCashier);
                dbCommand.Parameters.AddWithValue("@COIN_VALUE", coinBalance.coinValue);
                dbCommand.Parameters.AddWithValue("@QUANTITY", coinBalance.quantity);
                //dbCommand.ExecuteNonQuery();

                using (var reader = dbCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        coinBalance.idCoinBalance = Convert.ToInt32(reader["ID_COIN_BALANCE"]);
                    }
                }
            }
        }
    }
}
