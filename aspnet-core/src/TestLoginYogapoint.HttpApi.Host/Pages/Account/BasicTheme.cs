//using Microsoft.Win32;
//using Volo.Abp.AspNetCore.Mvc.UI.Theming;
//using Volo.Abp.DependencyInjection;
//using static Volo.Abp.Identity.IdentityPermissions;

//namespace TestLoginYogapoint.Pages.Account
//{
//    public class BasicTheme : ITheme, ITransientDependency
//    {

//        public const string Name = "Basic";

//        public string GetLayout(string name, bool fallbackToDefault = true)
//        {
//          switch(name)
//            {
//                case StandardLayouts.Account:
//                    return "~/Themes/LeptonXLite/Layouts/Account.cshtml";
//                    default:
//                    return fallbackToDefault ? "~Pages/Account/Register.cshtml" : null;
//            }
//        }
//    }
//}
