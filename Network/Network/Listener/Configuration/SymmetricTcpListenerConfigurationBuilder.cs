using Helpers.Validation;
using Network.Architecture.Interfaces;
using Network.Client.Configuration;
using Network.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Network.Listener.Configuration;

public class SymmetricTcpListenerConfigurationBuilder<TMessage> : EnhancedTcpListenerConfigurationBuilder<TMessage, TMessage>
    where TMessage : IMessage
{
}
