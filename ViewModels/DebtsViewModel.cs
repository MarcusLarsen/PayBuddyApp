using System.Collections.ObjectModel;
using System.Windows.Input;
using PayBuddyApp.DTOs.Debt;
using PayBuddyApp.Interfaces;

namespace PayBuddyApp.ViewModels
{
    public class DebtsViewModel : BaseViewModel
    {
        private readonly IDebtService _debtService;

        public ObservableCollection<DebtRequestDto> DebtRequests { get; } = new();
        public ObservableCollection<DebtDto> Debts { get; } = new();

        public ICommand AcceptDebtCommand { get; }
        public ICommand DeclineDebtCommand { get; }
        public ICommand MarkAsPaidCommand { get; }

        public DebtsViewModel(IDebtService debtService)
        {
            _debtService = debtService;

            AcceptDebtCommand = new Command<DebtRequestDto>(async (debt) => await AcceptDebtAsync(debt));
            DeclineDebtCommand = new Command<DebtRequestDto>(async (debt) => await DeclineDebtAsync(debt));
            MarkAsPaidCommand = new Command<DebtDto>(async (debt) => await MarkAsPaidAsync(debt));
        }

        public async Task LoadDebtsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                DebtRequests.Clear();
                Debts.Clear();

                var requests = await _debtService.GetDebtRequestsAsync();
                var debts = await _debtService.GetUserDebtsAsync();

                foreach (var request in requests)
                    DebtRequests.Add(request);

                foreach (var debt in debts)
                    Debts.Add(debt);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task AcceptDebtAsync(DebtRequestDto? debt)
        {
            if (debt == null)
                return;

            var success = await _debtService.AcceptDebtAsync(debt.DebtId);

            if (success)
            {
                DebtRequests.Remove(debt);
                await LoadDebtsAsync();

                await Application.Current!.MainPage!.DisplayAlert("Succes", "Gæld accepteret.", "OK");
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", "Kunne ikke acceptere gæld.", "OK");
            }
        }

        private async Task DeclineDebtAsync(DebtRequestDto? debt)
        {
            if (debt == null)
                return;

            var success = await _debtService.DeclineDebtAsync(debt.DebtId);

            if (success)
            {
                DebtRequests.Remove(debt);

                await Application.Current!.MainPage!.DisplayAlert("Afvist", "Gæld afvist.", "OK");
            }
            else
            {
                await Application.Current!.MainPage!.DisplayAlert("Fejl", "Kunne ikke afvise gæld.", "OK");
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