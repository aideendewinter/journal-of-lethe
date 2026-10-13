using Godot;
using System;

[GlobalClass]
public partial class Speaker : Resource
{
	[Export]
	public String speakerName = "Missingno";
	[Export]
	public Color speakerColor = new Color(1,1,1);
	
}
