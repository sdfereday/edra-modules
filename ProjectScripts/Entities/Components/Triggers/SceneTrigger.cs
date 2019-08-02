using UnityEngine;
using RedPanda.Toolbox.Effects;

namespace RedPanda.Entities
{
    public class SceneTrigger : MonoBehaviour
    {
        public string SceneName;
        public ChangeScene SceneChanger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                SceneChanger.DoTransition(ChangeScene.FadeDirection.In, SceneName);
            }
        }
    }
}
