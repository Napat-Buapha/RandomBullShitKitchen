
public interface IDropReceiver
{   
    /// <summary>
    /// if return true mean the card get received (may destroy the refference if need)
    /// </summary>
    public bool DropReceive(CardCardGame_Base card);
}
