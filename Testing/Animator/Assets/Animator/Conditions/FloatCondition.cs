using RedPanda.Utils;

namespace RedPanda.Animator
{
    public class FloatCondition : ConditionObject<float>
    {
        public FloatCondition(string id, float expected, float initial, LOGIC_METHOD_TYPE logicMethodType)
            : base(id, initial, expected, logicMethodType) { }

        public override bool Assert()
        {
            switch (LogicMethodType)
            {
                case LOGIC_METHOD_TYPE.FLOAT_EQUAL:
                    return LogicHelpers.FloatEqual(Value, Expected);
                case LOGIC_METHOD_TYPE.FLOAT_GREATER_THAN:
                    return LogicHelpers.FloatGreaterThan(Value, Expected);
                case LOGIC_METHOD_TYPE.FLOAT_LESS_THAN:
                    return LogicHelpers.FloatLessThan(Value, Expected);
                default:
                    return false;
            }
        }
    }
}