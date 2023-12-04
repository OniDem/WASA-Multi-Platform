using WASA_Multi_Platform.Const;
using ZXing.Net.Maui;
using CommunityToolkit.Maui.Alerts;
using WASA_Multi_Platform.Entity;
using WASA_Multi_Platform.Activities;

namespace WASA_Multi_Platform.Pages;

public partial class BarcodeReaderPage : ContentPage
{
    bool navigated = false;
    public BarcodeReaderPage()
    {
        InitializeComponent();
        if (!FileIOActivities.HaveSettingsApplied())
            Navigation.PushAsync(new SettingsPage());
        else
        {
            if (!FileIOActivities.UserAuthorized())
            {
                Navigation.PushAsync(new AuthPage());
            }
        }

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
        cameraBarcodeReaderView.IsTorchOn = !cameraBarcodeReaderView.IsTorchOn;
        cameraBarcodeReaderView.AutoFocus();

        cameraBarcodeReaderView.CameraLocation = CameraLocation.Rear;
        BarcodeInfo.Barcode = null;
        BarcodeInfo.BarcodeSended = false;

    }

    public BarcodeReaderPage(bool navigatedfrom)
    {
        InitializeComponent();
        navigated = navigatedfrom;
        if (!FileIOActivities.HaveSettingsApplied())
            Navigation.PushAsync(new SettingsPage());
        else
        {
            if (!FileIOActivities.UserAuthorized())
            {
                Navigation.PushAsync(new AuthPage());
            }
        }
        BarcodeLabel.IsVisible = false;
        NameLabel.IsVisible = false;
        PriceLabel.IsVisible = false;
        CountLabel.IsVisible = false;

        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormats.All,
            AutoRotate = true,
            Multiple = true
        };
        cameraBarcodeReaderView.IsTorchOn = !cameraBarcodeReaderView.IsTorchOn;
        cameraBarcodeReaderView.AutoFocus();

        cameraBarcodeReaderView.CameraLocation = CameraLocation.Rear;
        BarcodeInfo.Barcode = null;
        BarcodeInfo.BarcodeSended = false;

    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        //Дописать два варианта после направления(получение товара взять с productShow) 

        if (navigated)
        {
            if (!BarcodeInfo.BarcodeSended)
            {
                
                Dispatcher.Dispatch(() =>
                {
                    var toast = Toast.Make("(DevInfo) Barcode detected ", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                    toast.Show();
                });
                if (!BarcodeInfo.BarcodeSended)
                {
                    Dispatcher.Dispatch(() =>
                    {
                        BarcodeInfo.Barcode = e.Results[0].Value;
                        BarcodeInfo.BarcodeSended = true;
                        var toast = Toast.Make("Sended barcode: " + BarcodeInfo.Barcode, CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                        toast.Show();
                        //DisplayAlert("Barcode", barcode.Format + "->" + barcode.Value, "OK");
                    });
                }
            }

            Dispatcher.Dispatch(() =>
            {
                Navigation.RemovePage(this);
            });
            
        }
        else
        {
            Dispatcher.Dispatch(() =>
            {
                ProductShowEntity entity = ProductActivities.GetProductByCode(e.Results[0].Value);
                BarcodeLabel.Text = "Штрихкод: " + entity.Barcode;
                NameLabel.Text = "Наименование: " + entity.Name;
                PriceLabel.Text = "Цена: " + entity.Price.ToString();
                CountLabel.Text = "Количество: " + entity.Count.ToString();
            });
        }
    }


    private void SwitchCameraButton_Clicked(object sender, EventArgs e)
    {
        cameraBarcodeReaderView.CameraLocation = cameraBarcodeReaderView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }
}
