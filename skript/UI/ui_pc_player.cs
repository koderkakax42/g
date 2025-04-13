using Godot;
using Godot.NativeInterop;
using System;
using System.Xml.XPath;
using System.Data;

public partial class ui_pc_player :PanelContainer
{	
	public static event Action time_stop = delegate{} ;
	public static event Action save = delegate{};
	PackedScene godmode ;
	public static int nomber_open_chest = 0;
	private int chest_nomber=0;
	 public PackedScene chest {get;set;}
	Label text{get;set;}
	private ProgressBar value{get;set;}
	Player player;

	spawn spawn = new spawn();
	PanelContainer windows;
	 PackedScene meny;


    public override void _Ready()
    {
		 chest = GD.Load<PackedScene>("res://scene/inventori/shest.tscn");
		 godmode = GD.Load<PackedScene>("res://scene/ui/setting/god_mode.tscn");
		 meny = GD.Load<PackedScene>("res://scene/ui/setting/speed_settings.tscn");
		 windows = GetNode<PanelContainer>("ui_pc_plaer");
		 value = GetNode<ProgressBar>("PanelContainer/Label5/xp");
		 if(value == null)
		  value = GetNode<ProgressBar>("xp");
		 text = GetNode<Label>("PanelContainer/Label5/money/vale");
		 if(text == null)
		   text = GetNode<Label>("vale");
		 player = GetParent<Player>();
		 if(player == null)
		   GD.Print("null player");
	

         GetWindow().MinSize = new Vector2I(480,280 );
        GetWindow().MaxSize = new Vector2I(1920,960);
        // Подключаемся к сигналу size_changed
		
        GetWindow().Connect("size_changed", new Callable(this, nameof(OnWindowSizeChanged)));

    }
    public void _on_xp()
	{
	  value.Value=Player.xp;
	}

	public void _on_vale(string money)
	{
		text.Text = money;

		if(Atack.damage != int.MaxValue)
		{
		    var i = (God)godmode.Instantiate();
			GetParent().AddChild(i);
	 		i.GlobalPosition = player.GlobalPosition;
		}	
	}
	private void _on_chest()
	{

      switch (chest_nomber)
	  {
		case 1 : 
		         chest_nomber--;
		         Openchest();
		break;		 
		default:
		        chest_nomber = 1;
	            _on_chest();
		break;
	  }
	}
    private void Openchest()
	{
		if (nomber_open_chest == 0)
		{
		var cheste = (shest)chest.Instantiate() as shest;
	    GetParent().AddChild(cheste);
	    cheste.GlobalPosition = player.GlobalPosition;
 
	    nomber_open_chest = 1;

		time_stop?.Invoke();

		return;
		}
	}


    private void OnWindowSizeChanged()
    {
        Vector2 newSize = GetWindow().Size;
    }

	private void _on_button()
	{
	  save?.Invoke();
	  if(save == null)
	  {
		GD.Print("null save ");
	  }

      player.SaveGamePlayer();

     foreach (enemy enemy in GetTree().GetNodesInGroup("enemy"))
     {
        if (IsInstanceValid(enemy))
        enemy.GetSaveData();
      }

	  spawn.savegame();

	  GD.Print(" save is truy . ");
	}
	private void _on_meny()
	{
		if (nomber_open_chest == 0)
		{
	    	time_stop?.Invoke();
			if(time_stop == null)
			{
				GD.Print("null time stop");
			}


            var i = (speed_settings)meny.Instantiate();
			  GetParent().AddChild(i);
			  i.GlobalPosition = new Vector2(player.GlobalPosition.X-200,player.GlobalPosition.Y-200);
			  nomber_open_chest = 1;
		}
	}
}