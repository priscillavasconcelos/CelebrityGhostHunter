using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScreenController : MonoBehaviour
{
    ParticleSystem particle;
    public float particleQtd = 10;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (BlowController.MicLoudness > 0.85f)
        {
            BlowSmoke(2f);
        }
        else if (BlowController.MicLoudness > 0.5f)
        {
            BlowSmoke(4f);
        }
        else if (BlowController.MicLoudness > 0.2f)
        {
            BlowSmoke(8f);
        }
    }

    // Update is called once per frame
    public void AddSmoke()
    {
        if (particleQtd > 200)
        {
            GameManager.Instance.GameOver();
            return;
        }

        particleQtd = particleQtd * 2;

        var emission = particle.emission;
        emission.rateOverTime = particleQtd;
    }

    public void BlowSmoke(float blowPower)
    {
        particleQtd = particleQtd / blowPower;

        var emission = particle.emission;
        emission.rateOverTime = particleQtd;

        if (particleQtd <= 10)
        {
            gameObject.SetActive(false);
        }
    }
}
