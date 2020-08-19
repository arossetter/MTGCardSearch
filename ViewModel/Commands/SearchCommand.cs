using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Windows.Input;

namespace MTGCardSearch.ViewModel.Commands
{
    class SearchCommand : ICommand
    {
        private CardSearchVM VM = null;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public SearchCommand(CardSearchVM vm)
        {
            VM = vm;
        }

        public bool CanExecute(object parameter)
        {
            string query = parameter as string;
            return !string.IsNullOrWhiteSpace(query);
        }

        public void Execute(object parameter)
        {
            VM.MakeQuery();
        }
    }
}
