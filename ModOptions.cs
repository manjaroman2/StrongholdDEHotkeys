using System.Linq;
using System.Reflection;
using HarmonyLib;
using MelonLoader;
using Noesis;
using Stronghold1DE;


namespace Ritterschlag
{
    public static class ModOptions
    {
        private const string ModButtonXml = @"
<Button
    xmlns:local=""clr-namespace:Stronghold1DE;assembly=Assembly-CSharp""
    Name=""MainMenu_OptionsButton""
    Width=""380""
    Height=""103""
    Margin=""0,0,1055,150""
    HorizontalAlignment=""Right""
    VerticalAlignment=""Bottom""
    local:PropEx.TextCentre=""Mod Menu""
    local:PropEx.TextLeft=""""
    local:PropEx.TextRight=""""
    local:PropEx.Sprite1=""{StaticResource shield_options}""
    local:PropEx.Sprite1Margin=""{Binding FrontEndButtonMargin}""
    Command=""{Binding ButtonMainMenuCommand}""
    CommandParameter=""ModOptions""
    MouseEnter=""MouseEnterMainButtonHandler""
    Typography.Capitals=""SmallCaps""
    RenderTransformOrigin=""0.5,0.5""
    Style=""{StaticResource BTN_NewFrontend}"">
    <Button.RenderTransform>
        <ScaleTransform ScaleX=""1.00"" ScaleY=""1.00"" x:Name=""MainMenu_OptionsButtonScale"" />
    </Button.RenderTransform>
</Button>
";

        private const string ModButtonAnimationXml = @"
<DoubleAnimationUsingKeyFrames Timeline.DesiredFrameRate=""300"" Storyboard.TargetName=""MainMenu_ModsButton"" Storyboard.TargetProperty=""Opacity"">
    <EasingDoubleKeyFrame KeyTime=""0:0:0.0"" Value=""0.00"" />
    <EasingDoubleKeyFrame KeyTime=""{Binding FrontEnd_Row5_Step1}"" Value=""0"" />
    <EasingDoubleKeyFrame KeyTime=""{Binding FrontEnd_Row5_Step2}"" Value=""1.0"">
        <EasingDoubleKeyFrame.EasingFunction>
            <CircleEase EasingMode=""EaseOut"" />
        </EasingDoubleKeyFrame.EasingFunction>
    </EasingDoubleKeyFrame>
</DoubleAnimationUsingKeyFrames>
";

        // private static FrontendMenus frontendMenus;
        // private static bool _show_HUD_ModOptions;
        // public static bool Show_HUD_ModOptions
        // {
        //     get => _show_HUD_ModOptions;
        //     set
        //     {
        //         if (_show_HUD_ModOptions == value)
        //             return;
        //         _show_HUD_ModOptions = value;
        //         if (!value)
        //             Save();
        //         MainViewModel.Instance.NotifyPropertyChanged(nameof (Show_HUD_ModOptions));
        //         MainViewModel.Instance.Show_HUD_FrontEndBlackout = true;
        //     }
        // }
        //
        // private static void Save()
        // {
        //     
        // }
        //
        // private static void OpenModOptions()
        // {
        //     frontendMenus.UpdateFrontMenuPopupScale();
        //     Show_HUD_ModOptions = true;
        //     
        //     HUD_Options.OpenOptions(false);
        // }
        //
        // private static void Update()
        // {
        //     
        // }
        //
        // [HarmonyPatch(typeof(MainViewModel))]
        // internal static class PATCH_MainViewModel
        // {
        //     [HarmonyPrefix]
        //     [HarmonyPatch(nameof(MainViewModel.Show_HUD_FrontEndBlackout), MethodType.Getter)]
        //     private static bool PRE_get_Show_HUD_FrontEndBlackout(ref bool __result)
        //     {
        //         __result |= _show_HUD_ModOptions;
        //         return false;
        //     }
        // }
        //
        // [HarmonyPatch(typeof(FatControler))]
        // internal static class PATCH_FatController
        // {
        //     [HarmonyPostfix]
        //     [HarmonyPatch(nameof(FatControler.NoesisGUIUpdateChecksComplete))]
        //
        //     private static void POST_NoesisGUIUpdateChecksComplete()
        //     {
        //         if (Show_HUD_ModOptions) Update();
        //     }
        // }

        private static void Test()
        {
            // this.menuSection = param - 1;
            // this.UpdateMenus();
        }

        [HarmonyPatch(typeof(FrontendMenus))]
        internal static class PATCH_FrontendMenus
        {
            [HarmonyPostfix]
            [HarmonyPatch(MethodType.Constructor)]
            private static void FrontEndMenus(FrontendMenus __instance)
            {
                // MelonLogger.Msg($"{__instance} {__instance.Name}");

                Grid FrontEndGrid = (Grid)__instance.Content;
                Button OptionsButton = (Button) FrontEndGrid.FindName("MainMenu_OptionsButton");
                // MelonLogger.Msg(OptionsButton.Name);

                Button ModsButton = (Button)XamlReader.Parse(ModButtonXml);
                Grid parent = (Grid)VisualTreeHelper.GetParent(OptionsButton);
                // MelonLogger.Msg(parent);
                parent.Children.Add(ModsButton);
                ModsButton.Visibility = Visibility.Visible;

                Storyboard storyboard = (Storyboard) __instance.Resources["MainMenu_ShowMainMenu"];
                DoubleAnimationUsingKeyFrames OptionsButtonAnimation = storyboard.Children
                    .OfType<DoubleAnimationUsingKeyFrames>().FirstOrDefault(timeline =>
                        Storyboard.GetTargetName(timeline) == "MainMenu_OptionsButton");
                // MelonLogger.Msg(Storyboard.GetTargetName(OptionsButtonAnimation));
                DoubleAnimationUsingKeyFrames ModButtonAnimation =
                    XamlReader.Parse(ModButtonAnimationXml) as DoubleAnimationUsingKeyFrames;
                // MelonLogger.Msg(Storyboard.GetTargetName(ModButtonAnimation));
                storyboard.Children.Add(ModButtonAnimation);
            }

            [HarmonyPrefix]
            [HarmonyPatch(nameof(FrontendMenus.ButtonClicked))]
            private static void PRE_ButtonClicked(string param)
            {
                // MelonLogger.Msg($"FrontendMenus.ButtonClicked {param}");
                // if (param != "ModOptions") return true;
                // OpenModOptions();
                // return false;
            }
        }
    }
}