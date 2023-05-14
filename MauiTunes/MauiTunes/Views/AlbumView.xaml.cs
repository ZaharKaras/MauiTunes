using MauiTunes.ViewModels;
using TinyMvvm;
namespace MauiTunes.Views;

public partial class AlbumView
{
	public AlbumView(AlbumViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}