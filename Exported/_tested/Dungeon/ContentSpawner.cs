using UnityEngine;
using System.Collections.Generic;
using RedPanda.Toolbox.Helpers;

namespace RedPanda.Dungeon
{
    public class ContentSpawner : MonoBehaviour
    {
        /*
         Similar to room spawner, this will take a prefab and spawn it in the center
         of the spawned room. Prefabs could be anything, but the idea is they handle
         themselves entirely so you can have a mini game within your room basically.

        I guess we'll call them micro-games for now? Either way, they should be able
        to communicate out and read stuff, they're not black boxes, more transparent.
         */
        [System.Serializable]
        public class WeightedContent
        {
            public GameObject contentToSpawn;
            public float weight;
        }

        public DynamicRandomSelector<GameObject> randomContentPicker;
        public List<WeightedContent> randomContentItems;
        public GameObject contentParent;

        private void Start()
        {
            randomContentPicker = new DynamicRandomSelector<GameObject>();
            randomContentItems.ForEach(item => randomContentPicker.Add(item.contentToSpawn, item.weight));
            randomContentPicker.Build();

            var picked = randomContentPicker.SelectRandomItem();
            GameObject spawnedContent = Instantiate(picked, transform.position, Quaternion.identity, contentParent.transform);
        }
    }
}