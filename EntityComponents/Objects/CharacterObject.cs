using UnityEngine;

namespace RedPanda.Entities
{
    [CreateAssetMenu(fileName = "New Character Object", menuName = "Character Object", order = 51)]
    public class CharacterObject : ScriptableObject
    {
        public string Id;
        public string Name;

        public GameObject _GraphicalPrefab;
        public GameObject GraphicalPrefab => _GraphicalPrefab;

        [TextArea]
        public string _CharacterMeta;
        public string CharacterMeta => _CharacterMeta;
    }
}