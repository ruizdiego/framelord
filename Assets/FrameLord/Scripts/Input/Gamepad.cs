// Mono Framework
using System.Collections.Generic;

// Unity Framework
using UnityEngine;

/* ------------------------------------------------------------------------------------------
  XBox Controller Mac:
	A: Button 16
	X: Button 18
	B: Button 17
	Y: Button 19
	RB: Button 14
	RT: Axis 6th
	LB: Button 13
	LT: Axis 5th
	Pad Up: Button 5
	Pad Down: Button 6
	Pad Left: Button 7
	Pad Right: Button 8
	Central Left: Button 10
	Central Right: Button 9
	Xbox Button: Button 15
	Left Stick Button: Button 11
	Right Stick Button: Button 12
	
	iOS Controller:
	A: Button 14
	X: Button 15
	B: Button 13
	Y: Button 12
	RB: Button 9
	RT: Button 11
	LB: Button 8
	LT: Button 10
	Pad Up: Button 4
	Pad Down: Button 6
	Pad Left: Button 7
	Pad Right: Button 5
	Central Left: -- 
	Central Right: -- 
	Xbox Button: Button --
	Left Stick Button: -- 
	Right Stick Button: -- 
	
	Android Controller:
	A: Button 0
	X: Button 2
	B: Button 1
	Y: Button 3
	RB: Button 5
	RT: Axis 8
	LB: Button 4
	LT: Axis 7
	Pad Up: Button 
	Pad Down: Button 
	Pad Left: Button 
	Pad Right: Button 
	Central Left: Button 8
	Central Right: Button 9
	Xbox Button: Button --
	Left Stick Button: Axis 1 (X) Axis 2 (Y) 
	Right Stick Button: Axis 3 (X) Axis 4 (Y)
------------------------------------------------------------------------------------------ */
namespace FrameLord
{
	public static class GamePad
	{
		#region GamePad Button defines

#if UNITY_EDITOR
		//  XBox Controller Mac:
		// Axis:
		// RT: Axis 6th
		// LT: Axis 5th

		// Buttons:
		/*public const int GamepadButtonA = 16;
		public const int GamepadButtonX = 18;
		public const int GamepadButtonB = 17;
		public const int GamepadButtonY = 19;
		public const int GamepadButtonRB = 14;
		public const int GamepadButtonLB = 13;
		public const int GamepadButtonRT = -1; // Axis in Mac
		public const int GamepadButtonLT = -1; // Axis in Mac
		public const int GamepadPadUp = 5;
		public const int GamepadPadDown = 6;
		public const int GamepadPadLeft = 7;
		public const int GamepadPadRight = 8;
		public const int GamepadCentralLeft = 10;
		public const int GamepadCentralRight = 9;
		public const int GamepadXboxButton = 15;
		public const int GamepadLeftStickButton = 11;
		public const int GamepadRightStickButton = 12;*/

		// PS4
		public const int GamepadButtonA = 1;
		public const int GamepadButtonX = 0;
		public const int GamepadButtonB = 2;
		public const int GamepadButtonY = 3;
		public const int GamepadButtonRB = 5;
		public const int GamepadButtonLB = 4;
		public const int GamepadButtonRT = 7;

		public const int GamepadButtonLT = 6;

		//public const int GamepadPadUp = 5;
		//public const int GamepadPadDown = 6;
		//public const int GamepadPadLeft = 7;
		//public const int GamepadPadRight = 8;
		public const int GamepadCentralLeft = 8;

		public const int GamepadCentralRight = 9;

		//public const int GamepadXboxButton = 15;
		public const int GamepadLeftStickButton = 10;
		public const int GamepadRightStickButton = 11;

#elif UNITY_IPHONE
		// iOS Controller:
		public const int GamepadButtonA = 14;
		public const int GamepadButtonX = 15;
		public const int GamepadButtonB = 13;
		public const int GamepadButtonY = 12;
		public const int GamepadButtonRB = 9;
		public const int GamepadButtonLB = 8;
		public const int GamepadButtonRT = 11;
		public const int GamepadButtonLT = 10;
		public const int GamepadPadUp = 4;
		public const int GamepadPadDown = 6;
		public const int GamepadPadLeft = 7;
		public const int GamepadPadRight = 5;
		public const int GamepadCentralLeft = -1; // This button is not supported on iOS
		public const int GamepadCentralRight = -1; // This button is not supported on iOS
		public const int GamepadXboxButton = -1; // This button is not supported on iOS
		public const int GamepadLeftStickButton = -1; // This button is not supported on iOS
		public const int GamepadRightStickButton = -1; // This button is not supported on iOS

#elif UNITY_ANDROID
		// Android Controller:
		// Axis:
		// RT: Axis 8th
		// LT: Axis 7th
		// Left Stick Button: Axis 1 (X) Axis 2 (Y) 
		// Right Stick Button: Axis 3 (X) Axis 4 (Y)
	
		public const int GamepadButtonA = 0;
		public const int GamepadButtonX = 2;
		public const int GamepadButtonB = 1;
		public const int GamepadButtonY = 3;
		public const int GamepadButtonRB = 5;
		public const int GamepadButtonLB = 4;
		public const int GamepadButtonRT = -1; // Axis on Android
		public const int GamepadButtonLT = -1; // Axis on Android
		public const int GamepadPadUp = -1; // Unknown
		public const int GamepadPadDown = -1; // Unknown
		public const int GamepadPadLeft = -1; // Unknown
		public const int GamepadPadRight = -1; // Unknown
		public const int GamepadCentralLeft = 8;
		public const int GamepadCentralRight = 9;
		public const int GamepadXboxButton = -1; // This button is not supported on iOS
		public const int GamepadLeftStickButton = -1; // Axis on Android
		public const int GamepadRightStickButton = -1; // Axis on Android
#endif

		#endregion
	}
}