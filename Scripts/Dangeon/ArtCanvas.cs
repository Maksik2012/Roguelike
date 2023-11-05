using Godot;
using System;
using System.Collections.Generic;

namespace Roguelike.Dangeon
{
    public class ArtCanvas : Node2D
    {
        private AStar2D _CorridorGraph;
        private Color _PathColor = new Color(0, 0.5f, 1);

        public void DrawCorridor(AStar2D graph)
        {
            _CorridorGraph = graph;
            Update();
        }

        public override void _Draw()
        {
            if (_CorridorGraph != null)
            {
                foreach(int point in _CorridorGraph.GetPoints())
                {
                    Vector2 currentPointPos = _CorridorGraph.GetPointPosition(point);
                    foreach(int connect in _CorridorGraph.GetPointConnections(point))
                    {
                        Vector2 connectPointPos = _CorridorGraph.GetPointPosition(connect);
                        DrawLine(currentPointPos, connectPointPos, _PathColor, 10, true);
                    }
                }
            }
        }
    }
}
