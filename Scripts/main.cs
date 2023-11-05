using Godot;
using Roguelike.Entities;
using Roguelike.Dangeon;

public class main : Node
{
	private Player _Player;
	private Level _Dungeon;
	public override void _Ready()
	{
		_Dungeon = new Level(new Vector2(100, 100));
		_Dungeon.ConnectLevel(this);

		_Player = new Player(); 
		
		
		_Player.ConnectToNode(_Dungeon.Walls);
		_Player.SetPosition(_Dungeon.ChunkList[0].Room.Center);
	}

}
