using BattleshipBG.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Input;

namespace BattleshipBG.Commands
{
    public class OceanClickedCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private GameViewModel gameViewModel;

        public OceanClickedCommand(GameViewModel gameViewModel)
        {
            this.gameViewModel = gameViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            var piece = gameViewModel.P1.Ocean[(int)parameter];
            var point = new Point(piece.X, piece.Y);
            var result = gameViewModel.P1.UnderAttack(point);
            piece.CurrentStatus = result;
        }
    }
}
