using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Interface
{
    public interface ILog
    {
        void Debug(string message);
        void Info(string message);
        void Warning(string message);
        void Error(string message);
        void Exception(Exception exception);
        void Exception(string message, Exception exception);
    }
}
