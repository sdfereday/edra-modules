using UnityEngine;

namespace RedPanda.Dungeon
{
    public class RoomSpawner : MonoBehaviour
    {
        /*
         At present if you want more chance for a room type to spawn, then we
         must drag the same prefab in to the same list of room types.
         You could of course do this a little easier with math and weights in
         the chance methods below.
        */
        public ROOM_DIRECTIONS needsRoomWith;

        private RoomTemplates templates;
        private bool spawned;
        private float timeToDestruct = 3f;

        private void Start()
        {
            Destroy(gameObject, timeToDestruct);
            templates = GameObject
                .FindGameObjectWithTag("Rooms")
                .GetComponent<RoomTemplates>();

            Invoke("Spawn", .1f);
        }

        private void Spawn()
        {
            if (!spawned)
            {
                GameObject spawnedRoom = null;

                if (needsRoomWith == ROOM_DIRECTIONS.BOTTOM)
                {
                    var picked = templates.randomBottom.SelectRandomItem();
                    spawnedRoom = Instantiate(picked, transform.position, picked.transform.rotation);
                }

                if (needsRoomWith == ROOM_DIRECTIONS.TOP)
                {
                    var picked = templates.randomTop.SelectRandomItem();
                    spawnedRoom = Instantiate(picked, transform.position, picked.transform.rotation);
                }

                if (needsRoomWith == ROOM_DIRECTIONS.RIGHT)
                {
                    var picked = templates.randomRight.SelectRandomItem();
                    spawnedRoom = Instantiate(picked, transform.position, picked.transform.rotation);
                }

                if (needsRoomWith == ROOM_DIRECTIONS.LEFT)
                {
                    var picked = templates.randomLeft.SelectRandomItem();
                    spawnedRoom = Instantiate(picked, transform.position, picked.transform.rotation);
                }

                if (spawnedRoom)
                {
                    // Use the 'addRoom' script to return the top level transform for 'this' room.
                    Transform originTransform = GetComponentInParent<AddRoom>()
                        .TransformIdentity;

                    // Then pass that over to the newly instantiated room and register it.
                    spawnedRoom.GetComponentInParent<AddRoom>()
                        .RegisterOrigin(originTransform);
                }

                spawned = true;
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("SpawnPoint"))
            {
                // In the event that two spawners have arrived at the same point, but not registered collision (edge-case).
                if (!other.GetComponent<RoomSpawner>().spawned && !spawned)
                {
                    var cr = Instantiate(templates.closedRoom, transform.position, Quaternion.identity);

                    // Make sure we aren't overlapping any other rooms
                    if (Helpers.Vectors.Dist(cr.transform.position, templates.startRoom.transform.position) <= 1f)
                    {
                        Destroy(cr);
                    }

                    Destroy(gameObject);
                }
                spawned = true;
            }
        }
    }
}