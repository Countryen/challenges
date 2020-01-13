using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C0.CodechefChallenges.Medium
{
    /// <summary>
    /// Challenge.
    /// PaR, 5. June 2017 @ C0 | VS2017
    /// </summary>
    class ABCSTR : Challenge<string, long>
    {
        public ABCSTR() : base(Console.WriteLine)
        { }

        public ABCSTR(Action<long> resultReceiver) : base(resultReceiver)
        { }

        public override string Name { get => "ABC-Strings"; }
        public override string Code { get => "ABCSTR"; }
        public override string Url { get => "https://www.codechef.com/problems/ABCSTR"; }
        public override string Description { get => @"
            Mike likes strings. He is also interested in algorithms. A few days ago he discovered for himself a very nice problem:
            You are given an AB-string S. You need to count the number of substrings of S, which have an equal number of 'A'-s and 'B'-s.
            Do you know how to solve it? Good. Mike will make the problem a little bit more difficult for you.
            You are given an ABC-string S. You need to count the number of substrings of S, which have an equal number of 'A'-s, 'B'-s and 'C'-s.
            A string is called AB-string if it doesn't contain any symbols except 'A' or 'B'.
            A string is called ABC-string if it doesn't contain any symbols except 'A', 'B' or 'C'.
            
            Input:
            The first line of the input contains an ABC-string S.

            Output:
            Your output should contain the only integer, denoting the number of substrings of S, which have an equal number of 'A'-s, 'B'-s and 'C'-s.
            The answer can go above a 32-bit integer. Please, use 64-bit integers for storing and processing data.
            
            Constrains:
            1 ≤ |S| ≤ 1 000 000; where |S| denotes the length of the given ABC-string.

            Explanation:
            In the example you should count S[2..4] = ''BAC'' and S[4..6] = ''CAB''";
        }
        public override string Notes { get => "pretty easy"; }
        public override string Rating { get => "5 min prep., 20 min Do()"; }
        public override string Submitted { get => "5. June 2017"; }

        /// <summary>
        /// Changed Version. 
        /// Not only ABC, but all letters allowed.
        /// - Adding about 10 minutes.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected override long Do(string input)
        {
            string letterString = input;

            long substringCount = 0;
            string substring = "";

            // 1. Define the individual letters.
            var lettersAndCounts = new Dictionary<char, int>();
            foreach (char c in letterString)
            {
                // Add letter to the collection if not already inside.
                if (!(lettersAndCounts.ContainsKey(c)))
                {
                    lettersAndCounts.Add(c, 0);
                }
            }

            // 2. Iterate each letter of the string as a starting point for the substring
            int len = letterString.Length;
            for (int start = 0; start < len; start++)
            {
                // The substring IS the starting-char
                substring = letterString[start].ToString();

                // 3. Iterate each remaining letter of the string as a ending point for the substring.
                for (int end = start + 1; end < len; end++)
                {
                    // Adding the (new) end to the substring will increase the length, till the end
                    substring += letterString[end];

                    // Counting all the individual letters
                    lettersAndCounts.Keys.ToList().ForEach(letter => lettersAndCounts[letter] = substring.Count(c => c == letter));

                    // Each letter-count must be in the collection as often as the amount of letters.
                    // Then all letter-count's will be the same!
                    if (lettersAndCounts.Values.All(countX => lettersAndCounts.Values.Count(countY => (countY == countX)) == lettersAndCounts.Count))
                    {
                        // Valid substring with equal amount of individual letters.
                        substringCount += 1;
                    }
                }

            }

            return substringCount;

        }

        /// <summary>
        /// Standard version according to the description.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        protected long DoStandard(string input)
        {
            string abcString = input;

            long substringCount = 0;
            string substring = "";
            int countA = 0;
            int countB = 0;
            int countC = 0;

            int len = abcString.Length;
            // 1. Iterate each letter of the string as a starting point for the substring
            for (int start = 0; start < len; start++)
            {
                // the substring IS the starting-char
                substring = abcString[start].ToString();

                // 2. Iterate each remaining letter of the string as a ending point for the substring.
                for (int end = start + 1; end < len; end++)
                {
                    // adding the (new) end to the substring will increase the length, till the end
                    substring += abcString[end];

                    // counting all the a, b, c.
                    countA = substring.Count(c => c == 'A');
                    countB = substring.Count(c => c == 'B');
                    countC = substring.Count(c => c == 'C');

                    if (countA == countB && countA == countC)
                    {
                        // valid substring with equal amount of A, B, C
                        substringCount += 1;
                    }
                }
                
            }

            return substringCount;

        }

        public override IEnumerable<ChallengeInputOutput<string, long>> Input()
        {
            var inputs = new ChallengeInputOutputCollection<string, long>();

            inputs.Add("ABACABA", 2); // A(BAC)ABA + ABA(CAB)A

            inputs.Add("ABC", 1); // (ABC)
            inputs.Add("AABBCC", 1); // (AABBCC) (2A, 2B, 2C)
            inputs.Add("AAABBBCC", 0); // None
            inputs.Add("ABCABCABC", 3 + 2 + 2 + 2 + 1 + 1 + 1); // (ABC)(ABC)(ABC) A(BCA)(BCA)BC AB(CAB)(CAB)C ABC(ABC)(ABC) ABCA(BCA)BC ABCAB(CAB)C ABCABC(ABC)

            inputs.Add("X", 1); // (X)
            inputs.Add("XY", 1); // (XY)
            inputs.Add("XYZZ", 1); // None
            inputs.Add("_-!&&µ-_!µ", 2); // (_-!&&µ-_!µ)-2each   _-!&(&µ-_!)µ-1each 

            return inputs.AsEnumerable();
        }

    }
}
