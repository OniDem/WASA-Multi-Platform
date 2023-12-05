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
			"Провода",
            "Наушники",
            "СЗУ",
            "АЗУ",
            "БЗУ",
            "ПЗУ",
            "TWS",
            "Гарнитура",
            "Аксессуары",
            "Акустика",
            "Держатели",
            "Для ПК",
            "Плёнки Huawei",
            "Плёнки IPhone",
            "Плёнки Samsung",
            "Плёнки Xiaomi",
            "Стёкла Huawei",
            "Стёкла IPhone",
            "Стёкла Samsung",
            "Стёкла Xiaomi",
            "Стёкла Oppo",
            "Стёкла Tecno",
            "Стёкла Realme",
            "Стёкла Универсальные"
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

                                        var toast = Toast.Make("Товар успешно добавлен!", CommunityToolkit.Maui.Core.ToastDuration.Long, 14);
                                        toast.Show();
                                    }
                                    else
                                        TryLaterEntry.IsVisible = true;
                                });
                                
                                
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
}