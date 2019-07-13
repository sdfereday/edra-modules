using RedPanda.Utils;

namespace RedPanda.Animator
{
    public class BoolCondition : ConditionObject<bool>
    {
        public BoolCondition(string id, bool expected, bool initial, LOGIC_METHOD_TYPE logicMethodType)
            : base(id, initial, expected, logicMethodType) { }

        public override bool Assert()
        {
            switch (LogicMethodType)
            {
                case LOGIC_METHOD_TYPE.BOOL_TRUE:
                    return LogicHelpers.BoolTrue(Value, Expected);
                case LOGIC_METHOD_TYPE.BOOL_FALSE:
                    return LogicHelpers.BoolFalse(Value, Expected);
                default:
                    return false;
            }
        }
    }
}