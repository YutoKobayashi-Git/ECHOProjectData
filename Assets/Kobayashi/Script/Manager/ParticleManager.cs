using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : SingletonMonoBehaviour<ParticleManager>
{
    [SerializeField]
    [Tooltip("発生させるエフェクト")]
    private GameObject particle1;

    [SerializeField]
    private GameObject particle2;

    [SerializeField]
    private GameObject particle3;

    [SerializeField]
    private GameObject particle4;

    [SerializeField]
    private GameObject particle5;

    [SerializeField]
    private GameObject particle6;

    [SerializeField]
    private GameObject particle7;

    [SerializeField]
    private GameObject particle8;

    [SerializeField]
    private GameObject particle9;

    enum Effect
    {
        _summonsParticle,
        _summonsSlashParticle,
        _summonsThrowParticle,
    }

    public GameObject _CreateParticle()
    {
        return Instantiate(particle1);
    }


    public void _summonsParticle(Vector3 pos , float deletetime)
    {
        GameObject particle = Instantiate(particle1, new Vector3(pos.x,pos.y,pos.z), Quaternion.Euler(90, 0, 0));

        StartCoroutine(_deleteParticle(particle, deletetime));
    }

    public void _summonsSlashParticle(Vector3 pos , float deletetime)
    {
        GameObject particle = Instantiate(particle2, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(90, 0, 0));

        StartCoroutine(_deleteParticle(particle, deletetime));
    }

    public void _summonsThrowParticle(Vector3 pos, float deletetime)
    {
        GameObject particle = Instantiate(particle3, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(180, 90, 0));

        StartCoroutine(_deleteParticle(particle, deletetime));
    }

    public void _gunFireParticle(Vector3 pos, float deletetime)
    {
        GameObject particle = Instantiate(particle4, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(180, 90, 90));

        StartCoroutine(_deleteParticle(particle, deletetime));
    }

    public void _sparkParticle(Vector3 pos, float deletetime)
    {
        GameObject particle = Instantiate(particle5, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(180, 90, 90));

        StartCoroutine(_deleteParticle(particle, deletetime));
    }

    public void _summonsNewSlashParticle(Vector3 pos, float deletetime)
    {
        GameObject particle = Instantiate(particle6, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(270, 180, 180));

        StartCoroutine(_deleteParticle(particle, deletetime));
    }


    public void _summonsSpecoalParticle(Vector3 pos, float deletetime, float scale)
    {
        GameObject particle = Instantiate(particle7, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(270, 180, 180));

        StartCoroutine(_deleteParticle(particle, deletetime));
    }

    public WeaponBaseClass _LazerParticle(Vector3 pos, float deletetime, Vector3 Angls)
    {
        GameObject particle = Instantiate(particle8, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(Angls));

        WeaponBaseClass weapon = particle.GetComponent<WeaponBaseClass>();

        StartCoroutine(_deleteParticle(particle, deletetime));

        return weapon;
    }

    public void _DeadPaticle(Vector3 pos, float deletetime)
    {
        GameObject particle = Instantiate(particle9, new Vector3(pos.x, pos.y, pos.z), Quaternion.Euler(270, 180, 180));

        StartCoroutine(_deleteParticle(particle, deletetime));
    }

    IEnumerator _deleteParticle(GameObject particle , float deletetime)
    {
        // Debug.Log(particle);

        yield return new WaitForSeconds(deletetime);

        Destroy(particle);
    }


}
