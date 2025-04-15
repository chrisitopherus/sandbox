using Network.Architecture;
using Network.Architecture.Interfaces;
using Network.Architecture.Interfaces.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Network.Stream;

public class EnhancedNetworkStream<TMessage> : ILifecycleComponent, IMessageSender<TMessage>
{
    private LifecycleState state;

    private NetworkStream stream;
    private EnhancedNetworkStreamConfiguration<TMessage> configuration;

    private CancellationTokenSource? cancellationTokenSource;

    public EnhancedNetworkStream(NetworkStream networkStream, EnhancedNetworkStreamConfiguration<TMessage> configuration)
    {
        this.stream = networkStream;
        this.configuration = configuration;
        this.state = LifecycleState.Initialized;
    }

    public event EventHandler? Started;
    public event EventHandler? Stopped;
    public event EventHandler<EnhancedNetworkStreamDataReceivedEventArgs>? DataReceived;

    public LifecycleState State
    {
        get
        {
            return this.state;
        }

        private set
        {
            if (value != this.state)
            {
                this.state = value;
                switch (this.state)
                {
                    case LifecycleState.Started:
                        this.FireOnStarted();
                        break;
                    case LifecycleState.Stopped:
                        this.FireOnStopped();
                        break;
                }
            }
        }
    }

    public void Start()
    {
        if (this.state == LifecycleState.Started)
        {
            throw new InvalidOperationException("Network stream was already started.");
        }

        this.State = LifecycleState.Started;
        this.cancellationTokenSource = new CancellationTokenSource();
        Task _ = Task.Run(() => this.PollForDataAsync(this.cancellationTokenSource.Token));
    }

    public void Stop()
    {
        if (this.state != LifecycleState.Started)
        {
            throw new InvalidOperationException("Network stream was not started.");
        }

        this.cancellationTokenSource?.Cancel();
        this.cancellationTokenSource = null;
    }

    public async Task Send(TMessage message, CancellationToken cancellationToken = default)
    {
        try
        {
            ReadOnlyMemory<byte> encodedMessage = this.configuration.MessageProtocol.Encode(message);
            await this.stream.WriteAsync(encodedMessage, cancellationToken);
        }
        catch (OperationCanceledException)
        {
            // Expected
        }
        catch
        {
            this.Stop();
        }
    }

    protected virtual void FireOnStarted()
    {
        this.Started?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void FireOnStopped()
    {
        this.Stopped?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void FireOnDataReceived(EnhancedNetworkStreamDataReceivedEventArgs e)
    {
        this.DataReceived?.Invoke(this, e);
    }

    private async Task PollForDataAsync(CancellationToken cancellationToken = default)
    {
        Memory<byte> buffer = new byte[this.configuration.NetworkBufferSize];
        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (!this.stream.DataAvailable)
                {
                    await Task.Delay(this.configuration.PollDelayMs, cancellationToken);
                    continue;
                }

                int readBytesCount = await this.stream.ReadAsync(buffer, cancellationToken);

                if (readBytesCount == 0)
                {
                    break;
                }

                ReadOnlyMemory<byte> data = buffer[0..readBytesCount];
                if (this.configuration.MessageProtocol.IsAliveMessage(data))
                {
                    // ignore
                    continue;
                }

                this.FireOnDataReceived(new EnhancedNetworkStreamDataReceivedEventArgs(data));
            }
        }
        catch (OperationCanceledException)
        {
            // Expected
        }
        finally
        {
            this.State = LifecycleState.Stopped;
        }
    }
}
