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

	public void SetContent(Speaker speaker, string dialogue, string passiveCheck = "" )
	{
		string result = "[color=";
		result += speaker.speakerColor.ToHtml();
		result += "]" + speaker.speakerName.ToUpper() + "[/color]";
		if(passiveCheck != "")
		{
			result += " [color=#636056]" + passiveCheck + "[/color]";
		}
		result += " — [indent]" + dialogue;
		_text.Text = result;
	}
}
