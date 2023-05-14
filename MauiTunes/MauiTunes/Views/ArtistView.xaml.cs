using MauiTunes.ViewModels;
using TinyMvvm;

namespace MauiTunes.Views;

public partial class ArtistView
{
	public ArtistView(ArtistViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}