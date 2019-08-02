using UnityEngine;

namespace RedPanda.Toolbox.Helpers
{
    public class DontDestroy : MonoBehaviour
    {
        private void Awake()
        {
            GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestroy");

            if (objs.Length > 1)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }
    }
}