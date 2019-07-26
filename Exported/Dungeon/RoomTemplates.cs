using UnityEngine;
using System.Collections.Generic;

namespace RedPanda.Dungeon
{
    public class RoomTemplates : MonoBehaviour
    {
        // TODO: This class shape is used in multiple places, let's make it generic.
        [System.Serializable]
        public class WeightedRoom
        {
            public GameObject roomTemplate;
            public float weight;
        }

        public Helpers.DynamicRandomSelector<GameObject> randomBottom;
        public List<WeightedRoom> bottomRooms;
        public Helpers.DynamicRandomSelector<GameObject> randomTop;
        public List<WeightedRoom> topRooms;
        public Helpers.DynamicRandomSelector<GameObject> randomRight;
        public List<WeightedRoom> rightRooms;
        public Helpers.DynamicRandomSelector<GameObject> randomLeft;
        public List<WeightedRoom> leftRooms;

        // Provide a reference to our start room for extra calculation.
        public GameObject startRoom;

        // Gives guarantee that player doesn't escape dungeon (could be a hazard or anything, get creative).
        public GameObject closedRoom;

        // Rooms for exits, etc.
        public List<GameObject> rooms;

        // Boss spawner test (maybe use events instead rather than time).
        public float waitTime = 2f;
        public GameObject boss;
        private bool bossSpawned;

        private void Awake()
        {
            // Don't forget to call .Build, otherwise the picker isn't initialised.
            randomBottom = new Helpers.DynamicRandomSelector<GameObject>();
            bottomRooms.ForEach(item => randomBottom.Add(item.roomTemplate, item.weight));
            randomBottom.Build();

            randomTop = new Helpers.DynamicRandomSelector<GameObject>();
            topRooms.ForEach(item => randomTop.Add(item.roomTemplate, item.weight));
            randomTop.Build();

            randomRight = new Helpers.DynamicRandomSelector<GameObject>();
            rightRooms.ForEach(item => randomRight.Add(item.roomTemplate, item.weight));
            randomRight.Build();

            randomLeft = new Helpers.DynamicRandomSelector<GameObject>();
            leftRooms.ForEach(item => randomLeft.Add(item.roomTemplate, item.weight));
            randomLeft.Build();
        }

        private void Update()
        {
            // TODO: Could be improved.
            // We wait to ensure rooms have been spawned.
            if (waitTime <= 0 && bossSpawned == false)
            {
                // Get the last room and do something with it.
                for (int i = 0; i < rooms.Count; i++)
                {
                    if (i == rooms.Count - 1)
                    {
                        Instantiate(boss, rooms[i].transform.position, Quaternion.identity);
                        bossSpawned = true;
                    }
                }
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}