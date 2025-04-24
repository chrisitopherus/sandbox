using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Architecture.Interfaces;

/// <summary>
/// Defines something that can send messages.
/// </summary>
/// <typeparam name="TMessage">The type of message that can be sent.</typeparam>
public interface IMessageSender<TMessage>
    where TMessage : IMessage
{
    /// <summary>
    /// Sends a given <paramref name="message"/>.
    /// </summary>
    /// <param name="message">The message that should be sent.</param>
    void Send(TMessage message);

    /// <summary>
    /// Sends a given <paramref name="message"/> asynchronously.
    /// </summary>
    /// <param name="message">The message to send.</param>
    /// <param name="cancellationToken">A token to cancel the async operation.</param>
    /// <returns>A task that represents the async send operation.</returns>
    Task SendAsync(TMessage message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send the specified binary <paramref name="data"/> asynchronously.
    /// </summary>
    /// <param name="data">The binary data to send.</param>
    /// <param name="cancellationToken">A token to cancel the async operation.</param>
    /// <returns>A Task the represents the async send operation.</returns>
    Task SendAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default);
}
