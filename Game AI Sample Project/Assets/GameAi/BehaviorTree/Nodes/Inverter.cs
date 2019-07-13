/*
* MIT License
*
* Copyright(c) 2019 Raimund Krämer (kraemer-raimund)
*
* For the full copyright and license information, please refer to the LICENSE file
* that was distributed with this source code.
*/


using System;

namespace RaKrae.GameAi.BehaviorTree.Nodes
{
    public class Inverter : Decorator
    {
        public Inverter(INode child) : base(child) { }

        public override NodeResult Execute(IContext context)
        {
            NodeResult childResult = Child.Execute(context);

            switch (childResult)
            {
                case NodeResult.Success:
                    return NodeResult.Failure;
                case NodeResult.Failure:
                    return NodeResult.Success;
                case NodeResult.Running:
                    return NodeResult.Running;
                default:
                    throw new ArgumentException($"Unknown Enum value '{childResult}' for Enum type '{typeof(NodeResult)}'.");
            }
        }
    }
}
