namespace Seo.Languages
{
    public class Window
    {
        public static string ReadonlyMode = null;
        public static string ReadonlyModeTip = null;
        public static string FrontPage = null;
        public static string EnvironmentPage = null;
        public static string OperatorPage = null;
        public static string SettingsPage = null;
        public static string AboutPage = null;

        public static string NewVersionTitle = null;
        public static string NewVersionContent = null;
        public static string NewVersionButton = null;

        public static string ApplicationAd = null;
        public static string WorldList = null;
        public static string WorldCrack = null;
        public static string WorldRestore = null;
        public static string WorldNotExisted = null;
        public static string WorldExisted = null;
        public static string WorldCracking = null;
        public static string WorldCracked = null;
        public static string WorldRestoring = null;
        public static string WorldSupported = null;
        public static string WorldUnknow = null;

        public static string MultiDocTitle = null;
        public static string MultiDocContent = null;

        public static string WeatherDescription = null;
        public static string WeatherExpander = null;
        public static string ChangeWeather = null;
        public static string SetWeatherWeight = null;
        public static string LockWeatherWeight = null;

        public static string SelectEnvironment = null;
        public static string EnvironmentRestartForLanguage = null;
        public static string ApplyEnvironment = null;
        public static string EnvironmentNotSelectedTip = null;
        public static string DeleteEnvironment = null;
        public static string EnvironmentProtectedTip = null;
        public static string ImportEnvironment = null;
        public static string DownloadEnvironment = null;
        public static string ExportEnvironment = null;
        public static string ExportTitle = null;
        public static string ExportName = null;
        public static string ExportCreator = null;
        public static string ExportImage = null;
        public static string ExportDescription = null;
        public static string ExportSaveTo = null;

        public static string SimsDirTitle = null;
        public static string SimsDirContent = null;
        public static string SimsDirInvalid = null;
        public static string SelectImageTitle = null;
        public static string AllImage = null;
        public static string AllFiles = null;
        public static string ExportFileTitle = null;
        public static string OpenEnvironmentTitle = null;

        public static string Save = null;
        public static string Default = null;
        public static string Undo = null;
        public static string Redo = null;
        
        public static string SelectLanguage = null;
        public static string ProgramHeader = null;
        public static string AutoUpdate = null;
        public static string VisualHeader = null;
        public static string AeroGlass = null;
        public static string BackgroundImage = null;

        public static string OK = null;
        public static string Cancel = null;
        public static string Yes = null;
        public static string No = null;

        private const string Node = "Window";
        public static void ReadFile(XmlFiles reader)
        {
            ReadonlyMode = reader.TryRead(ReadonlyMode, Node, "ReadonlyMode/Name");
            ReadonlyModeTip = reader.TryRead(ReadonlyModeTip, Node, "ReadonlyMode/Tip");

            FrontPage = reader.TryRead(FrontPage, Node, "Navigation/FrontPage");
            EnvironmentPage = reader.TryRead(EnvironmentPage, Node, "Navigation/EnvironmentPage");
            OperatorPage = reader.TryRead(OperatorPage, Node, "Navigation/OperatorPage");
            SettingsPage = reader.TryRead(SettingsPage, Node, "Navigation/SettingsPage");
            AboutPage = reader.TryRead(AboutPage, Node, "Navigation/AboutPage");

            NewVersionTitle = reader.TryRead(NewVersionTitle, Node, "Update/NewVersion/Title");
            NewVersionContent = reader.TryRead(NewVersionContent, Node, "Update/NewVersion/Content");
            NewVersionButton = reader.TryRead(NewVersionButton, Node, "Update/NewVersion/Button");

            ApplicationAd = reader.TryRead(ApplicationAd, Node, "Front/Description");
            WorldList = reader.TryRead(WorldList, Node, "Worlds/List");
            WorldCrack = reader.TryRead(WorldCrack, Node, "Worlds/Crack");
            WorldRestore = reader.TryRead(WorldRestore, Node, "Worlds/Restore");
            WorldNotExisted = reader.TryRead(WorldNotExisted, Node, "Worlds/NotExisted");
            WorldExisted = reader.TryRead(WorldExisted, Node, "Worlds/Existed");
            WorldCracking = reader.TryRead(WorldCracking, Node, "Worlds/Cracking");
            WorldCracked = reader.TryRead(WorldCracked, Node, "Worlds/Cracked");
            WorldRestoring = reader.TryRead(WorldRestoring, Node, "Worlds/Restoring");
            WorldSupported = reader.TryRead(WorldSupported, Node, "Worlds/Supported");
            WorldUnknow = reader.TryRead(WorldUnknow, Node, "Worlds/Unknow");

            MultiDocTitle = reader.TryRead(MultiDocTitle, Node, "Dialog/MultiDocuments/Title");
            MultiDocContent = reader.TryRead(MultiDocContent, Node, "Dialog/MultiDocuments/Content");

            SelectEnvironment = reader.TryRead(SelectEnvironment, Node, "Environment/Select");
            EnvironmentNotSelectedTip = reader.TryRead(EnvironmentNotSelectedTip, Node, "Environment/NotSelectedTip");
            ApplyEnvironment = reader.TryRead(ApplyEnvironment, Node, "Environment/Apply");
            DeleteEnvironment = reader.TryRead(DeleteEnvironment, Node, "Environment/Delete");
            EnvironmentProtectedTip = reader.TryRead(EnvironmentProtectedTip, Node, "Environment/ProtectedTip");
            ImportEnvironment = reader.TryRead(ImportEnvironment, Node, "Environment/Import");
            DownloadEnvironment = reader.TryRead(DownloadEnvironment, Node, "Environment/Download");
            ExportEnvironment = reader.TryRead(ExportEnvironment, Node, "Environment/Export");
            ExportTitle = reader.TryRead(ExportTitle, Node, "Environment/ExportTitle");
            ExportName = reader.TryRead(ExportName, Node, "Environment/ExportName");
            ExportCreator = reader.TryRead(ExportCreator, Node, "Environment/ExportCreator");
            ExportImage = reader.TryRead(ExportImage, Node, "Environment/ExportImage");
            ExportDescription = reader.TryRead(ExportDescription, Node, "Environment/ExportDescription");
            ExportSaveTo = reader.TryRead(ExportSaveTo, Node, "Environment/ExportSaveTo");
            EnvironmentRestartForLanguage = reader.TryRead(EnvironmentRestartForLanguage, Node, "Environment/RestartForLanguage");

            WeatherDescription = reader.TryRead(WeatherDescription, Node, "Operator/WeatherDescription");
            WeatherExpander = reader.TryRead(WeatherExpander, Node, "Operator/WeatherExpander");
            ChangeWeather = reader.TryRead(ChangeWeather, Node, "Operator/ChangeWeather");
            SetWeatherWeight = reader.TryRead(SetWeatherWeight, Node, "Operator/SetWeatherWeight");
            LockWeatherWeight = reader.TryRead(LockWeatherWeight, Node, "Operator/LockWeatherWeight");

            Save = reader.TryRead(Save, Node, "Operator/Save");
            Default = reader.TryRead(Default, Node, "Operator/Default");
            Undo = reader.TryRead(Undo, Node, "Operator/Undo");
            Redo = reader.TryRead(Redo, Node, "Operator/Redo");

            SimsDirTitle = reader.TryRead(SimsDirTitle, Node, "Dialog/SimsDir/Title");
            SimsDirContent = reader.TryRead(SimsDirContent, Node, "Dialog/SimsDir/Content");
            SimsDirInvalid = reader.TryRead(SimsDirInvalid, Node, "Dialog/SimsDir/Invalid");
            SelectImageTitle = reader.TryRead(SelectImageTitle, Node, "Dialog/OpenImage/Title");
            AllImage = reader.TryRead(AllImage, Node, "Dialog/OpenImage/AllPicture");
            AllFiles = reader.TryRead(AllFiles, Node, "Dialog/OpenImage/AllFile");
            ExportFileTitle = reader.TryRead(ExportFileTitle, Node, "Dialog/ExportEnvironment/Title");
            OpenEnvironmentTitle = reader.TryRead(OpenEnvironmentTitle, Node, "Dialog/OpenEnvironment/Title");
            
            SelectLanguage = reader.TryRead(SelectLanguage, Node, "Settings/SelectLanguage");
            ProgramHeader = reader.TryRead(ProgramHeader, Node, "Settings/Program");
            AutoUpdate = reader.TryRead(AutoUpdate, Node, "Settings/AutoUpdate");
            VisualHeader = reader.TryRead(VisualHeader, Node, "Settings/Visual");
            AeroGlass = reader.TryRead(AeroGlass, Node, "Settings/AeroGlass");
            BackgroundImage = reader.TryRead(BackgroundImage, Node, "Settings/Background");

            OK = reader.TryRead(OK, Node, "Dialog/Basic/OK");
            Cancel = reader.TryRead(Cancel, Node, "Dialog/Basic/Cancel");
            Yes = reader.TryRead(Yes, Node, "Dialog/Basic/Yes");
            No = reader.TryRead(No, Node, "Dialog/Basic/No");
        }
    }
}
