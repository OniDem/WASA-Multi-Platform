using CommunityToolkit.Maui.Alerts;
using WASA_Multi_Platform.Activities;
using WASA_Multi_Platform.Entity;

namespace WASA_Multi_Platform.Pages;

public partial class ProductAddPage : ContentPage
{
	public ProductAddPage()
	{
		List<string> list = new()
		{
			"�������",
            "��������",
            "���",
            "���",
            "���",
            "���",
            "TWS",
            "���������",
            "����������",
            "��������",
            "���������",
            "��� ��",
            "����� Huawei",
            "����� IPhone",
            "����� Samsung",
            "����� Xiaomi",
            "����� Huawei",
            "����� IPhone",
            "����� Samsung",
            "����� Xiaomi",
            "����� Oppo",
            "����� Tecno",
            "����� Realme",
            "����� �������������"
        };
		InitializeComponent();
        TryLaterEntry.IsVisible = false;
        TypePicker.ItemsSource = list;
	}

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        TryLaterEntry.IsVisible = false;
        if (Validations.EntryValid(BarcodeEntry.Text) && BarcodeEntry.Text.Length == 13)
        {
            if (Validations.EntryValid(ArticleEntry.Text) && ArticleEntry.Text.Length == 6)
            {
                if (TypePicker.SelectedItem != null)
                {
                    if (Validations.EntryValid(NameEntry.Text))
                    {
                        if (Validations.EntryValid(PriceEntry.Text))
                        {
                            if (Validations.EntryValid(CountEntry.Text))
                            {
                                ProductAddEntity product = new()
                                {
                                    Barcode = BarcodeEntry.Text,
                                    Article = ArticleEntry.Text,
                                    Type = TypePicker.SelectedItem.ToString(),
                                    Name = NameEntry.Text,
                                    Price = Convert.ToInt32(PriceEntry.Text),
                                    Count = Convert.ToInt32(CountEntry.Text)
                                };
                                Dispatcher.Dispatch(async () => {
                                    var result = await ProductAddActivities.AddProductAsync(product);
                                    if (result)
                                    {
                                        BarcodeEntry.Text = "";
                                        ArticleEntry.Text = "";
                                        TypePicker.SelectedItem = null;
                                        NameEntry.Text = "";
                                        PriceEntry.Text = "";
                                        CountEntry.Text = "";

                                        var toast = Toast.Make("����� ������� ��������!", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                                        toast.Show();
                                    }
                                    else
                                        TryLaterEntry.IsVisible = true;
                                });
                                
                                
                            }
                            else
                            {
                                var toast = Toast.Make("���� ���������� ������", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                                toast.Show();
                            }
                        }
                        else
                        {
                            var toast = Toast.Make("���� ���� ������", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                            toast.Show();
                        }
                    }
                    else
                    {
                        var toast = Toast.Make("���� ������������ ������", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                        toast.Show();
                    }
                }
                else
                {
                    var toast = Toast.Make("��� �� ����� ���� ������", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                    toast.Show();
                }
            }
            else
            {
                var toast = Toast.Make("���� ������� ������ ��� ����� �������� �� ����� 6 ", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                toast.Show();
            }
        }
        else
        {
            var toast = Toast.Make("���� �������� ������ ��� ����� �������� �� ����� 13", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
            toast.Show();
        }
    }
}