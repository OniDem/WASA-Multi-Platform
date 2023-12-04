using CommunityToolkit.Maui.Alerts;

namespace WASA_Multi_Platform.Pages;

public partial class ProductAddPage : ContentPage
{
	public ProductAddPage()
	{
		List<string> list = new List<string>()
		{
			"Провода",
            "Наушники",
            "СЗУ",
            "АЗУ",
            "БЗУ",
            "ПЗУ",
            "TWS",
            "Гарнитура",
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
                                var toast = Toast.Make("Товар успешно добавлен!", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                                toast.Show();
                                //Сделать метод добавки товара на подобии WASA
                            }
                            else
                            {
                                var toast = Toast.Make("Поле Количество пустое", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                                toast.Show();
                            }
                        }
                        else
                        {
                            var toast = Toast.Make("Поле Цена пустое", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                            toast.Show();
                        }
                    }
                    else
                    {
                        var toast = Toast.Make("Поле Наименование пустое", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                        toast.Show();
                    }
                }
                else
                {
                    var toast = Toast.Make("Тип не может быть пустым", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                    toast.Show();
                }
            }
            else
            {
                var toast = Toast.Make("Поле Артикул пустое или длина символов не равна 6 ", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                toast.Show();
            }
        }
        else
        {
            var toast = Toast.Make("Поле Штрихкод пустое или длина символов не равна 13", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
            toast.Show();
        }
    }

    private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
    {

        var toast = Toast.Make(TypePicker.SelectedItem.ToString(), CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
        toast.Show();
        
    }
}