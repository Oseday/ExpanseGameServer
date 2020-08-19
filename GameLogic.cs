using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
	class GameLogic
	{
		public static void Update()
		{
			using Packet NetTickUpdatePacket = new Packet();

			int ClientCount = 0;

			foreach (Client _client in Server.clients.Values)
			{
				if (_client.player != null)
				{
					ClientCount++;
					_client.player.Update(NetTickUpdatePacket);
				}
			}

			ThreadManager.UpdateMain();
		}
		public static void PhysicsUpdate()
		{
			using Packet PhysicsTick = new Packet();


			int ShipCount = 0;


			foreach (var ship in Ship.ShipIdtoShipRef.Values)
			{
				ShipCount++;
				ship.UpdatePacket(PhysicsTick);
			}

			PhysicsTick.InsertInt(ShipCount);

			PhysicsTick.InsertLong(DateTime.UtcNow.Ticks);

			PhysicsTick.InsertInt((int)GameServer.ServerPackets.PhysicsTick);


			ServerSend.SendUDPDataToAll(PhysicsTick);
		}
	}
}
