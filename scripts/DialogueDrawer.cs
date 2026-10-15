using Godot;
using Godot.Collections;
using System;

public partial class DialogueDrawer : MarginContainer
{

	const String speakerResourcesFolder = "res://assets/speakers/";

	//const DialogueEntry = preload("res://scenes/dialoge_system/dialogue_entry.tscn"); //in GDSCRIPT shifted to C# becomes 
	//https://docs.godotengine.org/en/stable/tutorials/best_practices/logic_preferences.html
	public PackedScene dialogueEntryPrefab = ResourceLoader.Load<PackedScene>("res://scenes/dialoge_system/dialogue_entry.tscn");

	[Export]
	public Control dialogueEntriesContainer;
	public ScrollContainer scrollContainer;
	public ScrollBar scrollBar;
	public Dialogue dialogueObject;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		scrollContainer = (ScrollContainer) dialogueEntriesContainer.GetParentControl();
		scrollBar = scrollContainer.GetVScrollBar();
		scrollBar.Changed += OnScrollBarChanged;
		this.Start("test");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Start(String dialogueName)
	{
		dialogueObject = new Dialogue();
		dialogueObject.LoadDialogue(dialogueName);
		//initialize dialogue object
		//load dialogue based on name
		this.Next();

	}

	public void Next()
	{
		Dictionary content = dialogueObject.GetContent();

		switch(content["type"].ToString())
		{
			case Dialogue.CONTENT_TYPE_LINE:
				this.AddLine(content);
				break;
			case Dialogue.CONTENT_TYPE_OPTIONS:
				break;
			case Dialogue.CONTENT_TYPE_END:
				GD.Print("dialogue Ended");
				break;
		};
	}

	public void AddLine(Dictionary content)
	{
		DialogueEntry entry = dialogueEntryPrefab.Instantiate<DialogueEntry>();
		dialogueEntriesContainer.AddChild(entry);
		
		entry.SetContent(this.GetSpeakerResource(content["speaker"].ToString()	),content["text"].ToString());		
	}

	public Speaker GetSpeakerResource(String speakerName)
	{
		String speakerPath = speakerResourcesFolder + speakerName+".tres";
		if(ResourceLoader.Exists(speakerPath))
		{
			return (Speaker) ResourceLoader.Load(speakerPath);
		}
		return new Speaker();
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
			this.Next();
		}
	}
}
