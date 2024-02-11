using UnityEngine.SceneManagement;

public class PlayerDamagable : Damagable
{
    protected override void Death()
    {
        base.Death();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}