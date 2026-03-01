using System;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using WebRtcVadSharp;


class Program
{
    static void Main()
    {
        var enumerator = new MMDeviceEnumerator();
        var vad = new WebRtcVad
        {
            OperatingMode = OperatingMode.Aggressive
        };
    

        var devices = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active);
        Console.WriteLine("Select Number available audio output devices:");
        if (devices.Count == 0)
        {
            Console.WriteLine("No audio output devices found.");
            return;
        }

        for (int i = 0; i < devices.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {devices[i].FriendlyName}");
        }
        int selectedDeviceIndex = 0;
        while (selectedDeviceIndex <= 0 || selectedDeviceIndex >= devices.Count)
        {
            Console.Write("Enter the number of the device you want to use: ");
            string? input = Console.ReadLine();

            if (input != null && int.TryParse(input, out int choise) && choise > 0 && choise <= devices.Count)
            {
                selectedDeviceIndex = choise - 1; 
                break;
            }
            else
            {
                Console.WriteLine("Invalid selection. Please enter a valid device number.");
            }
        }

        var selectedDevice = devices[selectedDeviceIndex];

        using var capture = new WasapiLoopbackCapture(selectedDevice);
        capture.DataAvailable += (s, e) =>
        {
            Console.WriteLine($"Captured {e.BytesRecorded} bytes of audio data.");
        };
    
        capture.StartRecording();
        Console.WriteLine("Press any key to stop recording...");
        Console.ReadKey();
        capture.StopRecording();
        vad.Dispose();
    }

    static IEnumerable<byte[]> SplitIntoFrames(byte[] buffer, int bytesRecorded, int sampleRate, int frameDurationMs)
    {
        int bytesPerSample = 2; 
        int samplesPerFrame = sampleRate * frameDurationMs / 1000;
        int bytesPerFrame = samplesPerFrame * bytesPerSample;

        for (int offset = 0; offset + bytesPerFrame <= bytesRecorded; offset += bytesPerFrame)
        {
            byte[] frame = new byte[bytesPerFrame];
            Array.Copy(buffer, offset, frame, 0, bytesPerFrame);
            yield return frame;
        }
    }
}
