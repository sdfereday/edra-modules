using System;

namespace RedPanda.Animator
{
    public abstract class ConditionObject<T>
    {
        public string Id;
        public T Value;
        public T Expected;
        public Func<T, T, bool> LogicMethod;
        public LOGIC_METHOD_TYPE LogicMethodType;

        public ConditionObject(string id, T initial, T expected, LOGIC_METHOD_TYPE logicMethodType)
        {
            Id = id;
            Value = initial;
            Expected = expected;
            LogicMethodType = logicMethodType;
        }

        // public bool Assert() => LogicMethod(Value, Expected);
        public abstract bool Assert();
    }
}