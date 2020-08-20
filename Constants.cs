using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
	public enum ServerPackets
	{
		welcome = 1,
		spawnPlayer = 2,
		PhysicsTick = 3,
		despawnPlayer = 4,
		SpawnShip = 5,
		DespawnShip = 6,
		AllShipInfo = 7,
	}

	/// <summary>Sent from client to server.</summary>
	public enum ClientPackets
	{
		welcomeReceived = 1,
		ShipPhysicsUpdate = 2,
	}

public static class Constants {
	public const int TICKS_PER_SEC = 30;
	public const float MS_PER_TICK = 1000f / TICKS_PER_SEC;
		
	public const int PHYS_TICKS_PER_SEC = 10;
	public const float PHYS_MS_PER_TICK = 1000f / PHYS_TICKS_PER_SEC;

    
    public static Dictionary<short,string> ShipTypeDict = new Dictionary<short,string>(){
		{1,DefaultShip},
	};
    public static Dictionary<string,short> ShipTypeDictFromValue = new Dictionary<string,short>(){
		{DefaultShip,1},
	};

    public const string DefaultShip = "DefaultShip"; 


	//Specifics
	public static Dictionary<int, Server.PacketHandler> InitPacketDict()
		{
			return new Dictionary<int, Server.PacketHandler>
			{
				{ (int)ClientPackets.welcomeReceived, ServerHandle.WelcomeReceived },
				{ (int)ClientPackets.ShipPhysicsUpdate, ServerHandle.ShipPhysicsUpdate },
			};
		}

}


}

