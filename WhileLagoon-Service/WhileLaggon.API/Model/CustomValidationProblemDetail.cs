using Microsoft.AspNetCore.Mvc;

namespace WhileLaggon.API.Model
{
    public class CustomValidationProblemDetail: ProblemDetails
    {
        public IDictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
