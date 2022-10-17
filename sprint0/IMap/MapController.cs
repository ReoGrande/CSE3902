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
    Rectangle[] roomBlocks;
    int loaded;//0 for false, 1 for true
    public MapController(Game1 game, Texture2D map, Rectangle screen){
        allMap = map;
        currentScreen = screen; 
        myGame = game;
        drawScreen = new SpriteBatch(game.GraphicsDevice);
        screenSize = new Rectangle(0,0,game.GraphicsDevice.PresentationParameters.BackBufferWidth,game.GraphicsDevice.PresentationParameters.BackBufferHeight);
        roomBlocks = new Rectangle[1];
        roomBlocks[0] = new Rectangle(352, 190, 50, 40);
        loaded = 0;
    }
    
    public void LoadItemsPerRoom(){

        myGame.blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(new Rectangle(352, 190, 50, 40)));
        myGame.blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(new Rectangle(402, 190, 50, 40)));
        myGame.blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(new Rectangle(352, 233, 50, 40)));
        myGame.blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(new Rectangle(402, 233, 50, 40)));
        myGame.blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(new Rectangle(352, 275, 50, 40)));
        myGame.blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(new Rectangle(402, 275, 50, 40)));

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

         if(roomY == 700 && loaded == 0){
            LoadItemsPerRoom();
            loaded = 1;
         }else if (roomY!=700){
            loaded = 0;
            myGame.blockSpace.Clear();
         }

         

         
    }

    public void Draw(){
        drawScreen.Begin();
        drawScreen.Draw(allMap,screenSize,new Rectangle(roomX,roomY,255,175),Color.White);
        drawScreen.End();

    }
}

}