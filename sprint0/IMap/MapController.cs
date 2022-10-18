using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;

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
    Rectangle[] rooms;
    Rectangle currentRoom;

    int loaded;//0 for false, 1 for true
    public MapController(Game1 game, Texture2D map, Rectangle screen){
        allMap = map;
        currentScreen = screen; 
        myGame = game;
        drawScreen = new SpriteBatch(game.GraphicsDevice);
        screenSize = new Rectangle(0,0,game.GraphicsDevice.PresentationParameters.BackBufferWidth,game.GraphicsDevice.PresentationParameters.BackBufferHeight);
        rooms = new Rectangle[17];
        rooms[0] = new Rectangle(256,880,255,175);
        rooms[1] = new Rectangle(512,880,255,175);
        rooms[2] = new Rectangle(767,880,255,175);
        rooms[3] = new Rectangle(512,705,255,175);
        rooms[4] = new Rectangle(256,530,255,175);
        rooms[5] = new Rectangle(512,530,255,175);
        rooms[6] = new Rectangle(767,530,255,175);
        rooms[7] = new Rectangle(512,880,255,175);
        rooms[8] = new Rectangle(512,880,255,175);
        rooms[9] = new Rectangle(512,880,255,175);
        rooms[10] = new Rectangle(512,880,255,175);
        currentRoom = rooms[0];



        loaded = 0;
    }
    
    public void LoadItemsPerRoom(){

        // var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        // {
        //     HasHeaderRecord = false
        // };

        // using var streamReader = File.OpenText("");
        // using var csvReader = new CsvReader(streamReader, csvConfig);
        // string value;

        // while (csvReader.Read())
        // {
        //     for (int i = 0; csvReader.TryGetField<string>(i, out value); i++)
        //     {
        //      Console.Write($"{value} ");
        //     }

        //     Console.WriteLine();
        // }

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

         

         

        ChangeRoom();//importcant code, updates room rectangle displayed
    }

    public void ChangeRoom(){
        for(int i = 0; i < rooms.Length; i++){
            if(rooms[i].X == roomX && rooms[i].Y == roomY){
                currentRoom = rooms[i];
                //myGame.blockSpace.Clear();
                //LoadItemsPerRoom();
            }
        }
    }

    public void Draw(){
        drawScreen.Begin();
        drawScreen.Draw(allMap,screenSize,currentRoom,Color.White);
        drawScreen.End();

    }
}

}