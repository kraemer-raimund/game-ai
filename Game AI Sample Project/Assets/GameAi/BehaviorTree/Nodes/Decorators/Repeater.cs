/*
* MIT License
*
* Copyright(c) 2019 Raimund Krämer (kraemer-raimund)
*
* For the full copyright and license information, please refer to the LICENSE file
* that was distributed with this source code.
*/


using System;

namespace RaKrae.GameAi.BehaviorTree.Nodes.Decorators
{
    public class Repeater : Decorator
    {
        private const string CounterKey = "counter";

        private readonly Guid NodeId;
        private readonly bool UntilFailure;
        private readonly int HowOften;

        public Repeater(INode child, bool untilFailure, int howOften) : base(child)
        {
            NodeId = Guid.NewGuid();
            UntilFailure = untilFailure;
            HowOften = howOften;
        }

        public override NodeResult Execute(IContext context)
        {
            if (HowOften >= 1)
            {
                int counter;

                if (!context.TryGetNodeData(NodeId, CounterKey, out object counterObj))
                {
                    counter = 0;
                }
                else
                {
                    counter = (int)counterObj;
                }

                if (counter == HowOften)
                {
                    context.RemoveNodeData(NodeId, CounterKey);
                    return NodeResult.Success;
                }
                else
                {
                    context.SetNodeData(NodeId, CounterKey, counter + 1);
                }
            }

            if (UntilFailure)
            {
                var childResult = Child.Execute(context);

                return childResult == NodeResult.Failure
                    ? NodeResult.Success
                    : NodeResult.Running;
            }
            else
            {
                Child.Execute(context);
                return NodeResult.Running;
            }
        }
    }
}
