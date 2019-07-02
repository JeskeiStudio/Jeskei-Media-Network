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
using Catalyst.Common.Interfaces.IO.Messaging;

namespace Catalyst.Common.IO.Messaging
{
    /// <inheritdoc/>
    public sealed class CorrelationId : ICorrelationId
    {
        public Guid Id { get; set; }
        
        /// <summary>
        ///     Provide a known correlation Id
        /// </summary>
        /// <param name="id"></param>
        public CorrelationId(Guid id) { Id = id; } 
        
        /// <summary>
        ///     Provide a correlation Id from a protocol message as byte string to get an ICorrelationId Type
        /// </summary>
        /// <param name="bytes"></param>
        public CorrelationId(byte[] bytes) { Id = new Guid(bytes); } 

        /// <summary>
        /// Gets a new CorrelationId
        /// </summary>
        private CorrelationId() { Id = Guid.NewGuid(); }

        /// <summary>
        ///     Static helper to get new CorrelationId
        /// </summary>
        /// <returns></returns>
        public static ICorrelationId GenerateCorrelationId()
        {
            return new CorrelationId();
        }
    }
}