public class Snowball : WeaponAmmo
{
    private void Awake()
    {
        _lifespan = 3f;
        _speed = 15f;
        _damage = 2f;
    }
}