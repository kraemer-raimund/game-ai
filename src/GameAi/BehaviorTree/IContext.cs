/*
* MIT License
*
* Copyright(c) 2019 Raimund Krämer (kraemer-raimund)
*
* For the full copyright and license information, please refer to the LICENSE file
* that was distributed with this source code.
*/


using System;

namespace RaKrae.GameAi.BehaviorTree
{
    public interface IContext
    {
        bool TryGetData(string key, out object data);
        void SetData(string key, object value);
        void RemoveData(string key);

        bool TryGetNodeIndex(Guid compositeNodeId, out int nodeIndex);
        void SetNodeIndex(Guid compositeNodeId, int index);
        void RemoveNodeIndex(Guid compositeNodeId);

        bool TryGetNodeData(Guid nodeId, string key, out object data);
        void SetNodeData(Guid nodeId, string key, object value);
        void RemoveNodeData(Guid nodeId, string key);
        void ClearNodeData(Guid nodeId);
    }
}
