# Game AI

A library for AI behaviors in games, compatible with Unity.

## Summary

So far this only contains an implementation of behavior trees, ready to be used, which I have already used in some personal game projects. I might add some example projects later on. If you are already familiar with the concept of behavior trees, you can contribute by creating a pull request with an example project.

The goal is to create a library of reusable mechanisms for creating both simple and complex AI behaviors for games, implemented in C#. They can be used e. g. with Unity, but not only with Unity.

## Behavior Trees

Behavior trees can be built up using individual nodes, or by composing multiple behavior trees together to create very complex behaviors using simple modular building blocks. You can also add your own node types by implementing the `INode` interface.

To create your own behavior, create a sub-class of `BehaviorTree`, and in its constructor instantiate the nodes or sub-trees that comprise your desired AI behavior. The `BehaviorTree` class has some convenience methods for instantiating some of the most common node types.

In case you are new to behavior trees, [here's an article](https://www.gamasutra.com/blogs/ChrisSimpson/20140717/221339/Behavior_trees_for_AI_How_they_work.php) with a good overview of the concept.

# License

The code is licensed under the MIT license. Please refer to the LICENSE file for more info.
