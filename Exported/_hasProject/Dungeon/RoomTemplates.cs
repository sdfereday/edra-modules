using UnityEngine;
using System.Collections.Generic;
using RedPanda.Toolbox.Helpers;

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

        public DynamicRandomSelector<GameObject> randomBottom;
        public List<WeightedRoom> bottomRooms;
        public DynamicRandomSelector<GameObject> randomTop;
        public List<WeightedRoom> topRooms;
        public DynamicRandomSelector<GameObject> randomRight;
        public List<WeightedRoom> rightRooms;
        public DynamicRandomSelector<GameObject> randomLeft;
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
            // Spawn the start room
            Instantiate(startRoom, transform.position, Quaternion.identity, transform);

            // Don't forget to call .Build, otherwise the picker isn't initialised.
            randomBottom = new DynamicRandomSelector<GameObject>();
            bottomRooms.ForEach(item => randomBottom.Add(item.roomTemplate, item.weight));
            randomBottom.Build();

            randomTop = new DynamicRandomSelector<GameObject>();
            topRooms.ForEach(item => randomTop.Add(item.roomTemplate, item.weight));
            randomTop.Build();

            randomRight = new DynamicRandomSelector<GameObject>();
            rightRooms.ForEach(item => randomRight.Add(item.roomTemplate, item.weight));
            randomRight.Build();

            randomLeft = new DynamicRandomSelector<GameObject>();
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
                        Instantiate(boss, rooms[i].transform.position, Quaternion.identity, transform);
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