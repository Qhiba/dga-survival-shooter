public class ShootCommand : Command
{
    private PlayerShooting playerShooting;

    public ShootCommand(PlayerShooting _playerShooting)
    {
        playerShooting = _playerShooting;
    }

    public override void Execute()
    {
        //Player Menembak
        playerShooting.Shoot();
    }

    public override void Unexecute()
    {

    }
}
