using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using Noesis;
using Stronghold1DE;
using UnityEngine;

// ReSharper disable UnusedMember.Local

namespace Ritterschlag
{
    public static class BetterHotkeys
    {
        public class HotkeyAction
        {
            // public Action Action;
            public KeyCode key;
            public Action Action;

            protected HotkeyAction(KeyCode key)
            {
                this.key = key;
            }
        }

        public class _BuildingAction : HotkeyAction
        {
            public _BuildingAction(string name, KeyCode key) : base(key)
            {
                string buttonName = $"ButtonBuild{name}";
                Action = () =>
                {
                    Button button =
                        (Button)MainViewModel.Instance.HUDmain.FindName(buttonName);
                    MainViewModel.Instance.ButtonBuildCommand.Execute(button.CommandParameter);
                };
            }
        }


        public class _BaseTabAction : HotkeyAction
        {
            private readonly int SubMode;

            public int getSubMode()
            {
                return SubMode;
            }

            protected _BaseTabAction(KeyCode key, int subMode) : base(key)
            {
                SubMode = subMode;
            }
        }

        public class _TabAction : _BaseTabAction
        {
            private RadioButton _radioButton;
            protected readonly MethodInfo tabMethod;

            public void setRadioButton(RadioButton radioButton)
            {
                _radioButton = radioButton;
            }

            protected _TabAction(int SubMode, KeyCode key, string methodName) : base(key, SubMode)
            {
                tabMethod = typeof(HUD_Main).GetMethod(
                    methodName,
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new[] { typeof(object), typeof(RoutedEventArgs) },
                    null);
                Action = () =>
                {
                    tabMethod!.Invoke(MainViewModel.Instance.HUDmain, new[] { (object)null, null });
                    _radioButton!.IsChecked = true;
                };
            }
        }

        public class _SubTabAction : _BaseTabAction
        {
            private Button Button;

            public void setButton(Button button)
            {
                Button = button;
            }

            protected _SubTabAction(int SubMode, KeyCode key, string methodName) : base(key, SubMode)
            {
                MethodInfo tabMethod = typeof(HUD_Main).GetMethod(
                    methodName,
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    new[] { typeof(object), typeof(RoutedEventArgs) },
                    null);
                Action = () => { tabMethod!.Invoke(MainViewModel.Instance.HUDmain, new[] { (object)null, null }); };
            }
        }

        public class TabCastle : _TabAction
        {
            public readonly _BuildingAction Stairs = new("Stairs", KeyCode.Q);
            public readonly _BuildingAction WoodenWall = new("WoodenWall", KeyCode.W);
            public readonly _BuildingAction StoneWall = new("StoneWall", KeyCode.E);
            public readonly _BuildingAction CrenalatedWall = new("CrenalatedWall", KeyCode.R);

            public class _Military : _SubTabAction
            {
                public readonly _BuildingAction EngineersGuild = new("EngineersGuild", KeyCode.Q);
                public readonly _BuildingAction Mangonel = new("Mangonel", KeyCode.W);
                public readonly _BuildingAction Balista = new("Balista", KeyCode.E);
                public readonly _BuildingAction Stables = new("Stables", KeyCode.R);
                public readonly _BuildingAction TunnelersGuild = new("TunnelersGuild", KeyCode.T);
                public readonly _BuildingAction Cauldron = new("Cauldron", KeyCode.A);

                public _Military(KeyCode key) : base(14, key, nameof(HUD_Main.NewBuildScreenMilitaryBuildings))
                {
                }
            }

            public readonly _Military Military = new(KeyCode.F);
            public readonly _BuildingAction StoneBarracks = new("StoneBarracks", KeyCode.A);
            public readonly _BuildingAction WoodenBarracks = new("WoodenBarracks", KeyCode.S);
            public readonly _BuildingAction Armoury = new("Armoury", KeyCode.D);

            public class _Gates : _SubTabAction
            {
                public class _WoodenGate : _SubTabAction
                {
                    public readonly _BuildingAction WoodenGatehouseNorth = new("WoodenGatehouseNorth", KeyCode.Q);
                    public readonly _BuildingAction WoodenGatehouseEast = new("WoodenGatehouseEast", KeyCode.W);
                    public readonly _BuildingAction WoodenGatehouseSouth = new("WoodenGatehouseSouth", KeyCode.E);
                    public readonly _BuildingAction WoodenGatehouseWest = new("WoodenGatehouseWest", KeyCode.R);

                    public _WoodenGate(KeyCode key) : base(17, key,
                        nameof(HUD_Main.NewBuildScreenSubSubWoodenGates))
                    {
                    }
                }

                public class _SmallGate : _SubTabAction
                {
                    public readonly _BuildingAction SmallGatehouseNS = new("SmallGatehouseNS", KeyCode.Q);
                    public readonly _BuildingAction SmallGatehouseEW = new("SmallGatehouseEW", KeyCode.W);

                    public _SmallGate(KeyCode key) : base(18, key,
                        nameof(HUD_Main.NewBuildScreenSubSubSmallGates))
                    {
                    }
                }

                public class _LargeGate : _SubTabAction
                {
                    public readonly _BuildingAction LargeGatehouseNS = new("LargeGatehouseNS", KeyCode.Q);
                    public readonly _BuildingAction LargeGatehouseEW = new("LargeGatehouseEW", KeyCode.W);

                    public _LargeGate(KeyCode key) : base(19, key,
                        nameof(HUD_Main.NewBuildScreenSubSubSmallGates))
                    {
                    }
                }

                public readonly _WoodenGate WoodenGate = new(KeyCode.Q);
                public readonly _SmallGate SmallGate = new(KeyCode.W);
                public readonly _LargeGate LargeGate = new(KeyCode.E);
                public readonly _BuildingAction Drawbridge = new("Drawbridge", KeyCode.R);
                public readonly _BuildingAction DogCage = new("DogCage", KeyCode.T);
                public readonly _BuildingAction PitchDitch = new("PitchDitch", KeyCode.A);
                public readonly _BuildingAction KillingPit = new("KillingPit", KeyCode.S);
                public readonly _BuildingAction Brazier = new("Brazier", KeyCode.D);
                public readonly _BuildingAction Moat = new("Moat", KeyCode.F);
                public readonly _BuildingAction AntiMoat = new("AntiMoat", KeyCode.G);

                public _Gates(KeyCode key) : base(12, key, nameof(HUD_Main.NewBuildScreenGates))
                {
                }
            }

            public readonly _Gates Gates = new(KeyCode.G);

            public class _Towers : _SubTabAction
            {
                public readonly _BuildingAction WoodenTower = new("TowerA", KeyCode.Q);
                public readonly _BuildingAction SmallTower = new("TowerB", KeyCode.W);
                public readonly _BuildingAction MidTower = new("TowerC", KeyCode.E);
                public readonly _BuildingAction LargeTower = new("TowerD", KeyCode.R);
                public readonly _BuildingAction RoundTower = new("TowerE", KeyCode.T);

                public _Towers(KeyCode key) : base(11, key, nameof(HUD_Main.NewBuildScreenTowers))
                {
                }
            }

            public readonly _Towers Towers = new(KeyCode.T);

            public TabCastle(KeyCode key) : base(10, key, nameof(HUD_Main.NewBuildScreenCastle))
            {
            }
        }

        public class TabIndustry : _TabAction
        {
            public readonly _BuildingAction Stockpile = new("Stockpile", KeyCode.Q);
            public readonly _BuildingAction Woodcutter = new("Woodcutter", KeyCode.W);
            public readonly _BuildingAction Quarry = new("Quarry", KeyCode.E);
            public readonly _BuildingAction OxTether = new("OxTether", KeyCode.R);
            public readonly _BuildingAction IronMine = new("IronMine", KeyCode.A);
            public readonly _BuildingAction PitchRig = new("PitchRig", KeyCode.S);
            public readonly _BuildingAction Market = new("Market", KeyCode.D);

            public TabIndustry(KeyCode key) : base(20, key,
                nameof(HUD_Main.NewBuildScreenIndustry))
            {
            }
        }

        public class TabFarms : _TabAction
        {
            public readonly _BuildingAction Hunter = new("Hunter", KeyCode.Q);
            public readonly _BuildingAction DairyFarm = new("DairyFarm", KeyCode.W);
            public readonly _BuildingAction AppleFarm = new("AppleFarm", KeyCode.E);
            public readonly _BuildingAction WheatFarm = new("WheatFarm", KeyCode.R);
            public readonly _BuildingAction HopsFarm = new("HopsFarm", KeyCode.A);

            public TabFarms(KeyCode key) : base(40, key, nameof(HUD_Main.NewBuildScreenFarms))
            {
            }
        }

        public class TabTown : _TabAction
        {
            public readonly _BuildingAction Hovel = new("Hovel", KeyCode.Q);
            public readonly _BuildingAction SmallChurch = new("SmallChurch", KeyCode.W);
            public readonly _BuildingAction MidChurch = new("MedChurch", KeyCode.E);
            public readonly _BuildingAction LargeChurch = new("LargeChurch", KeyCode.R);
            public readonly _BuildingAction Apocathery = new("Apocathery", KeyCode.A);
            public readonly _BuildingAction Well = new("Well", KeyCode.S);

            public class _GoodStuff : _SubTabAction
            {
                public readonly _BuildingAction Maypole = new("Maypole", KeyCode.Q);
                public readonly _BuildingAction DancingBear = new("DancingBear", KeyCode.W);
                public readonly _BuildingAction GardenSmall = new("GardenSmall", KeyCode.E);
                public readonly _BuildingAction GardenMid = new("GardenMed", KeyCode.R);
                public readonly _BuildingAction GardenLarge = new("GardenLarge", KeyCode.T);
                public readonly _BuildingAction PilgrimsCross = new("PilgrimsCross", KeyCode.A);
                public readonly _BuildingAction Shrine = new("Shrine", KeyCode.S);
                public readonly _BuildingAction PondSmall = new("PondSmall", KeyCode.D);
                public readonly _BuildingAction PondLarge = new("PondLarge", KeyCode.F);
                public readonly _BuildingAction Flag1 = new("Flag1", KeyCode.G);
                public readonly _BuildingAction Flag2 = new("Flag4", KeyCode.Z);
                public readonly _BuildingAction Flag3 = new("Flag3", KeyCode.X);
                public readonly _BuildingAction Flag4 = new("Flag2", KeyCode.C);

                public _GoodStuff(KeyCode key) : base(34, key, nameof(HUD_Main.NewBuildScreenGoodStuff))
                {
                }
            }

            public class _BadStuff : _SubTabAction
            {
                public readonly _BuildingAction Gallows = new("Gallows", KeyCode.Q);
                public readonly _BuildingAction CessPit = new("CessPit", KeyCode.W);
                public readonly _BuildingAction Stocks = new("Stocks", KeyCode.E);
                public readonly _BuildingAction HeadOnSpike = new("HeadOnSpike", KeyCode.R);
                public readonly _BuildingAction BurningPost = new("BurningPost", KeyCode.T);
                public readonly _BuildingAction Dungeon = new("Dungeon", KeyCode.A);
                public readonly _BuildingAction StretchingRack = new("StretchingRack", KeyCode.S);
                public readonly _BuildingAction Gibbet = new("Gibbet", KeyCode.D);
                public readonly _BuildingAction ChoppingBlock = new("ChoppingBlock", KeyCode.F);
                public readonly _BuildingAction DunkingStool = new("DunkingStool", KeyCode.G);

                public _BadStuff(KeyCode key) : base(33, key, nameof(HUD_Main.NewBuildScreenBadStuff))
                {
                }
            }

            public readonly _GoodStuff GoodStuff = new(KeyCode.T);
            public readonly _BadStuff BadStuff = new(KeyCode.F);

            public TabTown(KeyCode key) : base(30, key, nameof(HUD_Main.NewBuildScreenTown))
            {
            }
        }

        public class TabWeapons : _TabAction
        {
            public readonly _BuildingAction Fletcher = new("Fletcher", KeyCode.Q);
            public readonly _BuildingAction Poleturner = new("Poleturner", KeyCode.W);
            public readonly _BuildingAction Blacksmith = new("Blacksmith", KeyCode.E);
            public readonly _BuildingAction Tanner = new("Tanner", KeyCode.R);
            public readonly _BuildingAction Armourer = new("Armourer", KeyCode.A);

            public TabWeapons(KeyCode key) : base(28, key, nameof(HUD_Main.NewBuildScreenWeapons))
            {
            }
        }

        public class TabFood : _TabAction
        {
            public readonly _BuildingAction Granary = new("Granary", KeyCode.Q);
            public readonly _BuildingAction Baker = new("Baker", KeyCode.W);
            public readonly _BuildingAction Mill = new("Mill", KeyCode.E);
            public readonly _BuildingAction Brewer = new("Brewer", KeyCode.R);
            public readonly _BuildingAction Inn = new("Inn", KeyCode.A);

            public TabFood(KeyCode key) : base(25, key, nameof(HUD_Main.NewBuildScreenFood))
            {
            }
        }

        // private enum TabModifier
        // {
        //     SHIFT,
        //     CTRL,
        //     ALT
        // }
        //
        // private static MethodInfo GetTabModifierMethod(TabModifier modifier)
        // {
        //     return modifier switch
        //     {
        //         TabModifier.SHIFT => typeof(KeyManager).GetMethod(nameof(KeyManager.isShiftDown)),
        //         TabModifier.CTRL => typeof(KeyManager).GetMethod(nameof(KeyManager.isCtrlDown)),
        //         TabModifier.ALT => typeof(KeyManager).GetMethod(nameof(KeyManager.isAltDown)),
        //         _ => null
        //     };
        // }

        // private static TabModifier CurrentTabModifier = TabModifier.SHIFT;

        public static class TabActions
        {
            public static readonly TabCastle TabCastle = new(KeyCode.Q);
            public static readonly TabIndustry TabIndustry = new(KeyCode.W);
            public static readonly TabFarms TabFarms = new(KeyCode.E);
            public static readonly TabTown TabTown = new(KeyCode.R);
            public static readonly TabWeapons TabWeapons = new(KeyCode.T);
            public static readonly TabFood TabFood = new(KeyCode.Y);
        }

        private static readonly List<_TabAction> _TabActions = typeof(TabActions)
            .GetFields(BindingFlags.Public | BindingFlags.Static).Select(info => (_TabAction)info.GetValue(null))
            .ToList();

        private static readonly List<_SubTabAction> _SubTabActions =
            _TabActions.SelectMany(Utils.GetFieldsRecursively<_SubTabAction>).ToList();

        private static readonly List<_BaseTabAction> _BaseTabActions =
            _TabActions.Concat<_BaseTabAction>(_SubTabActions).ToList();

        private static readonly Dictionary<_TabAction, List<HotkeyAction>> TabActionToHotkeyActions =
            _TabActions.ToDictionary(action => action,
                action => action.GetType().GetFields()
                    .Where(info => typeof(HotkeyAction).IsAssignableFrom(info.FieldType))
                    .Select(info => (HotkeyAction)info.GetValue(action)).ToList());

        private static Dictionary<KeyCode, _TabAction> KeyCode2TabAction =
            _TabActions.ToDictionary(action => action.key, action => action);

        private static readonly Dictionary<_BaseTabAction, List<HotkeyAction>> BaseTabAction2HotkeyActions =
            _BaseTabActions.ToDictionary(action => action,
                action => action.GetType().GetFields()
                    .Where(info => typeof(HotkeyAction).IsAssignableFrom(info.FieldType))
                    .Select(info => (HotkeyAction)info.GetValue(action)).ToList());

        private static readonly Dictionary<_BaseTabAction, Dictionary<KeyCode, HotkeyAction>>
            BaseTabAction2KeyCodeHotkeyAction =
                _BaseTabActions.ToDictionary(action => action,
                    action => action.GetType().GetFields()
                        .Where(info => typeof(HotkeyAction).IsAssignableFrom(info.FieldType))
                        .Select(info => (HotkeyAction)info.GetValue(action))
                        .ToDictionary(hotkeyAction => hotkeyAction.key, hotkeyAction => hotkeyAction));
        
        private static readonly Dictionary<int, _BaseTabAction> SubModeToBaseTabAction =
            _BaseTabActions.ToDictionary(action => action.getSubMode(), action => action);

        private static KeyCode? LastKey;
        private static DateTime LastKeyTime = DateTime.MinValue;

        public static int DoubleHitMillis = 200;

        // private static IEnumerable<KeyCode> AllKeyCodes = Enum.GetValues(typeof(KeyCode)).Cast<KeyCode>();
        private static IEnumerable<KeyCode>
            AllKeyCodes = _TabActions.SelectMany(Utils.GetFieldsRecursivelyAll<KeyCode>).Distinct();

        private static void UpdateAllKeyCodes()
        {
            KeyCode2TabAction = _TabActions.ToDictionary(action => action.key, action => action);
            AllKeyCodes = _TabActions.SelectMany(Utils.GetFieldsRecursivelyAll<KeyCode>).Distinct();
        }

        [HarmonyPatch(typeof(HUD_Main))]
        internal static class PATCH_HUD_Main
        {
            [HarmonyPostfix]
            [HarmonyPatch(MethodType.Constructor)]
            private static void POST_HUD_Main(ref HUD_Main __instance, ref Button[] ___buildButtons)
            {
                MelonLogger.Msg($"{___buildButtons}");
                // foreach (KeyCode allKeyCode in AllKeyCodes)
                // {
                //     MelonLogger.Msg($"{allKeyCode}");
                // }

                TabActions.TabCastle.setRadioButton(__instance.RefTabBuildCastle);
                TabActions.TabCastle.Military.setButton(___buildButtons[68]);
                TabActions.TabCastle.Gates.setButton(___buildButtons[70]);
                TabActions.TabCastle.Towers.setButton(___buildButtons[60]);

                TabActions.TabIndustry.setRadioButton(__instance.RefTabBuildIndustry);
                TabActions.TabFarms.setRadioButton(__instance.RefTabBuildFarms);
                TabActions.TabTown.setRadioButton(__instance.RefTabBuildTown);
                TabActions.TabTown.GoodStuff.setButton(___buildButtons[67]);
                TabActions.TabTown.BadStuff.setButton(___buildButtons[78]);

                TabActions.TabWeapons.setRadioButton(__instance.RefTabBuildWeapons);
                TabActions.TabFood.setRadioButton(__instance.RefTabBuildFood);
            }
        }

        [HarmonyPatch(typeof(KeyManager))]
        internal static class PATCH_KeyManager
        {
            [HarmonyPrefix]
            [HarmonyPatch("Update")]
            private static void PRE_Update(ref KeyManager __instance, ref bool ___hotKeySelectorMode)
            {
                if (___hotKeySelectorMode || Director.instance == null || !Director.instance.SimRunning) return;
#if try
                try
                {
#endif
                int millis = (int)(DateTime.Now - LastKeyTime).TotalMilliseconds;
                if (LastKey == null || millis > DoubleHitMillis)
                {
                    if (SubModeToBaseTabAction.TryGetValue(MainViewModel.Instance.SubMode,
                            out _BaseTabAction baseTabAction))
                    {
                        // List<HotkeyAction> HotkeyActions = BaseTabAction2HotkeyActions[baseTabAction];
                        Dictionary<KeyCode, HotkeyAction> KeyCodeToHotkeyAction =
                            BaseTabAction2KeyCodeHotkeyAction[baseTabAction];
                        foreach (KeyCode key in AllKeyCodes.Where(Input.GetKeyDown))
                        {
                            if (KeyCodeToHotkeyAction.TryGetValue(key, out HotkeyAction hotkeyAction))
                            {
                                MelonLogger.Msg(hotkeyAction);
                                MelonLogger.Msg(hotkeyAction.key);
                                hotkeyAction.Action();
                            }
                            LastKey = key;
                            LastKeyTime = DateTime.Now;   
                            break;
                        }
                    }
                    else
                    {
                        MelonLogger.Msg(
                            $"module:BetterHotkeys | Unhandled SubMode: {MainViewModel.Instance.SubMode}");
                    }
                }
                else if (millis < DoubleHitMillis)
                {
                    if (KeyCode2TabAction.TryGetValue((KeyCode)LastKey, out _TabAction tabAction))
                    {
                        foreach (HotkeyAction hotkeyAction in TabActionToHotkeyActions[tabAction]
                                     .Where(hotkeyAction => Input.GetKeyDown(hotkeyAction.key)))
                        {
                            MelonLogger.Msg("DOUBLE HIT");
                            MelonLogger.Msg(millis);
                            MelonLogger.Msg(hotkeyAction);
                            MelonLogger.Msg(hotkeyAction.key);

                            MelonLogger.Msg(hotkeyAction);
                            tabAction.Action();
                            hotkeyAction.Action();

                            LastKey = null;
                            LastKeyTime = DateTime.MinValue;
                            break;
                        }
                    }
                    else
                    {
                        MelonLogger.Msg($"Key does not correspond a TabAction: {LastKey}");
                    }
                }
#if try
                }
                catch (Exception e)
                {
                    MelonLogger.Msg($"{e}");
                    throw;
                }
#endif
            }
        }
    }
}