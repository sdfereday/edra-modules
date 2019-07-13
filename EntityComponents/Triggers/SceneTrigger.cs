using UnityEngine;
using RedPanda.Effects;

namespace RedPanda.Entities
{
    public class SceneTrigger : MonoBehaviour
    {
        public string SceneName;
        public ChangeScene SceneChanger;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag(CharacterConsts.PLAYER_INTERACTOR))
            {
                SceneChanger.DoTransition(ChangeScene.FadeDirection.In, SceneName);
            }
        }
    }
}
