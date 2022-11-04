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
    int border = 43;
    int roomX = 256;
    int roomY = 880;
    Rectangle[] rooms;
    Rectangle currentRoom;
    Rectangle[] currentRoomBounds;
    Boolean changed;
    int roomNum;
    Boolean overrided;
    Texture2D tempFill;

    public MapController(Game1 game, Texture2D map, Rectangle screen){
        tempFill = new Texture2D(game.GraphicsDevice,1,1);
        tempFill.SetData<Color>(new Color[]{Color.White});
        changed = false;
        overrided=false;
        allMap = map;
        currentScreen = screen; 
        myGame = game;
        drawScreen = new SpriteBatch(game.GraphicsDevice);
        screenSize = new Rectangle(0,0,game.GraphicsDevice.PresentationParameters.BackBufferWidth,game.GraphicsDevice.PresentationParameters.BackBufferHeight);
        rooms = new Rectangle[17];
        //Temporarily hard coded to test first level, will eventually be delegated to csvfile.
        rooms[0] = new Rectangle(256,880,255,175);
        rooms[1] = new Rectangle(512,880,255,175);
        rooms[2] = new Rectangle(768,880,255,175);
        
        rooms[3] = new Rectangle(512,704,255,175);
        
        rooms[4] = new Rectangle(256,528,255,175);
        rooms[5] = new Rectangle(512,528,255,175);
        rooms[6] = new Rectangle(768,528,255,175);
        
        rooms[7] = new Rectangle(0,352,255,175);
        rooms[8] = new Rectangle(256,352,255,175);
        rooms[9] = new Rectangle(512,352,255,175);
        rooms[10] = new Rectangle(768,352,255,175);
        rooms[11] = new Rectangle(1024,352,255,175);

        rooms[12] = new Rectangle(512,176,255,175);
        rooms[13] = new Rectangle(1024,176,255,175);
        rooms[14] = new Rectangle(1280,176,255,175);

        rooms[15] = new Rectangle(256,0,255,175);
        rooms[16] = new Rectangle(512,0,255,175);



        currentRoom = rooms[0];
        currentRoomBounds = createBounds(rooms[0], border);
        roomNum=0;
    }
     Rectangle[] createBounds(Rectangle room, int offset){
        Rectangle[] bounds = new Rectangle[4];//the number of sides a room has
        bounds[0] = new Rectangle(screenSize.X,screenSize.Y+(offset),screenSize.Width,offset);//top side
        bounds[1] = new Rectangle(screenSize.X+(offset),screenSize.Y,offset,screenSize.Height);//left side
        bounds[2] = new Rectangle(screenSize.X,screenSize.Y+screenSize.Height-(offset*2),screenSize.Width,offset);//bottom side
        bounds[3] = new Rectangle(screenSize.X+screenSize.Width-(offset*2),screenSize.Y,offset,screenSize.Height);//right side
    return bounds;
    }     
    
    public void DisplayItem(int[] item){
        Rectangle itemDetail = new Rectangle(item[2], item[3], item[4], item[5]);
        switch(item[1]){
            case 0://SquareBlock
            myGame.blockSpace.Add(BlockFactory.Instance.CreateSquareBlock(itemDetail));
            break;
            case 1://Pushable block
            myGame.blockSpace.Add(BlockFactory.Instance.CreatePushAbleBlock(itemDetail));
            break;
            case 2://Fire
            myGame.blockSpace.Add(BlockFactory.Instance.CreateFire(itemDetail));
            break;
            case 3://Blue gap
            myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueGap(itemDetail));
            break;
            case 4://Stairs
            myGame.blockSpace.Add(BlockFactory.Instance.CreateStairs(itemDetail));
            break;
            case 5://White Brick
            myGame.blockSpace.Add(BlockFactory.Instance.CreateWhiteBrick(itemDetail));
            break;
            case 6://Ladder
            myGame.blockSpace.Add(BlockFactory.Instance.CreateLadder(itemDetail));
            break;
            case 7://Blue floor
            myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueFloor(itemDetail));
            break;
            case 8://Blue sand
            myGame.blockSpace.Add(BlockFactory.Instance.CreateBlueSand(itemDetail));
            break;
            case 9://Compass
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateCompass(itemDetail));
            break;
            case 10://Map
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateMap(itemDetail));
            break;
            case 11://Key
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateKey(itemDetail));
            break;
            case 12://HeartContainer
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateHeartContainer(itemDetail));           
            break;
            case 13://TriForcePiece
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateTriforcePiece(itemDetail)); 
            break;
            case 14://WoodenBoomerang
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateWoodenBoomerang(itemDetail)); 
            break;
            case 15://Bow
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateBow(itemDetail));
            break;
            case 16://Rupee
            myGame.outItemSpace.Add(ItemFactory.Instance.Createrupee(itemDetail));
            break;
            case 17://Arrow
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateArrow(itemDetail));
            break;
            case 18://Bomb
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateBomb(itemDetail));
            break;
            case 19://Fairy
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateFairy(itemDetail));
            break;
            case 20://Clock
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateClock(itemDetail));
            break;
            case 21://BlueCandle
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateBlueCandle(itemDetail));
            break;
            case 22://BluePotion
            myGame.outItemSpace.Add(ItemFactory.Instance.CreateBluePotion(itemDetail));
            break;
            case 23://Boss
            myGame.enemySpace.Add(EnemyFactory.Instance.CreateBoss(itemDetail));
            break;
            case 24://Bat
            myGame.enemySpace.Add(EnemyFactory.Instance.CreateBat(itemDetail));
            break;
            case 25://Skeleton
            myGame.enemySpace.Add(EnemyFactory.Instance.CreateSkeleton(itemDetail));
            break;
            case 26://Rope
            myGame.enemySpace.Add(EnemyFactory.Instance.CreateRope(itemDetail));
            break;
            case 27://Trap
            myGame.enemySpace.Add(EnemyFactory.Instance.CreateTrap(itemDetail));
            break;
            case 28://WallMaster
            myGame.enemySpace.Add(EnemyFactory.Instance.CreateWallMaster(itemDetail));
            break;
            case 29://Goriya Blue
            myGame.enemySpace.Add(EnemyFactory.Instance.CreateGoriyaBlue(itemDetail));
            break;
            default:
            Console.WriteLine("Invalid item ID");
            break;
        }
    }
    public void LoadItemsPerRoom(){
        
        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };

        using var streamReader = File.OpenText("Content/maps/Level1ZeldaItems.csv");
        using var csvReader = new CsvReader(streamReader, csvConfig);
        string value;
        int[] item = new int[6];
        int spotItem;
        myGame.blockSpace.Clear();
        myGame.outItemSpace.Clear();
        myGame.enemySpace.Clear();
        
        if(csvReader.Read()){
        while (csvReader.Read())
        {
            spotItem = 0;
        
            for (int i = 0; csvReader.TryGetField<string>(i, out value); i++)
            {
                    try{
                    item[spotItem] = Int32.Parse(value);
                    spotItem = spotItem+1;
                    }catch{
                        Console.WriteLine("Cannot parse integer from file");
                    }
            }
            if(spotItem == item.Length &&  item[0] == roomNum){
                DisplayItem(item);
            }
        }
        }
        streamReader.Close();


    }


    public void ChangeRoom(){
        
        int oldX = roomX;
        int oldY = roomY;
        
        Rectangle tempPosition = myGame.character.GetPosition();
        if(myGame.character.GetPosition().X + myGame.character.GetPosition().Width >
         myGame.GraphicsDevice.PresentationParameters.BackBufferWidth){//character right side
            roomX = roomX + 256;
            tempPosition.X = 1;
         }
         else if(myGame.character.GetPosition().X < 0){//character left side
            roomX = roomX - 256;
            tempPosition.X = (screenSize.Width - myGame.character.GetPosition().Width);
         }else if(myGame.character.GetPosition().Y > screenSize.Height- myGame.character.GetPosition().Height){//character down
            roomY = roomY + 176;
            tempPosition.Y = 1;   
         }else if(myGame.character.GetPosition().Y < 0){//character up
            roomY = roomY - 176;
            tempPosition.Y = (screenSize.Height - myGame.character.GetPosition().Height);  
         }
        if(oldX != roomX || oldY != roomY || overrided){
        for(int i = 0; i < rooms.Length; i++){
            if(rooms[i].X == roomX && rooms[i].Y == roomY && changed == false){
                currentRoom = rooms[i];
                currentRoomBounds = createBounds(rooms[i], border);
                changed = true;
                roomNum = i;
                LoadItemsPerRoom();
                break;
            }else{
                changed = false;
            }    
        }
        }
        
        if(!changed && (oldX != roomX || oldY != roomY)){
            roomX = oldX;
            roomY = oldY;

        }else{
            myGame.character.ChangePosition(tempPosition);

        }
        
 
    }
    public void NextRoom(){
        roomNum = (roomNum+1)%(rooms.Length);
        Console.WriteLine(roomNum);
        roomX = rooms[roomNum].X;
        roomY = rooms[roomNum].Y;
        overrided = !overrided;
        ChangeRoom();
        overrided = !overrided;

    }
    public void PreviousRoom(){
        roomNum = (roomNum-1);
        if(roomNum <0)roomNum = rooms.Length-1;
        roomX = rooms[roomNum].X;
        roomY = rooms[roomNum].Y;
        overrided = !overrided;
        ChangeRoom();
        overrided = !overrided;
    }

    void drawBounds(){
        for(int i = 0; i < currentRoomBounds.Length; i++){
            Console.WriteLine(currentRoomBounds[i].X);
            drawScreen.Draw(tempFill,currentRoomBounds[i],Color.Red);
        }
    }
    public void Update(){
        ChangeRoom();
    }



    public void Draw(){
        drawScreen.Begin();
        drawScreen.Draw(allMap,screenSize,currentRoom,Color.White);
        drawBounds();
        //drawScreen.Draw(tempFill,currentRoomBounds[3],Color.Red);
        drawScreen.End();

    }
}

}