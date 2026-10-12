using Godot;
using System;

public partial class DialogueEntry : MarginContainer
{

	[Export]
	private RichTextLabel _text;

	public override void _Ready()
	{
		//setContent("Vini", "some dialogue line", "[Medium: Success]");
	}

	public void setContent(string speaker, string dialogue, string passiveCheck = "" )
	{
		string result = "[color=#f00]" + speaker + "[/color]";
		if(passiveCheck != "")
		{
			result += " [color=#636056]" + passiveCheck + "[/color]";
		}
		result += " — " + dialogue;
		_text.Text = result;
	}
}
