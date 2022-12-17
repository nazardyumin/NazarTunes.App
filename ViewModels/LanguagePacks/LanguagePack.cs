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
        public string AdministratorModeHeader { get; set; }
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
        public string ColumnFrozenHeader { get; set; }
        public string ColumnSoldHeader { get; set; }     
        public string ColumnPriceHeader { get; set; }
        public string ColumnAvailableHeader { get; set; }
    }

    public struct AdminTabEditAndNewNomenclature
    {
        public string HeaderEditNomenclatures { get; set; }
        public string HeaderNewNomenclature { get; set; }
        public string AddBandHintText { get; set; }
        public string AddPerformerHintText { get; set; }
        public string AddGenreHintText { get; set; }
        public string IdHintText { get; set; }
        public string CoverPathHintText { get; set; }
        public string TitleHintText { get; set; }
        public string YearHintText { get; set; }
        public string BandHintText { get; set; }
        public string PerformerHintText { get; set; }
        public string GenreHintText { get; set; }
        public string PublisherHintText { get; set; }
        public string TypeHintText { get; set; }
        public string DurationHintText { get; set; }
        public string PriceHintText { get; set; }
        public string TracksHintText { get; set; }
        public string ButtonFindText { get; set; }
        public string ButtonClearText { get; set; }
        public string ButtonSaveText { get; set; }
        public string ButtonSaveChangesText { get; set; }
        public string HelperTextInvalidId { get; set; }
        public string HelperTextEnterId { get; set; }
    }

    public struct AdminTabSuppliers
    {
        public string Header { get; set; }
        public string ButtonEditSuppliersText { get; set; }
        public string ButtonAddNewSupplierText { get; set; }
        public string ChooseSupplierHintText { get; set; }
        public string SupplierHintTextAndHeader { get; set; }
        public string ContactInfoHintTextAndHeader { get; set; }       
        public string CooperatingHintTextAndHeader { get; set; }
        public string ButtonSaveText { get; set; }
        public string ButtonSaveChangesText { get; set; }
        public string ColumnSupplierIdHeader { get; set; }      
    }

    public struct AdminTabProcurements
    {
        public string Header { get; set; }
        public string ButtonAddNewProcurementText { get; set; }
        public string NomenclatureHintTextAndHeader { get; set; }
        public string SupplierHintTextAndHeader { get; set; }
        public string AmountHintTextAndHeader { get; set; }
        public string CostPriceHintTextAndHeader { get; set; }
        public string ColumnProcurementIdHeader { get; set; }
        public string ColumnDateHeader { get; set; }
        public string ButtonAddText { get; set; }
    }

    public struct AdminTabPromotions
    {
        public string Header { get; set; }
        public string ButtonAddPromotionByGenreText{ get; set; }
        public string ButtonAddPromotionByRecordText { get; set; }
        public string ButtonAddPromotionByBandText { get; set; }
        public string ButtonAddPromotionByPerformerText { get; set; }
        public string ButtonStartPromotionText { get; set; }
        public string ButtonFinishPromotionText { get; set; }

        public string ButtonAddText { get; set; }
        public string ChooseGenreHintText { get; set; }
        public string ChooseRecordHintText { get; set; }
        public string ChoosePerformerHintText { get; set; }
        public string ChooseBandHintText { get; set; }
        public string StartPromotionImmediatelyHintText { get; set; }

        public string ChoosePromotionHintText { get; set; }
        public string ButtonStartText { get; set; }
        public string ButtonFinishText { get; set; }

        public string ColumnPromotionIdHeader { get; set; }
        public string ColumnPromotionSubjectHeader { get; set; }
        public string DiscountHintTextAndHeader { get; set; }
        public string ColumnPromotionIsStartedHeader { get; set; }
        public string ColumnPromotionIsFinishedHeader { get; set; }
        public string ColumnPromotionStartDateHeader { get; set; }
        public string ColumnPromotionEndDateHeader { get; set; }

        public string PromoByGenreRowText { get; set; }
        public string PromoByBandRowText { get; set; }
        public string PromoByPerformerRowText { get; set; }
        public string PromoByRecordRowText { get; set; }
    }

    public struct AdminTabFreezeNomenclature
    {
        public string Header { get; set; }

        public string ClientIdHintText { get; set; }
        public string ClientsPhoneHintText { get; set; }
        public string ClientsEmailHintText { get; set; }
        public string ChooseNomenclatureHintText { get; set; }
        public string AmountHintText { get; set; }
        public string ButtonFindText { get; set; }
        public string ButtonFreezeText { get; set; }
        public string HelperTextClientsNotFound { get; set; }
        public string HelperTextEnteredAmountExceedsActual { get; set; }



    }

    public struct AdminTabSalesReport
    {
        public string Header { get; set; }

    }


    public class LanguagePack
    {
        public Authorization Authorization { get; set; }

        public AdminTabNomenclatureDb AdminTabNomenclatureDb { get; set; }

        public AdminTabEditAndNewNomenclature AdminTabEditAndNewNomenclature { get; set; }

        public AdminTabSuppliers AdminTabSuppliers { get; set; }

        public AdminTabProcurements AdminTabProcurements { get; set; }

        public AdminTabPromotions AdminTabPromotions { get; set; }

        public AdminTabFreezeNomenclature AdminTabFreezeNomenclature { get; set; }

        public AdminTabSalesReport AdminTabSalesReport { get; set; }


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
                    HelperTextOccupiedLogin = "This login is occupied!",
                    AdministratorModeHeader = "Administrator Mode"
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
                    ColumnFrozenHeader = "Frozen",
                    ColumnSoldHeader = "Sold",
                    ColumnPriceHeader = "Price",
                    ColumnAvailableHeader = "Available"
                },
                AdminTabEditAndNewNomenclature = new AdminTabEditAndNewNomenclature()
                {
                    HeaderEditNomenclatures = "Edit Nomenclatures",
                    HeaderNewNomenclature = "New Nomenclature",
                    AddBandHintText = "Add Band",
                    AddPerformerHintText = "Add Performer",
                    AddGenreHintText = "Add Genre",
                    IdHintText = "ID",
                    CoverPathHintText = "Cover Path",
                    TitleHintText = "Title",
                    YearHintText = "Year",
                    BandHintText = "Band",
                    PerformerHintText = "Performer",
                    GenreHintText = "Genre",
                    PublisherHintText = "Publisher",
                    TypeHintText = "Type",
                    DurationHintText = "Duration",
                    PriceHintText = "Price",
                    TracksHintText = "Tracks",
                    ButtonFindText = "Find",
                    ButtonClearText = "Clear",
                    ButtonSaveText = "Save",
                    ButtonSaveChangesText = "Save Changes",
                    HelperTextInvalidId = "Invalid ID!",
                    HelperTextEnterId = "Enter ID!"
                },
                AdminTabSuppliers = new AdminTabSuppliers()
                {
                    Header = "Suppliers",
                    ButtonEditSuppliersText = "Edit Suppliers",
                    ButtonAddNewSupplierText = "Add New Supplier",
                    ChooseSupplierHintText = "Choose Supplier",
                    SupplierHintTextAndHeader = "Supplier",
                    ContactInfoHintTextAndHeader = "Contact Info",
                    CooperatingHintTextAndHeader = "Cooperating",
                    ButtonSaveText = "Save",
                    ButtonSaveChangesText = "Save Changes",
                    ColumnSupplierIdHeader = "Supplier ID"
                },
                AdminTabProcurements = new AdminTabProcurements()
                {
                    Header = "Procurements",
                    ButtonAddNewProcurementText = "Add New Procurement",
                    NomenclatureHintTextAndHeader = "Nomenclature",
                    SupplierHintTextAndHeader = "Supplier",
                    AmountHintTextAndHeader = "Amount",
                    CostPriceHintTextAndHeader = "Cost Price",
                    ColumnProcurementIdHeader = "Procurement ID",
                    ColumnDateHeader = "Date",
                    ButtonAddText = "Add"
                },
                AdminTabPromotions = new AdminTabPromotions()
                {
                    Header = "Promotions",
                    ButtonAddPromotionByGenreText = "Add Genre Promo",
                    ButtonAddPromotionByRecordText = "Add Record Promo",
                    ButtonAddPromotionByBandText = "Add Band Promo",
                    ButtonAddPromotionByPerformerText = "Add Performer Promo",
                    ButtonStartPromotionText = "Start Promotion",
                    ButtonFinishPromotionText = "Finish Promotion",


                    ButtonAddText = "Add",
                    ChooseGenreHintText = "Choose Genre",
                    ChooseRecordHintText = "Choose Record",
                    ChoosePerformerHintText = "Choose Performer",
                    ChooseBandHintText = "Choose Band",
                    StartPromotionImmediatelyHintText = "Start Immediately",



                    ChoosePromotionHintText = "Choose Promotion",
                    ButtonStartText = "Start",
                    ButtonFinishText = "Finish",



                    ColumnPromotionIdHeader = "Promotion ID",
                    ColumnPromotionSubjectHeader = "Promotion Subject",
                    DiscountHintTextAndHeader = "Discount (%)",
                    ColumnPromotionIsStartedHeader = "Started",
                    ColumnPromotionIsFinishedHeader = "Finished",
                    ColumnPromotionStartDateHeader = "Start Date",
                    ColumnPromotionEndDateHeader = "End Date",
                    PromoByGenreRowText = "Promotion for genre: ",
                    PromoByBandRowText = "Promotion for band: ",
                    PromoByPerformerRowText = "Promotion for performer: ",
                    PromoByRecordRowText = "Promotion for record: "

                },
                AdminTabFreezeNomenclature = new AdminTabFreezeNomenclature() 
                {
                    Header = "Freeze Nomenclature",
                    ChooseNomenclatureHintText = "Choose Nomenclature",
                    AmountHintText = "Amount",
                    ClientIdHintText = "Client ID",
                    ClientsPhoneHintText = "Phone Number",
                    ClientsEmailHintText = "Email",
                    ButtonFindText = "Find",
                    ButtonFreezeText = "Freeze",
                    HelperTextClientsNotFound = "Client's not found!",
                    HelperTextEnteredAmountExceedsActual = "Entered amount exceeds actual!"
                },
                AdminTabSalesReport = new AdminTabSalesReport()
                {
                    Header = "Sales Report"
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
            CreateJson();
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

        public static (string promoByGenre, string promoByBand, string promoByPerformer, string promoByRecord) GetPromotionSubjects()
        {
            var index = int.Parse(File.ReadAllText(@"Language\config"));
            LanguagePack lang;
            if (index == 0)
                lang = Load("EN");
            else lang = Load("RU");

            return (lang.AdminTabPromotions.PromoByGenreRowText, lang.AdminTabPromotions.PromoByBandRowText, lang.AdminTabPromotions.PromoByPerformerRowText, lang.AdminTabPromotions.PromoByRecordRowText);
        }

        public static string GetClientsNotFoundHelperText()
        {
            var index = int.Parse(File.ReadAllText(@"Language\config"));
            LanguagePack lang;
            if (index == 0)
                lang = Load("EN");
            else lang = Load("RU");

            return lang.AdminTabFreezeNomenclature.HelperTextClientsNotFound;
        }

        public static string GetEnteredAmountHelperText()
        {
            var index = int.Parse(File.ReadAllText(@"Language\config"));
            LanguagePack lang;
            if (index == 0)
                lang = Load("EN");
            else lang = Load("RU");

            return lang.AdminTabFreezeNomenclature.HelperTextEnteredAmountExceedsActual;
        }

    }
}
