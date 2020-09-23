using System;
using System.Collections.Generic;
using System.Text;

namespace Application_DataRepository.DapperDataRepository.DapperDbConnection.ConnectionExceptionsHandler
{
    public partial class ConnectionStringException : Exception
    {
        #region var

        #endregion

        #region prop
        public string _errorCode { get; set; }
        #endregion
        #region ctor
        public ConnectionStringException() : base()
        {

        }
        public ConnectionStringException(string errorCode) : base()=> _errorCode = errorCode;
       
        public ConnectionStringException(string message, string errorCode) : base(message) => _errorCode = errorCode;
        
        public ConnectionStringException(string message, Exception innerException, string errorCode) : base(message, innerException) => _errorCode = errorCode;
        
        #endregion
        #region func

        #endregion
    }
}
