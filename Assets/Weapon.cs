using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    //TODO GUNSTATS here

    public virtual void Fire()
    {
        
    }


    [SerializeField] protected float _duration = 0.4f;
    private float _time;
    protected virtual bool CanFire()
    {

        _time += Time.deltaTime;

        if (_time >= _duration)
        {
            _time = 0;
            return true;
        }

        return false;
    }
    
    
}
