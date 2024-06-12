namespace OpenMatchDirector.Handlers;

public abstract class AbstractHandler
{
    protected AbstractHandler successor;
    
    public void SetSuccessor(AbstractHandler successor) 
        => this.successor = successor;
    
    public abstract void ProcessRequest();
}
