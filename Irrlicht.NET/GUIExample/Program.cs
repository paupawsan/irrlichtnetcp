using System;
using System.Text;
using IrrlichtNETCP;

namespace GUIExample
{
    class Program
    {
    	//Our objects such as video driver or GUI environment
    	//We don't need any scene manager in this example
        static VideoDriver driver;
        static GUIEnvironment guienv;
		

		static  void testGUI(GUIEnvironment gui)
        {
            GUIListBox lstResolution = null;
            lstResolution = gui.AddListBox(new Rect(10,10,110,220), null, -1, false);
            lstResolution.AddItem("Dummy1");
            lstResolution.ToolTipText = "Nothing important!";
            lstResolution.AddItem("Dummy2");
            lstResolution.AddItem("Dummy3");
            lstResolution.AddItem("Dummy4");

            //value is "" always, same with tooltip when evaluated
            string value = lstResolution.GetListItem(2);
			
			Console.WriteLine(value);

           
        } 
		
        static void Main(string[] args)
        {
        	//We choosed OpenGL because it is cross-platform and GUI does not need
        	//Amazing performances...
            IrrlichtDevice device = new IrrlichtDevice(DriverType.OpenGL,
                                                    new Dimension2D(640, 480),
                                                    16, false, false, false, false);
            //Delegate that will catch our events
            device.OnEvent += new OnEventDelegate(device_OnEvent);
            
            //Does not seem to work on Linux but well...
            device.Resizeable = false;

			//We set a basic caption
            device.WindowCaption = "Irrlicht .NET CP General User Interface Example";
			
			//We set our handlers
            driver = device.VideoDriver;
            guienv = device.GUIEnvironment;
			
			//We set the skin as a metallic windows skin
            guienv.Skin = guienv.CreateSkin(GUISkinTypes.WindowsMetallic);

			//Our fader. We set it as first because we don't want him to hide our buttons.
            guienv.AddInOutFader(null, (int)GUIItems.Fader);
            //We add several buttons with the IDs we defined down
            guienv.AddButton(new Rect(new Position2D(250, 0), new Dimension2D(140, 100)),
                             null, (int)GUIItems.ClickMe, "Click me !");
            guienv.AddButton(new Rect(new Position2D(250, 380), new Dimension2D(140, 100)),
                             null, (int)GUIItems.DontClickMe, "Don't click me !");
            guienv.AddButton(new Rect(new Position2D(0, 190), new Dimension2D(140, 100)),
                             null, (int)GUIItems.FadeMe, "Fade Me !");
            guienv.AddButton(new Rect(new Position2D(500, 190), new Dimension2D(140, 100)),
                             null, (int)GUIItems.FunnyEffect, "Funny Effect !");
            //Our logo
            guienv.AddImage(driver.GetTexture("../../medias/NETCPlogo.png"), new Position2D(0, 0), true, null, -1, "");
			
			//And now the scrollbar
            GUIScrollBar scroll = guienv.AddScrollBar(true, new Rect(new Position2D(200, 220), new Dimension2D(240, 40)), null,
                                (int)GUIItems.ScrollBar);

			testGUI(guienv);
			
			//Var for the funny effects
            int toAdd = 1;
            //While the device is running and we don't want him to exit
            while (device.Run() && !Exit)
            {
            	//If our funny effect is enabled, we just scroll the scrollbar
            	//It may seems strange but the "ScrollBarChanged" event is NOT fired
            	//When we manually change the position... It's a gift and a cursed, depending on the situation.
                if (FunnyEffectEnabled)
                {
                    scroll.Pos += toAdd;
                    if(scroll.Pos >= 100 || scroll.Pos <= 0)
                        toAdd = -toAdd;
                    BackColor = Color.From(255, scroll.Pos * 5 / 2, 255 - (scroll.Pos * 5 / 2), scroll.Pos * 5 / 2);                            
                }
                //Here we are, we begin the scene
                //Notice that we DO NOT use the ZBuffer because we have only two dimensions
                driver.BeginScene(true, false, BackColor);
                //We draw all the GUI
                guienv.DrawAll();
                //And we end
                driver.EndScene();
            }
            //As in the first example, we need to release main resources
            device.Close();
        }
        //Our main vars such as the back color or the one which says if we want the "funny effect"
        static Color BackColor = Color.Blue;
        static bool FunnyEffectEnabled = false;
        static bool Exit = false;

		//Fired when an event occurs
        static bool device_OnEvent(Event ev)
        {
        	//If the event is a GUI one
            if (ev.Type == EventType.GUIEvent)
            {
                switch (ev.GUIEvent)
                {
                	//Has a button been clicked ?
                    case GUIEventType.ButtonClicked:
                        if (ev.Caller.ID == (int)GUIItems.DontClickMe)
                        {
                        	//We just add a stupid message box... with a stupid text
						
							guienv.AddMessageBox("Why have you clicked ?", "I told you not to do this !\nI need now to hack your computer !\nYahahahahahahahaha !\nDo you agree ?",
                                                 true, MessageBoxFlag.Yes | MessageBoxFlag.No, null, (int)GUIItems.StupidMessageBox);
                            return true;
                        }
                        else if (ev.Caller.ID == (int)GUIItems.FadeMe)
                        {
                        	//We get the fader and fade the back screen.
                            GUIInOutFader fader = (GUIInOutFader)guienv.RootElement.GetElementFromID((int)GUIItems.Fader, true);
                            fader.FadeIn(4000);
                        }
                        else if (ev.Caller.ID == (int)GUIItems.FunnyEffect)
                        {
                        	//We enable/disable the funny effect
                            FunnyEffectEnabled = !FunnyEffectEnabled;
                        }
                        else if (ev.Caller.ID == (int)GUIItems.ClickMe)
                        {
                        	//We just write a text on the console
                            Console.WriteLine("Clicked on Click Me !");
                        }                                  
                        return false;

                    case GUIEventType.MessageBoxYes:
                        if (ev.Caller.ID == (int)GUIItems.StupidMessageBox)
                        {
                        	//Again a stupid text
                            guienv.AddMessageBox("Good choice !", "I see you are reasonable !\nNow I will proceed !",
                                                 true, MessageBoxFlag.OK, null, -1);
                            return true;
                        }
                        return false;

                    case GUIEventType.MessageBoxNo:
                        if (ev.Caller.ID == (int)GUIItems.StupidMessageBox)
                        {
                        	//Another pointless text
                            guienv.AddMessageBox("Doesn't matter", "Why would a great hacker need your agreement ?!",
                                                 true, MessageBoxFlag.No, null, -1);
                            return true;
                        }
                        return false;

					//Our scrollbar has been scrolled by the user
                    case GUIEventType.ScrollBarChanged:
                        if (ev.Caller.ID == (int)GUIItems.ScrollBar)
                        {
                        	//We define anew BackColor
                            GUIScrollBar caller = ev.Caller as GUIScrollBar;
                            BackColor = Color.From(255, caller.Pos * 5 / 2, 255 - (caller.Pos * 5 / 2), caller.Pos * 5 / 2);
                            return true;
                        }
                        return false;

                    default:
                        return false;
                }
            }
            //The event wasn't handled
            return false;
        }
		
    } 

    //Our item list... Our program will be prettier than if we had created
    //hundredth of pointless integers
    public enum GUIItems
    {
        ClickMe,
        ScrollBar,
        DontClickMe,
        StupidMessageBox,
        FadeMe,
        Fader,
        FunnyEffect
    }
}
