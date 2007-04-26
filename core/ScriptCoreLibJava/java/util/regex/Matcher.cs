using ScriptCoreLib;

using java.lang;

namespace java.util.regex
{
    /// <summary>
    /// An engine that performs match operations on a character sequence by interpreting a Pattern. 
    /// </summary>
    [Script(IsNative = true)]
    public class Matcher
    {
        // Method Summary
        /// <summary>
        /// Implements a non-terminal append-and-replace step.
        /// </summary>
        public Matcher appendReplacement(StringBuffer sb, string replacement)
        {
            return default(Matcher);
        }

        /// <summary>
        /// Implements a terminal append-and-replace step.
        /// </summary>
        public StringBuffer appendTail(StringBuffer sb)
        {
            return default(StringBuffer);
        }

        /// <summary>
        /// Returns the index of the last character matched, plus one.
        /// </summary>
        public int end()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the index of the last character, plus one, of the subsequence captured by the given group during the previous match operation.
        /// </summary>
        public int end(int group)
        {
            return default(int);
        }

        /// <summary>
        /// Attempts to find the next subsequence of the input sequence that matches the pattern.
        /// </summary>
        public bool find()
        {
            return default(bool);
        }

        /// <summary>
        /// Resets this matcher and then attempts to find the next subsequence of the input sequence that matches the pattern, starting at the specified index.
        /// </summary>
        public bool find(int start)
        {
            return default(bool);
        }

        /// <summary>
        /// Returns the input subsequence matched by the previous match.
        /// </summary>
        public string group()
        {
            return default(string);
        }

        /// <summary>
        /// Returns the input subsequence captured by the given group during the previous match operation.
        /// </summary>
        public string group(int group)
        {
            return default(string);
        }

        /// <summary>
        /// Returns the number of capturing groups in this matcher's pattern.
        /// </summary>
        public int groupCount()
        {
            return default(int);
        }

        /// <summary>
        /// Attempts to match the input sequence, starting at the beginning, against the pattern.
        /// </summary>
        public bool lookingAt()
        {
            return default(bool);
        }

        /// <summary>
        /// Attempts to match the entire input sequence against the pattern.
        /// </summary>
        public bool matches()
        {
            return default(bool);
        }

        /// <summary>
        /// Returns the pattern that is interpreted by this matcher.
        /// </summary>
        public Pattern pattern()
        {
            return default(Pattern);
        }

        /// <summary>
        /// Replaces every subsequence of the input sequence that matches the pattern with the given replacement string.
        /// </summary>
        public string replaceAll(string replacement)
        {
            return default(string);
        }

        /// <summary>
        /// Replaces the first subsequence of the input sequence that matches the pattern with the given replacement string.
        /// </summary>
        public string replaceFirst(string replacement)
        {
            return default(string);
        }

        /// <summary>
        /// Resets this matcher.
        /// </summary>
        public Matcher reset()
        {
            return default(Matcher);
        }

        /// <summary>
        /// Resets this matcher with a new input sequence.
        /// </summary>
        public Matcher reset(CharSequence input)
        {
            return default(Matcher);
        }

        /// <summary>
        /// Returns the start index of the previous match.
        /// </summary>
        public int start()
        {
            return default(int);
        }

        /// <summary>
        /// Returns the start index of the subsequence captured by the given group during the previous match operation.
        /// </summary>
        public int start(int group)
        {
            return default(int);
        }


    }
}

