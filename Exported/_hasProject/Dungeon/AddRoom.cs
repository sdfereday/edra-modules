using UnityEngine;
using System.Collections.Generic;
using RedPanda.Toolbox.Helpers;

namespace RedPanda.Dungeon
{
    public class AddRoom : MonoBehaviour
    {
        // TODO: Maybe rename this script to something else since it does a little more,
        // perhaps just call it 'room'?
        public string useMapId;
        public GameObject wallPrefab;
        public Transform wallParent;
        public List<Transform> originRoom;
        public Transform TransformIdentity => transform;
        private RoomTemplates templates;

        private void Awake()
        {
            BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>() as BoxCollider2D;
            collider.isTrigger = true;
            collider.size = new Vector2(20, 20);

            Rigidbody2D rbody = gameObject.AddComponent<Rigidbody2D>() as Rigidbody2D;
            rbody.bodyType = RigidbodyType2D.Kinematic;

            if (wallPrefab != null)
            {
                int[,] mapData = MapData
                    .GetMapEntry(useMapId)
                    .MapData;

                /* TODO: Not overly efficient, will improve. You could just
                 * make a map/room editor and avoid this completely. */
                int[,] rotatedMap = Arrays.RotateArrayClockwise(mapData);

                for (int row = 0; row < rotatedMap.GetLength(0); row++)
                {
                    for (int col = 0; col < rotatedMap.GetLength(1); col++)
                    {
                        var currentTile = rotatedMap[row, col];

                        if (currentTile == (int)MapData.TILE_TYPE.WALL)
                        {
                            // Zero-based unit squares (actually 10 on grid count).
                            float unitSquares = 9f;

                            // Since we're at the centre, we need to cater for it.
                            float unitSquareCentre = unitSquares / 2;

                            // scale up by the number of unit squares covered per tile.
                            Vector2 pos = (Vector2)transform.position + new Vector2(row - unitSquareCentre, col - unitSquareCentre) * 2;
                            // And then make it happen.
                            GameObject wall = Instantiate(wallPrefab, pos, Quaternion.identity, wallParent);
                            wall.tag = "Wall";
                        }
                    }
                }
            }
        }

        private void Start()
        {
            templates = GameObject
                .FindGameObjectWithTag("Rooms")
                .GetComponent<RoomTemplates>();

            // TODO: Convert to method
            templates.rooms.Add(gameObject);
        }

        public void RegisterOrigin(Transform neighbour) => originRoom.Add(neighbour);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player entered the room:" + gameObject.name);
                // ...
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Player left the room:" + gameObject.name);
                // ...
            }
        }
    }
}