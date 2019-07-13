using System.Collections.Generic;
using RedPanda.Animator;

namespace RedPanda.MockServices
{
    public static class MockAnimationService
    {
        public static List<GateModelCollection> GateData = new List<GateModelCollection>()
        {
            new GateModelCollection()
            {
                targetId = "player",
                gates = new List<GateModel>()
                {
                    new GateModel()
                    {
                        animationName = "Idle",
                        floatConditions = new List<GateFloat>()
                        {
                            new GateFloat()
                            {
                                Id = "magnitude",
                                Value = 0.0f,
                                Expected = 0.0f,
                                LogicMethod = LOGIC_METHOD_TYPE.FLOAT_EQUAL
                            }
                        },
                        boolConditions = null
                    },
                    new GateModel()
                    {
                        animationName = "Walk",
                        floatConditions = new List<GateFloat>()
                        {
                            new GateFloat()
                            {
                                Id = "magnitude",
                                Value = 0.0f,
                                Expected = 0.0f,
                                LogicMethod = LOGIC_METHOD_TYPE.FLOAT_GREATER_THAN
                            }
                        },
                        boolConditions = null
                    }
                }
            }
        };
    }
}
