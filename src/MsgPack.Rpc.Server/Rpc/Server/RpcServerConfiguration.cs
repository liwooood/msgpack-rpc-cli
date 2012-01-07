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
using System.Net;
using MsgPack.Rpc.Server.Dispatch;
using MsgPack.Rpc.Server.Protocols;

namespace MsgPack.Rpc.Server
{
	public sealed partial class RpcServerConfiguration : FreezableObject
	{
		private static readonly RpcServerConfiguration _default = new RpcServerConfiguration().Freeze();

		/// <summary>
		///		Gets the default frozen instance.
		/// </summary>
		/// <value>
		///		The default frozen instance.
		///		This value will not be <c>null</c>.
		/// </value>
		public static RpcServerConfiguration Default
		{
			get { return RpcServerConfiguration._default; }
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="RpcServerConfiguration"/> class.
		/// </summary>
		public RpcServerConfiguration() { }

		public ObjectPoolConfiguration CreateListeningContextPoolConfiguration()
		{
			return new ObjectPoolConfiguration() { ExhausionPolicy = ExhausionPolicy.ThrowException, MaximumPooled = this.MaximumConnection, MinimumReserved = this.MinimumConnection };
		}

		public ObjectPoolConfiguration CreateTcpTransportPoolConfiguration()
		{
			return new ObjectPoolConfiguration() { ExhausionPolicy = ExhausionPolicy.BlockUntilAvailable, MaximumPooled = this.MaximumConcurrentRequest, MinimumReserved = this.MinimumConcurrentRequest };
		}

		public ObjectPoolConfiguration CreateUdpTransportPoolConfiguration()
		{
			return new ObjectPoolConfiguration() { ExhausionPolicy = ExhausionPolicy.BlockUntilAvailable, MaximumPooled = this.MaximumConcurrentRequest, MinimumReserved = this.MinimumConcurrentRequest };
		}

		public ObjectPoolConfiguration CreateRequestContextPoolConfiguration()
		{
			return new ObjectPoolConfiguration() { ExhausionPolicy = ExhausionPolicy.BlockUntilAvailable, MaximumPooled = this.MaximumConcurrentRequest, MinimumReserved = this.MinimumConcurrentRequest };
		}

		public ObjectPoolConfiguration CreateResponseContextPoolConfiguration()
		{
			return new ObjectPoolConfiguration() { ExhausionPolicy = ExhausionPolicy.BlockUntilAvailable, MaximumPooled = this.MaximumConcurrentRequest, MinimumReserved = this.MinimumConcurrentRequest };
		}
				
		static partial void ValidateBindingEndPoint( EndPoint value )
		{
			if ( value == null )
			{
				throw new ArgumentNullException( "value" );
			}
		}

		static partial void ValidateListeningContextPoolProvider( Func<Func<ListeningContext>, ObjectPoolConfiguration, ObjectPool<ListeningContext>> value )
		{
			if ( value == null )
			{
				throw new ArgumentNullException( "value" );
			}
		}

		static partial void ValidateRequestContextPoolProvider( Func<Func<ServerRequestContext>, ObjectPoolConfiguration, ObjectPool<ServerRequestContext>> value )
		{
			if ( value == null )
			{
				throw new ArgumentNullException( "value" );
			}
		}

		static partial void ValidateResponseContextPoolProvider( Func<Func<ServerResponseContext>, ObjectPoolConfiguration, ObjectPool<ServerResponseContext>> value )
		{
			if ( value == null )
			{
				throw new ArgumentNullException( "value" );
			}
		}

		static partial void ValidateExecutionTimeout( TimeSpan? value )
		{
			if ( value != null && value.Value.Ticks < 0 )
			{
				throw new ArgumentOutOfRangeException( "value" );
			}
		}

		static partial void ValidateHardExecutionTimeout( TimeSpan? value )
		{
			if ( value != null && value.Value.Ticks < 0 )
			{
				throw new ArgumentOutOfRangeException( "value" );
			}
		}

		static partial void ValidateTransportManagerProvider( Func<RpcServer, ServerTransportManager> value )
		{
			if ( value == null )
			{
				throw new ArgumentNullException( "value" );
			}
		}

		static partial void ValidateDispatcherProvider( Func<RpcServer, Dispatcher> value )
		{
			if ( value == null )
			{
				throw new ArgumentNullException( "value" );
			}
		}

		static partial void ValidateServiceTypeLocatorProvider( Func<RpcServerConfiguration, ServiceTypeLocator> value )
		{
			if ( value == null )
			{
				throw new ArgumentNullException( "value" );
			}
		}

		static partial void ValidateTcpTransportPoolProvider( Func<Func<TcpServerTransport>, ObjectPoolConfiguration, ObjectPool<TcpServerTransport>> value )
		{
			if ( value == null )
			{
				throw new ArgumentNullException( "value" );
			}
		}

		static partial void ValidateUdpTransportPoolProvider( Func<Func<UdpServerTransport>, ObjectPoolConfiguration, ObjectPool<UdpServerTransport>> value )
		{
			if ( value == null )
			{
				throw new ArgumentNullException( "value" );
			}
		}

		static partial void ValidateListenBackLog( int value )
		{
			if ( value < 0 )
			{
				throw new ArgumentOutOfRangeException( "ListenBackLog must not be negative.", "value" );
			}
		}

		static partial void ValidateMaximumConcurrentRequest( int value )
		{
			if ( value < 1 )
			{
				throw new ArgumentOutOfRangeException( "MaximumConcurrentRequest must not be negative nor 0.", "value" );
			}
		}

		static partial void ValidateMaximumConnection( int value )
		{
			if ( value < 1 )
			{
				throw new ArgumentOutOfRangeException( "MaximumConnection must not be negative nor 0.", "value" );
			}
		}

		static partial void ValidateMinimumConcurrentRequest( int value )
		{
			if ( value < 0 )
			{
				throw new ArgumentOutOfRangeException( "MinimumConcurrentRequest must not be negative.", "value" );
			}
		}

		static partial void ValidateMinimumConnection( int value )
		{
			if ( value < 0 )
			{
				throw new ArgumentOutOfRangeException( "MinimumConnection must not be negative.", "value" );
			}
		}

		static partial void ValidatePortNumber( int value )
		{
			if ( value < 0 || UInt16.MaxValue < value )
			{
				throw new ArgumentOutOfRangeException( "PortNumber must be between 0 and 65,535.", "value" );
			}
		}

		/// <summary>
		///		Clones all of the fields of this instance.
		/// </summary>
		/// <returns>
		///		The shallow copy of this instance.
		/// </returns>
		public RpcServerConfiguration Clone()
		{
			return this.CloneCore() as RpcServerConfiguration;
		}

		/// <summary>
		///		Freezes this instance.
		/// </summary>
		/// <returns>
		///		This instance.
		/// </returns>
		public RpcServerConfiguration Freeze()
		{
			return this.FreezeCore() as RpcServerConfiguration;
		}

		/// <summary>
		/// Gets the frozen copy of this instance.
		/// </summary>
		/// <returns>
		/// This instance if it is already frozen.
		/// Otherwise, frozen copy of this instance.
		/// </returns>
		public RpcServerConfiguration AsFrozen()
		{
			return this.AsFrozenCore() as RpcServerConfiguration;
		}
	}
}
