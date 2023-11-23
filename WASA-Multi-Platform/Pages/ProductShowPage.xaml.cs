using WASA_Multi_Platform.Entity;
using Npgsql;
using WASA_Multi_Platform.Const;
using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;

namespace WASA_Multi_Platform.Pages;

public partial class ProductShowPage : ContentPage
{
    bool seller_type = true;

    List<ProductShowEntity> products = new();

    public ProductShowPage()
    {
        InitializeComponent();
        if (UserEntity.ID == null)
            Navigation.PushAsync(new AuthPage());

        var timer = new Stopwatch();
        timer.Start();
        if (!BarcodeInfo.BarcodeSended)
        {
            ProductShowListView.ItemsSource = ProductActivities.ProductsLoad();
        }
        timer.Stop();

        TimeSpan timeTaken = timer.Elapsed;
        string foo = timeTaken.ToString(@"m\:ss\.fff");
        var toast = Toast.Make("Time to load data from db: " + foo, CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
        toast.Show();

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

    private void articleEntry_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (articleEntry.Text.Length == 6)
        {
            articleEntry.TextColor = Color.FromArgb("#000000");
            ProductShowListView.ItemsSource = ProductActivities.GetProductByCode(6, articleEntry.Text);
        }
            
        else
            articleEntry.TextColor = Color.FromArgb("#F08080");
    }

    private void barcodeEntry_TextChanged(object sender, TextChangedEventArgs e)
    {

        if (barcodeEntry.Text.Length == 13)
        {
            barcodeEntry.TextColor = Color.FromArgb("#000000");
            ProductShowListView.ItemsSource = ProductActivities.GetProductByCode(13, barcodeEntry.Text);
        }
        else
            barcodeEntry.TextColor = Color.FromArgb("#F08080");
    }

    private void ReadBarcodeButton_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new BarcodeReaderPage());
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (BarcodeInfo.BarcodeSended)
        {
            Dispatcher.Dispatch(() =>
            {
                barcodeEntry.Text = BarcodeInfo.Barcode;
                BarcodeInfo.BarcodeSended = false;
            });
        }
        
    }

    private void tonextpage_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        if (UserEntity.ID == null)
            Navigation.PushAsync(new AuthPage());
    }
}