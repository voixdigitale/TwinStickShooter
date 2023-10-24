using System.Runtime.InteropServices.WindowsRuntime;

public interface IEnemy : IDamageable {

    void Moving();
    void Shooting();
}