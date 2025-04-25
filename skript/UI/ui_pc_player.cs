using Godot;


public partial class ui_pc_player :PanelContainer
{	
	public static event Action time_stop = delegate{} ;
	public static event Action save = delegate{};

	public static int nomber_open_chest = 0;
	private int chest_nomber=0;
	 public PackedScene chest {get;set;}
	[Export]Label text{get;set;}
	[Export]private ProgressBar value{get;set;}
	[Export]Player player;

	spawn spawn = new spawn();
	 PackedScene meny;


    public override void _Ready()
    {
		 chest = GD.Load<PackedScene>("res://scene/inventori/shest.tscn");
		
		 meny = GD.Load<PackedScene>("res://scene/ui/setting/speed_settings.tscn");

         GetWindow().MinSize = new Vector2I(480,280 );
        GetWindow().MaxSize = new Vector2I(1920,960);
        // Подключаемся к сигналу size_changed
		
        GetWindow().Connect("size_changed", new Callable(this, nameof(OnWindowSizeChanged)));

    }
    public void _on_xp(int healtch)
	{
	  value.Value= healtch;
	}

	public void _on_vale(string money)
	{
		text.Text = money;

		
#if DEBUG
		/*if(Atack.damage != int.MaxValue)
		{
		    var i = (God)godmode.Instantiate();
			GetParent().AddChild(i);
	 		i.GlobalPosition = player.GlobalPosition;
		}*/	
#endif		
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

public void saveplayer()
{
	if(SaveGame.SaveGame.save != null)
	{
		SaveGame.SaveGame.save.Clear();
	}
	    foreach (Player player in GetTree().GetNodesInGroup("Player"))
		{ 
		  GD.Print(1);	

			if (IsInstanceValid(player))
			{
				SaveGame.SaveGame.save = new Dictionary<string, float>();
				
			      SaveGame.SaveGame.save.TryAdd("player positon x",player.GlobalPosition.X);
				  SaveGame.SaveGame.save.TryAdd("player position y" , player.GlobalPosition.Y);
				  SaveGame.SaveGame.save.TryAdd("player health",player.xp);
				  SaveGame.SaveGame.save.TryAdd("player money ", player.money.ToFloat());
				
				 saveenemy();
			}
		}

}
private void saveenemy()
{
     foreach (enemy enemy in GetTree().GetNodesInGroup("enemy"))
     {
		GD.Print(3);
              if (IsInstanceValid(enemy))
		       {
				GD.Print(4);
				
				 SaveGame.SaveGame.save.TryAdd(enemy.EnemyId+"enemy X position", enemy.GlobalPosition.X);
				 SaveGame.SaveGame.save.TryAdd(enemy.EnemyId+"enemy Y position", enemy.GlobalPosition.Y);
				 SaveGame.SaveGame.save.TryAdd(enemy.EnemyId+"health enemy",enemy.Health); 
				
		       }
	 } 

			       save?.Invoke();
    
}

	private void _on_button()
	{
	 
	  if(save == null)
	  {
		GD.Print("null save ");
	  }

      
	   saveplayer();

	   SaveGame.SaveGame.Save_data_Game();


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

