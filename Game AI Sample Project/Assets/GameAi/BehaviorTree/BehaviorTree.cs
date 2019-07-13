/*
* MIT License
*
* Copyright(c) 2019 Raimund Krämer (kraemer-raimund)
*
* For the full copyright and license information, please refer to the LICENSE file
* that was distributed with this source code.
*/


using System;
using RaKrae.GameAi.BehaviorTree.Nodes;

namespace RaKrae.GameAi.BehaviorTree
{
    public abstract class BehaviorTree : INode
    {
        protected INode Root { get; set; }

        public NodeResult Execute(IContext context)
        {
            if (Root == null)
            {
                throw new ArgumentNullException("The Root node of this Behaviour Tree is null. Did you forget to build up the tree in its constructor?");
            }

            return Root.Execute(context);
        }

        protected Selector Select(params INode[] children)
        {
            return new Selector(children);
        }

        protected Sequence Sequence(params INode[] children)
        {
            return new Sequence(children);
        }

        protected Inverter Invert(INode child)
        {
            return new Inverter(child);
        }

        protected Succeeder Succeed(INode child)
        {
            return new Succeeder(child);
        }

        protected Repeater Repeat(INode child, int howOften = -1)
        {
            return new Repeater(child, false, howOften);
        }

        protected Repeater RepeatUntilFailure(INode child)
        {
            return new Repeater(child, true, -1);
        }
    }
}
