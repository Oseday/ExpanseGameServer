using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
	public class Player
	{
		public int id;
		public string username;

		public Ship ship;

		/*public Vector3 position;
		public Quaternion rotation;

		public Vector3 velocity;
		public Vector3 rotvelocity;*/

		//private float moveSpeed = 5f / Constants.TICKS_PER_SEC;

		public Player(int _id, string _username)
		{
			id = _id;
			username = _username;
			/*position = new Vector3(0,10,0);
			rotation = Quaternion.Identity;
			velocity = new Vector3(0);
			rotvelocity = new Vector3(0);*/

			Ship.SendAllShipInfo(id);

			ship = new Ship(id, new Vector3(0,2,0), new Quaternion());
		}

		public void Update(Packet NetUpdatePacket)
		{
			
		}

		/*public void PhysicsUpdate(Packet NetUpdatePacket)
		{
			NetUpdatePacket.Write(id);
			NetUpdatePacket.Write(position);
			NetUpdatePacket.Write(rotation);
			NetUpdatePacket.Write(velocity);
			NetUpdatePacket.Write(rotvelocity);
		}

		public void PlayerPosVelUpdate(Vector3 pos, Quaternion rot, Vector3 vel, Vector3 rotvel)
		{
			position = pos;
			rotation = rot;
			velocity = vel;
			rotvelocity = vel;
		}*/
		
		public void OnDisconnecting(){
			/*using Packet p = new Packet((int)ServerPackets.despawnPlayer);
			p.Write(id);
			ServerSend.SendTCPDataToAll(id,p);*/

			ship.Remove();
		}
	}
}
