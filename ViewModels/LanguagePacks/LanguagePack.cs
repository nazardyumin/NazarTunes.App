using System.IO;
using System.Text.Json;

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
        public string ButtonLogoutText { get; set; }
        public string ButtonNoAccountText { get; set; }
        public string ButtonCancelText { get; set; }
        public string ButtonRegisterText { get; set; }
        public string HelperTextInvalidCredentials { get; set; }
        public string HelperTextDeletedAccount { get; set; }
        public string HelperTextPasswordsDontMatch { get; set; }
        public string HelperTextUnexpectedError { get; set; }
        public string HelperTextOccupiedLogin { get; set; }
    }

    public struct AdminTabNomenclatureDb
    { 
        public string Header { get; set; }

        public string ButtonEditPerformersText { get; set; }
        public string ChoosePerformerHintText { get; set; }
        public string FirstNameHintText { get; set; }
        public string LastNameHintText { get; set; }

        public string ButtonEditBandsText { get; set; }
        public string ChooseBandHintText { get; set; }
        public string BandNameHintText { get; set; }

        public string ButtonEditGenresText { get; set; }
        public string ChooseGenreHintText { get; set; }
        public string GenreNameHintText { get; set; }

        public string ButtonSaveChangesText { get; set; }


        public string ColumnIdHeader { get; set; }
        public string ColumnTitleHeader { get; set; }
        public string ColumnBandHeader { get; set; }
        public string ColumnPerformerHeader { get; set; }
        public string ColumnTracksHeader { get; set; }
        public string ColumnGenreHeader { get; set; }
        public string ColumnTypeHeader { get; set; }
        public string ColumnPublisherHeader { get; set; }
        public string ColumnYearHeader { get; set; }
        public string ColumnAmountHeader { get; set; }
        public string ColumnSoldHeader { get; set; }
        public string ColumnPriceHeader { get; set; }
        public string ColumnAvailableHeader { get; set; }
    }


    public class LanguagePack
    {
        public Authorization Authorization { get; set; }

        public AdminTabNomenclatureDb AdminTabNomenclatureDb { get; set; }

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
                    ButtonLogoutText = "Logout",
                    ButtonNoAccountText = "No account? Register!",
                    ButtonCancelText = "Cancel",
                    ButtonRegisterText = "Register",
                    HelperTextInvalidCredentials = "Invalid login or password!",
                    HelperTextDeletedAccount = "This account is deleted! Please contact 8-800-000-00-00!",
                    HelperTextPasswordsDontMatch = "Passwords don't match!",
                    HelperTextUnexpectedError = "Unexpected error! Try again later!",
                    HelperTextOccupiedLogin = "This login is occupied!"
                },
                AdminTabNomenclatureDb = new AdminTabNomenclatureDb()
                {
                    Header = "Nomenclature DB",

                    ButtonEditPerformersText = "Edit Performers",
                    ChoosePerformerHintText = "Choose Performer",
                    FirstNameHintText = "First Name",
                    LastNameHintText = "Last Name",

                    ButtonEditBandsText = "Edit Bands",
                    ChooseBandHintText = "Choose Band",
                    BandNameHintText = "Band Name",

                    ButtonEditGenresText = "Edit Genres",
                    ChooseGenreHintText = "Choose Genre",
                    GenreNameHintText = "Genre",

                    ButtonSaveChangesText = "Save Changes",

                    ColumnIdHeader = "ID",
                    ColumnTitleHeader = "Title",
                    ColumnBandHeader = "Band",
                    ColumnPerformerHeader = "Performer",
                    ColumnTracksHeader = "Tracks",
                    ColumnGenreHeader = "Genre",
                    ColumnTypeHeader = "Type",
                    ColumnPublisherHeader = "Publisher",
                    ColumnYearHeader = "Year",
                    ColumnAmountHeader = "Amount",
                    ColumnSoldHeader = "Sold",
                    ColumnPriceHeader = "Price",
                    ColumnAvailableHeader = "Available"
                }



            };


            var file = JsonSerializer.Serialize(langpack);
            if(!Directory.Exists("Language"))
            {
                Directory.CreateDirectory("Language");
            }
            File.WriteAllText(@"Language\EN.lang", file);
        }

        public static LanguagePack Load(string language)
        {
            //CreateJson();
            var file = File.ReadAllText($"Language\\{language}.lang");
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
