using System.Collections.ObjectModel;
using System.Windows.Input;
using PayBuddyApp.DTOs.Debt;
using PayBuddyApp.Interfaces;

namespace PayBuddyApp.ViewModels
{
    public class DebtsViewModel : BaseViewModel
    {
        private readonly IDebtService _debtService;

        public ObservableCollection<DebtDto> Debts { get; } = new();

        public ICommand MarkAsPaidCommand { get; }

        public DebtsViewModel(IDebtService debtService)
        {
            _debtService = debtService;

            MarkAsPaidCommand = new Command<DebtDto>(async (debt) => await MarkAsPaidAsync(debt));
        }

        public async Task LoadDebtsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Debts.Clear();

                var debts = await _debtService.GetUserDebtsAsync();

                foreach (var debt in debts)
                {
                    Debts.Add(debt);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task MarkAsPaidAsync(DebtDto? debt)
        {
            if (debt == null || debt.IsPaid)
                return;

            var success = await _debtService.MarkAsPaidAsync(debt.DebtId);

            if (success)
            {
                Debts.Remove(debt);
                await Application.Current!.MainPage!.DisplayAlert("Succes", "Gæld markeret som betalt.", "OK");
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", "Kunne ikke opdatere gæld.", "OK");
            }
        }
    }
}
