using Network.Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Network.Interfaces;

public interface ILifecycleComponent
{
    LifecycleState State { get; }
}
