public interface ILinkState
{
    void Standing();
    void Moving();
	void Attacking();
	void Update();
	// Draw() might also be included here
}

public class Link
{
	public ILinkState state;
	
	public Link()
	{
		state = new StandingLinkState(this);
	}
	
	public void Standing()
	{
		state.Standing();
	}

	public void Moving();
	{
		state.Moving();
	}

	public void Attacking();
	{
		state.Attacking();
	}

    public void Update();
    {
        state.Update();
    }

    // Draw and other methods omitted
}

public class LeftMovingLinkState : ILinkState
{
	private Link link;
	
	public LeftMovingLinkState(Link link)
	{
		this.link = link;
		// construct goomba's sprite here too
	}
	
	public void Standing()
	{
		
	}
	
	public void Moving()
	{
		link.state = new LeftMovingStompedLinkState(link);	
	}
	
	public void Attacking()
	{
		
	}
	
	public void Update()
	{
		// call something like goomba.MoveLeft() or goomba.Move(-x,0);
	}
}

public class LeftMovingStompedLinkState : ILinkState
{
	private Link link;
	
	public LeftMovingStompedLinkState(Link link)
	{
		this.link = link;
		// construct link's sprite here too
	}
	
	public void ChangeDirection()
	{
		
	}
	
	public void BeStomped()
	{
		// NO-OP
		// already stomped, do nothing
	}
	
	public void BeFlipped()
	{
		// NO-OP
		// if stomped, do not respond to being attacked by star mario (assumed but not tested behavior)
	}
	
	public void Update()
	{
		// call something like goomba.MoveLeft() or goomba.Move(-x,0);
		// link.MoveLeft();
	}
}

public class StandingLinkState : ILinkState
{

}