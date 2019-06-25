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

using Catalyst.Common.Config;
using Catalyst.Common.Interfaces.IO.Messaging;
using Catalyst.Common.Interfaces.IO.Messaging.Dto;
using Catalyst.Common.IO.Handlers;
using Catalyst.Common.IO.Messaging;
using Catalyst.Protocol.IPPN;
using Catalyst.TestUtils;
using DotNetty.Transport.Channels;
using Google.Protobuf;
using NSubstitute;
using Xunit;

namespace Catalyst.Common.UnitTests.IO.Handlers
{
    public sealed class CorrelatableHandlerUnitTests
    {
        private readonly IChannelHandlerContext _fakeContext;
        private readonly IMessageCorrelationManager _fakeMessageCorrelationManager;

        public CorrelatableHandlerUnitTests()
        {
            _fakeMessageCorrelationManager = Substitute.For<IMessageCorrelationManager>();
            _fakeContext = Substitute.For<IChannelHandlerContext>();
        }

        [Fact]
        public void Does_Process_IMessageDto_Types()
        {
            var fakeRequestMessageDto = Substitute.For<IMessageDto>();
            fakeRequestMessageDto.MessageType.Returns(MessageTypes.Request);
            fakeRequestMessageDto.Message.Returns(Substitute.For<IMessage>());
            fakeRequestMessageDto.Sender.Returns(PeerIdentifierHelper.GetPeerIdentifier("Im_The_Sender"));
            fakeRequestMessageDto.Message.Descriptor.Returns(PingRequest.Descriptor);

            var correlatableHandler = new CorrelatableHandler(_fakeMessageCorrelationManager);

            correlatableHandler.WriteAsync(_fakeContext, fakeRequestMessageDto);
            _fakeMessageCorrelationManager
               .ReceivedWithAnyArgs()
               .AddPendingRequest(Arg.Any<CorrelatableMessage>()
                );

            _fakeContext.ReceivedWithAnyArgs(1).WriteAsync(Arg.Any<IMessageDto>());
        }

        [Fact]
        public void Does_Not_Process_OtherTypes_Types()
        {
            var fakeRequestMessageDto = Substitute.For<IProtocolMessageDto<IMessage>>();

            var correlatableHandler = new CorrelatableHandler(_fakeMessageCorrelationManager);
            
            correlatableHandler.WriteAsync(_fakeContext, fakeRequestMessageDto);
            
            _fakeMessageCorrelationManager
               .DidNotReceiveWithAnyArgs()
               .AddPendingRequest(Arg.Any<CorrelatableMessage>()
                );

            _fakeContext.ReceivedWithAnyArgs(1).WriteAsync(Arg.Any<IProtocolMessageDto<IMessage>>());
        }
    }
}