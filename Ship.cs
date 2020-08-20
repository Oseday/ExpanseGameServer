using System;
using System.Numerics;
using System.Collections.Generic;

namespace GameServer {

	public class Ship {

		//Physics

		private static int shipidcounter = 0;
		public int ship_id;

		public short type = Constants.ShipTypeDictFromValue[Constants.DefaultShip];

		public Vector3 position;
		public Quaternion rotation;
		public Vector3 velocity = new Vector3();
		public Vector3 rotvelocity = new Vector3();

		public void GatherAllShipInfo(Packet p){
			p.Write(ship_id);
			p.Write(owner_id);
			p.Write((int)type);
			p.Write(position);
			p.Write(rotation);
			p.Write(velocity);
			p.Write(rotvelocity);
		}

		public void UpdatePacket(Packet p){
			p.Write(ship_id);
			p.Write(owner_id);
			p.Write(position);
			p.Write(rotation);
			p.Write(velocity);
			p.Write(rotvelocity);
		}

		public void UpdatePhysics(Vector3 pos, Quaternion rot, Vector3 vel, Vector3 rotvel){
			position = pos;
			rotation = rot;
			velocity = vel;
			rotvelocity = rotvel;
		}

		public void SpawnSend(){
			using Packet p = new Packet((int)ServerPackets.SpawnShip);
			this.GatherAllShipInfo(p);
			ServerSend.SendTCPDataToAll(p);
		}

		/*public static void ClientShipSpawnSend(int user_id, int ship_id){

			Ship ship = GetShipFromShipId(ship_id);
			if (ship==null){return;}

			using Packet p = new Packet((int)ServerPackets.SpawnShip);
			p.Write(ship_id);
			p.Write(ship.owner_id);
			p.Write((int)ship.type);
			p.Write(ship.position);
			p.Write(ship.rotation);
			p.Write(ship.velocity);
			p.Write(ship.rotvelocity);
			ServerSend.SendTCPData(user_id,p);
		}*/


		public int owner_id;

		public List<int> plrsInside;

		public void AddPlayer(int id){
			AddPlrShipDict(id, this);
			plrsInside.Add(id);
		}

		public bool RemovePlayer(int id){
			RemovePlrShipDict(id, this);
			return plrsInside.Remove(id);
		} 

		public void Remove(){
			RemoveShipDict(this);
			using Packet p = new Packet((int)ServerPackets.DespawnShip);
			p.Write(ship_id);
			ServerSend.SendTCPDataToAll(p);
		}

		public Ship(int _owner_id, Vector3 pos, Quaternion rot) {
			shipidcounter = Math.mod(shipidcounter + 1, 1048576);
			ship_id = shipidcounter;
			owner_id = _owner_id;

			position = pos;
			rotation = rot;

			AddShipDict(this);

			SpawnSend();
		}



		//Static methods

		private static Dictionary<int,Ship> OwnerShipRef = new Dictionary<int, Ship>();
		private static Dictionary<int,Ship> PlrInShipRef = new Dictionary<int, Ship>();

		public static Dictionary<int,Ship> ShipIdtoShipRef = new Dictionary<int, Ship>();


		public static Ship GetShipFromOwner(int owner_id){
			return OwnerShipRef[owner_id];
		}
		public static Ship GetShipFromPlr(int user_id){
			return PlrInShipRef[user_id];
		}
		public static Ship GetShipFromShipId(int ship_id){
			return ShipIdtoShipRef[ship_id];
		}

		private static void AddShipDict(Ship ship){
			OwnerShipRef[ship.owner_id] = ship;
			ShipIdtoShipRef[ship.ship_id] = ship;
		}
		private static void RemoveShipDict(Ship ship){
			OwnerShipRef[ship.owner_id] = null;
			ShipIdtoShipRef[ship.ship_id] = null;
		}

		private static void AddPlrShipDict(int uid, Ship ship){
			PlrInShipRef[uid] = ship;
		}
		private static void RemovePlrShipDict(int uid, Ship ship){
			PlrInShipRef[uid] = null;
		}

		public static void SendAllShipInfo(int uid){
			using Packet p = new Packet();

			//p.Write(ShipIdtoShipRef.Values.Count);

			int shipc = 0;
			
			foreach (var ship in ShipIdtoShipRef.Values)
			{
				if (ship!=null){ 
					shipc++;
					ship.GatherAllShipInfo(p);
				}
			}

			p.InsertInt(shipc);

			p.InsertInt((int)ServerPackets.AllShipInfo);
			ServerSend.SendTCPData(uid, p);
		}

	}
}
