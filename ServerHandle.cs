using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
	/// <summary>Sent from server to client.</summary>
	public enum ServerPackets
	{
		welcome = 1,
		spawnPlayer = 2,
		PhysicsTick = 3,
		despawnPlayer = 4,
		SpawnShip = 5,
		DespawnShip = 6,
	}

	/// <summary>Sent from client to server.</summary>
	public enum ClientPackets
	{
		welcomeReceived = 1,
		ShipPhysicsUpdate = 2,
	}

	class ServerHandle
	{

		public static Dictionary<int, Server.PacketHandler> InitPacketDict()
		{
			return new Dictionary<int, Server.PacketHandler>
			{
				{ (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
				{ (int)ClientPackets.ShipPhysicsUpdate, ServerHandle.ShipPhysicsUpdate },
			};
		}

		public static void WelcomeReceived(int _fromClient, Packet _packet)
		{
			int _clientIdCheck = _packet.ReadInt();
			string _username = _packet.ReadString();

			Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
			if (_fromClient != _clientIdCheck)
			{
				Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
			}
			Server.clients[_fromClient].CreatePlayer(_username);
		}

		public static void SendWelcome(int _toClient, string _msg)
		{
			using Packet _packet = new Packet((int)ServerPackets.welcome);

			_packet.Write(_msg);
			_packet.Write(_toClient);

			ServerSend.SendTCPData(_toClient, _packet);
		}

		public static void ShipPhysicsUpdate(int _fromClient, Packet _packet)
		{
			if (Server.clients[_fromClient].player==null) { return; }

			Ship ship = Ship.GetShipFromOwner(Server.clients[_fromClient].player.id);
			if (ship==null){ return; }

			Vector3 pos = _packet.ReadVector3();
			Quaternion rot = _packet.ReadQuaternion();
			Vector3 vel = _packet.ReadVector3();
			Vector3 rotvel = _packet.ReadVector3();

			ship.UpdatePhysics(pos, rot, vel, rotvel);
		}


		/*public static void SpawnPlayer(int _toClient, Player _player)
		{
			using Packet _packet = new Packet((int)ServerPackets.spawnPlayer);
			_packet.Write(_player.id);
			_packet.Write(_player.username);
			_packet.Write(_player.position);
			_packet.Write(_player.rotation);

			ServerSend.SendTCPData(_toClient, _packet);
		}

		public static void PlayerPosVelUpdate(int _fromClient, Packet _packet)
		{
			if (Server.clients[_fromClient].player==null) { return; }
			Vector3 pos = _packet.ReadVector3();
			Quaternion rot = _packet.ReadQuaternion();
			Vector3 vel = _packet.ReadVector3();
			Vector3 rotvel = _packet.ReadVector3();
			Server.clients[_fromClient].player.PlayerPosVelUpdate(pos, rot, vel, rotvel);
		}*/


	}
}
