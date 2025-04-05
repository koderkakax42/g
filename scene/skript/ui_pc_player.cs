using Godot;
using System;
using System.Xml.XPath;

public partial class ui_pc_player :PanelContainer
{
    
	
	public static int nomber_open_chest = 0;
	private int chest_nomber=0;
	[Export] public PackedScene chest {get;set;}
	public static bool time = false;
	[Export]Label text{get;set;}
	[Export]private ProgressBar value{get;set;}
	[Export]Player player ;
	enemy enemy = new enemy();
	spawn spawn = new spawn();
	[Export] PanelContainer windows;
    public void _on_xp()
	{
	  GD.Print("player xp =" +Player.xp);
	  value.Value=Player.xp;
	}

	public void _on_vale(string money)
	{
		text.Text = money;
	}
	private void _on_chest()
	{
      switch (chest_nomber)
	  {
		case 1 : 
		         chest_nomber++;
		         Openchest();
		break;		 
		default:
		        chest_nomber = 1;

				if (nomber_open_chest <= 0)
				{
					Openchest();
				} 
		break;
	  }
	}
    private void Openchest()
	{
		var cheste = (shest)chest.Instantiate() as shest;
	  GetParent().AddChild(cheste);
	  cheste.GlobalPosition = player.GlobalPosition;

	  nomber_open_chest++;
	}

    public override void _Ready()
    {
         GetWindow().MinSize = new Vector2I(480,280 );
        GetWindow().MaxSize = new Vector2I(1920,960);
        // Подключаемся к сигналу size_changed
		
        GetWindow().Connect("size_changed", new Callable(this, nameof(OnWindowSizeChanged)));
    }

    private void OnWindowSizeChanged()
    {
        Vector2 newSize = GetWindow().Size;
    }

	public void _on_button()
	{
      player.SaveGamePlayer();
      enemy.savedata();
	  spawn.savegame();
	  GD.Print(" save is truy . ");
	}
}