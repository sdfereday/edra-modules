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
                        interrupts = false,
                        floatConditions = new List<GateFloat>()
                        {
                            new GateFloat()
                            {
                                Id = "magnitude",
                                Value = 0.0f,
                                Expected = 0.0f,
                                LogicMethod = LOGIC_METHOD_TYPE.FLOAT_EQUAL
                            }
                        }
                    },
                    new GateModel()
                    {
                        animationName = "Walk",
                        interrupts = false,
                        floatConditions = new List<GateFloat>()
                        {
                            new GateFloat()
                            {
                                Id = "magnitude",
                                Value = 0.0f,
                                Expected = 0.0f,
                                LogicMethod = LOGIC_METHOD_TYPE.FLOAT_GREATER_THAN
                            }
                        }
                    },
                    new GateModel()
                    {
                        animationName = "Attack",
                        interrupts = true,
                        boolConditions = new List<GateBool>()
                        {
                            new GateBool()
                            {
                                Id = "attack",
                                Value = false,
                                Expected = true,
                                LogicMethod = LOGIC_METHOD_TYPE.BOOL_TRUE
                            }
                        }
                    }
                }
            }
        };
    }
}
