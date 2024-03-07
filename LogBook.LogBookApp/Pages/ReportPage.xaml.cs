using LogBook.LogBookCore.ViewModels;

namespace LogBook.LogBookApp.Pages;

public partial class ReportPage : ContentPage
{
	public ReportPage(ReportViewModel viewModel)
	{
		InitializeComponent();
		this.BindingContext = viewModel;
	}
}