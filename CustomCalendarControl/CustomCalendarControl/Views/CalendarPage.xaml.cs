using CustomCalendarControl.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CustomCalendarControl.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : Rg.Plugins.Popup.Pages.PopupPage
    {
        public CalendarPage(DateTime date)
        {
            InitializeComponent();
            this.BindingContext = new CalendarPageViewModel(date);
        }
        private void TapGestureRecognizer_Tapped_ForCancelDate(object sender, EventArgs e)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await PopupNavigation.Instance.PopAsync();
            });
        }
        public ICommand SelectedDateCommand { get; set; }

        private void TapGestureRecognizer_Tapped_ForSelectDate(object sender, EventArgs e)
        {
            var btnObject = (Label)sender;
            var context = (CalendarPageViewModel)btnObject.BindingContext;
            SelectedDateCommand.Execute(context.SelectedDate);
            TapGestureRecognizer_Tapped_ForCancelDate(sender, null);
        }
    }
}