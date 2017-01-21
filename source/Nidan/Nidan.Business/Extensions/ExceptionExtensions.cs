using System;

namespace Nidan.Business.Extensions
{
    public static class ExceptionExtensions
    {
        public static string InnerMessage(this Exception ex)
        {
            return ex.InnerException == null
                 ? ex.Message
                 : ex.Message + " --> " + ex.InnerException.InnerMessage();
        }
    }
}
