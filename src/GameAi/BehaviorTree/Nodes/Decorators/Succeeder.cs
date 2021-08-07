/*
* MIT License
*
* Copyright(c) 2019 Raimund Krämer (kraemer-raimund)
*
* For the full copyright and license information, please refer to the LICENSE file
* that was distributed with this source code.
*/


namespace RaKrae.GameAi.BehaviorTree.Nodes.Decorators
{
    public class Succeeder : Decorator
    {
        public Succeeder(INode child) : base(child) { }

        public override NodeResult Execute(IContext context)
        {
            NodeResult childResult = Child.Execute(context);
            return childResult == NodeResult.Running ? NodeResult.Running : NodeResult.Success;
        }
    }
}
