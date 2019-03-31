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

using Catalyst.Node.Common.Helpers.Shell;

namespace Catalyst.Node.Common.Interfaces
{
    public interface IShell
    {
        /// <summary>
        /// </summary>
        /// <returns></returns>
        bool RunConsole();
        
        /// <summary>
        /// </summary>
        /// <returns></returns>
        bool OnStart(string[] args);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        bool OnStartWork(string[] args);

        /// <summary>
        /// </summary>
        bool OnStop(string[] args);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        bool OnStopNode(string[] args);

        /// <summary>
        /// </summary>
        /// <returns></returns>
        bool OnStopWork(string[] args);

        bool IsConnectedNode(string nodeId);

        IRpcNode GetConnectedNode(string nodeId);

        bool OnCommand(params string[] args);
    }
}