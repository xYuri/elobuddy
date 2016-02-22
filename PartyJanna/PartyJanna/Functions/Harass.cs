﻿using EloBuddy;
using EloBuddy.SDK;
using System;

namespace PartyJanna.Functions
{
    public static class Harass
    {
        private static bool IgnoreMinionCollision { get; set; }

        public static void Execute()
        {
            Startup.CurrentFunction = "Harass";

            if (Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass) && TargetSelector.SelectedTarget.IsValid && TargetSelector.SelectedTarget.IsEnemy)
            {
                if (Config.Harass.UseQ.CurrentValue && Player.Instance.Mana >= Config.Spells.manaQ[Config.Spells.Q.Level] && TargetSelector.SelectedTarget.IsInRange(Player.Instance, Config.Spells.Q.Range))
                {
                    if (Player.Instance.CountEnemiesInRange(Config.Spells.Q.Range + 525) <= 2)
                    {
                        IgnoreMinionCollision = false;
                    }
                    else
                    {
                        IgnoreMinionCollision = true;
                    }

                    Config.Spells.Q.Cast(Prediction.Position.GetPrediction(TargetSelector.SelectedTarget, new Prediction.Position.PredictionData(Prediction.Position.PredictionData.PredictionType.Circular, Convert.ToInt32(Config.Spells.Q.Range), Config.Spells.Q.Width, Config.Spells.Q.ConeAngleDegrees, Config.Spells.Q.CastDelay, Config.Spells.Q.Speed), IgnoreMinionCollision).CastPosition);

                    if (Config.Harass.UseE.CurrentValue && Player.Instance.Mana >= Config.Spells.manaE[Config.Spells.E.Level])
                    {
                        foreach (AIHeroClient Enemy in EntityManager.Heroes.Enemies)
                        {
                            if (Enemy.Spellbook.SpellWasCast && Player.Instance.IsInRange(Enemy, Enemy.CastRange))
                            {
                                Config.Spells.E.Cast(Player.Instance);
                            }
                        }
                    }

                }

                if (Config.Harass.UseW.CurrentValue && Player.Instance.Mana >= Config.Spells.manaW[Config.Spells.W.Level] && TargetSelector.SelectedTarget.IsInRange(Player.Instance, Config.Spells.W.Range))
                {
                    Config.Spells.W.Cast(TargetSelector.SelectedTarget);

                    if (Config.Harass.UseE.CurrentValue && Player.Instance.Mana >= Config.Spells.manaE[Config.Spells.E.Level])
                    {
                        foreach (AIHeroClient Enemy in EntityManager.Heroes.Enemies)
                        {
                            if (Enemy.Spellbook.SpellWasCast && Player.Instance.IsInRange(Enemy, Enemy.CastRange))
                            {
                                Config.Spells.E.Cast(Player.Instance);
                            }
                        }
                    }
                }
            }
        }
    }
}

