/*
* MIT License
*
* Copyright(c) 2019 Raimund Krämer (kraemer-raimund)
*
* For the full copyright and license information, please refer to the LICENSE file
* that was distributed with this source code.
*/


namespace RaKrae.GameAi.BehaviorTree.Nodes
{
    public abstract class Decorator : Node
    {
        protected INode Child { get; }

        public Decorator(INode child)
        {
            Child = child;
        }
    }
}
