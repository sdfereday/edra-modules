namespace RedPanda.Utils
{
    public static class LogicHelpers
    {
        // Floats
        public static bool FloatEqual(float a, float b) => a.Equals(b);
        public static bool FloatGreaterThan(float a, float b) => a > b;
        public static bool FloatLessThan(float a, float b) => a < b;

        // Bools
        public static bool BoolTrue(bool a, bool b) => a.Equals(b);
        public static bool BoolFalse(bool a, bool b) => !a.Equals(b);
    }
}