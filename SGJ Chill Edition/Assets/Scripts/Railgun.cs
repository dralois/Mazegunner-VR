using UnityEngine;

public class Railgun : MonoBehaviour {
    private const int sound_ready = 0;
    private const int sound_shoot = 1;

    [SerializeField]
    private Turret myGun;
    [SerializeField]
    private bool shouldFire;

    Animator animator;
    AudioSync _audio;

    private bool _shooting = false;
    private bool _active = false;

    public bool isShooting {
		get {return _shooting;}
		set {
			animator.SetBool("shooting", value);
			_shooting = value;
		}
	}

	public bool isActive {
		get {return _active;}
		set {
			if(value && !_active){
                _audio.PlaySound(sound_ready);
			}
			animator.SetBool("activated", value);
			_active = value;
		}
	}

	void Start () {
		animator = GetComponent<Animator>();
		_audio = GetComponent<AudioSync>();
	}	

	public void FireLaser (string whichone) {
        if (shouldFire)
        {
            myGun.StartCoroutine("Shoot" + whichone);
            _audio.PlaySound(sound_shoot);
        }
	}
}
