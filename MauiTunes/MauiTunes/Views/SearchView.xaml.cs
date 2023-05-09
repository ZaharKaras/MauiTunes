using MauiTunes.ViewModels;

namespace MauiTunes.View;

public partial class SearchView
{
	private readonly SearchViewModel _viewModel;
	public SearchView(SearchViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}
}