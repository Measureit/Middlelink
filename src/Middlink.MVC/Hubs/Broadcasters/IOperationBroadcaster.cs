﻿using Middlink.Messages;
using System.Threading.Tasks;

namespace Middlink.MVC.Hubs.Broadcasters
{
    public interface IOperationBroadcaster
    {
        Task operationPending(IMessage status);
        Task operationRejected(IMessage status);
        Task operationCompleted(IMessage status);
    }
}