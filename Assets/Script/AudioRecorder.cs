using System.Collections;
using UnityEngine;

public class AudioRecorder : MonoSingleton<AudioRecorder>
{ 

    //Recording main features:
    AudioClip _clip; // where the microphone records the input
    bool _clipLooping = false; // loop after reaching the end of the clip?
    int _sampleRate = 16000; //samples per second
    int _clipLength = 10; // in seconds

    //Recording location features:
    int _startRecOffset =0;
    int _endRecOffset = 0;

    //Constructor/public default parameters for a recording:
    public float recDelay = 0.5f; //delay before recording, at least 0.5 seconds. A delay allows mic to be fully open and allows to get accurate sample position.
    public float recDuration = 1f; // length of the recording in seconds
    public string filename = "example.wav"; //name of the file to store the recording. Wav file.

    //DSP time measurment:
    double micInit = 0;
    double micDSPstart = 0;
    double recDSPstart = 0;

    //file management
    string wavPath;

    protected AudioRecorder() { }

    // Use this for initialization
    void Start () {

        pathSettings();

	}

    //A simple utility to decide where to store the files:
    void pathSettings()
    {
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsEditor)
        {
            wavPath = Application.dataPath;
        }
        else // ( Application.platform == RuntimePlatform.Android )
        {
            wavPath = Application.persistentDataPath;
        }
    }

    // Update is called once per frame
    void Update () {
		
	}

    IEnumerator MicStart()
    {
        _clip = Microphone.Start(null, _clipLooping, _clipLength, _sampleRate);
        micInit = AudioSettings.dspTime;
        while ((Microphone.GetPosition(null) < 1)) { micDSPstart = AudioSettings.dspTime; }
        yield return null;
    }

    IEnumerator ScheduledRecording(float startDelay, float duration, string filename)
    {
        recDSPstart = AudioSettings.dspTime;
        yield return new WaitForSecondsRealtime(startDelay);
        if (Microphone.IsRecording(null))
        {
            _startRecOffset = Microphone.GetPosition(null);

        }
        else
        {
            Debug.Log("AudioRecorder: Microphone is not open!");
        }
        yield return new WaitForSecondsRealtime(duration);
        if (Microphone.IsRecording(null))
        {
            _endRecOffset = Microphone.GetPosition(null);

            var samples = new float[(int)(_clip.frequency * duration)];//i.e., duration in seconds
            _clip.GetData(samples, _startRecOffset);
            Microphone.End(null);

            wav.save(samples, filename);
        }
        else
        {
           Debug.Log("AudioRecorder: Microphone is already closed!");
        }
        yield return null;
    }

    //kinda of constructor
    public void StartRecorder(float delay=0.5f, float duration=1f, string filename="example.wav")
    {
        filename = wavPath + "/" + filename;// this is to be changed

        StartCoroutine(MicStart());
        StartCoroutine(ScheduledRecording(delay, duration, filename));
    }
}
