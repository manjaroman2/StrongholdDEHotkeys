using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Binarysharp.MemoryManagement;
using HarmonyLib;
using MelonLoader;
using Noesis;
using Stronghold1DE;
using UnityEngine;

namespace Ritterschlag
{
    public class Ritterschlag : MelonMod
    {
        private static Process currentProc;

        /*
        public static MethodInfo PrefixFactory(MethodInfo method)
        {
            DynamicMethod prefix = new DynamicMethod(method.Name + "_Patch", typeof(void),
                method.GetParameters().Select(info => info.ParameterType).ToArray(), typeof(EngineInterface), false);
            // DynamicMethod prefix = new(method.Name + "_Patch", typeof(void),
            //     method.GetParameters().Types(), typeof(EngineInterface), false);
            ILGenerator il = prefix.GetILGenerator();
            il.Emit(OpCodes.Ldarg_0);
            il.Emit(OpCodes.Ldstr, method.Name);
            il.EmitCall(OpCodes.Call, MelonloggerMsg, new[] { typeof(string) });
            il.Emit(OpCodes.Ret);
            return prefix;
        }
         */

        public override void OnEarlyInitializeMelon()
        {
            MelonLogger.Msg("[Ritterschlag]: OnEarlyInitializeMelon");
        }


        public override void OnInitializeMelon()
        {
            currentProc = Process.GetCurrentProcess();
            MelonLogger.Msg("OnInitializeMelon");
        }

        public override void OnLateInitializeMelon()
        {
            MelonLogger.Msg("OnLateInitializeMelon");
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            MelonLogger.Msg($"Scene: {buildIndex} {sceneName}");
            if (buildIndex == 1)
            {


                // foreach (Button hudMainButton in BetterHotkeys.HUD_Main_Buttons)
                // {
                //     if (hudMainButton.Command != null)
                //     {
                //         MelonLogger.Msg($"{hudMainButton.Command} {hudMainButton.CommandParameter}");
                //         Utils.dumpObject(hudMainButton.Command);   
                //     }
                // }
            }
        }

        public override void OnLateUpdate()
        {
            // if (Director.instance != null && Director.instance.SimRunning) Hotkeys.processKey();

            if (Input.GetKeyDown(KeyCode.L))
            {
                // CustomLogger.PrintFull();
            }
            else if (Input.GetKeyDown(KeyCode.K) && Director.gameStarted)
            {
                // foreach (ProcessModule module in Process.GetCurrentProcess().Modules)
                // {
                //     if (module.ModuleName.Contains("Stronghold") || module.ModuleName.Contains("stronghold"))
                //     {
                //         MelonLogger.Msg(module);
                //         MelonLogger.Msg(module.BaseAddress.ToString("X"));
                //         MelonLogger.Msg(module.ModuleName);
                //         MelonLogger.Msg(module.FileName);
                //     }
                // }
                //
                // using (MemorySharp memory = new(currentProc))
                // {
                //     int gold = memory["StrongholdDE.dll"].Read<int>(0x1AD000 + 0x1fbd564);
                //     MelonLogger.Msg(gold);
                //     memory["StrongholdDE.dll"].Write(0x1AD000 + 0x1fbd564, gold + 50000);
                // }
                //
                // MelonLogger.Msg("+50000");


                // EngineInterface.GameAction(Enums.GameActionCommand.Game_Paused, 0, 0);
                // OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_GAME_PAUSED, 0);
                // EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 3, 0);
                // EngineInterface.GameAction(Enums.GameActionCommand.SH1Cheats, 2, 0);

                // EngineInterface.GameAction(Enums.GameActionCommand.MakeTroop, 5, 22); // 22 = archer in MainViewModel.Setup_eChimpDictionary

                // EngineInterface.GameAction(Enums.GameActionCommand.Game_Paused, 1, 1);
                // OnScreenText.Instance.addOSTEntry(Enums.eOnScreenText.OST_GAME_PAUSED, 1);
            }
        }
    }
}