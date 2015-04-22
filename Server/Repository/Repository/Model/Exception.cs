using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class Exception
    {
        #region Fields

        public int Id { get; set; }
        public string Exception_Type { get; set; }
        public string Message { get; set; }
        public string Source { get; set; }
        public string Stacktrace { get; set; }

        #endregion

        #region Constructor

        public Exception(int id, string exception_type, string message, string source, string stacktrace)
        {
            Id = id;
            Exception_Type = exception_type;
            Message = message;
            Source = source;
            Stacktrace = stacktrace;
        }

        #endregion
    }
}
