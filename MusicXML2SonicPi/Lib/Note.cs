using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicXml.SonicPi.MusicScore.ScorePart.MeasureNote
{
    public class Note : INote
    {
        public int Index { get; set; }
        public Pitch NotePitch { get; set; }
        public int Length { get; set; }
        public int VoiceId { get; set; }
        public bool IsChord { get; set; }
        public NoteAccidental Accidental { get; set; }

        public Note(int length = 0, int voiceId = 1, bool isChord = false)
        {
            Length = length;
            VoiceId = voiceId;
            IsChord = isChord;
            Accidental = NoteAccidental.nil;
        }
        public Note(Pitch notePitch, int length, int voiceId, bool isChord, NoteAccidental accidental)
        {
            NotePitch = notePitch;
            Length = length;
            VoiceId = voiceId;
            IsChord = isChord;
            Accidental = accidental;
        }
    }
}
