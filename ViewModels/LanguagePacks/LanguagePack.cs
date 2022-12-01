using System.Text.Json;
using System.IO;
using System.Windows.Shapes;

namespace NazarTunes.ViewModels.LanguagePacks
{

    public struct Authorization
    {
        public string WelcomeText { get; set; }
        public string RegistrationHeaderText { get; set; }
        public string LoginHintText { get; set; }
        public string PasswordHintText { get; set; }
        public string FirstNameHintText { get; set; }
        public string LastNameHintText { get; set; }
        public string ButtonEnterText { get; set; }
        public string ButtonNoAccountText { get; set; }
        public string ButtonCancelText { get; set; }
        public string ButtonRegisterText { get; set; }
        public string HelperTextInvalidCredentials { get; set; }
        public string HelperTextDeletedAccount { get; set; }
        public string HelperTextPasswordsDontMatch { get; set; }
        public string HelperTextUnexpectedError { get; set; }
        public string HelperTextOccupiedLogin { get; set; }
    }
    public class LanguagePack
    {
        public Authorization Authorization { get; set; }

        public static void CreateJson()
        {
            var langpack = new LanguagePack()
            {
                Authorization = new Authorization()
                {
                    WelcomeText = "Welcome to NazarTunes!",
                    RegistrationHeaderText = "Registration",
                    LoginHintText = "Login",
                    PasswordHintText = "Password",
                    FirstNameHintText = "First Name",
                    LastNameHintText = "Last Name",
                    ButtonEnterText = "Enter",
                    ButtonNoAccountText = "No account? Register!",
                    ButtonCancelText = "Cancel",
                    ButtonRegisterText = "Register",
                    HelperTextInvalidCredentials = "Invalid login or password!",
                    HelperTextDeletedAccount = "This account is deleted! Please contact 8-800-000-00-00!",
                    HelperTextPasswordsDontMatch = "Passwords don't match!",
                    HelperTextUnexpectedError = "Unexpected error! Try again later!",
                    HelperTextOccupiedLogin = "This login is occupied!"
                },
            };


            var file = JsonSerializer.Serialize(langpack);
            if(!Directory.Exists("Language"))
            {
                Directory.CreateDirectory("Language");
            }
            File.WriteAllText(@"Language\EN.lang", file);
        }

        public static LanguagePack Load(string path)
        {
            CreateJson();
            var file = File.ReadAllText(path);
            return JsonSerializer.Deserialize<LanguagePack>(file)!;
        }

        public static void SaveIndex(int index)
        {
            File.WriteAllText(@"Language\config", $"{index}");
        }

        public static int GetIndex()
        {
            return int.Parse(File.ReadAllText(@"Language\config"));
        }

    }
}
