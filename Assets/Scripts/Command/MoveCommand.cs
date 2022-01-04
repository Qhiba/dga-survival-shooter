public class MoveCommand : Command
{
    private PlayerMovement playerMovement;
    private float h, v;
    
    public MoveCommand(PlayerMovement _playerMovement, float _h, float _v)
    {
        this.playerMovement = _playerMovement;
        this.h = _h;
        this.v = _v;
    }

    //Trigger perintah movement
    public override void Execute()
    {
        playerMovement.Move(h, v);

        //Menganimasi Player
        playerMovement.Animating(h, v);
    }

    public override void Unexecute()
    {
        //Invers arah dari movement player
        playerMovement.Move(-h, -v);

        //Menganimasi player
        playerMovement.Animating(h, v);
    }
}
