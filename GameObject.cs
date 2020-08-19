using System;
using System.Numerics;

namespace GameServer
{
    public class GameObject 
    {

        public int owner_id;
        public int obj_id;

        /*public GameObject(int _owner_id)
        {
            owner_id = _owner_id;

        }*/

        public static GameObject New(int _owner_id){
            GameObject a = new GameObject();
            a.owner_id = _owner_id; 
            return a;
        }

    }

    public class GameObject_Physics : GameObject
    {
        
        

    }

    public class Tests
    {
        public void Maina(){
            GameObject a = GameObject_Physics.New(1);
        }
    }
}

