// FX Quest
// Version: 0.4.1
// Author: Gold Experience Team (http://www.ge-team.com/pages)
// Support: geteamdev@gmail.com
// Please direct any bugs/comments/suggestions to geteamdev@gmail.com

#region Namespaces

	using UnityEngine;
	using System.Collections;

#endregion //Namespaces

// ######################################################################
// This class is Singleton pattern class.
// It contains AudioClips to play background music and sound of buttons
// Note this is just sample class to work with sample script of  UI Pack: Toony. So it has limiation features.
// ######################################################################

public class FXQ_SoundController : MonoBehaviour
{
	#region Variables

		// Private reference which can be accessed by this class only
		private static FXQ_SoundController instance;

		// Public static reference that can be accesd from anywhere
		public static FXQ_SoundController Instance
		{
			get
			{
				// Check if instance has not been set yet and set it it is not set already
				// This takes place only on the first time usage of this reference
				if(instance==null)
				{
					instance = GameObject.FindObjectOfType<FXQ_SoundController>();
					DontDestroyOnLoad(instance.gameObject);
				}
				return instance;
			}
		}

		// Max number of AudioSource components
		public int m_MaxAudioSource = 3;

		// AudioClip component for buttons
		public AudioClip m_ButtonBack = null;
		public AudioClip m_ButtonClick = null;
		public AudioClip m_ButtonPress = null;

		// Sound volume
		public float m_SoundVolume = 1.0f;

	#endregion //Variables
		
	// ######################################################################
	// MonoBehaviour functions
	// ######################################################################

	#region MonoBehaviour functions

		// Awake is called when the script instance is being loaded.
		void Awake()
		{
			if(instance == null)
			{
				// Make the current instance as the singleton
				instance = this;

				// Make it persistent  
				DontDestroyOnLoad(this);
			}
			else
			{
				// If more than one singleton exists in the scene find the existing reference from the scene and destroy it
				if(this != instance)
				{
					InitAudioListener();
					Destroy(this.gameObject);
				}
			}
		}

		// Use this for initialization
		void Start ()
		{
			// Initial AudioListener
			InitAudioListener();
		}

		// Update is called once per frame
		void Update ()
		{
	
		}

	#endregion //MonoBehaviour functions

	// ######################################################################
	// Utilitie functions
	// ######################################################################

	#region Functions
	
		// Initial AudioListener
		// This function remove all AudioListener in other objects then it adds new one this object.
		void InitAudioListener()
		{
			// Destroy other's AudioListener components
			AudioListener[] pAudioListenerToDestroy = GameObject.FindObjectsOfType<AudioListener>();
			foreach(AudioListener child in pAudioListenerToDestroy)
			{ 
				if(child.gameObject.GetComponent<FXQ_SoundController>()==null)
				{
					Destroy(child);
				}
			}

			// Adds new AudioListener to this object
			AudioListener pAudioListener = gameObject.GetComponent<AudioListener>();
			if(pAudioListener==null)
			{ 
				pAudioListener = gameObject.AddComponent<AudioListener>();
			}
		}

		// Play music
		void PlayMusic(AudioClip pAudioClip)
		{
			// Return if the given AudioClip is null
			if(pAudioClip==null)
				return;
						
			AudioListener pAudioListener = GameObject.FindObjectOfType<AudioListener>();
			if(pAudioListener!=null)
			{
				// Look for an AudioListener component that is not playing background music or sounds.
				bool IsPlaySuccess = false;
				AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
				if(pAudioSourceList.Length>0)
				{
					for(int i=0;i<pAudioSourceList.Length;i++)
					{
						// Play music
						if(pAudioSourceList[i].isPlaying == false)
						{
							pAudioSourceList[i].loop = true;
							pAudioSourceList[i].clip = pAudioClip;
							pAudioSourceList[i].ignoreListenerVolume = true;
							pAudioSourceList[i].playOnAwake = false;
							pAudioSourceList[i].Play();
							break;
						}
					}
				}
			
				// If there is not enough AudioListener to play AudioClip then add new one and play it
				if(IsPlaySuccess==false && pAudioSourceList.Length<16)
				{
					AudioSource pAudioSource = pAudioListener.gameObject.AddComponent<AudioSource>();
					pAudioSource.rolloffMode = AudioRolloffMode.Linear;
					pAudioSource.loop = true;
					pAudioSource.clip = pAudioClip;
					pAudioSource.ignoreListenerVolume = true;
					pAudioSource.playOnAwake = false;
					pAudioSource.Play();
				}
			}
		}

		// Play sound one shot
		void PlaySoundOneShot(AudioClip pAudioClip)
		{
			
			// Return if the given AudioClip is null
			if(pAudioClip==null)
				return;
			
			// We wait for a while after scene loaded
			if(Time.timeSinceLevelLoad<1.5f)
				return;
			
			// Look for an AudioListener component that is not playing background music or sounds.
			AudioListener pAudioListener = GameObject.FindObjectOfType<AudioListener>();
			if(pAudioListener!=null)
			{
				bool IsPlaySuccess = false;
				AudioSource[] pAudioSourceList = pAudioListener.gameObject.GetComponents<AudioSource>();
				if(pAudioSourceList.Length>0)
				{
					for(int i=0;i<pAudioSourceList.Length;i++)
					{
						if(pAudioSourceList[i].isPlaying == false)
						{
							// Play sound
							pAudioSourceList[i].PlayOneShot(pAudioClip);
							break;
						}
					}
				}
			
				// If there is not enough AudioListener to play AudioClip then add new one and play it
				if(IsPlaySuccess==false && pAudioSourceList.Length<16)
				{
					// Play sound
					AudioSource pAudioSource = pAudioListener.gameObject.AddComponent<AudioSource>();
					pAudioSource.rolloffMode = AudioRolloffMode.Linear;
					pAudioSource.playOnAwake = false;
					pAudioSource.PlayOneShot(pAudioClip);
				}
			}
		}
	
		// Set sound volume between 0.0 to 1.0
		public void SetSoundVolume(float volume)
		{
			m_SoundVolume = volume;
			AudioListener.volume = volume;
		}	
	
		// Play Back button sound
		public void Play_SoundBack()
		{
			PlaySoundOneShot(m_ButtonBack);
		}
	
		// Play Click sound
		public void Play_SoundClick()
		{
			PlaySoundOneShot(m_ButtonClick);
		}

		// Play SoundPress button sound
		public void Play_SoundPress()
		{
			PlaySoundOneShot(m_ButtonPress);
		}

	#endregion //Functions
}
