using UnityEngine;

//plays specific sounds 
public class PlayerSounds : MonoBehaviour
{
    public static PlayerSounds instance;
    [SerializeField] private AudioSource[] source = null;            //0 = jump, 1 = coin, 2 = powerup, 3 = squish enemy
    public enum SoundNames
    {
        Jump,
        Coin,
        Powerup,
        Squish,
        DestructionBlock,
        NonDestructionBlock,
        Death,
        FlagPass,
        PlayerHit,
        Powerup2,
    }
    public SoundNames nameOfSound;              //visual represntation of which sound to trigger for source
    private void Awake() => instance = this;

    //gets called to play selected sound
    public void PlaySound(SoundNames givenName)
    {
        source[(int)givenName].Play();
    }
        
}
