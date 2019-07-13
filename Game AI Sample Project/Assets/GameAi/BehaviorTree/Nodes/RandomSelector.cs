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

namespace RaKrae.GameAi.BehaviorTree.Nodes
{
    public class RandomSelector : Composite
    {
        public RandomSelector(INode[] children) : base(children) { }

        public override NodeResult Execute(IContext context)
        {
            if (!context.TryGetNodeIndex(CompositeNodeId, out int index))
            {
                index = 0;
                ShuffleChildren();
            }

            NodeResult childResult = Children[index].Execute(context);

            switch (childResult)
            {
                case NodeResult.Success:
                    context.SetNodeIndex(CompositeNodeId, -1);
                    return NodeResult.Success;
                case NodeResult.Running:
                    return NodeResult.Running;
                case NodeResult.Failure:
                    index++;
                    if (index == Children.Count)
                    {
                        context.SetNodeIndex(CompositeNodeId, -1);
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

        private void ShuffleChildren()
        {
            var random = new Random();
            var shuffledChildren = new List<INode>(Children);
            int n = shuffledChildren.Count;

            // Continually swap the item at the current index with a random one below that index.
            while (n > 1)
            {
                int k = random.Next(0, n--);
                INode temp = shuffledChildren[n];
                shuffledChildren[n] = shuffledChildren[k];
                shuffledChildren[k] = temp;
            }

            Children = shuffledChildren;
        }
    }
}
