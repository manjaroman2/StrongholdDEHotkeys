using System;
using System.IO;
using System.Linq;
using System.Text;
using HarmonyLib;
using MelonLoader;
using Noesis;
using Stronghold1DE;

namespace Ritterschlag
{
    public static class BetterHotkeysGUI
    {
        private static string XMLOriginal = @"
<Grid 
    xmlns:local=""clr-namespace:Stronghold1DE;assembly=Assembly-CSharp""
    Name=""KeySettings""
    Visibility=""Hidden"" 
    Height=""510""
    VerticalAlignment=""Top""
    Background=""{StaticResource Brush.InsetColour1}"">
    <Border
        Width=""397""
        Height=""510""
        Margin=""0,0,0,0""
        HorizontalAlignment=""Right""
        VerticalAlignment=""Top""
        BorderBrush=""#FF000000""
        BorderThickness=""2"" />
    <TextBlock
        HorizontalAlignment=""Center""
        Margin=""0,10,0,0""
        FontSize=""24""
        VerticalAlignment=""Top""
        Text=""{Binding GameTexts[TEXT_NEW_TEXT_077], Source={x:Static local:Translate.Instance}}""
        />

    <ListView Margin=""4,50,4,120"" Name=""HotKeyList"" ScrollViewer.HorizontalScrollBarVisibility=""Hidden"" >
        <ListView.View>
            <GridView  ColumnHeaderContainerStyle=""{StaticResource HiddenStyle}"" >
                <GridViewColumn Header="""" Width=""392"">
                    <GridViewColumn.CellTemplate>
                        <DataTemplate >
                            <Button
                                Margin=""-6""
                                Height=""30""
                                HorizontalAlignment=""Left""
                                Foreground=""#FF000000"" 
                                FontSize=""16""
                                local:PropEx.TextLeft=""{Binding Text1}""
                                local:PropEx.TextCentre=""{Binding Text2}""
                                Style=""{StaticResource LST_SH_HotKeys}""
                                />
                        </DataTemplate>
                    </GridViewColumn.CellTemplate>
                </GridViewColumn>
            </GridView>
        </ListView.View>
    </ListView>

    <!-- Keys -->
    <Button
        Name=""OptionsKeys1""
        Width=""350""
        Margin=""23,0,0,70""
        HorizontalAlignment=""Left""
        VerticalAlignment=""Bottom""
        local:PropEx.TextCentre=""{Binding Source={x:Static local:Translate.Instance}, Path=GameTexts[TEXT_NEW_TEXT_066]}""
        local:PropEx.TextLeft=""""
        local:PropEx.TextRight=""""
        Command=""{Binding OptionsCommand}""
        CommandParameter=""41""
        MouseEnter=""CommonRedButtonEnter""
        Style=""{StaticResource BTN_SH_GlowS}"" />
    <Button
        Name=""OptionsKeys2""
        Width=""350""
        Margin=""23,0,0,30""
        HorizontalAlignment=""Left""
        VerticalAlignment=""Bottom""
        local:PropEx.TextCentre=""{Binding Source={x:Static local:Translate.Instance}, Path=GameTexts[TEXT_NEW_TEXT_067]}""
        local:PropEx.TextLeft=""""
        local:PropEx.TextRight=""""
        Command=""{Binding OptionsCommand}""
        CommandParameter=""42""
        MouseEnter=""CommonRedButtonEnter""
        Style=""{StaticResource BTN_SH_GlowS}"" />
</Grid>
";

        private static string XMLKeySettingsBetter = @"
<Grid 
    xmlns:local=""clr-namespace:Stronghold1DE;assembly=Assembly-CSharp""
    Name=""KeySettings""
    Visibility=""Hidden"" 
    Height=""510""
    VerticalAlignment=""Top""
    Background=""{StaticResource Brush.InsetColour1}"">

    <Border
        Width=""397""
        Height=""510""
        Margin=""0,0,0,0""
        HorizontalAlignment=""Right""
        VerticalAlignment=""Top""
        BorderBrush=""#FF000000""
        BorderThickness=""2"" />
    <TabControl
        Background=""{StaticResource Brush.InsetColour1}"">
        <TabItem>
            <TabItem.Header>
                <TextBlock
                    HorizontalAlignment=""Center""
                    Margin=""0,10,0,0""
                    FontSize=""14""
                    VerticalAlignment=""Top""
                    Text=""{Binding GameTexts[TEXT_NEW_TEXT_077], Source={x:Static local:Translate.Instance}}""
                    />
            </TabItem.Header> 
            <ListView Margin=""4,50,4,120"" Name=""HotKeyList"" ScrollViewer.HorizontalScrollBarVisibility=""Hidden"" >
                <ListView.View>
                    <GridView  ColumnHeaderContainerStyle=""{StaticResource HiddenStyle}"" >
                        <GridViewColumn Header="""" Width=""392"">
                            <GridViewColumn.CellTemplate>
                                <DataTemplate >
                                    <Button
                                        Margin=""-6""
                                        Height=""30""
                                        HorizontalAlignment=""Left""
                                        Foreground=""#FF000000"" 
                                        FontSize=""16""
                                        local:PropEx.TextLeft=""{Binding Text1}""
                                        local:PropEx.TextCentre=""{Binding Text2}""
                                        Style=""{StaticResource LST_SH_HotKeys}""
                                        />
                                </DataTemplate>
                            </GridViewColumn.CellTemplate>
                        </GridViewColumn>
                    </GridView>
                </ListView.View>
            </ListView>
        </TabItem>
        <TabItem>
            <TabItem.Header>
                <TextBlock
                    HorizontalAlignment=""Center""
                    Margin=""0,10,0,0""
                    FontSize=""14""
                    VerticalAlignment=""Top""
                    Text=""BetterHotkeys""
                    />
            </TabItem.Header> 
            <TextBlock
                HorizontalAlignment=""Center""
                Margin=""0,10,0,0""
                FontSize=""24""
                VerticalAlignment=""Top""
                Text=""BetterHotkeys Content"" />
        </TabItem>
    </TabControl>

    <!-- Keys -->
    <Button
        Name=""OptionsKeys1""
        Width=""350""
        Margin=""23,0,0,70""
        HorizontalAlignment=""Left""
        VerticalAlignment=""Bottom""
        local:PropEx.TextCentre=""{Binding Source={x:Static local:Translate.Instance}, Path=GameTexts[TEXT_NEW_TEXT_066]}""
        local:PropEx.TextLeft=""""
        local:PropEx.TextRight=""""
        Command=""{Binding OptionsCommand}""
        CommandParameter=""41""
        MouseEnter=""CommonRedButtonEnter""
        Style=""{StaticResource BTN_SH_GlowS}"" />
    <Button
        Name=""OptionsKeys2""
        Width=""350""
        Margin=""23,0,0,30""
        HorizontalAlignment=""Left""
        VerticalAlignment=""Bottom""
        local:PropEx.TextCentre=""{Binding Source={x:Static local:Translate.Instance}, Path=GameTexts[TEXT_NEW_TEXT_067]}""
        local:PropEx.TextLeft=""""
        local:PropEx.TextRight=""""
        Command=""{Binding OptionsCommand}""
        CommandParameter=""42""
        MouseEnter=""CommonRedButtonEnter""
        Style=""{StaticResource BTN_SH_GlowS}"" />
</Grid>
";

        [HarmonyPatch(typeof(HUD_Options))]
        internal static class PATCH_HUD_Options
        {
            [HarmonyPostfix]
            [HarmonyPatch("InitializeComponent")]
            private static void POST_InitializeComponent(ref HUD_Options __instance, ref Grid ___RefKeySettings)
            {
                MelonLogger.Msg("++ PATCH_HUD_Options.POST_InitializeComponent");
                Grid RefKeySettings = (Grid)__instance.FindName("KeySettings");
                Grid Column1 = (Grid)VisualTreeHelper.GetParent(RefKeySettings);
                int idx = Column1.Children.IndexOf(RefKeySettings);
                Grid RefKeySettingsBetter = (Grid)XamlReader.Parse(XMLKeySettingsBetter);
                // Grid RefKeySettingsBetter = RefKeySettings;
                ListView RefHotKeyListBetter = null;
                Button RefOptionsKeys1Better = null;
                Button RefOptionsKeys2Better = null;
                __instance.UnregisterName("KeySettings");
                __instance.RegisterName("KeySettings", RefKeySettingsBetter);
                try
                {

                    foreach (FrameworkElement element in Utils.NoesisEnumerateChildrenFrameworkElements(RefKeySettingsBetter))
                    {
                        if (element.GetType().GetProperty("Name") == null) continue;
                        MelonLogger.Msg(element);
                        switch (element.Name)
                        {
                            case "HotKeyList" when RefHotKeyListBetter == null:
                                RefHotKeyListBetter = element as ListView;
                                if (RefHotKeyListBetter == null) break;
                                __instance.UnregisterName("HotKeyList");
                                __instance.RegisterName("HotKeyList", RefHotKeyListBetter);
                                break;
                            case "OptionsKeys1" when RefOptionsKeys1Better == null:
                                RefOptionsKeys1Better = element as Button;
                                if (RefOptionsKeys1Better == null) break;
                                __instance.UnregisterName("OptionsKeys1");
                                __instance.RegisterName("OptionsKeys1", RefOptionsKeys1Better);
                                break;
                            case "OptionsKeys2" when RefOptionsKeys2Better == null:
                                RefOptionsKeys2Better = element as Button;
                                if (RefOptionsKeys2Better == null) break;
                                __instance.UnregisterName("OptionsKeys2");
                                __instance.RegisterName("OptionsKeys2", RefOptionsKeys2Better);
                                break;
                        }
                    }
                }
                catch (Exception e)
                {
                    MelonLogger.Msg($"{e}");
                }

                Column1.Children.RemoveAt(idx);
                Column1.Children.Insert(idx, RefKeySettingsBetter);

                MelonLogger.Msg("-- PATCH_HUD_Options.POST_InitializeComponent");
            }

            [HarmonyPrefix]
            [HarmonyPatch(MethodType.Constructor)]
            private static void PRE_HUD_Options()
            {
                MelonLogger.Msg("+ PRE_HUD_Options");

                MelonLogger.Msg("- PRE_HUD_Options");
            }

            [HarmonyPostfix]
            [HarmonyPatch(MethodType.Constructor)]
            private static void POST_HUD_Options()
            {
                MelonLogger.Msg("+ POST_HUD_Options");

                MelonLogger.Msg("- POST_HUD_Options");
            }

            // [HarmonyPrefix]
            // [HarmonyPatch("CreateHotkeyList")]
            // private static void PRE_CreateHotkeyList(ref Grid ___RefKeySettings)
            // {
            //     MelonLogger.Msg("+ PRE_CreateHotkeyList");
            //     MelonLogger.Msg($"{___RefKeySettings.Name}:PARENT={___RefKeySettings.Parent}");
            //     Utils.NoesisDumpElementToFile(___RefKeySettings, "KeySettings_Better.xmldump");
            //     MelonLogger.Msg("- PRE_CreateHotkeyList");
            // }

            [HarmonyPostfix]
            [HarmonyPatch("CreateHotkeyList")]
            private static void POST_CreateHotkeyList(ref Grid ___RefKeySettings)
            {
                MelonLogger.Msg("+ POST_CreateHotkeyList");

                // StringBuilder stringBuilder = new StringBuilder();
                // foreach (var (key, value) in Translate.Instance.GameTexts)
                // {
                //     stringBuilder.Append()
                // }
                File.WriteAllText("GameTexts.dump",
                    string.Join(Environment.NewLine,
                        Translate.Instance.GameTexts.Select(pair => $"{pair.Key}={pair.Value}")));
                MelonLogger.Msg($"{___RefKeySettings.Name}:PARENT={___RefKeySettings.Parent}");
                Utils.NoesisDumpElementToFile(___RefKeySettings, "KeySettings_Better.xmldump");
                MelonLogger.Msg("- POST_CreateHotkeyList");
            }
        }
    }
}