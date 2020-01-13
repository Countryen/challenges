using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C0.CodechefChallenges.Beginner
{
    /// <summary>
    /// Challenge.
    /// PaR, 5. June 2017 @ C0 | VS2017
    /// </summary>
    class ACBALL : Challenge<string[], string>
    {
        public ACBALL() : base(Console.WriteLine)
        { }

        public ACBALL(Action<string> resultReceiver) : base(resultReceiver)
        { }

        public override string Name { get => "Akhil And Colored Balls"; }
        public override string Code { get => "ACBALL"; }
        public override string Url { get => "https://www.codechef.com/problems/ACBALL"; }
        public override string Description { get => @"
            Akhil has many balls of white and black colors. 
            One day, he was playing with them. During the play, he arranged the balls into two rows both consisting of N number of balls. 
            These two rows of balls are given to you in the form of strings X, Y. 
            Both these string consist of 'W' and 'B', where 'W' denotes a white colored ball and 'B' a black colored.
            Other than these two rows of balls, Akhil has an infinite supply of extra balls of each color.
            He wants to create another row of N balls, Z in such a way that the sum of hamming distance between X and Z, and hamming distance between Y and Z is maximized.
            -> Hamming Distance between two strings X and Y is defined as the number of positions where the color of balls in row X differs from the row Y ball at that position.
               E.g: hamming distance between ''WBB'', ''BWB'' is 2, as at position 1 and 2, corresponding colors in the two strings differ.
            As there can be multiple such arrangements of row Z, Akhil wants you to find the lexicographically smallest arrangement which will maximize the above value.
            
            Input:
            [The first line of the input contains an integer T denoting the number of test cases.]
            The description of T test cases follows:
            - First line of each test case will contain a string X denoting the arrangement of balls in first row
            - Second line will contain the string Y denoting the arrangement of balls in second row.

            Output:
            For each test case, output a single line containing the string of length N denoting the arrangement of colors of the balls belonging to row Z
            
            Constrains:
            1 ≤ T ≤ 3
            
            Subtasks:
            Subtask #1 (10 points) : 1 ≤ N ≤ 16
            Subtask #2 (20 points) : 1 ≤ N ≤ 10^3
            Subtask #3 (70 points) : 1 ≤ N ≤ 10^5";
        }
        public override string Notes { get => "I changed the input into 1 array for both lines (instead of 2 separate lines/inputs)"; }
        public override string Rating { get => "30min (confusing description)"; }
        public override string Submitted { get => "5. June 2017"; }

        protected override string Do(string[] input)
        {
            string result = "";

            // Split input into StringX and StringY
            string x = input[1];
            string y = input[0];

            char partX, partY;

            // Traverse the string, both strings are the same length!
            // Then check every letter of the string for what the result must be.
            int len = x.Length;
            for (int i = 0; i < len; i++)
            {
                partX = x[i];
                partY = y[i];

                // Same color -> Z = opposite color
                if (partX == partY)
                {
                    if (partX == 'B')
                        result += "W";
                    else
                        result += "B";
                }
                else
                {
                    // If they are not the same color -> Z = always B (because of lexicographical order)
                    result += "B";
                }

            }

            return result;
        }

        public override IEnumerable<ChallengeInputOutput<string[], string>> Input()
        {
            var inputs = new ChallengeInputOutputCollection<string[], string>();

            inputs.Add(new string[] { "WBWB", "WBBB" }, "BWBW");

            return inputs.AsEnumerable();
        }

    }
}
