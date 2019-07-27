using CommonClasses.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinCashierDAL
{
    public static class CashierDAL
    {
        public static CashierDTO GetCashier(int idCashier)
        {
            CashierDTO cashier = null;

            var dbConnection = SQLiteAccess.CreateConnection();

            dbConnection.Open();

            using (var dbCommand = new System.Data.SQLite.SQLiteCommand(dbConnection))
            {
                dbCommand.CommandText = @"
                    SELECT ID_CASHIER, DESCRIPTION
                        FROM CASHIER
                        WHERE ID_CASHIER = @ID_CASHIER";
                dbCommand.Parameters.AddWithValue("@ID_CASHIER", idCashier);

                /*
                 IF YOU ARE GETTING AN EXCEPTION HERE IT'S PROBABLY BECAUSE I COULDN'T CONFIGURE THE DATABASE PROPERLY
                 IT IS THE FILE "CoinCashierDB.db" IN CoinCashierDAL
                 THAT IS NOT BEING COPYED TO WHERE VISUAL STUDIO RUN THE APPLICATION (the "bin" folder)
                 */

                using (var reader = dbCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cashier = new CashierDTO()
                        {
                            idCashier = Convert.ToInt32(reader["ID_CASHIER"]),
                            description = reader["DESCRIPTION"].ToString()
                        };
                    }
                }
            }

            if (cashier != null)
            {
                cashier.coinBalanceDTOs = CoinBalanceDAL.GetCoinBalance(idCashier, dbConnection);
            }

            dbConnection.Close();
            dbConnection.Dispose();

            return cashier;
        }
    }
}
