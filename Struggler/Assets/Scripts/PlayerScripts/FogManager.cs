using UnityEngine;
using UnityEngine.UI;
public class FogManager : MonoBehaviour
{

    private Image img;
    public Transform player;
    public Vector2 playerStartPosition = new Vector2(-12f,-3f);
    public ParticleSystem snowStorm;
    void Start(){
        img = GetComponent<Image>();
        img.color = new Color(1f,1f,1f,0);
    }

    void Update(){
        
        img.color = SetFogOpacityBasedOnPlayerX();

        var emissionModule = snowStorm.emission;
        emissionModule.rateOverTime = SetParticleSystemEmissionBasedOnPlayerX();

        var mainModule = snowStorm.main;
        mainModule.startSpeed = SetParticleSystemStartSpeedBasedOnPlayerX();
        mainModule.startLifetime = SetParticleSystemStartLifetimeBasedOnPlayerX();

    }

    private Color SetFogOpacityBasedOnPlayerX(){
        
        /*
        Racunica je sljedeca
        opacity koji mi se svidio je 49/255 a posto se u kodu to izrazava od 0 do 1
        znaci da je opacity u floatu otprilike 0.2f i to podijeljeno otprilike 200 x blokova
        koliki je level daje increment od 0.001 po bloku u x smjeru
        
        UPDATE, smanjia sam ga jer je prejak
        */
        float opacityValue = 0.0006f * (player.position.x - playerStartPosition.x);
        //kad player krene unatrag
        if(opacityValue < 0f){
            opacityValue = 0;
        }

        return new Color(1f,1f,1f,opacityValue);
    }

    private float SetParticleSystemEmissionBasedOnPlayerX(){
        /*
        Racunica je sljedeca
        emission podići za 700 na 195 blokova, increment od 3.56 po bloku
        
        */
       float emission = 1000f + 3.56f * (player.position.x - playerStartPosition.x);
        //kad player krene unatrag
        if(emission < 0f){
            emission = 0;
        }
       return emission;
    }

    private float SetParticleSystemStartSpeedBasedOnPlayerX(){
        /*
        Racunica je sljedeca
        start speed podižemo sa -6 na -9 to je -3 na 195 blokova, increment je -0.015
        
        */
       float startSpeed = -6f - (0.015f * (player.position.x - playerStartPosition.x));
       
       return startSpeed;
    }

    private float SetParticleSystemStartLifetimeBasedOnPlayerX(){
        /*
        Racunica je sljedeca
        sa 5.2 padamo na 3.5, to je 1.7 na 195 blokova
        
        */
       float startLifetime = 5.2f - (0.0087f * (player.position.x - playerStartPosition.x));
        //kad player krene unatrag
        if(startLifetime < 0f){
            startLifetime = 0;
        }
       return startLifetime;
    }


}
