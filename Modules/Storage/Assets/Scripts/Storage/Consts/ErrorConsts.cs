namespace RedPanda.Storage
{
    public static class ErrorConsts
    {
        public const string DUPLICATE_SCENE_MODEL = "Cannot have more than one scene data node of the same name. Please check your scene data.";
        public const string SCENE_DATA_NULL = "Cannot load scene data, it seems to be null.";
    }
}