namespace RedPanda.Global
{
    public static class ErrorConsts
    {
        public const string DUPLICATE_SCENE_MODEL = "Cannot have more than one scene data node of the same name. Please check your scene data.";
        public const string SCENE_DATA_NULL = "Cannot load scene data, it seems to be null.";
        public const string NON_KEY_ITEM_ERROR = "Tried to add a non-key item to the key item inventory, this isn't allowed.";
        public const string NON_NORMAL_ITEM_ERROR = "Tried to add a key item to non-key item store. This is not allowed.";
        public const string KEY_ITEM_INTEGRITY_FAILUE = "Mismatch between scene data and inventory, both must be in sync.";
    }
}