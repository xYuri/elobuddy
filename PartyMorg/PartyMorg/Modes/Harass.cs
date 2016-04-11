﻿using EloBuddy;
using EloBuddy.SDK;
using System;
using System.Diagnostics;
using Humanizer = PartyMorg.Config.Settings.Humanizer;
using Settings = PartyMorg.Config.Settings.Harass;

namespace PartyMorg.Modes
{
    public sealed class Harass : ModeBase
    {
        public override bool ShouldBeExecuted()
        {
            /*if (Settings.AutoHarass && Player.Instance.ManaPercent >= Settings.AutoHarassManaPercent)
            {
                var target = GetTarget(W, DamageType.Magical);

                if (target != null)
                {
                    W.Cast(target);
                }
            }*/

            return Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Harass);
        }

        public static Stopwatch stopwatch = new Stopwatch();

        public override void Execute()
        {
            var target = GetTarget(Q, DamageType.Magical);

            if (target != null && target.IsTargetable && !target.HasBuffOfType(BuffType.SpellImmunity) && Settings.UseQ)
            {
                if (Humanizer.QCastDelayEnabled)
                {
                    if (Humanizer.QRndmDelay)
                    {
                        stopwatch.Start();

                        if (stopwatch.ElapsedMilliseconds >= new Random().Next(250, Humanizer.QCastDelay))
                        {
                            var pred = Q.GetPrediction(target);

                            if (pred.HitChancePercent >= Settings.QMinHitChance)
                            {
                                Q.Cast(pred.CastPosition);
                            }

                            stopwatch.Reset();
                        }
                    }
                    else
                    {
                        stopwatch.Start();

                        if (stopwatch.ElapsedMilliseconds >= Humanizer.QCastDelay)
                        {
                            var pred = Q.GetPrediction(target);

                            if (pred.HitChancePercent >= Settings.QMinHitChance)
                            {
                                Q.Cast(pred.CastPosition);
                            }

                            stopwatch.Reset();
                        }
                    }
                }
                else
                {
                    var pred = Q.GetPrediction(target);

                    if (pred.HitChancePercent >= Settings.QMinHitChance)
                    {
                        Q.Cast(pred.CastPosition);
                    }
                }
            }

            target = GetTarget(W, DamageType.Magical);

            if (target != null && Settings.UseW)
            {
                W.Cast(W.GetPrediction(target).CastPosition);
            }
        }
    }
}
