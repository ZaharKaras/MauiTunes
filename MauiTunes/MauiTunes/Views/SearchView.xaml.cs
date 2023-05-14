using MauiTunes.ViewModels;
using TinyMvvm;

namespace MauiTunes.Views;

public partial class SearchView
{
	private readonly SearchViewModel viewModel;
	public SearchView(SearchViewModel viewModel)
	{
		InitializeComponent();
		this.viewModel = viewModel;
		BindingContext = this.viewModel;
	}
}