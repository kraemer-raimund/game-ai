using System;
using RaKrae.GameAi.BehaviorTree;
using Action = RaKrae.GameAi.BehaviorTree.Nodes.Action;

namespace BehaviorTreeSample.Actions
{
    public class HelloWorldAction : Action
    {
        public override NodeResult Execute(IContext context)
        {
            Console.WriteLine("Hello, World!");
            return NodeResult.Success;
        }
    }
}