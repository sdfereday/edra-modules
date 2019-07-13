namespace RedPanda.Utils
{
    public static class Log
    {
        public static void Out<T>(T thing) => UnityEngine.Debug.Log(thing);
    }
}