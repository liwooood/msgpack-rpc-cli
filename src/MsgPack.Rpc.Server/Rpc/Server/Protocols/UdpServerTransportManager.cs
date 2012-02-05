#region -- License Terms --
//
// MessagePack for CLI
//
// Copyright (C) 2010 FUJIWARA, Yusuke
//
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//        http://www.apache.org/licenses/LICENSE-2.0
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//
#endregion -- License Terms --

using System;
using System.Globalization;
using System.Net.Sockets;

namespace MsgPack.Rpc.Server.Protocols
{
	public sealed class UdpServerTransportManager : ServerTransportManager<UdpServerTransport>
	{
		public UdpServerTransportManager( RpcServer server )
			: base( server )
		{
#if !API_SIGNATURE_TEST
			base.SetTransportPool( server.Configuration.UdpTransportPoolProvider( () => new UdpServerTransport( this ), server.Configuration.CreateUdpTransportPoolConfiguration() ) );
#endif
		}

		protected sealed override UdpServerTransport GetTransportCore( Socket bindingSocket )
		{
			if ( bindingSocket == null )
			{
				throw new InvalidOperationException( String.Format( CultureInfo.CurrentCulture, "'bindingSocket' is required in {0}.", this.GetType() ) );
			}

			var transport = base.GetTransportCore( bindingSocket );
			transport.BoundSocket = bindingSocket;
			return transport;
		}
	}
}
