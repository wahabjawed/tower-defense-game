using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Sage_Engine;

namespace EleTD
{
    public class SoundLibrary : SoundHandler
    {

        public void Initailise()
        {
            engine = new AudioEngine("Content\\Materia TD.xgs");
            soundBank = new SoundBank(engine, "Content\\Sound Bank.xsb");
            waveBank = new WaveBank(engine, "Content\\Wave Bank.xwb");

            soundLibrary["MainMenu"] = soundBank.GetCue("MainMenu");
            soundLibrary["SilverSageIntro"] = soundBank.GetCue("SilverSageIntro");
            soundLibrary["PauseScreen"] = soundBank.GetCue("PauseScreen");
            soundLibrary["MenuButtonClick"] = soundBank.GetCue("MenuButtonClick");
            soundLibrary["TowersFiring"] = soundBank.GetCue("TowersFiring");
            soundLibrary["EnemiesDyingNew"] = soundBank.GetCue("EnemiesDyingNew");
            soundLibrary["OnTowerSell"] = soundBank.GetCue("OnTowerSell");
            soundLibrary["OnTowerCreation"] = soundBank.GetCue("OnTowerCreation");
            soundLibrary["WaitingForWaves"] = soundBank.GetCue("WaitingForWaves");
            soundLibrary["EarthEnemies"] = soundBank.GetCue("EarthEnemies");
            soundLibrary["WaterEnemies"] = soundBank.GetCue("WaterEnemies");
            soundLibrary["WindEnemies"] = soundBank.GetCue("WindEnemies");
            soundLibrary["FireEnemies"] = soundBank.GetCue("FireEnemies");
            soundLibrary["LightningEnemies"] = soundBank.GetCue("LightningEnemies");
            soundLibrary["DarknessEnemies"] = soundBank.GetCue("DarknessEnemies");
            soundLibrary["DarknessAlly"] = soundBank.GetCue("DarknessAlly");
            soundLibrary["CannotBuildTower"] = soundBank.GetCue("CannotBuildTower");
            soundLibrary["FoolishDecision"] = soundBank.GetCue("FoolishDecision");
            soundLibrary["SoYouThink"] = soundBank.GetCue("SoYouThink");
            soundLibrary["EarthTower"] = soundBank.GetCue("EarthTower");

            soundLibrary["DarkTower"] = soundBank.GetCue("DarkTower");
            soundLibrary["FireTower"] = soundBank.GetCue("FireTower");
            soundLibrary["WaterTower"] = soundBank.GetCue("WaterTower");
            soundLibrary["WindTower"] = soundBank.GetCue("WindTower");
            soundLibrary["LightTower"] = soundBank.GetCue("LightTower");
            soundLibrary["TowersUpgrade"] = soundBank.GetCue("TowersUpgrade");

            soundLibrary["Marching"] = soundBank.GetCue("Marching");

            soundLibrary["Evil_laugh"] = soundBank.GetCue("Evil_laugh");
     
        }

    }
}
