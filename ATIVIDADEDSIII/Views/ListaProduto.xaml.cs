using ATIVIDADEDSIII.Models;
using System.Collections.ObjectModel;

namespace ATIVIDADEDSIII.Views;

public partial class ListaProduto : ContentPage
{
	ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

	public ListaProduto()
	{
        InitializeComponent();

		lst_produtos.ItemsSource = lista;
    }

	protected async override void OnAppearing()
	{
		List<Produto> tmp = await App.Db.GetAll();

		tmp.ForEach( i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Navigation.PushAsync(new Views.NovoProduto());

        } catch (Exception ex)
		{
			DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
		string q = e.NewTextValue;

		lista.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;

            Produto produto = selecionado.BindingContext as Produto;
            bool confirm = await DisplayAlert("Deseja Excluir?", $"Remover {produto.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(produto.Id);
                lista.Remove(produto);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}