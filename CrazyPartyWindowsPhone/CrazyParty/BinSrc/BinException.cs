using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CrazyParty.BinSrc
{
    public class ServerException : Exception
    {
        public ServerException(string message)
            : base(message)
        {
        }
    }

    public class RegisterDataException : Exception
    {
        public RegisterDataException(string message)
            : base(message)
        {
        }
    }
    public class ConnectServerException : Exception
    {
        public ConnectServerException(string message)
            : base(message)
        {
        }
    }
}
