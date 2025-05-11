using UFO.Types;

namespace UFO.Evaluator;

public class Evaluator
{
    private readonly Stack<UFOObject> OStack;

    public Evaluator()
    {
        OStack = new Stack<UFOObject>();
    }

    public void PushObj(UFOObject obj)
    {
        OStack.Push(obj);
    }

    public UFOObject PopObj()
    {
        return OStack.Pop();
    }
}
