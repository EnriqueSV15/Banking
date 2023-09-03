using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Api.Domain.Exceptions
{
    public class BankingException : Exception
    {
        public string ErrorCode { get; set; }
        public BankingException(string errorCode, Exception exception) : base(exception.Message, exception)
        { 
            ErrorCode = errorCode;
        }
    }
}
