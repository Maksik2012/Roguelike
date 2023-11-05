using Godot;
using System;
using System.Collections.Generic;

namespace Roguelike.Dangeon
{
	public class Level
	{
		public TileMap Walls { get; private set; }
		public TileMap Floor { get; private set; }

		private int _Tilesize = 16;
		private Vector2 _Size;

		private int _ChunkSize;
		public List<Chunk> ChunkList = new List<Chunk>();

		private int _MinRoomSize;
		private int _MaxRoomSize;
		private int _NumRooms;
		private List<Vector2> _RoomsPos = new List<Vector2>();

		private Random _Rand = new Random();

		private AStar2D _Path = new AStar2D();

		private ArtCanvas _ArtCanvas = new ArtCanvas();

		public Level(Vector2 size, int chunkSize=20, int minRoomSize=5, int maxRoomSize = 16, int numRooms = 17)
		{
			_Size = size;
			_ChunkSize = chunkSize;
			_MinRoomSize = minRoomSize;
			_MaxRoomSize = maxRoomSize;
			_NumRooms = numRooms;

			Walls = new TileMap();
			Floor = new TileMap();

			Floor.CellSize = new Vector2(_Tilesize, _Tilesize);
			Walls.CellSize = new Vector2(_Tilesize, _Tilesize);

			Walls.CellTileOrigin = TileMap.TileOrigin.Center;
			Walls.CellYSort = true;

			Walls.TileSet = (TileSet)ResourceLoader.Load("res://Resuorces/TileSets/walls.tres");
			Floor.TileSet = (TileSet)ResourceLoader.Load("res://Resuorces/TileSets/floor.tres");

			GenerateDangeon();
		}

		private void GenerateDangeon()
		{
			FillArea(Walls, Vector2.Zero, _Size, 0);
			CreateChunks();
			ShuffleChunks();
			GenerateRooms();
			FindPath();
			GenerateCorridor();
		}

		private void FillArea(TileMap tilemap, Vector2 pos, Vector2 size, int IdTile)
		{
			Vector2 tilePos;

			for (int y = 0; y < size.y; y++)
			{
				for(int x = 0; x < size.x; x++)
				{
					tilePos.x = pos.x + x;
					tilePos.y = pos.y + y;
					tilemap.SetCellv(tilePos, IdTile);
				}
			}

			tilemap.UpdateBitmaskRegion();
		}

		public void ConnectLevel(Node parent)
		{
			parent.AddChild(Floor);
			parent.AddChild(Walls);
			parent.AddChild(_ArtCanvas);
		}
		public void CreateChunks()
		{
			Vector2 chunkPos = Vector2.Zero;

			for (int chunkY = 0; chunkY <  _Size.y / _ChunkSize; chunkY++)
			{
				for(int chunkX = 0; chunkX < _Size.x / _ChunkSize; chunkX++)
				{
					chunkPos.x = chunkX * _ChunkSize;
					chunkPos.y = chunkY * _ChunkSize;

					ChunkList.Add(new Chunk(chunkPos));
				}
			}
		}

		public void ShuffleChunks()
		{
			for(int i = ChunkList.Count - 1; i >= 1; i--)
			{
				int j = _Rand.Next(i + 1);

				Chunk currentChunk = ChunkList[j];
				ChunkList[j] = ChunkList[i];
				ChunkList[i] = currentChunk;
			}
		}

		public void GenerateRooms()
		{
			for (int i = 0; i < _NumRooms; i++)
			{
				Room room = new Room(Walls, ChunkList[i].Position, _ChunkSize, _MinRoomSize, _MaxRoomSize);
				ChunkList[i].Room = room;
				FillArea(Floor, room.Position, new Vector2(room.Width, room.Height), 0);
				_RoomsPos.Add(room.Position);
			}
		}
		private void FindPath()
		{
			Vector2 startPos = _RoomsPos[0];

			_Path.AddPoint(0, startPos);
			_RoomsPos.Remove(startPos);

			foreach (Vector2 posRoom in _RoomsPos)
			{
				int currentPoint = _Path.GetAvailablePointId();
				int nearPoint = _Path.GetClosestPoint(posRoom);
				_Path.AddPoint(currentPoint, posRoom);
				_Path.ConnectPoints(nearPoint, currentPoint);
			}
			_ArtCanvas.DrawCorridor(_Path);
		}
		private void GenerateCorridor()
		{
			foreach(int point in _Path.GetPoints())
			{
				Vector2 currentPointPos = _Path.GetPointPosition(point) / _Tilesize;

				foreach(int connect in _Path.GetPointConnections(point))
				{
					Vector2 currentPointPosTile = currentPointPos;
					Vector2 connectPointPos = _Path.GetPointPosition(connect) / _Tilesize;
					Vector2 lenght = connectPointPos - currentPointPosTile;

					int incrementX;
					if (lenght.x > 0)
						incrementX = 1;
					else
						incrementX = -1;
					int incrementY;
					if (lenght.y > 0)
						incrementY = 1;
					else
						incrementY = -1         ;

					for(int x = 0; x < Math.Abs(lenght.x); x++)
					{
						currentPointPosTile.x += incrementX;
						Walls.SetCellv(currentPointPosTile, -1);
						Floor.SetCellv(currentPointPosTile, 0);
					}
					for (int y = 0; y < Math.Abs(lenght.y); y++)
					{
						currentPointPosTile.y += incrementY;
						Walls.SetCellv(currentPointPosTile, -1);
						Floor.SetCellv(currentPointPosTile, 0);
					}
				}
			}
			Walls.UpdateBitmaskRegion();
		}
	}
}
