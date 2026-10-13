using Godot;
using System;

public partial class DialogueDrawer : MarginContainer
{

	//const DialogueEntry = preload("res://scenes/dialoge_system/dialogue_entry.tscn"); //in GDSCRIPT shifted to C# becomes 
	//https://docs.godotengine.org/en/stable/tutorials/best_practices/logic_preferences.html
	public PackedScene dialogueEntryPrefab = ResourceLoader.Load<PackedScene>("res://scenes/dialoge_system/dialogue_entry.tscn");

	[Export]
	public Control dialogueEntriesContainer;
	public ScrollContainer scrollContainer;
	public ScrollBar scrollBar;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scrollContainer = (ScrollContainer) dialogueEntriesContainer.GetParentControl();
		scrollBar = scrollContainer.GetVScrollBar();
		scrollBar.Changed += OnScrollBarChanged;
		this.next();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void next()
	{
		DialogueEntry entry = dialogueEntryPrefab.Instantiate<DialogueEntry>();
		dialogueEntriesContainer.AddChild(entry);
		
		entry.setContent(new Speaker(), "this is some dialogue, blah blah blah we are going to go on for quite some time to make sure this text takes up multiple lines at this point.");		
	}

	//TODO why does this get called twice when we call next?
	public void OnScrollBarChanged()
	{
		//TODO do we want to make this a preference?
		int scrollValue = (int) scrollBar.MaxValue;
		if(scrollValue != scrollContainer.ScrollVertical)
		{
			scrollContainer.ScrollVertical = scrollValue;
		}
	}

	//TODO remove this test input driver?
	public override void _Input(InputEvent @event)	
	{
		if (@event.IsActionPressed("ui_accept"))
    	{
			GD.Print("test...");
			this.next();
		}
	}
}
