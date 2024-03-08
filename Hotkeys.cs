using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Timers;
using HarmonyLib;
using MelonLoader;
using UnityEngine;

namespace Ritterschlag
{
    public static class Hotkeys
    {
        private class BuildingChain
        {
            public readonly KeyCode[] keys;
            public readonly Enums.eMappers buildingType;

            public BuildingChain(KeyCode[] k, Enums.eMappers t)
            {
                keys = k;
                buildingType = t;
            }
        }

        public static readonly FieldInfo FunctionMapField = typeof(KeyManager).GetField("functionMap", AccessTools.all);
        public static int[,] functionMap;
        public static Dictionary<KeyCode, Func<object>> CustomMap = new();
        public static Dictionary<KeyCode, Enums.KeyFunctions> KeyMap = new();

        private static readonly List<BuildingChain> AllChains = new List<BuildingChain>
        {
            // Walls 
            new(new[] { KeyCode.Q, KeyCode.Q }, Enums.eMappers.MAPPER_STAIR),
            new(new[] { KeyCode.Q, KeyCode.W }, Enums.eMappers.MAPPER_WOODWALL),
            new(new[] { KeyCode.Q, KeyCode.E }, Enums.eMappers.MAPPER_WALL),
            new(new[] { KeyCode.Q, KeyCode.R }, Enums.eMappers.MAPPER_CRENAL),
            // Towers 
            new(new[] { KeyCode.Q, KeyCode.T, KeyCode.Q }, Enums.eMappers.MAPPER_TOWER1),
            new(new[] { KeyCode.Q, KeyCode.T, KeyCode.W }, Enums.eMappers.MAPPER_TOWER2),
            new(new[] { KeyCode.Q, KeyCode.T, KeyCode.E }, Enums.eMappers.MAPPER_TOWER3),
            new(new[] { KeyCode.Q, KeyCode.T, KeyCode.R }, Enums.eMappers.MAPPER_TOWER4),
            new(new[] { KeyCode.Q, KeyCode.T, KeyCode.T }, Enums.eMappers.MAPPER_TOWER5),
            // Engineer
            new(new[] { KeyCode.Q, KeyCode.Y, KeyCode.Q }, Enums.eMappers.MAPPER_ENGINEERS_GUILD),
            new(new[] { KeyCode.Q, KeyCode.Y, KeyCode.W }, Enums.eMappers.MAPPER_ENGINEERS_GUILD),
            new(new[] { KeyCode.Q, KeyCode.Y, KeyCode.E }, Enums.eMappers.MAPPER_STAIR),
            new(new[] { KeyCode.Q, KeyCode.Y, KeyCode.R }, Enums.eMappers.MAPPER_STAIR),
            new(new[] { KeyCode.Q, KeyCode.Y, KeyCode.T }, Enums.eMappers.MAPPER_STAIR),
            new(new[] { KeyCode.Q, KeyCode.G, KeyCode.Q }, Enums.eMappers.MAPPER_STAIR),
            new(new[] { KeyCode.Q, KeyCode.G, KeyCode.W }, Enums.eMappers.MAPPER_STAIR),
            new(new[] { KeyCode.Q, KeyCode.G, KeyCode.E }, Enums.eMappers.MAPPER_STAIR),
            new(new[] { KeyCode.Q, KeyCode.G, KeyCode.R }, Enums.eMappers.MAPPER_STAIR),
            new(new[] { KeyCode.Q, KeyCode.G, KeyCode.T }, Enums.eMappers.MAPPER_STAIR),
        };

        private static List<BuildingChain> possibleChains = new(AllChains);

        private static Timer buildChainTimer()
        {
            Timer t = new(1000);
            t.Elapsed += (_, _) =>
            {
                chainIndex = 0;
                MelonLogger.Msg(possibleChains.Count);
                if (possibleChains.Count != 1) return;
                EngineInterface.StartMapperItem((int)possibleChains[0].buildingType);
            };
            t.AutoReset = false;
            return t;
        }

        private static readonly Timer chainTimeout = buildChainTimer();
        private static int chainIndex = 0;

        public static void processKey()
        {
            List<BuildingChain> filteredPossibleChains = possibleChains.Where(possibleChain =>
                chainIndex < possibleChain.keys.Length &&
                Input.GetKeyDown(possibleChain.keys[chainIndex])).ToList();
            MelonLogger.Msg(
                $"{chainIndex}, {filteredPossibleChains.Count}, {possibleChains.Count}, {filteredPossibleChains.Count == possibleChains.Count}");

            if (filteredPossibleChains.Count == 0 || filteredPossibleChains.Count == possibleChains.Count) return;
            // MelonLogger.Msg(
            //     $"{chainIndex}, {filteredPossibleChains.Count}, {possibleChains.Count}, {filteredPossibleChains.Count == possibleChains.Count}");
            possibleChains = new List<BuildingChain>(filteredPossibleChains);
            // MelonLogger.Msg($"{chainIndex}, {possibleChains.Count}");
            chainIndex += 1;
            chainTimeout.Stop();
            chainTimeout.Start();
        }
    }

#if AAA
    [HarmonyPatch(typeof(ConfigSettings))]
    internal static class ConfigSettings_Patch
    {
        [HarmonyPostfix]
        [HarmonyPatch(nameof(ConfigSettings.LoadSettings))]
        private static void LoadSettings()
        {
            Hotkeys.functionMap = Hotkeys.FunctionMapField.GetValue(KeyManager.instance) as int[,];
            if (Hotkeys.functionMap == null)
            {
                MelonLogger.Error("HOTKEY MODULE ERROR: functionMap == null");
            }
            else
            {
                for (int i = 0; i < 97; i++)
                {
                    Hotkeys.KeyMap[(KeyCode)Hotkeys.functionMap[i, 0]] = (Enums.KeyFunctions)i;
                    // functionMap[i, 1]  
                }
            }
        }
    }

    [HarmonyPatch(typeof(KeyManager))]
    internal static class KeyManager_Patch
    {
        [HarmonyPrefix]
        [HarmonyPatch(nameof(KeyManager.SetNewKey))]
        private static bool SetNewKey(Enums.KeyFunctions func, int newKey, int column, ref KeyManager __instance)
        {
            int[,] functionMap = Hotkeys.FunctionMapField.GetValue(__instance) as int[,];
            Hotkeys.functionMap ??= functionMap;
            if (newKey > 0)
            {
                for (int index = 0; index < 97; ++index)
                {
                    if (functionMap![index, 0] == newKey)
                        functionMap[index, 0] = -1;
                    if (functionMap[index, 1] == newKey)
                        functionMap[index, 1] = -1;
                }
            }

            functionMap![(int)func, column] = newKey;
            Hotkeys.KeyMap[(KeyCode)newKey] = func;
            return false;
        }
    }
#endif
}