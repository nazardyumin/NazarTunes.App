using NazarTunes.Models.MySQLConnections;
using System.Globalization;
using System.Windows.Controls;

namespace NazarTunes.ViewModels.Validations
{
    public class ExistingLoginValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var db = new AuthorizationSectionDb();
            return db.IfLoginExists(value.ToString()!) ? new ValidationResult(false, "This login is occupied!") : ValidationResult.ValidResult;
        }
    }
}
