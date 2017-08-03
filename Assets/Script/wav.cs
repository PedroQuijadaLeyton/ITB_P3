//********************************************************************************************************************************************
// Class to pass an array of floats to a wav file
//
// Adapted from:
// https://bitbucket.org/Unity-Technologies/nativeaudioplugins/src/6150f1bb6e19d772bf1908a2f51dbcbbbbdccd64/Assets/scripts/AudioRecorder.cs?at=default&fileviewer=file-view-default
//

using System.IO;

public class wav
{

    public static void save(float[] data, string file = "__default__.wav", int sampleRate = 16000, int bits = 32, int channels = 1, int format = 3)
    {
        FileStream stream = new FileStream(file, FileMode.Create);
        BinaryWriter binwriter = new BinaryWriter(stream);
        for (int n = 0; n < 44; n++)
            binwriter.Write((byte)0);

        for (int i = 0; i < data.Length; i++)
        {
            binwriter.Write(data[i]);
        }
        var closewriter = binwriter;
        binwriter = null;
        int subformat = format; //This is format code for "IEEE Float" == 3
        int numchannels = channels; //MONO == 1
        int numbits = bits;//for IEEE float == 32
        int samplerate = sampleRate; // 16000;//SampleRate; //2*AudioSettings.outputSampleRate;
        long pos = closewriter.BaseStream.Length;
        closewriter.Seek(0, SeekOrigin.Begin);
        closewriter.Write((byte)'R'); closewriter.Write((byte)'I'); closewriter.Write((byte)'F'); closewriter.Write((byte)'F');
        closewriter.Write((uint)(pos - 8));
        closewriter.Write((byte)'W'); closewriter.Write((byte)'A'); closewriter.Write((byte)'V'); closewriter.Write((byte)'E');
        closewriter.Write((byte)'f'); closewriter.Write((byte)'m'); closewriter.Write((byte)'t'); closewriter.Write((byte)' ');
        closewriter.Write((uint)16);
        closewriter.Write((ushort)subformat);
        closewriter.Write((ushort)numchannels);
        closewriter.Write((uint)samplerate);
        closewriter.Write((uint)((samplerate * numchannels * numbits) / 8));
        closewriter.Write((ushort)((numchannels * numbits) / 8));
        closewriter.Write((ushort)numbits);
        closewriter.Write((byte)'d'); closewriter.Write((byte)'a'); closewriter.Write((byte)'t'); closewriter.Write((byte)'a');
        closewriter.Write((uint)(pos - 36));
        closewriter.Seek((int)pos, SeekOrigin.Begin);
        closewriter.Flush();
        stream.Close();

    }

}
