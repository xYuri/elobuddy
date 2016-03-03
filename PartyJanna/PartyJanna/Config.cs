﻿using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Collections.Generic;

namespace PartyJanna
{
    public static class Config
    {
        private const string MenuName = "PartyJanna";

        private static readonly Menu Menu;

        static Config()
        {
            Menu = MainMenu.AddMenu(MenuName, MenuName.ToLower());
            Menu.AddGroupLabel("Welcome to PartyJanna settings menu!");

            Settings.Initialize();
        }

        public static void Initialize() { }

        public static class Settings
        {
            private static readonly Menu Menu;

            static Settings()
            {
                Menu = Config.Menu.AddSubMenu("Settings");

                Draw.Initialize();
                Menu.AddSeparator(13);

                AntiGapcloser.Initialize();
                Menu.AddSeparator(13);

                Interrupter.Initialize();
                Menu.AddSeparator(13);

                Items.Initialize();
                Menu.AddSeparator(13);

                AutoShield.Initialize();
                Menu.AddSeparator(13);

                Combo.Initialize();
                Menu.AddSeparator(13);

                Flee.Initialize();
                Menu.AddSeparator(13);

                Harass.Initialize();
                Menu.AddSeparator(13);
            }

            public static void Initialize()
            {
            }

            public static class Draw
            {
                private static readonly CheckBox _drawQ;
                private static readonly CheckBox _drawW;
                private static readonly CheckBox _drawE;
                private static readonly CheckBox _drawR;

                public static bool DrawQ
                {
                    get { return _drawQ.CurrentValue; }
                }
                public static bool DrawW
                {
                    get { return _drawW.CurrentValue; }
                }
                public static bool DrawE
                {
                    get { return _drawE.CurrentValue; }
                }
                public static bool DrawR
                {
                    get { return _drawR.CurrentValue; }
                }

                static Draw()
                {
                    Menu.AddGroupLabel("Draw");

                    _drawQ = Menu.Add("drawQ", new CheckBox("Draw Q Range"));
                    _drawW = Menu.Add("drawW", new CheckBox("Draw W Range"));
                    _drawE = Menu.Add("drawE", new CheckBox("Draw E Range"));
                    _drawR = Menu.Add("drawR", new CheckBox("Draw R Range"));
                }

                public static void Initialize() { }
            }

            public static class Items
            {
                private static readonly CheckBox _useItems;

                public static bool UseItems
                {
                    get { return _useItems.CurrentValue; }
                }

                static Items()
                {
                    Menu.AddGroupLabel("Items");

                    _useItems = Menu.Add("useItems", new CheckBox("Use Items"));
                }

                public static void Initialize() { }
            }

            public static class AntiGapcloser
            {
                private static readonly CheckBox _antiGapcloser;

                public static bool AntiGap
                {
                    get { return _antiGapcloser.CurrentValue; }
                }

                static AntiGapcloser()
                {
                    Menu.AddGroupLabel("Anti-Gapcloser");

                    _antiGapcloser = Menu.Add("antiGapcloser", new CheckBox("Anti-Gapcloser"));
                }

                public static void Initialize() { }
            }

            public static class Interrupter
            {
                private static readonly CheckBox _qInterrupt;
                private static readonly CheckBox _qInterruptDangerous;
                private static readonly CheckBox _rInterruptDangerous;

                public static bool QInterrupt
                {
                    get { return _qInterrupt.CurrentValue; }
                }
                public static bool QInterruptDangerous
                {
                    get { return _qInterruptDangerous.CurrentValue; }
                }
                public static bool RInterruptDangerous
                {
                    get { return _rInterruptDangerous.CurrentValue; }
                }

                static Interrupter()
                {
                    Menu.AddGroupLabel("Interrupter");

                    _qInterrupt = Menu.Add("qInterrupt", new CheckBox("Interrupt low/med-danger spells with Q"));
                    Menu.AddSeparator(13);

                    _qInterruptDangerous = Menu.Add("rInterrupt", new CheckBox("Interrupt high-danger spells with Q"));
                    Menu.AddSeparator(13);

                    _rInterruptDangerous = Menu.Add("rInterruptDangerous", new CheckBox("Interrupt high-danger spells with R"));
                }

                public static void Initialize() { }
            }

            public static class AutoShield
            {
                private static readonly CheckBox _boostAD;
                private static readonly CheckBox _selfShield;
                private static readonly ComboBox _priorMode;
                private static readonly List<Slider> _sliders;
                private static readonly List<AIHeroClient> _heros;
                private static readonly List<CheckBox> _shieldAllyList;

                public static bool BoostAD
                {
                    get { return _boostAD.CurrentValue; }
                }
                public static bool SelfShield
                {
                    get { return _selfShield.CurrentValue; }
                }
                public static int PriorMode
                {
                    get { return _priorMode.SelectedIndex; }
                }
                public static List<Slider> Sliders
                {
                    get { return _sliders; }
                }
                public static List<AIHeroClient> Heros
                {
                    get { return _heros; }
                }
                public static List<CheckBox> ShieldAllyList
                {
                    get { return _shieldAllyList; }
                }

                static AutoShield()
                {
                    Menu.AddGroupLabel("AutoShield");

                    _boostAD = Menu.Add("autoShieldBoostAd", new CheckBox("Use E to boost ally AD"));
                    Menu.AddSeparator(13);

                    _selfShield = Menu.Add("selfShield", new CheckBox("Shield Yourself"));
                    Menu.AddSeparator(13);

                    _priorMode = Menu.Add("autoShieldPriorMode", new ComboBox("AutoShield Priority Mode:", 0, new string[] { "Lowest Health", "Priority Level" }));
                    Menu.AddSeparator(13);

                    _sliders = new List<Slider>();
                    _heros = new List<AIHeroClient>();
                    _shieldAllyList = new List<CheckBox>();

                    foreach (var ally2 in EntityManager.Heroes.Allies)
                    {
                        _shieldAllyList.Add(Menu.Add<CheckBox>("Shield " + ally2.ChampionName, new CheckBox(string.Format("Shield {0} ({1})", ally2.ChampionName, ally2.Name))));
                    }

                    Menu.AddSeparator(13);

                    foreach (var ally in EntityManager.Heroes.Allies)
                    {
                        if (ally.ChampionName != Program.ChampName)
                        {
                            Slider PrioritySlider = Menu.Add<Slider>(ally.ChampionName, new Slider(string.Format("{0} Priority:", ally.ChampionName, ally.Name), 1, 1, EntityManager.Heroes.Allies.Count - 1));

                            Menu.AddSeparator(13);

                            _sliders.Add(PrioritySlider);

                            _heros.Add(ally);
                        }
                    }
                }

                public static void Initialize() { }
            }

            public static class Combo
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly Slider _qUseRange;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static int QUseRange
                {
                    get { return _qUseRange.CurrentValue; }
                }

                static Combo()
                {
                    Menu.AddGroupLabel("Combo");

                    _useQ = Menu.Add("comboUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("comboUseW", new CheckBox("Use W"));
                    Menu.AddSeparator();

                    _qUseRange = Menu.Add<Slider>("qUseRangeCombo", new Slider("Use Q at range:", 1100, 1100, 1700));
                }

                public static void Initialize() { }
            }

            public static class Flee
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                //private static readonly CheckBox _useR;
                private static readonly Slider _qUseRange;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                /*public static bool UseR
                {
                    get { return _useR.CurrentValue; }
                }*/
                public static int QUseRange
                {
                    get { return _qUseRange.CurrentValue; }
                }

                static Flee()
                {
                    Menu.AddGroupLabel("Flee");

                    _useQ = Menu.Add("fleeUseQ", new CheckBox("Use Q"));
                    _useW = Menu.Add("fleeUseW", new CheckBox("Use W"));
                    //_useR = Menu.Add("comboUseR", new CheckBox("Use R", false));
                    Menu.AddSeparator();

                    _qUseRange = Menu.Add<Slider>("qUseRangeFlee", new Slider("Use Q at range:", 1100, 1100, 1700));
                }

                public static void Initialize() { }
            }

            public static class Harass
            {
                private static readonly CheckBox _useQ;
                private static readonly CheckBox _useW;
                private static readonly CheckBox _autoHarass;
                private static readonly Slider _autoHarassManaPercent;
                private static readonly Slider _qUseRange;

                public static bool UseQ
                {
                    get { return _useQ.CurrentValue; }
                }
                public static bool UseW
                {
                    get { return _useW.CurrentValue; }
                }
                public static bool AutoHarass
                {
                    get { return _autoHarass.CurrentValue; }
                }
                public static int AutoHarassManaPercent
                {
                    get { return _autoHarassManaPercent.CurrentValue; }
                }
                public static int QUseRange
                {
                    get { return _qUseRange.CurrentValue; }
                }

                static Harass()
                {
                    Menu.AddGroupLabel("Harass");

                    _useQ = Menu.Add("harassUseQ", new CheckBox("Use Q"));
                    Menu.AddSeparator(13);

                    _qUseRange = Menu.Add<Slider>("qUseRangeHarass", new Slider("Use Q at range:", 1100, 1100, 1700));
                    Menu.AddSeparator();

                    _useW = Menu.Add("harassUseW", new CheckBox("Use W"));
                    Menu.AddSeparator();

                    _autoHarass = Menu.Add("autoHarass", new CheckBox("Auto Harass with W at mana %"));
                    Menu.AddSeparator(13);

                    _autoHarassManaPercent = Menu.Add<Slider>("autoHarassManaPercent", new Slider("Auto Harass min. mana %:", 75));
                }

                public static void Initialize() { }
            }
        }
    }
}
