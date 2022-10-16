using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sprint0
{

public class MapLoader{
    Rectangle screen;
    Texture2D map;
    Game1 myGame;
    SpriteBatch tempDraw;

    public MapLoader(Game1 game, int level){
        string levelname = "maps/Level"+level+"Zelda";
        map = game.Content.Load<Texture2D>(levelname);
        myGame = game;
        tempDraw = new SpriteBatch(myGame.GraphicsDevice);
        screen = new Rectangle(0,0, myGame.GraphicsDevice.PresentationParameters.BackBufferWidth, myGame.GraphicsDevice.PresentationParameters.BackBufferHeight);
    }
    public Texture2D getMap(){
        return map;
    }
    public Rectangle getScreen(){
        return screen;
    }
    public void Draw(){
        tempDraw.Begin();
        tempDraw.Draw(map,screen,Color.White);
        tempDraw.End();
    }

}

}