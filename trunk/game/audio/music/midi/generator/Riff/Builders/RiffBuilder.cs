using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AbrahmanAdventure.level;

namespace AbrahmanAdventure.audio.midi.generator
{
    /// <summary>
    /// Build music riff
    /// </summary>
    public class RiffBuilder
    {
        #region Parts
        /// <summary>
        /// To reduce volume of cymbals and stuff like that
        /// </summary>
        private DrumVelocityHacker drumVelocityHacker = new DrumVelocityHacker();

        private IntervalAnalyzer intervalAnalyzer = new IntervalAnalyzer();
        #endregion

        #region Fields
        /// <summary>
        /// desired riff length (how many bars)
        /// </summary>
        private int desiredRiffLength = -1;

        /// <summary>
        /// Pitch wave
        /// </summary>
        private AbstractWave pitchWave;

        /// <summary>
        /// Velocity wave
        /// </summary>
        private AbstractWave velocityWave;

        /// <summary>
        /// Rythm pattern
        /// </summary>
        private RythmPattern rythmPattern;

        /// <summary>
        /// Music scale
        /// </summary>
        private Scale scale;

        /// <summary>
        /// Mid pitch
        /// </summary>
        private int midPitch = -1;

        /// <summary>
        /// Pitch radius
        /// </summary>
        private int radius = -1;

        /// <summary>
        /// Midi instrument number (0 to 127)
        /// </summary>
        private int midiInstrument = -1;

        /// <summary>
        /// Minimum velocity
        /// </summary>
        private int minimumVelocity = -1;

        /// <summary>
        /// Maximum velocity
        /// </summary>
        private int maximumVelocity = -1;

        /// <summary>
        /// Default modulation key
        /// </summary>
        private int defaultKey = -1000;

        /// <summary>
        /// Tempo (bpm)
        /// </summary>
        private int tempo;

        /// <summary>
        /// Whether riff is drum or not
        /// </summary>
        private bool isDrum = false;

        /// <summary>
        /// Whether riff is ultra rigid drum (snare, kick etc) or not
        /// </summary>
        private bool isUltraRigidDrum = false;
        
        /// <summary>
        /// Key modulator
        /// </summary>
        private Modulator modulator;

        /// <summary>
        /// Time frame (sometimes riffs are on or off)
        /// </summary>
        private TimeFrame timeFrame;

        /// <summary>
        /// Whether we override key
        /// </summary>
        private bool isOverrideKey;

        /// <summary>
        /// Facultative key to override (only works if IsOverrideKey)
        /// </summary>
        private int forcedModulationOffset;
        #endregion

        #region Public Methods
        /// <summary>
        /// Build riff
        /// </summary>
        /// <param name="currentRiffId">current id in riffPack (facultative)</param>
        /// <returns></returns>
        public Riff Build(int currentRiffId)
        {
            if (pitchWave == null)
                throw new RiffBuilderException("Missing PitchWave property");
            else if (pitchWave == null)
                throw new RiffBuilderException("Missing VelocityWave property");
            else if (rythmPattern == null)
                throw new RiffBuilderException("Missing RythmPattern property");
            else if (scale == null)
                throw new RiffBuilderException("Missing Scale property");
            else if (desiredRiffLength == -1)
                throw new RiffBuilderException("Missing DesiredRiffLength property");
            else if (midPitch == -1)
                throw new RiffBuilderException("Missing MidPitch property");
            else if (radius == -1)
                throw new RiffBuilderException("Missing Radius property");
            else if (midiInstrument == -1)
                throw new RiffBuilderException("Missing MidiInstrument property");
            else if (minimumVelocity == -1)
                throw new RiffBuilderException("Missing MinimumVelocity property");
            else if (maximumVelocity == -1)
                throw new RiffBuilderException("Missing MaximumVelocity property");
            else if (defaultKey == -1000)
                throw new RiffBuilderException("Missing DefaultKey property");

            if (isOverrideKey)
                midPitch = GetMatchedMidPitch(midPitch, forcedModulationOffset);

            Riff riff = new Riff();

            riff.MidiInstrument = MidiInstrument;

            rythmPattern.ResetPosition();

            double currentPosition = 0;
            double currentNoteLength;
            int currentPitch;
            int currentVelocity;
            int currentModulationOffset = 0;



            if (DesiredRiffLength > 0)
            {
                do
                {
                    currentNoteLength = rythmPattern.GetNextLength();
                    if (currentPosition + currentNoteLength > DesiredRiffLength)
                        currentNoteLength = DesiredRiffLength - currentPosition;

                    currentPitch = BuildPitch(currentPosition, pitchWave, scale, midPitch, radius, isDrum);

                    currentVelocity = BuildVelocity(currentPosition, velocityWave, minimumVelocity, maximumVelocity);




                    if (isDrum)
                    {
                        if (!isUltraRigidDrum)
                            currentVelocity = drumVelocityHacker.HackVelocity(currentPitch, currentVelocity);

                        currentModulationOffset = 0;
                    }
                    else
                    {
                        currentModulationOffset = defaultKey;
                        if (!isOverrideKey)
                            currentModulationOffset += modulator.GetModulationOffset(currentPosition * 2);
                        else
                            currentModulationOffset = 0;
                    }



                    while (currentModulationOffset > 6)
                        currentModulationOffset -= 12;
                    while (currentModulationOffset < -6)
                        currentModulationOffset += 12;


                    if (timeFrame == null || timeFrame.IsAllowTime(currentPosition, currentRiffId))
                        riff.Add(new Note(currentPosition, currentNoteLength, currentPitch + currentModulationOffset, currentVelocity));
                    else
                        riff.Add(new Note(currentPosition, currentNoteLength, currentPitch + currentModulationOffset, 0));

                    currentPosition += currentNoteLength;
                } while (currentPosition < DesiredRiffLength);
            }

            riff.IsDrum = IsDrum;
            riff.Tempo = Tempo;

            return riff;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Return a pitch position that matches the other (same music key, but may not be same octave)
        /// </summary>
        /// <param name="toMatch">pitch to be matched</param>
        /// <param name="toMatchWith">pitch to match with</param>
        /// <returns>matched pitch</returns>
        private int GetMatchedMidPitch(int toMatch, int toMatchWith)
        {
            while (toMatchWith - toMatch >= 12)
                toMatchWith -= 12;

            while (toMatch - toMatchWith >= 12)
                toMatchWith += 12;

            return toMatchWith;
        }

        /// <summary>
        /// Build note pitch
        /// </summary>
        /// <param name="currentPosition">current time position</param>
        /// <param name="wave">wave</param>
        /// <param name="scale">scale</param>
        /// <param name="midPitch">mid pitch</param>
        /// <param name="radius">pitch radius</param>
        /// <param name="isDrum">whether is drum</param>
        /// <returns>Note's pitch</returns>
        private int BuildPitch(double currentPosition, AbstractWave wave, Scale scale, int midPitch, int radius, bool isDrum)
        {
            int roundedPitch;
            double y = wave[currentPosition] * 7.0;

            roundedPitch = scale.GetRoundValue(y, radius);

            while (roundedPitch > 12)
                roundedPitch -= 12;

            while (roundedPitch < 0)
                roundedPitch += 12;

            roundedPitch += midPitch;

            while (roundedPitch > 127)
                roundedPitch -= 12;
            while (roundedPitch < 0)
                roundedPitch += 12;

            return roundedPitch;
        }

        /// <summary>
        /// Build velocity
        /// </summary>
        /// <param name="currentPosition">current time</param>
        /// <param name="wave">velocity wave</param>
        /// <param name="minimumVelocity">minimum velocity</param>
        /// <param name="maximumVelocity">maximum velocity</param>
        /// <returns>velocity</returns>
        private int BuildVelocity(double currentPosition, AbstractWave wave, int minimumVelocity, int maximumVelocity)
        {
            double y = Math.Abs(wave[currentPosition]);

            y *= ((float)(maximumVelocity) - (float)(minimumVelocity));
            y += (float)(minimumVelocity);

            int velocity = (int)(Math.Round(y));

            if (velocity > 127)
                velocity = 127;
            else if (velocity < 0)
                velocity = 0;

            return velocity;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Pitch wave
        /// </summary>
        public AbstractWave PitchWave
        {
            get { return pitchWave; }
            set { pitchWave = value; }
        }
        
        /// <summary>
        /// Velocity wave
        /// </summary>
        public AbstractWave VelocityWave
        {
            get { return velocityWave; }
            set { velocityWave = value; }
        }

        /// <summary>
        /// Rythm pattern
        /// </summary>
        public RythmPattern RythmPattern
        {
            get { return rythmPattern; }
            set { rythmPattern = value; }
        }

        /// <summary>
        /// Desired scale
        /// </summary>
        public Scale Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        /// <summary>
        /// Desired riff length
        /// </summary>
        public int DesiredRiffLength
        {
            get{return desiredRiffLength;}
            set { desiredRiffLength = value; }
        }

        /// <summary>
        /// Mid pitch
        /// </summary>
        public int MidPitch
        {
            get { return midPitch; }
            set { midPitch = value; }
        }

        /// <summary>
        /// Pitch radius
        /// </summary>
        public int Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        /// <summary>
        /// Midi instrument (0 to 127)
        /// </summary>
        public int MidiInstrument
        {
            get { return midiInstrument; }
            set { midiInstrument = value; }
        }

        /// <summary>
        /// Minimum velocity
        /// </summary>
        public int MinimumVelocity
        {
            get { return minimumVelocity; }
            set { minimumVelocity = value; }
        }

        /// <summary>
        /// Maximum velocity
        /// </summary>
        public int MaximumVelocity
        {
            get { return maximumVelocity; }
            set { maximumVelocity = value; }
        }
        
        /// <summary>
        /// Drum or not
        /// </summary>
        public bool IsDrum
        {
            get{return isDrum;}
            set{isDrum = value;}
        }

        /// <summary>
        /// Ultra Rigid Drum (snare, kick etc) or not
        /// </summary>
        public bool IsUltraRigidDrum
        {
            get { return isUltraRigidDrum; }
            set { isUltraRigidDrum = value; }
        }

        /// <summary>
        /// Desired tempo
        /// </summary>
        public int Tempo
        {
            get { return tempo; }
            set { tempo = value; }
        }

        /// <summary>
        /// Key Modulator
        /// </summary>
        public Modulator Modulator
        {
            get { return modulator; }
            set { modulator = value; }
        }

        /// <summary>
        /// Time frame (sometimes riffs are on, sometimes they are off)
        /// </summary>
        public TimeFrame TimeFrame
        {
            get { return timeFrame; }
            set { timeFrame = value; }
        }

        /// <summary>
        /// Default modulation key
        /// </summary>
        public int DefaultKey
        {
            get { return defaultKey; }
            set { defaultKey = value; }
        }

        /// <summary>
        /// Facultative key to override (only works if IsOverrideKey)
        /// </summary>
        public int ForcedModulationOffset
        {
            get { return forcedModulationOffset; }
            set { forcedModulationOffset = value; }
        }

        /// <summary>
        /// Whether we override key
        /// </summary>
        public bool IsOverrideKey
        {
            get { return isOverrideKey; }
            set { isOverrideKey = value; }
        }
        #endregion
    }
}
