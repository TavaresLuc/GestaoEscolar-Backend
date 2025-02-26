using System.Net;

namespace Escola.Exception
{
    public class ErrorOnValidationException : EscolaException
    {

        private readonly List<string> _errors;

        public ErrorOnValidationException(List<string> errorMessages)
        {
            _errors = errorMessages;
        }

        public override List<string> GetErrorMessages() => _errors;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
