// Dialogue.cs Csharp wrapper for ClydeDialogue. 
// If we want to use a different dialogue system later we can swap that out 
// here without effecting everything else too much
// Reference: https://docs.godotengine.org/en/stable/tutorials/scripting/cross_language_scripting.html
using Godot;
using System;
using Godot.Collections;

//do we want to make our own dialogue or use clyde from the tutorial i do not know if it will blend with csharp

[GlobalClass]
public partial class Dialogue : GodotObject
{
    GDScript ClydeDialogue = GD.Load<GDScript>("res://addons/clyde/ClydeDialogue.gd");
    GodotObject ClydeDialogueObject; // This is a GodotObject.
    public const String CONTENT_TYPE_LINE = "line";
    public const String CONTENT_TYPE_OPTIONS = "options";
    public const String CONTENT_TYPE_END = "end";

    public Dialogue()
    {
        ClydeDialogueObject = (GodotObject)ClydeDialogue.New();
        ClydeDialogueObject.Set("dialogue_folder", "assets/dialogues");
    }

    public void LoadDialogue(String fileName, String block = "")
    {
        ClydeDialogueObject.Call("load_dialogue", fileName, block);
    }

    public Dictionary GetContent()
    {
        return (Dictionary) ClydeDialogueObject.Call("get_content");
    }
}
