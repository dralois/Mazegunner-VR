using UnityEngine;

public class Railgun : MonoBehaviour {

    [SerializeField]
    private Turret myGun;
    [SerializeField]
    private bool shouldFire;

    public AudioClip railgun_activate;
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
			if(value && !_active &&  railgun_activate != null){
				//_audio.PlaySound
			}
			animator.SetBool("activated", value);
			_active = value;
		}
	}

	void Start () {
		animator = GetComponent<Animator>();
		_audio = GetComponent<AudioSource>();
	}	

	public void FireLaser (string whichone) {
        if (shouldFire)
        {
            myGun.StartCoroutine("Shoot" + whichone);
		    _audio.Play();
        }
	}
}
