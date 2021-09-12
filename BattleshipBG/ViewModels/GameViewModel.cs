﻿using BattleshipBG.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace BattleshipBG.ViewModels
{
    public class GameViewModel : BaseViewModel
    {
        public PlayerViewModel P1 { get; set; } = new Human();

        public PlayerViewModel P2 { get; set; } = new Cpu();

        public ICommand PlaceShipRandomlyCommand { get; }
        public ICommand OceanClickedCommand { get; set; }
        public ICommand DropBoatCommand { get; }
        public GameViewModel()
        {
            PlaceShipRandomlyCommand = new PlaceShipRandomlyCommand(this);
            OceanClickedCommand = new OceanClickedCommand(this);
            DropBoatCommand = new DropBoatCommand(this);
        }
    }
}
