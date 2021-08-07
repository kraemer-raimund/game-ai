/*
* MIT License
*
* Copyright(c) 2019 Raimund Krämer (kraemer-raimund)
*
* For the full copyright and license information, please refer to the LICENSE file
* that was distributed with this source code.
*/


using System;
using System.Collections.Generic;

namespace RaKrae.GameAi.BehaviorTree.Nodes.Composites
{
    public class Selector : Composite
    {
        public Selector(IList<INode> children) : base(children) { }

        public override NodeResult Execute(IContext context)
        {
            if (!context.TryGetNodeIndex(CompositeNodeId, out int index))
            {
                index = 0;
            }

            NodeResult childResult = Children[index].Execute(context);

            switch (childResult)
            {
                case NodeResult.Success:
                    context.RemoveNodeIndex(CompositeNodeId);
                    return NodeResult.Success;
                case NodeResult.Running:
                    return NodeResult.Running;
                case NodeResult.Failure:
                    index++;
                    if (index == Children.Count)
                    {
                        context.RemoveNodeIndex(CompositeNodeId);
                        return NodeResult.Failure;
                    }
                    else
                    {
                        context.SetNodeIndex(CompositeNodeId, index);
                        return NodeResult.Running;
                    }
                default:
                    throw new ArgumentException($"Unknown Enum value '{childResult}' for Enum type '{typeof(NodeResult)}'.");
            }
        }
    }
}
