using UnityEngine;

public class ExplosionEffect : PoolObject
{
    public override void OnSpawn()
    {
        GetComponent<ParticleSystem>().Play();
    }
    private void OnParticleSystemStopped()
    {
        OnDeSpawn();
    }

}
