using CommunityToolkit.Maui.Alerts;
using Npgsql;
using WASA_Multi_Platform.Const;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform
{
    static class ProductActivities
    {
        private static NpgsqlConnection con = new(DBConnection.ConnectionString);
        private static NpgsqlCommand command;

        public static List<ProductShowEntity> ProductListLoad()
        {
            List<ProductShowEntity> products = new();
            try
            {
                con.Open();
                command = new($"SELECT barcode, product_name, product_price, product_count FROM products", con);
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    products.Add(
                        new ProductShowEntity
                        {
                            Barcode = "Штрихкод: " + reader["barcode"].ToString(),
                            Name = reader["product_name"].ToString(),
                            Description = "Остаток на складе: " + reader["product_count"].ToString() + "  Цена: " + reader["product_price"].ToString()
                        }
                    );
                }
                con.Close();
                return products;
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message);
                toast.Show();
                return products;
            }

        }

        public static List<ProductShowEntity> GetProductListByCode(int lenght, string code)
        {
            List<ProductShowEntity> products = new();
            try
            {
                ProductShowEntity product = new();
                con.Open();

                switch (lenght)
                {
                    case 6:

                        command = new($"SELECT barcode FROM products WHERE article = '{code}'", con);
                        product.Barcode = Convert.ToString(command.ExecuteScalar());
                        command = new($"SELECT product_name FROM products WHERE article = '{code}'", con);
                        product.Name = Convert.ToString(command.ExecuteScalar());
                        command = new($"SELECT product_price FROM products WHERE article = '{code}'", con);
                        product.Description = "Цена: " + Convert.ToString(command.ExecuteScalar());
                        break;
                    case 13:
                        command = new($"SELECT barcode FROM products WHERE barcode = '{code}'", con);
                        product.Barcode = Convert.ToString(command.ExecuteScalar());
                        command = new($"SELECT product_name FROM products WHERE barcode = '{code}'", con);
                        product.Name = Convert.ToString(command.ExecuteScalar());
                        command = new($"SELECT product_price FROM products WHERE barcode = '{code}'", con);
                        product.Description = "Цена: " + Convert.ToString(command.ExecuteScalar());
                        break;
                }
                con.Close();
                products.Add(product);
                return products;
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message);
                toast.Show();
                return products;
            }
        }

        public static ProductShowEntity GetProductByCode(string barcode)
        {
            try
            {
                ProductShowEntity product = new();
                con.Open();
                product.Barcode = barcode;
                command = new($"SELECT product_name FROM products WHERE barcode = '{barcode}'", con);
                product.Name = Convert.ToString(command.ExecuteScalar());
                command = new($"SELECT product_price FROM products WHERE barcode = '{barcode}'", con);
                product.Price = Convert.ToInt32(command.ExecuteScalar());
                command = new($"SELECT product_count FROM products WHERE barcode = '{barcode}'", con);
                product.Count = Convert.ToInt32(command.ExecuteScalar());
                con.Close();
                return product;
            }
            catch (Exception ex)
            {
                var toast = Toast.Make(ex.Message);
                toast.Show();
                return null;
            }
        }
    }
}
