using ATIVIDADEDSIII.Models;
using System.Linq;

namespace ATIVIDADEDSIII.Views;

public partial class RelatorioPage : ContentPage
{
    public RelatorioPage()
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Busca todos os produtos do banco de dados
        var todos_os_produtos = await App.Db.GetAll();

        // A MÁGICA DO LINQ ACONTECE AQUI!
        var relatorio = todos_os_produtos
            .GroupBy(p => p.Categoria) // 1. Agrupa todos os produtos pela propriedade "Categoria"
            .Select(grupo => new GastoPorCategoria // 2. Para cada grupo, cria um novo objeto
            {
                Categoria = grupo.Key, // 3. A "Key" do grupo é o nome da categoria (ex: "Alimentos")
                TotalGasto = grupo.Sum(item => item.Total) // 4. Soma a propriedade "Total" de todos os itens do grupo
            })
            .ToList(); // 5. Converte o resultado para uma lista

        // Exibe o relatório final na ListView da nossa página
        lst_gastos_categoria.ItemsSource = relatorio;
    }
}