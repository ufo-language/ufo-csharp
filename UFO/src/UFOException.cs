using UFO.Types;

namespace UFO;

public class UFOException(string message, UFOObject payload) : Exception(message)
{
    public readonly UFOObject Payload = payload;
}
