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

	public void Moving()
	{
		state.Moving();
	}

	public void Attacking()
	{
		state.Attacking();
	}

	public void Update()
	{
		state.Update();
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

public class MovingLinkState : ILinkState
{
	private Link link;
	
	public MovingLinkState(Link link)
	{
		this.link = link;
		// construct link's sprite here too
	}
	
	public void Standing()
	{
		
	}
	
	public void Moving()
	{
		
	}
	
	public void Attacking()
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