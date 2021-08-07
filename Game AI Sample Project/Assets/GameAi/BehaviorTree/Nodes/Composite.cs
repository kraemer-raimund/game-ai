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
    public abstract class Composite : Node
    {
        protected Guid CompositeNodeId { get; private set; }
        protected IReadOnlyList<INode> Children { get; set; }

        public Composite(IEnumerable<INode> children)
        {
            CompositeNodeId = Guid.NewGuid();
            Children = new List<INode>(children);
        }
    }
}
