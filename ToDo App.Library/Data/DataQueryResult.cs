using System;
using System.Collections.Generic;
using System.Text;

namespace ToDo_App.Library.Data
{
    public class DataQueryResult
    {
        public bool IsSuccess;
        public Exception? Exception;

        public DataQueryResult(bool isSuccess, Exception? exception = null)
        {
            IsSuccess = isSuccess;
            Exception = exception;
        }
    }
}
