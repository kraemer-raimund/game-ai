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

        private readonly Guid _nodeId;
        private readonly bool _untilFailure;
        private readonly int _howOften;

        public Repeater(INode child, bool untilFailure, int howOften) : base(child)
        {
            _nodeId = Guid.NewGuid();
            _untilFailure = untilFailure;
            _howOften = howOften;
        }

        public override NodeResult Execute(IContext context)
        {
            var counter = RestoreCounter(context);
            
            if (_howOften >= 1 && counter == _howOften)
            {
                context.RemoveNodeData(_nodeId, CounterKey);
                return NodeResult.Success;
            }

            var childResult = Child.Execute(context);

            if (childResult != NodeResult.Running)
            {
                context.SetNodeData(_nodeId, CounterKey, counter + 1);
            }

            if (_untilFailure && childResult == NodeResult.Failure)
            {
                return NodeResult.Success;
            }
            else
            {
                return NodeResult.Running;
            }
        }

        private int RestoreCounter(IContext context)
        {
            if (context.TryGetNodeData(_nodeId, CounterKey, out var counterObj))
            {
                return (int) counterObj;
            }

            return 0;
        }
    }
}
