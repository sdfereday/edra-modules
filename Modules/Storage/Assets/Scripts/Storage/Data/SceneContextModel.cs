using System.Collections.Generic;

namespace RedPanda.Storage
{
    [System.Serializable]
    public class SceneContextModel
    {
        public string sceneName;
        public List<BoolStateModel> boolStates;
        public List<QuantityStateModel> quantityStates;
    }
}