#pragma strict

private var mainMenuWidth : int = 720;
private var mainMenuHeight : int = 80;
private var buttonRect = Rect(0, 0, 80, 30);
private var infoText = "Select EffectName Button";

var Type01 : Transform;
var Type02 : Transform;
var Type03 : Transform;
var Type04 : Transform;
var Type05 : Transform;
var Type06 : Transform;
var Type07 : Transform;

function Start(){
}

function Update(){
}

function OnGUI(){

	// MainMenu
	GUI.BeginGroup(Rect(Screen.width / 2 - mainMenuWidth / 2, 20, mainMenuWidth, mainMenuHeight));
	GUI.Box(Rect(0, 0, mainMenuWidth, mainMenuHeight), infoText);
	
	if(GUI.Button(Rect(20, 30, buttonRect.width, buttonRect.height), "Type01")){
		Instantiate(Type01, Vector3(0, 0, 0), Quaternion.identity);
	}
	if(GUI.Button(Rect(120, 30, buttonRect.width, buttonRect.height), "Type02")){
		Instantiate(Type02, Vector3(0, 0, 0), Quaternion.identity);
	}
	if(GUI.Button(Rect(220, 30, buttonRect.width, buttonRect.height), "Type03")){
		Instantiate(Type03, Vector3(0, 0, 0), Quaternion.identity);
	}
	if(GUI.Button(Rect(320, 30, buttonRect.width, buttonRect.height), "Type04")){
		Instantiate(Type04, Vector3(0, 0, 0), Quaternion.identity);
	}
	if(GUI.Button(Rect(420, 30, buttonRect.width, buttonRect.height), "Type05")){
		Instantiate(Type05, Vector3(0, 0, 0), Quaternion.identity);
	}
	if(GUI.Button(Rect(520, 30, buttonRect.width, buttonRect.height), "Type06")){
		Instantiate(Type06, Vector3(0, 0, 0), Quaternion.identity);
	}
	if(GUI.Button(Rect(620, 30, buttonRect.width, buttonRect.height), "Type07")){
		Instantiate(Type07, Vector3(0, 0, 0), Quaternion.identity);
	}
	GUI.EndGroup();
}
