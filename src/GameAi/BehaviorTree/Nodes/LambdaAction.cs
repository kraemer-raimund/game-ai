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
    public class LambdaAction : Action
    {
        private readonly Func<IContext, NodeResult> _action;
        
        public LambdaAction(Func<IContext, NodeResult> action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public override NodeResult Execute(IContext context)
        {
            return _action.Invoke(context);
        }
    }
}
