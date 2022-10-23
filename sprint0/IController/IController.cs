using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace sprint0
{
    public interface IController
    {
        void RegisterCommand(Keys key, ICommand command);
        void Update();
    }

    public class IKeyboard : IController
    {
        private Dictionary<Keys,ICommand> controllerMappings;
        public IKeyboard(){
            controllerMappings = new Dictionary<Keys, ICommand>();
        }

        public void RegisterCommand(Keys key, ICommand command){
            controllerMappings.Add(key,command); 
        }

        public void Update(){
            Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
		    foreach (Keys key in pressedKeys)
		    {
                try{
			    controllerMappings[key].Execute();
                }
                catch{
                    Console.WriteLine("Error Executing Command "+key);
                }
		    }     
        }
    }

    
}

