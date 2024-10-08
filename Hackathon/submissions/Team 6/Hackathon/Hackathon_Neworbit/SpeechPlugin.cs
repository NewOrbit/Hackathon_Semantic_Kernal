using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace Hackathon_Neworbit
{
    public class SpeechPlugin
    {
        private readonly SpeechSynthesizer _speechSynthesizer;

        public SpeechPlugin()
        {
            var speechConfig = SpeechConfig.FromSubscription("e5a804c3a8c742b2b715916680756ca4", "uksouth");
            speechConfig.SetProperty(PropertyId.SpeechServiceResponse_PostProcessingOption, "TrueText");
            speechConfig.SpeechSynthesisVoiceName = "en-US-JennyNeural";
            var audioOutputConfig = AudioConfig.FromDefaultSpeakerOutput();
            
            _speechSynthesizer = new SpeechSynthesizer(speechConfig, audioOutputConfig);
        }

        [KernelFunction("ReadOutLoud")]
        [Description("Respond")]
        [return: Description("Read out loud")]
        public void Respond(string message)
        {
            // Check if we have a message to speak
            if (!string.IsNullOrWhiteSpace(message))
            {
                // Speak the SSML
                _speechSynthesizer.SpeakTextAsync(message);
            }
        }
    }
}
