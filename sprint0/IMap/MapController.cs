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
using System.ComponentModel;

namespace sprint0
{

public class MapController{
    Texture2D allMap;
    Rectangle currentScreen;
    Game1 myGame;
    SpriteBatch drawScreen;
    Rectangle screenSize;
    int offset = 43;
    int roomX;
    int roomY;
    Rectangle[] rooms;
    Rectangle currentRoom;
    private Rectangle[] currentRoomDoors;
    private Rectangle[] bounds;
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
        screenSize = game._playerScreen;
        //screenSize = new Rectangle(0,0,game.GraphicsDevice.PresentationParameters.BackBufferWidth,game.GraphicsDevice.PresentationParameters.BackBufferHeight);
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
        bounds = new Rectangle[4];//the number of sides a room has
        bounds[0] = new Rectangle(screenSize.X,screenSize.Y+(((int)Math.Ceiling(offset*0.8))),screenSize.Width,offset);//top side
        bounds[1] = new Rectangle(screenSize.X+(offset),screenSize.Y,offset,screenSize.Height);//left side
        bounds[2] = new Rectangle(screenSize.X,screenSize.Y+screenSize.Height-(((int)Math.Ceiling(offset*1.7))),screenSize.Width,offset);//bottom side
        bounds[3] = new Rectangle(screenSize.X+screenSize.Width-(((int)Math.Ceiling(offset*1.7))),screenSize.Y,offset,screenSize.Height);//right side

        currentRoom = rooms[1];
        roomNum=1;
        roomX = currentRoom.X;
        roomY = currentRoom.Y;
        LoadBoundsPerRoom();
    }

    public Rectangle[] getRoomDoors(){
        return currentRoomDoors;
    }
     public Rectangle[] getRoomBounds(){
        return bounds;
    }

    public Rectangle[] getRooms(){
        return rooms;
    }
    public int getRoomNum(){
        return roomNum;
    }

     Rectangle[] createDoors(){
        Rectangle[] bounds = new Rectangle[4];//the number of sides a room has
    return bounds;
    }     
    
    public void DisplayItem(int[] item){
        Rectangle itemDetail = new Rectangle(screenSize.X+item[2], screenSize.Y+item[3], item[4], item[5]);
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
            IItem key=ItemFactory.Instance.CreateKey(itemDetail);
            key.ChangeAttribute(ItemAttribute.Pickable);
            myGame.outItemSpace.Add(key);
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
            case 30://Old Man
            myGame.nPCSpace.Add(NPCFactory.Instance.CreateOldMan(itemDetail));
            break;
            default:
            Console.WriteLine("Invalid item ID");
            break;
        }
    }
    public void LoadBoundsPerRoom(){//Maximum number of doors in a single room is 10
        Rectangle[] tempDoors = new Rectangle[10];
        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };
        using var streamReaderDoors = File.OpenText("Content/maps/Level1ZeldaDoors.csv");
        using var csvReaderDoors = new CsvReader(streamReaderDoors, csvConfig);
        string value;
        int[] item;
        int spotItem;
        int count = 0;
        if(csvReaderDoors.Read()){
        while (csvReaderDoors.Read())
        {
            item = new int[5];
            spotItem = 0;
            for (int i = 0; csvReaderDoors.TryGetField<string>(i, out value); i++)
            {
                    try{
                        item[spotItem] = Int32.Parse(value);
                        spotItem = spotItem+1;
                    }catch{
                        Console.WriteLine("Cannot parse integer from file");
                    }
            }
            if(spotItem == item.Length &&  item[0] == roomNum){
                    tempDoors[count] = new Rectangle(screenSize.X+item[1],screenSize.Y+item[2],item[3],item[4]);
                    count = count+1;
            }
        }
        }
        currentRoomDoors = new Rectangle[count+1];
        for(int i = 0; i <currentRoomDoors.Length; i++){
            currentRoomDoors[i] = tempDoors[i];
        }
        
        streamReaderDoors.Close();
    }
    public void LoadItemsPerRoom(){
        
        var csvConfig = new CsvConfiguration(CultureInfo.CurrentCulture)
        {
            HasHeaderRecord = false
        };

        using var streamReaderItems = File.OpenText("Content/maps/Level1ZeldaItems.csv");
        using var csvReaderItems = new CsvReader(streamReaderItems, csvConfig);

        string value;
        int[] item = new int[6];
        int spotItem;
        myGame.blockSpace.Clear();
        myGame.outItemSpace.Clear();
        myGame.enemySpace.Clear();
        myGame.nPCSpace.Clear();
        
        if(csvReaderItems.Read()){
        while (csvReaderItems.Read())
        {
            spotItem = 0;
        
            for (int i = 0; csvReaderItems.TryGetField<string>(i, out value); i++)
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
       
        streamReaderItems.Close();


    }


    public void ChangeRoom(){
        
        int oldX = roomX;
        int oldY = roomY;
        
        Rectangle tempPosition = myGame.character.GetPosition();
        if(myGame.character.GetPosition().Right>
         bounds[3].Left+1){//character moving right
            roomX = roomX + 256;
            tempPosition.X = bounds[1].Right+1;//right bound of left side
         }
         else if(myGame.character.GetPosition().Left < bounds[1].Right){//character moving left
            roomX = roomX - 256;
            tempPosition.X = bounds[3].Left-tempPosition.Width-1;//left bound of right side
         }else if(myGame.character.GetPosition().Bottom > bounds[2].Top+1){//character moving down
            roomY = roomY + 176;
            tempPosition.Y = bounds[0].Bottom-1;   
         }else if(myGame.character.GetPosition().Top < bounds[0].Bottom-1){//character moving up
            roomY = roomY - 176;
            tempPosition.Y = bounds[2].Top-tempPosition.Height-1;  
         }
        if(oldX != roomX || oldY != roomY || overrided){
        for(int i = 0; i < rooms.Length; i++){
            if(rooms[i].X == roomX && rooms[i].Y == roomY && changed == false){
                currentRoom = rooms[i];
                changed = true;
                roomNum = i;
                LoadContent();
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
        currentRoom = rooms[roomNum];
        roomX = currentRoom.X;
        roomY = currentRoom.Y;
        changed = true;
        LoadContent();
        // roomX = rooms[roomNum].X;
        // roomY = rooms[roomNum].Y;
        // overrided = !overrided;
        // ChangeRoom();
        // //currentRoom = rooms[roomNum];
        // overrided = !overrided;

    }
    public void PreviousRoom(){
        roomNum = (roomNum-1);
        if(roomNum <0){
            roomNum = rooms.Length-1;
        }
        currentRoom = rooms[roomNum];
        roomX = currentRoom.X;
        roomY = currentRoom.Y;
        changed = true;
        LoadContent();
        // roomX = rooms[roomNum].X;
        // roomY = rooms[roomNum].Y;
        // Console.WriteLine(currentRoom);
        // Console.WriteLine(roomNum);
        // overrided = !overrided;
        // ChangeRoom();
        // Console.WriteLine(currentRoom);

        // //currentRoom = rooms[roomNum];
        // overrided = !overrided;
    }

    void drawDoors(){
        for(int i = 0; i < currentRoomDoors.Length; i++){
            drawScreen.Draw(tempFill,currentRoomDoors[i],Color.Green);
        }
    }
    void drawBounds(){
        for(int i = 0; i < bounds.Length; i++){
            drawScreen.Draw(tempFill,bounds[i],Color.Red);
        }
    }

    // public void translate(Rectangle screen){

    //         for(int i = 0; i < currentRoomDoors.Length; i++){
    //         currentRoomDoors[i].X = screen.X+currentRoomDoors[i].X;
    //         currentRoomDoors[i].Y = screen.X+currentRoomDoors[i].Y;
    //         currentRoomDoors[i].Height = (int)Math.Ceiling(screenSize.Height*((double)screen.Height/(double)currentRoomDoors[i].Height));
    //         currentRoomDoors[i].Width = (int)Math.Ceiling(screenSize.Width*((double)screen.Width/(double)currentRoomDoors[i].Width));
    //     }
    //         for(int i = 0; i < bounds.Length; i++){
    //         bounds[i].X = screen.X+bounds[i].X;
    //         bounds[i].Y = screen.X+bounds[i].Y;
    //         bounds[i].Height = (int)Math.Ceiling(screenSize.Height*((double)screen.Height/(double)bounds[i].Height));
    //         bounds[i].Width = (int)Math.Ceiling(screen.Width*((double)screen.Width/(double)bounds[i].Width));
    //     }
    //     screenSize.X =  screen.X+screenSize.X;
    //     screenSize.Y = screen.Y+screenSize.Y;
       
    // }
    public void LoadContent(){
        LoadBoundsPerRoom();
        LoadItemsPerRoom();
    }
    public void Update(){
        ChangeRoom();
        
    }

    public void Draw(){
        drawScreen.Begin();
        drawScreen.Draw(allMap,screenSize,currentRoom,Color.White);
        //COMMENT/UNCOMMENT TO TOGGLE DOOR AND BOUND DRAWING
        if(myGame._testMode){
        drawBounds();
        drawDoors();
        }
        drawScreen.End();

    }
}

}