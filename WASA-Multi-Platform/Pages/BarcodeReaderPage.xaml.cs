using WASA_Multi_Platform.Const;
using ZXing.Net.Maui;
using CommunityToolkit.Maui.Alerts;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Pages;

public partial class BarcodeReaderPage : ContentPage
{
    public BarcodeReaderPage()
    {
        InitializeComponent();
        if (UserEntity.ID == null)
            Navigation.PushAsync(new AuthPage());

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

    protected async void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        if (!BarcodeInfo.BarcodeSended)
        {
            await Dispatcher.DispatchAsync(() =>
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

    private void SwitchCameraButton_Clicked(object sender, EventArgs e)
    {
        cameraBarcodeReaderView.CameraLocation = cameraBarcodeReaderView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }

    private void ContentPage_NavigatedFrom(object sender, NavigatedFromEventArgs e)
    {
        if (UserEntity.ID == null)
            Navigation.PushAsync(new AuthPage());
    }
}