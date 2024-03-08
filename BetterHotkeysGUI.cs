using System.Collections.ObjectModel;
using HarmonyLib;
using MelonLoader;
using Noesis;
using Stronghold1DE;

namespace Ritterschlag
{
    public static class BetterHotkeysGUI
    {
        private static string OriginalXML = @"
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
        xmlns:local=""clr-namespace:Stronghold1DE;assembly=Assembly-CSharp""
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
                                xmlns:local=""clr-namespace:Stronghold1DE;assembly=Assembly-CSharp""
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

        private static string BetterXML = @"
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

    <TabControl>
        <TabItem Header=""Hotkeys"">
            <TextBlock
                HorizontalAlignment=""Center""
                Margin=""0,10,0,0""
                FontSize=""24""
                VerticalAlignment=""Top""
                Text=""{Binding GameTexts[TEXT_NEW_TEXT_077], Source={x:Static local:Translate.Instance}}""
            />

            <ListView Margin=""4,50,4,120"" Name=""HotKeyList""
                ScrollViewer.HorizontalScrollBarVisibility=""Hidden"">
                <ListView.View>
                    <GridView ColumnHeaderContainerStyle=""{StaticResource HiddenStyle}"">
                        <GridViewColumn Header="""" Width=""392"">
                            <GridViewColumn.CellTemplate>
                                <DataTemplate>
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
        <TabItem Header=""BetterHotkeys"">

            <TextBlock
                HorizontalAlignment=""Center""
                Margin=""0,10,0,0""
                FontSize=""24""
                VerticalAlignment=""Top""
                Text=""BetterHotkeys""
            />


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

        private static string TestXML = @"
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

        [HarmonyPatch(typeof(HUD_Options))]
        internal static class PATCH_HUD_Options
        {
            [HarmonyPostfix]
            [HarmonyPatch("InitializeComponent")]
            private static void POST_InitializeComponent(ref HUD_Options __instance)
            {
                MelonLogger.Msg("PATCH_HUD_Options.POST_InitializeComponent PATCH START");
                // ListView RefHotKeyList = (ListView)__instance.FindName("HotKeyList");
                Grid LayoutRoot = (Grid)__instance.Content;
                Utils.dumpNoesisElementToFile(LayoutRoot, "HUD_Options_old.dump");
                
                Grid RefKeySettings = (Grid)__instance.FindName("KeySettings");
                Grid Column1 = (Grid)VisualTreeHelper.GetParent(RefKeySettings);

                Utils.dumpObjectFile(RefKeySettings, "RefKeySettings1.dump");

                // Grid RefKeySettingsBetter = (Grid)XamlReader.Parse(TestXML);
                // int idx = Column1.Children.IndexOf(RefKeySettings);
                // Column1.Children.RemoveAt(idx);
                // Column1.Children.Insert(idx, RefKeySettingsBetter);
                // RefKeySettings = (Grid)__instance.FindName("KeySettings");
                // RefKeySettings.Visibility = Visibility.Visible;
                
                // Utils.dumpNoesisElementToFile(RefKeySettings, "RefKeySettings2.dump");
                Utils.dumpNoesisElementToFile(LayoutRoot, "HUD_Options_new.dump");
                MelonLogger.Msg("PATCH_HUD_Options.POST_InitializeComponent PATCH STOP");
            }

            [HarmonyPostfix]
            [HarmonyPatch(MethodType.Constructor)]
            private static void POST_HUD_Options(ref HUD_Options __instance, ref ListView ___RefHotKeyList, ref Grid ___RefKeySettings)
            {
                MelonLogger.Msg("PATCH_HUD_Options.POST_HUD_Options START");
                // Utils.dumpNoesisElementToFile(__instance, "HUD_Options.log");
                
                MelonLogger.Msg($"{___RefHotKeyList}");
                MelonLogger.Msg($"{___RefKeySettings}");
                ___RefKeySettings.Visibility = Visibility.Visible;
                Utils.dumpObjectFile(___RefKeySettings, "RefKeySettings3.dump");
                    
                
                MelonLogger.Msg("PATCH_HUD_Options.POST_HUD_Options STOP");
            }
        }
    }
}