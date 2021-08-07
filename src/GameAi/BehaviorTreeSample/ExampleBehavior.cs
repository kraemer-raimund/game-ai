/*
* MIT License
*
* Copyright(c) 2021 Raimund Krämer (kraemer-raimund)
*
* For the full copyright and license information, please refer to the LICENSE file
* that was distributed with this source code.
*/


using System;
using BehaviorTreeSample.Actions;
using RaKrae.GameAi.BehaviorTree;
using RaKrae.GameAi.BehaviorTree.Nodes;

namespace BehaviorTreeSample
{
    public class ExampleBehavior : BehaviorTree
    {
        public ExampleBehavior()
        {
            Root = Repeat(
                Sequence(
                    // We can reuse whole behavior trees when composing more complex behaviors:
                    // new SomeOtherBehaviorTree(),
                    
                    new HelloWorldAction(),
                    new LambdaAction(context =>
                    {
                        // The Context is the memory of the AI agent and can be used to remember information between
                        // behavior tree executions. The behavior tree itself is stateless, thus a single behavior tree
                        // instance can be used to execute many AI agents, each having its own Context.
                        // This way an AI agent can e. g. first search its surroundings for a target and then, if
                        // a target is found successfully, remember the target and attack it later in the same sequence.
                        // If no target is found (Failure) the sequence stops.
                        const string helloWorldCountKey = "HelloWorldCount";
                        int count;
                        if (context.TryGetData(helloWorldCountKey, out var countObj))
                        {
                            count = (int?) countObj ?? 1;
                        }
                        else
                        {
                            count = 1;
                        }
                        Console.WriteLine($"Hello, World! {count}");
                        context.SetData(helloWorldCountKey, count + 1);
                        return NodeResult.Success;
                    }),
                    // This will always succeed, despite its child node returning Failure.
                    Succeed(
                        new LambdaAction(_ => NodeResult.Failure)
                    ),
                    new HelloWorldAction(),
                    Invert(
                        new HelloWorldAction()
                    ),
                    // This won't be reached because the Success of the HelloWorldAction above has been inverted to
                    // Failure, thus ending the Sequence.
                    new HelloWorldAction()
                ),
                howOften: 2
            );
        }

        public static void Main()
        {
            var exampleBehavior = new ExampleBehavior();
            var context = new Context();

            while (true)
            {
                var result = exampleBehavior.Execute(context);
                
                if (result is NodeResult.Success or NodeResult.Failure)
                {
                    Console.WriteLine(result);
                    break;
                }
            }
        }
    }
}