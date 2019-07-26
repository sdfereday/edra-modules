using UnityEngine;

namespace RedPanda.Helpers
{
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            // TODO: Consts please.
            GameObject[] objs = GameObject.FindGameObjectsWithTag("GameData");

            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}