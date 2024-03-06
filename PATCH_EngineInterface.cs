// #define PATCHALL 

using HarmonyLib;

namespace Ritterschlag
{
    [HarmonyPatch(typeof(EngineInterface))]
    public class PATCH_EngineInterface
    {
        #if PATCHALL 
        [HarmonyPrefix]
        [HarmonyPatch("DLL_PreInitMap_Editor")]
        private static unsafe void DLL_PreInitMap_Editor_Patch(int mapSize, int mapType, bool siegeThat,
            bool multiplayerMap, byte* retData)
        {
            CustomLogger.Msg("DLL_PreInitMap_Editor");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_PreInitMap_Campaign")]
        private static void DLL_PreInitMap_Campaign_Patch(int difficulty)
        {
            CustomLogger.Msg("DLL_PreInitMap_Campaign");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_PreInitMap_EcoCampaign")]
        private static void DLL_PreInitMap_EcoCampaign_Patch()
        {
            CustomLogger.Msg("DLL_PreInitMap_EcoCampaign");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_EcoCampaign_ChangeDifficulty")]
        private static void DLL_EcoCampaign_ChangeDifficulty_Patch(int difficulty)
        {
            CustomLogger.Msg("DLL_EcoCampaign_ChangeDifficulty");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_EcoCampaign_ChangeDifficulty_briefing")]
        private static unsafe void DLL_EcoCampaign_ChangeDifficulty_briefing_Patch(int difficulty, int* retData)
        {
            CustomLogger.Msg("DLL_EcoCampaign_ChangeDifficulty_briefing");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_PreInitMap_SiegeThat")]
        private static void DLL_PreInitMap_SiegeThat_Patch(int difficulty, int playerID, int troop0, int troop1,
            int troop2, int troop3, int troop4, int troop5, int troop6, int troop7, int troop8, int troop9, int troop10,
            bool advancedMode)
        {
            CustomLogger.Msg("DLL_PreInitMap_SiegeThat");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_PreInitMap_Invasion")]
        private static void DLL_PreInitMap_Invasion_Patch(int difficulty)
        {
            CustomLogger.Msg("DLL_PreInitMap_Invasion");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_PreInitMap_EcoMap")]
        private static void DLL_PreInitMap_EcoMap_Patch(int difficulty)
        {
            CustomLogger.Msg("DLL_PreInitMap_EcoMap");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_PreInitMap_JustBuild")]
        private static void DLL_PreInitMap_JustBuild_Patch(bool advancedFreebuild, int freebuild_GoldLevel,
            int freebuild_FoodLevel, int freebuild_ResourcesLevel, int freebuild_WeaponsLevel,
            int freebuild_RandomEvents, int freebuild_Invasions, int freebuild_InvasionDifficulty,
            int freebuild_Peacetime)
        {
            CustomLogger.Msg("DLL_PreInitMap_JustBuild");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_PreInitMap_Multiplayer")]
        private static unsafe void DLL_PreInitMap_Multiplayer_Patch(byte* retData)
        {
            CustomLogger.Msg("DLL_PreInitMap_Multiplayer");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_PreInitTutorial")]
        private static void DLL_PreInitTutorial_Patch()
        {
            CustomLogger.Msg("DLL_PreInitTutorial");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_ApplyMultiplayerSetupData")]
        private static unsafe void DLL_ApplyMultiplayerSetupData_Patch(byte* retData)
        {
            CustomLogger.Msg("DLL_ApplyMultiplayerSetupData");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetMultiplayerSetupData")]
        private static unsafe void DLL_GetMultiplayerSetupData_Patch(byte* retData)
        {
            CustomLogger.Msg("DLL_GetMultiplayerSetupData");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_RegisterMultiplayerUser")]
        private static unsafe void DLL_RegisterMultiplayerUser_Patch(int playerID, byte* name, int nameLength, int team,
            bool localPlayer)
        {
            CustomLogger.Msg("DLL_RegisterMultiplayerUser");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_StartMultiplayerGame")]
        private static void DLL_StartMultiplayerGame_Patch(bool fromSave)
        {
            CustomLogger.Msg("DLL_StartMultiplayerGame");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_StartMultiplayerGameSynced")]
        private static void DLL_StartMultiplayerGameSynced_Patch()
        {
            CustomLogger.Msg("DLL_StartMultiplayerGameSynced");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetMPRandSeed")]
        private static void DLL_SetMPRandSeed_Patch(int seed)
        {
            CustomLogger.Msg("DLL_SetMPRandSeed");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_LoadSaveGame")]
        private static unsafe void DLL_LoadSaveGame_Patch(byte* data, int length, byte* retData, bool loadingEditorMap)
        {
            CustomLogger.Msg("DLL_LoadSaveGame");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_ReceiveChore")]
        private static unsafe void DLL_ReceiveChore_Patch(int playerID)
        {
            CustomLogger.Msg("DLL_ReceiveChore");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetMultiplayerChatInfo")]
        private static unsafe void DLL_GetMultiplayerChatInfo_Patch(int* players)
        {
            CustomLogger.Msg("DLL_GetMultiplayerChatInfo");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_KickMPPlayer")]
        private static void DLL_KickMPPlayer_Patch(int kickPlayerID)
        {
            CustomLogger.Msg("DLL_KickMPPlayer");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_PromoteMPHost")]
        private static void DLL_PromoteMPHost_Patch(int hostID)
        {
            CustomLogger.Msg("DLL_PromoteMPHost");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_TriggerMPSave")]
        private static unsafe void DLL_TriggerMPSave_Patch(byte* data)
        {
            CustomLogger.Msg("DLL_TriggerMPSave");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_TriggerMPLoad")]
        private static unsafe void DLL_TriggerMPLoad_Patch(byte* data)
        {
            CustomLogger.Msg("DLL_TriggerMPLoad");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_RemapPlayers")]
        private static unsafe void DLL_RemapPlayers_Patch(int* newMappings)
        {
            CustomLogger.Msg("DLL_RemapPlayers");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetMPRadarColours")]
        private static unsafe void DLL_SetMPRadarColours_Patch(int* newMappings)
        {
            CustomLogger.Msg("DLL_SetMPRadarColours");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_ConnectionPause")]
        private static void DLL_ConnectionPause_Patch(bool pause)
        {
            CustomLogger.Msg("DLL_ConnectionPause");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SaveSaveGame")]
        private static unsafe void DLL_SaveSaveGame_Patch(byte* data, int length, int screenCentreX, int screenCentreY,
            int realScreenCentreX, int realScreenCentreY, bool lockMap, bool tempLockOnly, bool mapSave)
        {
            CustomLogger.Msg("DLL_SaveSaveGame");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_LoadMapToPlay")]
        private static unsafe void DLL_LoadMapToPlay_Patch(int campaignMapID, byte* fileName, int length, byte* retData,
            bool mission6PreStart, byte* mapName, int maplength, bool multiplayerSave, bool trail, int trailID)
        {
            CustomLogger.Msg("DLL_LoadMapToPlay");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetScenarioTroopsInfo")]
        private static unsafe void DLL_GetScenarioTroopsInfo_Patch(byte* data)
        {
            CustomLogger.Msg("DLL_GetScenarioTroopsInfo");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_CampaignLevel")]
        private static unsafe void DLL_CampaignLevel_Patch(byte* path)
        {
            CustomLogger.Msg("DLL_CampaignLevel");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetColourMapping")]
        private static unsafe void DLL_GetColourMapping_Patch(int* retData)
        {
            CustomLogger.Msg("DLL_GetColourMapping");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_RunTick")]
        private static unsafe void DLL_RunTick_Patch(short* data, byte* radarMap, bool flattenedLandscape,
            int mouseOverX, int mouseOverY, bool shiftPressed, bool ctrlPressed, bool altPressed, byte* retData,
            bool paused, bool ambientSoundChannel1, bool ambientSoundChannel2, bool speechSoundChannel1,
            bool speechSoundChannel2, bool musicPlaying, bool musicAboutToLoop, bool binkPlaying, int screenCentrePosX,
            int screenCentrePosY, int screenTilesWide, int screenTilesHigh, int radarMapWidth, int radarMapHeight,
            int radarZoom, int screenZoom, bool SH1RtsControls, int screenCentreTileX, int screenCentreTileY,
            byte* choreBuffer, int* selectedChimpsBuffer, bool mpFrameSkip, int buildingOverDepth, int troopOverdepth)
        {
            CustomLogger.Msg("DLL_RunTick");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetPath")]
        private static unsafe void DLL_SetPath_Patch(byte* data, int length, byte* autoData, int autoLength,
            byte* saveFolderData, int saveFolderLength)
        {
            CustomLogger.Msg("DLL_SetPath");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_Unpack")]
        private static unsafe void DLL_Unpack_Patch(byte* source)
        {
            CustomLogger.Msg("DLL_Unpack");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_Pack")]
        private static unsafe void DLL_Pack_Patch(byte* source)
        {
            CustomLogger.Msg("DLL_Pack");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetunpackSize")]
        private static unsafe void DLL_GetunpackSize_Patch(byte* source)
        {
            CustomLogger.Msg("DLL_GetunpackSize");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_UnpackRadarToARGB")]
        private static unsafe void DLL_UnpackRadarToARGB_Patch(byte* source)
        {
            CustomLogger.Msg("DLL_UnpackRadarToARGB");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_CRC")]
        private static unsafe void DLL_CRC_Patch(byte* source)
        {
            CustomLogger.Msg("DLL_CRC");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetSaveRadar")]
        private static unsafe void DLL_GetSaveRadar_Patch(byte* dest)
        {
            CustomLogger.Msg("DLL_GetSaveRadar");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetMapRotation")]
        private static void DLL_SetMapRotation_Patch(int rotation)
        {
            CustomLogger.Msg("DLL_SetMapRotation");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_StartMapAction")]
        private static void DLL_StartMapAction_Patch(int action)
        {
            CustomLogger.Msg("DLL_StartMapAction");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_MapAction")]
        private static void DLL_MapAction_Patch(int action, int map_x, int map_y, int brushSize, int playerID,
            bool inGameNotMapEditor, bool constructingOnly, int mouseState)
        {
            CustomLogger.Msg("DLL_MapAction");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GameAction")]
        private static void DLL_GameAction_Patch(int action)
        {
            CustomLogger.Msg("DLL_GameAction");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_TroopSelection")]
        private static unsafe void DLL_TroopSelection_Patch(int mouseState, bool rightDown, bool rightUp, int count,
            int* selectedChimps, bool selection_on, bool selection_established, int underCursorCount,
            int* underCursorChimps, int mousePosX, int mousePosY, bool overTopHalf, int onScreenCount,
            int* onScreenChimps)
        {
            CustomLogger.Msg("DLL_TroopSelection");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_TroopSelectionChanged")]
        private static unsafe void DLL_TroopSelectionChanged_Patch(int count)
        {
            CustomLogger.Msg("DLL_TroopSelectionChanged");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetMapperSize")]
        private static void DLL_GetMapperSize_Patch(int action)
        {
            CustomLogger.Msg("DLL_GetMapperSize");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_IsMapperAvailable")]
        private static void DLL_IsMapperAvailable_Patch(int mapper)
        {
            CustomLogger.Msg("DLL_IsMapperAvailable");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetMapperCoord")]
        private static void DLL_GetMapperCoord_Patch(int mapper)
        {
            CustomLogger.Msg("DLL_GetMapperCoord");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetAchValues")]
        private static void DLL_SetAchValues_Patch(int food)
        {
            CustomLogger.Msg("DLL_SetAchValues");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetEditorPlayer")]
        private static void DLL_SetEditorPlayer_Patch(int playerID)
        {
            CustomLogger.Msg("DLL_SetEditorPlayer");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetUTF8MissionText")]
        private static unsafe void DLL_SetUTF8MissionText_Patch(byte* text)
        {
            CustomLogger.Msg("DLL_SetUTF8MissionText");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetUTF8MapName")]
        private static unsafe void DLL_SetUTF8MapName_Patch(byte* text, int length, byte* text2, int length2)
        {
            CustomLogger.Msg("DLL_SetUTF8MapName");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetScenarioOverview")]
        private static unsafe void DLL_GetScenarioOverview_Patch(byte* retData)
        {
            CustomLogger.Msg("DLL_GetScenarioOverview");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_CreateScenarioAction")]
        private static void DLL_CreateScenarioAction_Patch(int action)
        {
            CustomLogger.Msg("DLL_CreateScenarioAction");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetScenarioEvent")]
        private static unsafe void DLL_GetScenarioEvent_Patch(int eventID)
        {
            CustomLogger.Msg("DLL_GetScenarioEvent");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetScenarioMessage")]
        private static unsafe void DLL_GetScenarioMessage_Patch(int eventID)
        {
            CustomLogger.Msg("DLL_GetScenarioMessage");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetScenarioInvasion")]
        private static unsafe void DLL_GetScenarioInvasion_Patch(int eventID)
        {
            CustomLogger.Msg("DLL_GetScenarioInvasion");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_ApplyScenarioEvent")]
        private static unsafe void DLL_ApplyScenarioEvent_Patch(int eventID)
        {
            CustomLogger.Msg("DLL_ApplyScenarioEvent");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_ApplyScenarioMessage")]
        private static unsafe void DLL_ApplyScenarioMessage_Patch(int eventID)
        {
            CustomLogger.Msg("DLL_ApplyScenarioMessage");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_ApplyScenarioInvasion")]
        private static unsafe void DLL_ApplyScenarioInvasion_Patch(int eventID)
        {
            CustomLogger.Msg("DLL_ApplyScenarioInvasion");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_DeleteScenarioAction")]
        private static void DLL_DeleteScenarioAction_Patch(int eventID)
        {
            CustomLogger.Msg("DLL_DeleteScenarioAction");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_UpdateScenarioActionDate")]
        private static void DLL_UpdateScenarioActionDate_Patch(int entryID)
        {
            CustomLogger.Msg("DLL_UpdateScenarioActionDate");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetMapEditorParam")]
        private static void DLL_SetMapEditorParam_Patch(int SPMPMode, int gameType, int koth, int mapSize)
        {
            CustomLogger.Msg("DLL_SetMapEditorParam");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetScenarioMessageList")]
        private static unsafe void DLL_GetScenarioMessageList_Patch(int* messageIDs)
        {
            CustomLogger.Msg("DLL_GetScenarioMessageList");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetAppMode")]
        private static void DLL_SetAppMode_Patch(int app_mode)
        {
            CustomLogger.Msg("DLL_SetAppMode");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_TutorialAction")]
        private static void DLL_TutorialAction_Patch(int ID)
        {
            CustomLogger.Msg("DLL_TutorialAction");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetScoreData")]
        private static unsafe void DLL_GetScoreData_Patch(byte* retData)
        {
            CustomLogger.Msg("DLL_GetScoreData");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetMPScoreData")]
        private static unsafe void DLL_GetMPScoreData_Patch(byte* retData)
        {
            CustomLogger.Msg("DLL_GetMPScoreData");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_SetDebugMode")]
        private static void DLL_SetDebugMode_Patch(int action)
        {
            CustomLogger.Msg("DLL_SetDebugMode");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetLayerDebug")]
        private static unsafe void DLL_GetLayerDebug_Patch(int x)
        {
            CustomLogger.Msg("DLL_GetLayerDebug");
        }


        [HarmonyPrefix]
        [HarmonyPatch("DLL_GetLayerData")]
        private static unsafe void DLL_GetLayerData_Patch(int layedID)
        {
            CustomLogger.Msg("DLL_GetLayerData");
        }
#endif
    }
}