#region License

/* Copyright (c) 2005 Leslie Sanford
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software. 
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
 * THE SOFTWARE.
 */

#endregion

#region Contact

/*
 * Leslie Sanford
 * Email: jabberdabber@hotmail.com
 */

#endregion

namespace AbrahmanAdventure.audio.midi
{
    /// <summary>
    /// Defines constants representing the General MIDI instrument set.
    /// </summary>
    public enum GeneralMidiInstrument
    {
        /// <summary>
        /// Acoustic grand piano
        /// </summary>
        AcousticGrandPiano,
        /// <summary>
        /// Bright acoustic piano
        /// </summary>
        BrightAcousticPiano,
        /// <summary>
        /// Electric grand piano
        /// </summary>
        ElectricGrandPiano,
        /// <summary>
        /// Honky tonk piano
        /// </summary>
        HonkyTonkPiano,
        /// <summary>
        /// Electric piano 1
        /// </summary>
        ElectricPiano1,
        /// <summary>
        /// Electric piano 2
        /// </summary>
        ElectricPiano2,
        /// <summary>
        /// Harpsichord
        /// </summary>
        Harpsichord,
        /// <summary>
        /// Clavinet
        /// </summary>
        Clavinet,
        /// <summary>
        /// Celesta
        /// </summary>
        Celesta,
        /// <summary>
        /// Glockenspiel
        /// </summary>
        Glockenspiel,
        /// <summary>
        /// Music box
        /// </summary>
        MusicBox,
        /// <summary>
        /// Vibraphone
        /// </summary>
        Vibraphone,
        /// <summary>
        /// Marimba
        /// </summary>
        Marimba,
        /// <summary>
        /// Xylophone
        /// </summary>
        Xylophone,
        /// <summary>
        /// Tubular bells
        /// </summary>
        TubularBells,
        /// <summary>
        /// Dulcimer
        /// </summary>
        Dulcimer,
        /// <summary>
        /// Draw bar organ
        /// </summary>
        DrawbarOrgan,
        /// <summary>
        /// Percussive organ
        /// </summary>
        PercussiveOrgan,
        /// <summary>
        /// Rock organ
        /// </summary>
        RockOrgan,
        /// <summary>
        /// Church organ
        /// </summary>
        ChurchOrgan,
        /// <summary>
        /// Reed organ
        /// </summary>
        ReedOrgan,
        /// <summary>
        /// Accordion
        /// </summary>
        Accordion,
        /// <summary>
        /// Harmonica
        /// </summary>
        Harmonica,
        /// <summary>
        /// Tango accordion
        /// </summary>
        TangoAccordion, 
        /// <summary>
        /// Acoustic guitar nylon
        /// </summary>
        AcousticGuitarNylon, 
        /// <summary>
        /// Acoustic guitar steel
        /// </summary>
        AcousticGuitarSteel,
        /// <summary>
        /// Electric guitar jazz
        /// </summary>
        ElectricGuitarJazz,
        /// <summary>
        /// Electric guitar clean
        /// </summary>
        ElectricGuitarClean,
        /// <summary>
        /// Electric guitar muted
        /// </summary>
        ElectricGuitarMuted,
        /// <summary>
        /// Overdriven guitar
        /// </summary>
        OverdrivenGuitar,
        /// <summary>
        /// Distortion guitar
        /// </summary>
        DistortionGuitar,
        /// <summary>
        /// Guitar harmonics
        /// </summary>
        GuitarHarmonics,
        /// <summary>
        /// Acoustic bass
        /// </summary>
        AcousticBass,
        /// <summary>
        /// Electric bass finger
        /// </summary>
        ElectricBassFinger,
        /// <summary>
        /// Electric bass pick
        /// </summary>
        ElectricBassPick,
        /// <summary>
        /// Fretless bass
        /// </summary>
        FretlessBass,
        /// <summary>
        /// Slap bass 1
        /// </summary>
        SlapBass1,
        /// <summary>
        /// Slap bass 2
        /// </summary>
        SlapBass2,
        /// <summary>
        /// Synth bass 1
        /// </summary>
        SynthBass1,
        /// <summary>
        /// Synth bass 2
        /// </summary>
        SynthBass2,
        /// <summary>
        /// Violin
        /// </summary>
        Violin,
        /// <summary>
        /// Viola
        /// </summary>
        Viola,
        /// <summary>
        /// Cello
        /// </summary>
        Cello,
        /// <summary>
        /// Contrabass
        /// </summary>
        Contrabass,
        /// <summary>
        /// Tremolo strings
        /// </summary>
        TremoloStrings,
        /// <summary>
        /// Pizzicato strings
        /// </summary>
        PizzicatoStrings,
        /// <summary>
        /// Orchestra harp
        /// </summary>
        OrchestralHarp,
        /// <summary>
        /// Timpani
        /// </summary>
        Timpani,
        /// <summary>
        /// String ensembles 1
        /// </summary>
        StringEnsemble1,
        /// <summary>
        /// String ensembles 2
        /// </summary>
        StringEnsemble2,
        /// <summary>
        /// Synth string 1
        /// </summary>
        SynthStrings1,
        /// <summary>
        /// Synth strings 2
        /// </summary>
        SynthStrings2,
        /// <summary>
        /// Choir aah
        /// </summary>
        ChoirAahs,
        /// <summary>
        /// Voice ooh
        /// </summary>
        VoiceOohs,
        /// <summary>
        /// Synth voice
        /// </summary>
        SynthVoice,
        /// <summary>
        /// Orchestra hit
        /// </summary>
        OrchestraHit,
        /// <summary>
        /// Trumpet
        /// </summary>
        Trumpet,
        /// <summary>
        /// Trombone
        /// </summary>
        Trombone,
        /// <summary>
        /// Tuba
        /// </summary>
        Tuba,
        /// <summary>
        /// Muted trumped
        /// </summary>
        MutedTrumpet,
        /// <summary>
        /// French horn
        /// </summary>
        FrenchHorn,
        /// <summary>
        /// Brass section
        /// </summary>
        BrassSection,
        /// <summary>
        /// Synth brass 1
        /// </summary>
        SynthBrass1,
        /// <summary>
        /// Synth brass 2
        /// </summary>
        SynthBrass2,
        /// <summary>
        /// Soprano sax
        /// </summary>
        SopranoSax,
        /// <summary>
        /// Alto sax
        /// </summary>
        AltoSax,
        /// <summary>
        /// Tenor sax
        /// </summary>
        TenorSax,
        /// <summary>
        /// Baritone sax
        /// </summary>
        BaritoneSax,
        /// <summary>
        /// Oboe
        /// </summary>
        Oboe,
        /// <summary>
        /// English horn
        /// </summary>
        EnglishHorn,
        /// <summary>
        /// Bassoon
        /// </summary>
        Bassoon,
        /// <summary>
        /// Clarinet
        /// </summary>
        Clarinet,
        /// <summary>
        /// Piccolo
        /// </summary>
        Piccolo,
        /// <summary>
        /// Flute
        /// </summary>
        Flute,
        /// <summary>
        /// Recorder
        /// </summary>
        Recorder,
        /// <summary>
        /// Pan flute
        /// </summary>
        PanFlute,
        /// <summary>
        /// Blown bottle
        /// </summary>
        BlownBottle,
        /// <summary>
        /// Shakuhachi
        /// </summary>
        Shakuhachi,
        /// <summary>
        /// Whistle
        /// </summary>
        Whistle,
        /// <summary>
        /// Ocarina
        /// </summary>
        Ocarina,
        /// <summary>
        /// Lead 1 square
        /// </summary>
        Lead1Square,
        /// <summary>
        /// Lead 2 sawtooth
        /// </summary>
        Lead2Sawtooth,
        /// <summary>
        /// Lead 3 calliope
        /// </summary>
        Lead3Calliope,
        /// <summary>
        /// Lead 4 chiff
        /// </summary>
        Lead4Chiff,
        /// <summary>
        /// Lead 5 charang
        /// </summary>
        Lead5Charang,
        /// <summary>
        /// Lead 6 voice
        /// </summary>
        Lead6Voice,
        /// <summary>
        /// Lead 7 fifths
        /// </summary>
        Lead7Fifths,
        /// <summary>
        /// Lead 8 bass and lead
        /// </summary>
        Lead8BassAndLead,
        /// <summary>
        /// Pad 1 new age
        /// </summary>
        Pad1NewAge,
        /// <summary>
        /// Pad 2 warm
        /// </summary>
        Pad2Warm,
        /// <summary>
        /// Pad 3 polysynth
        /// </summary>
        Pad3Polysynth,
        /// <summary>
        /// Pad 4 choir
        /// </summary>
        Pad4Choir,
        /// <summary>
        /// Pad 5 bowed
        /// </summary>
        Pad5Bowed,
        /// <summary>
        /// Pad 6 metallic
        /// </summary>
        Pad6Metallic,
        /// <summary>
        /// Pad 7 halo
        /// </summary>
        Pad7Halo,
        /// <summary>
        /// Pad 8 sweep
        /// </summary>
        Pad8Sweep,
        /// <summary>
        /// Fx1 rain
        /// </summary>
        Fx1Rain,
        /// <summary>
        /// Fx2 Sound track
        /// </summary>
        Fx2Soundtrack,
        /// <summary>
        /// Fx 3 Crystal
        /// </summary>
        Fx3Crystal,
        /// <summary>
        /// Fx4 atmosphere
        /// </summary>
        Fx4Atmosphere,
        /// <summary>
        /// Fx5 brightness
        /// </summary>
        Fx5Brightness,
        /// <summary>
        /// Fx6 goblins
        /// </summary>
        Fx6Goblins,
        /// <summary>
        /// Fx7 echoes
        /// </summary>
        Fx7Echoes,
        /// <summary>
        /// Fx8 sci fi
        /// </summary>
        Fx8SciFi,
        /// <summary>
        /// Sitar
        /// </summary>
        Sitar,
        /// <summary>
        /// Banjo
        /// </summary>
        Banjo,
        /// <summary>
        /// Shamisen
        /// </summary>
        Shamisen,
        /// <summary>
        /// Koto
        /// </summary>
        Koto,
        /// <summary>
        /// Kalimba
        /// </summary>
        Kalimba,
        /// <summary>
        /// Bag pipe
        /// </summary>
        BagPipe,
        /// <summary>
        /// Fiddle
        /// </summary>
        Fiddle,
        /// <summary>
        /// Shanai
        /// </summary>
        Shanai,
        /// <summary>
        /// Tinkle bell
        /// </summary>
        TinkleBell,
        /// <summary>
        /// Agogo
        /// </summary>
        Agogo,
        /// <summary>
        /// Steel drums
        /// </summary>
        SteelDrums,
        /// <summary>
        /// Wood block
        /// </summary>
        Woodblock,
        /// <summary>
        /// Taiko drum
        /// </summary>
        TaikoDrum,
        /// <summary>
        /// Melodic drum
        /// </summary>
        MelodicTom,
        /// <summary>
        /// Synth drum
        /// </summary>
        SynthDrum,
        /// <summary>
        /// Reverse cymbal
        /// </summary>
        ReverseCymbal,
        /// <summary>
        /// Guitar fret noise
        /// </summary>
        GuitarFretNoise,
        /// <summary>
        /// Breath noise
        /// </summary>
        BreathNoise,
        /// <summary>
        /// Sea shore
        /// </summary>
        Seashore,
        /// <summary>
        /// Bird tweet
        /// </summary>
        BirdTweet,
        /// <summary>
        /// Telephone ring
        /// </summary>
        TelephoneRing,
        /// <summary>
        /// Helicopter
        /// </summary>
        Helicopter,
        /// <summary>
        /// Applause
        /// </summary>
        Applause,
        /// <summary>
        /// Gun shot
        /// </summary>
        Gunshot
    }
}