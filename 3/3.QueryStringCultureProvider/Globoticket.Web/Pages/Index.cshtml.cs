using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Globoticket.Web.Pages
{
    public class IndexModel : PageModel
    {
        public string Message = string.Empty;

        public string Title = "Ticket Purchase";

        public Dictionary<int, Category> CategoriesWithPrices = new Dictionary<int, Category>() {
            { 1, new Category("Standing", 33.33) },
            { 2, new Category("Upper Level", 44.44) },
            { 3, new Category("Lower Level", 55.55) },
        };

        public void OnGet()
        {
            string language = HttpContext.Request.Headers["Accept-Language"];
            if (language.StartsWith("de-") || language.StartsWith("de;"))
            {
                Title = "Ticketkauf";
            } else if (language.StartsWith("fr-") || language.StartsWith("fr;"))
            {
                Title = "Achat de billets";
            }
        }

        public void OnPost(DateTime eventdate, int category, int amount)
        {
            var totalAmount = amount * CategoriesWithPrices[category].Price;
            Message = String.Format(
                "You ordered {0} ticket{1} for a total of ${2} for the event on {3}.",
                amount,
                amount > 1 ? "s" : "",
                totalAmount,
                eventdate.ToShortDateString());
        }

        public readonly record struct Category(string Description, double Price);
    }
}