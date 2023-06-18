using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class AdminAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            // Użytkownik nie jest uwierzytelniony, możesz zaimplementować odpowiednią obsługę
            // np. przekierowanie na stronę logowania lub zwrócenie odpowiedniego komunikatu.
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!context.HttpContext.User.IsInRole("1"))
        {
            // Użytkownik nie ma wymaganej roli, możesz zaimplementować odpowiednią obsługę
            // np. przekierowanie na stronę błędu lub zwrócenie odpowiedniego komunikatu.
            context.Result = new ForbidResult();
            return;
        }
    }
}