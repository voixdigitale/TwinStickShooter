using System.Runtime.InteropServices.WindowsRuntime;

public interface IEnemy : IDamageable {

    void Movement();
    void Shooting();
}