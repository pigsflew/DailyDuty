﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using DailyDuty.Interfaces;
using DailyDuty.Utilities;
using Dalamud.Interface;

namespace DailyDuty.Components.Graphical
{
    internal class TreasureMapCountdown : ICountdownTimer
    {
        public bool Enabled => Service.Configuration.TimerSettings.TreasureMapCountdownEnabled;
        public int ElementWidth => Service.Configuration.TimerSettings.TimerWidth;
        public Vector4 Color => Service.Configuration.TimerSettings.TreasureMapCountdownColor;

        void ICountdownTimer.DrawContents()
        {
            var now = DateTime.Now;

            var harvestTime = Service.Configuration.Current().TreasureMap.LastMapGathered;
            var nextAvailableTime = harvestTime.AddHours(18);

            var timeRemaining = nextAvailableTime - now;
            var percentage = (float)(1 - timeRemaining / TimeSpan.FromHours(18));

            if (now > nextAvailableTime)
            {
                percentage = 1.0f;
                timeRemaining = TimeSpan.Zero;
            }

            Draw.DrawProgressBar(percentage, "Treasure Map", timeRemaining, ImGuiHelpers.ScaledVector2(ElementWidth, 20), Color);
        }
    }
}