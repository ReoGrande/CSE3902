using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0
{

public class MapController{
    Texture2D allMap;
    Rectangle currentScreen;
    Game1 myGame;
    SpriteBatch drawScreen;
    Rectangle screenSize;
    int roomX = 512;
    int roomY = 875;
    public MapController(Game1 game, Texture2D map, Rectangle screen){
        allMap = map;
        currentScreen = screen; 
        myGame = game;
        drawScreen = new SpriteBatch(game.GraphicsDevice);
        screenSize = new Rectangle(0,0,game.GraphicsDevice.PresentationParameters.BackBufferWidth,game.GraphicsDevice.PresentationParameters.BackBufferHeight);
    }
    public void Update(){
        Rectangle tempPosition = myGame.character.GetPosition();
        if(myGame.character.GetPosition().X + myGame.character.GetPosition().Width >
         myGame.GraphicsDevice.PresentationParameters.BackBufferWidth){//character right side
            roomX = roomX + 256;
            tempPosition.X = 1;
            myGame.character.ChangePosition(tempPosition);
         }
         else if(myGame.character.GetPosition().X < 0){//character left side
            roomX = roomX - 256;
            tempPosition.X = (screenSize.Width - myGame.character.GetPosition().Width);
            myGame.character.ChangePosition(tempPosition);
         }else if(myGame.character.GetPosition().Y > screenSize.Height- myGame.character.GetPosition().Height){//character down
            roomY = roomY + 175;
            tempPosition.Y = 1;
            myGame.character.ChangePosition(tempPosition);
         }else if(myGame.character.GetPosition().Y < 0){//character up
            roomY = roomY - 175;
            tempPosition.Y = (screenSize.Height - myGame.character.GetPosition().Height);
            myGame.character.ChangePosition(tempPosition);
         }

         
    }

    public void Draw(){
        drawScreen.Begin();
        drawScreen.Draw(allMap,screenSize,new Rectangle(roomX,roomY,255,175),Color.White);
        drawScreen.End();

    }
}

}