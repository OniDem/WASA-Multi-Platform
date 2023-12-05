using CommunityToolkit.Maui.Alerts;
using Npgsql;
using WASA_Multi_Platform.Const;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Activities
{
    static class ProductAddActivities
    {
        private static NpgsqlConnection con = new(DBConnection.ConnectionString);
        private static NpgsqlCommand command;

        public static async Task <bool> AddProductAsync(ProductAddEntity product)
        {
            try
            {
                SessionEntity user = FileIOActivities.GetSessionsData();
                con.Open();
                string sql = $"INSERT INTO products (article, barcode, product_type, product_name, product_count, product_price, add_man) VALUES ('{product.Article}', '{product.Barcode}', '{product.Type}', '{product.Name}', '{product.Count}', '{product.Price}', '{user.UserName}')";
                command = new(sql, con!);
                await command.ExecuteNonQueryAsync();
                con!.Close();
                return true;
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message, CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                toast.Show();
                return false;
            }
        }
    }
}
