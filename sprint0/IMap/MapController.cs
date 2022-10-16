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
    int roomY = 876;
    public MapController(Game1 game, Texture2D map, Rectangle screen){
        allMap = map;
        currentScreen = screen; 
        myGame = game;
        drawScreen = new SpriteBatch(game.GraphicsDevice);
        screenSize = new Rectangle(0,0,game.GraphicsDevice.PresentationParameters.BackBufferWidth,game.GraphicsDevice.PresentationParameters.BackBufferHeight);
    }
    public void Update(Rectangle nextScreen){
        currentScreen = nextScreen;
    }

    public void Draw(){
        drawScreen.Begin();
        drawScreen.Draw(allMap,screenSize,new Rectangle(roomX,roomY,255,175),Color.White);
        drawScreen.End();

    }
}

}