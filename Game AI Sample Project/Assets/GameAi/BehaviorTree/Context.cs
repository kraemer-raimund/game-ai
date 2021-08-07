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

namespace RaKrae.GameAi.BehaviorTree
{
    public class Context : IContext
    {
        private readonly Dictionary<Guid, int> _nodeIndexByCompositeNodeId = new Dictionary<Guid, int>();
        private readonly Dictionary<Guid, Dictionary<string, object>> _nodeSpecificDataByNodeId = new Dictionary<Guid, Dictionary<string, object>>();
        private readonly Dictionary<string, object> _contextWideData = new Dictionary<string, object>();

        public bool TryGetData(string key, out object data)
        {
            return _contextWideData.TryGetValue(key, out data);
        }

        public void SetData(string key, object value)
        {
            _contextWideData[key] = value;
        }

        public void RemoveData(string key)
        {
            _contextWideData.Remove(key);
        }

        public bool TryGetNodeIndex(Guid compositeNodeId, out int nodeIndex)
        {
            return _nodeIndexByCompositeNodeId.TryGetValue(compositeNodeId, out nodeIndex);
        }

        public void SetNodeIndex(Guid compositeNodeId, int index)
        {
            _nodeIndexByCompositeNodeId[compositeNodeId] = index;
        }

        public void RemoveNodeIndex(Guid compositeNodeId)
        {
            _nodeIndexByCompositeNodeId.Remove(compositeNodeId);
        }

        public bool TryGetNodeData(Guid nodeId, string key, out object data)
        {
            if (_nodeSpecificDataByNodeId.TryGetValue(nodeId, out var nodeData))
            {
                return nodeData.TryGetValue(key, out data);
            }
            else
            {
                data = null;
                return false;
            }
        }

        public void SetNodeData(Guid nodeId, string key, object value)
        {
            if (!_nodeSpecificDataByNodeId.ContainsKey(nodeId))
            {
                _nodeSpecificDataByNodeId[nodeId] = new Dictionary<string, object>();
            }

            _nodeSpecificDataByNodeId[nodeId][key] = value;
        }

        public void RemoveNodeData(Guid nodeId, string key)
        {
            if (_nodeSpecificDataByNodeId.ContainsKey(nodeId))
            {
                _nodeSpecificDataByNodeId[nodeId].Remove(key);
            }
        }

        public void ClearNodeData(Guid nodeId)
        {
            if (_nodeSpecificDataByNodeId.TryGetValue(nodeId, out var nodeData))
            {
                nodeData.Clear();
                _nodeSpecificDataByNodeId.Remove(nodeId);
            }
        }
    }
}
