using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace sprint0{
public interface ILinkState
{
	void ChangeFrame();
    void ToStanding();
    void ToMoving(int direction);
	void ToAttacking();
	void Update();
	// Draw() might also be included here
}


public class Link
{
	public ILinkState state;
	public Rectangle position;
	public SpriteBatch sprites;
	public Texture2D texture;
	public int timer;

	public Rectangle currentFrame;

	public int speed;
	
	public Link(Game1 game)
	{
		state = new StandingLinkState(this);

		position = new Rectangle(350,150,150,150);
		sprites = new SpriteBatch(game.GraphicsDevice);
		texture = game.Content.Load<Texture2D>("Zelda_Sheet");
		this.currentFrame = new Rectangle(1, 11, 15, 15);
		timer = 1;
		speed = 20;
	}
	
	public void ToStanding()
	{
		state.ToStanding();
	}

	public void ToMoving(int direction)
	{
		state.ToMoving(direction);
	}

	public void ToAttacking()
	{
		state.ToAttacking();
	}

	public void Update()
	 {
	 	state.Update();
	}

	public void Draw(){
            sprites.Begin();
            sprites.Draw(this.texture,this.position,this.currentFrame,Color.White);
            sprites.End();
	}

			// Draw and other methods omitted
}

public class StandingLinkState : ILinkState
{
	private Link link;
	
	public StandingLinkState(Link link)
	{
		this.link = link;
		// construct link's sprite here too
	}
	public void ChangeFrame(){
		//purposely empty
	}
	public void ToStanding()
	{
		//purposely empty
	}
	
	public void ToMoving(int direction)
	{
		//link.state = new LeftMovingStompedLinkState(link);

		switch(direction){
                case 0://down
					link.state = new DownMovingLinkState(link);
                break;
                case 1://left
					//link.state = new LeftMovingLinkState(link);
                break;
                case 2://right
					//link.state = new RightMovingLinkState(link);
                break;
                case 3://up
					link.state = new UpMovingLinkState(link);
                break;
                default:
                    Console.WriteLine("Error: Incorrect command to change Link State.");
                return;
            }	
	}
	
	public void ToAttacking()
	{
		
	}
	
	public void Update()
	{
		// call something like goomba.MoveLeft() or goomba.Move(-x,0);
		//purposely empty
	}
}

public class DownMovingLinkState : ILinkState
{
	private Link link;
	private Rectangle[] nextFrame;
	private int frame;

	
	public DownMovingLinkState(Link link)
	{
		nextFrame = new Rectangle[2];
		nextFrame[0] = new Rectangle(1, 11, 15, 15);//Stand Frame 1
        nextFrame[1] = new Rectangle(18, 11, 15, 15);//Stand Frame 2
		this.link = link;
		frame = 0;
		// construct link's sprite here too
	}
	public void ChangeFrame(){
		if(link.timer % 10 == 0){
		if(frame >= nextFrame.Length-1){
			frame = 0;
		}else{
			frame = frame + 1;
		}
		link.currentFrame = nextFrame[frame];
		link.timer = 1;
		link.position.Y = link.position.Y + link.speed;
		}else{
			link.timer = link.timer + 1;
		}
		
	}
	public void ToStanding()
	{
		
	}
	
	public void ToMoving(int direction)
	{
				switch(direction){
                case 1://left
					//link.state = new LeftMovingLinkState(link);
                break;
                case 2://right
					//link.state = new RightMovingLinkState(link);
                break;
                case 3://up
					link.state = new UpMovingLinkState(link);
                break;
                default:
                    Console.WriteLine("Error: Incorrect command to change Link State.");
                return;
            }
	}
	
	public void ToAttacking()
	{
		
	}
	
	public void Update()
	{
		this.ChangeFrame();
		// call something like goomba.MoveLeft() or goomba.Move(-x,0);
		// link.MoveLeft();
	}
}
public class UpMovingLinkState : ILinkState
{
	private Link link;
	
	public UpMovingLinkState(Link link)
	{
		this.link = link;
		// construct link's sprite here too
	}
	
	public void ChangeFrame(){
		//TODO:STUFF
	}

	public void ToStanding()
	{
		
	}
	
	public void ToMoving(int direction)
	{
				switch(direction){
                case 0://down
					link.state = new DownMovingLinkState(link);
                break;
                case 1://left
					//link.state = new LeftMovingLinkState(link);
                break;
                case 2://right
					//link.state = new RightMovingLinkState(link);
                break;
                default:
                    Console.WriteLine("Error: Incorrect command to change Link State.");
                return;
            }
	}
	
	public void ToAttacking()
	{
		
	}
	
	public void Update()
	{
		// call something like goomba.MoveLeft() or goomba.Move(-x,0);
		// link.MoveLeft();
	}
}

public class AttackingLinkState : ILinkState
{
    private Link link;

    public AttackingLinkState(Link link)
    {
        this.link = link;
        // construct link's sprite here too
    }

    public void ToStanding()
    {

    }
	public void ChangeFrame(){
		//TODO:STUFF
	}
    public void ToMoving(int direction)
    {
        //link.state = new LeftMovingStompedLinkState(link);
    }

    public void ToAttacking()
    {

    }

    public void Update()
    {
        // call something like goomba.MoveLeft() or goomba.Move(-x,0);
    }
}
}