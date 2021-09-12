using BattleshipBG.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleshipBG.Commands
{
    public class DropBoatCommand : ICommand
    {
        private GameViewModel gameViewModel;

        public DropBoatCommand(GameViewModel gameViewModel)
        {
            this.gameViewModel = gameViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
