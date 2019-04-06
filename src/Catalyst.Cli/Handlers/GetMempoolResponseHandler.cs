#region LICENSE
/**
* Copyright (c) 2019 Catalyst Network
*
* This file is part of Catalyst.Node <https://github.com/catalyst-network/Catalyst.Node>
*
* Catalyst.Node is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 2 of the License, or
* (at your option) any later version.
* 
* Catalyst.Node is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
* GNU General Public License for more details.
* 
* You should have received a copy of the GNU General Public License
* along with Catalyst.Node. If not, see <https://www.gnu.org/licenses/>.
*/
#endregion

using System;
using Catalyst.Node.Common.Helpers;
using Catalyst.Node.Common.Helpers.IO;
using Catalyst.Node.Common.Helpers.IO.Inbound;
using Catalyst.Node.Common.Helpers.Util;
using Catalyst.Protocol.Rpc.Node;
using Google.Protobuf.WellKnownTypes;
using ILogger = Serilog.ILogger;

namespace Catalyst.Cli.Handlers
{
    /// <summary>
    /// Handler responsible for handling the server's response for the GetMempool request.
    /// The handler reads the response's payload and formats it in user readable format and writes it to the console.
    /// The handler implements <see cref="MessageHandlerBase"/>. 
    /// </summary>
    public class GetMempoolResponseHandler : MessageHandlerBase<GetMempoolResponse>
    {
        /// <summary>
        /// Constructor. Calls the base class <see cref="MessageHandlerBase"/> constructor.
        /// </summary>
        /// <param name="messageStream">The message stream the handler is listening to through which the handler will
        /// receive the response from the server.</param>
        /// <param name="logger">Logger to log debug related information.</param>
        public GetMempoolResponseHandler(
            IObservable<IChanneledMessage<Any>> messageStream,
            ILogger logger)
            : base(messageStream, logger)
        {
            
        }

        /// <summary>
        /// Handles the VersionResponse message sent from the <see cref="GetMempoolRequestHandler" />. 
        /// </summary>
        /// <param name="message">An object of GetMempoolResponse</param>
        public override void HandleMessage(IChanneledMessage<Any> message)
        {
            if (message == NullObjects.ChanneledAny)
            {
                return;
            }
            
            try
            {
                Logger.Debug("Handling GetMempoolResponse");
                
                var deserialised = message.Payload.FromAny<GetMempoolResponse>();

                int index = 0;
                
                foreach (string encodedTx in deserialised.Info.Values)
                {
                    index++;
                    
                    Console.WriteLine(@"t{0}: {1},",index, encodedTx);
                }
                
                Console.WriteLine(@"Press Enter to continue ...");
            }
            catch (Exception ex)
            {
                Logger.Error(ex,
                    "Failed to handle GetMempoolResponse after receiving message {0}", message);
                throw;
            }
        }
    }
}