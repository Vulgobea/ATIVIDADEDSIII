using ATIVIDADEDSIII.Models;
using System.Threading.Tasks;

namespace ATIVIDADEDSIII.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto
			{
				Descricao = txt_descricao.Text,
				Quantidade = Convert.ToDouble(txt_quantidade.Text),
				Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = picker_categoria.SelectedItem as string
            };

			await App.Db.Insert(p);
			await DisplayAlert("Sucesso!", "Registro Inserido", "OK");
			await Navigation.PopAsync();

        }
		catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    
}