using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Exceptions
{
    public class DataAccessException : Exception
    {
        //public DataAccessException(Exception ex, string developerMassage, ILogger logger)
        //{
        //    logger.LogError($"Exception: {ex.Message}.\nMassage From Developer: {developerMassage}.");
        //}
        public DataAccessException()
        {
        }
        public DataAccessException(string message) : base(message)
        {
        }
        public DataAccessException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
