using WASA_Multi_Platform.Entity;
using Npgsql;
using WASA_Multi_Platform.Const;
using static Android.Icu.Text.Transliterator;

namespace WASA_Multi_Platform.Pages;

public partial class ProductShowPage : ContentPage
{
    bool seller_type = true;

    List<ProductShowEntity> products = new();

    public ProductShowPage()
	{
		InitializeComponent();
        Products_Load();
        switch (seller_type)
        {
            case false:
                Title = "Товары                                             Администатор";
                break;
            case true:
                Title = "Товары                                                           Кассир";
                break;
        }
    }

    private async void Products_Load()
    {
        NpgsqlConnection con = new(DBConnection.ConnectionString);
        NpgsqlCommand command;
        await con.OpenAsync();
        command = new($"SELECT product_name, product_count, product_price FROM products WHERE product_type = 'Провода'", con);
        var reader = await command.ExecuteReaderAsync();
        while (reader.Read())
        {
            ProductShowEntity product = new();
            product.Name = reader["product_name"].ToString();
            product.Description = "Остаток на складе: " + reader["product_count"].ToString() + "  Цена: " + reader["product_price"].ToString();
            products.Add(product);
        }
        await con.CloseAsync();

        ProductShowListView.ItemsSource = products;
    }

    private void ProductShowListView_ItemTapped(object sender, ItemTappedEventArgs e)
    {
        int index = e.ItemIndex;
        DisplayAlert("Tapped", index  + " item tapped", "OK");
    }

    private async void GetProduct(int lenght)
    {
        try
        {
            NpgsqlConnection con = new(DBConnection.ConnectionString);
            NpgsqlCommand command;
            ProductShowEntity product = new();
            await con.OpenAsync();

            switch (lenght)
            {
                case 6:

                    command = new($"SELECT product_name FROM products WHERE article = '{articleEntry.Text}'", con);
                    product.Name = Convert.ToString(await command.ExecuteScalarAsync());
                    command = new($"SELECT product_price FROM products WHERE article = '{articleEntry.Text}'", con);
                    product.Description = "Цена: " + Convert.ToString(await command.ExecuteScalarAsync());
                    break;
                case 13:
                    command = new($"SELECT product_name FROM products WHERE barcode = '{barcodeEntry.Text}'", con);
                    product.Name = Convert.ToString(await command.ExecuteScalarAsync());
                    command = new($"SELECT product_price FROM products WHERE barcode = '{barcodeEntry.Text}'", con);
                    product.Description = "Цена: " + Convert.ToString(await command.ExecuteScalarAsync());
                    break;
            }

            con.Close();
            if (products.Count > 0)
                products.Clear();
            products.Add(product);
            ProductShowListView.ItemsSource = products;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }

    private void articleEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (articleEntry.Text.Length == 6)
        {
            articleEntry.TextColor = Color.FromArgb("#000000");
            GetProduct(6);
        }
            
        else
            articleEntry.TextColor = Color.FromArgb("#F08080");
    }

    private void barcodeEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (barcodeEntry.Text.Length == 13)
        {
            barcodeEntry.TextColor = Color.FromArgb("#000000");
            GetProduct(13);
        }
        else
            barcodeEntry.TextColor = Color.FromArgb("#F08080");
    }
}