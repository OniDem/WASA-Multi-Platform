using CommunityToolkit.Maui.Alerts;

namespace WASA_Multi_Platform.Pages;

public partial class ProductAddPage : ContentPage
{
	public ProductAddPage()
	{
		List<string> list = new List<string>()
		{
			"�������",
            "��������",
            "���",
            "���",
            "���",
            "���",
            "TWS",
            "���������",
        };
		InitializeComponent();
        TypePicker.ItemsSource = list;
	}

    private void AddButton_Clicked(object sender, EventArgs e)
    {
        if (Validations.EntryValid(BarcodeEntry.Text) && BarcodeEntry.Text.Length == 13)
        {
            if (Validations.EntryValid(ArticleEntry.Text) && ArticleEntry.Text.Length == 6)
            {
                if (TypePicker.SelectedItem != null)
                {
                    if (Validations.EntryValid(NameEntry.Text) && NameEntry.Text.Length > 0)
                    {
                        if (Validations.EntryValid(PriceEntry.Text) && NameEntry.Text.Length > 0)
                        {
                            if (Validations.EntryValid(CountEntry.Text) && CountEntry.Text.Length > 0)
                            {
                                var toast = Toast.Make("����� ������� ��������!", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                                toast.Show();
                                //������� ����� ������� ������ �� ������� WASA
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

    private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {

        var toast = Toast.Make(TypePicker.SelectedItem.ToString(), CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
        toast.Show();
        
    }
}