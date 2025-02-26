using System.Net;

namespace Escola.Exception
{
    public abstract class EscolaException : SystemException
    {
        public abstract List<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();

    }
}