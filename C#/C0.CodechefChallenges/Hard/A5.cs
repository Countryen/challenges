using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C0.CodechefChallenges.Hard
{
    /// <summary>
    /// Challenge.
    /// PaR, 5. June 2017 @ C0 | VS2017
    /// </summary>
    class A5 : Challenge<(int numberOfBoxes, int[] boxSizes), (int cost, int numberOFDistinctWays)>, IAutomaticChallenge
    {
        public A5() : base(_receiver)
        { }

        public A5(Action<(int, int)> resultReceiver) : base(resultReceiver)
        { }

        private static Action<(int cost, int numberOFDistinctWays)> _receiver = (output) => { Console.WriteLine($"({output.cost}, {output.numberOFDistinctWays})"); };

        public override string Name { get => "Packing the Boxes"; }
        public override string Code { get => "A5"; }
        public override string Url { get => "https://www.codechef.com/problems/A5"; }
        public override string Description { get => @"
            Shaheen has bought some gifts for a friend, which are wrapped up in several boxes of different sizes (all of which are full).
            She will need to carry the gifts a long way to her friend's house,
            so she would prefer to add some extra packing, and accommodate everything in one extra box.
            Moreover, to avoid damaging the contents, she does not wish to place more than two boxes directly within any box;
            however, boxes can be placed within boxes which contain other boxes, etc.
            A box which is used for holding two boxes of sizes a and b will have size a+b, and will cost Shaheen a+b coins at the local store.
            Please help Shaheen determine the minimum cost required to achieve the complete packing,
            and the number of different possible packings (arrangements of boxes within each other) having such a minimal cost.
            
            Input:
            The first line of input is n<=2000, the number of boxes with Shaheen's gifts. 
            The next n lines of input contain one positive integer each, not greater than 106,
                representing the sizes of the successive boxes.

            Output:
            Print to output exactly 2 numbers separated by spaces: 
                the cost of the optimal solution, 
                and the number of distinct ways of achieving this solution (modulo 10^9).
            
            Example:
            5            3            2            3            5            1
            =31 3

            Explanation:
            1) pack the 2nd and the 5th box together, pack the resulting box together with the 1st box, 
               and pack the result together with an additional box used for packing the 3rd and 4th boxes.
            or
            2) pack the 2nd and the 5th box together, pack the resulting box together with the 3rd box,
               and pack the result together with an additional box used for packing the 1st and 4th boxes.
            or
            3) pack the 2nd and the 5th box together, pack the resulting box together with the 4th box, 
               and pack the result together with an additional box used for packing the 1st and 3rd boxes.";
        }
        public override string Notes { get => ""; }
        public override string Rating { get => ""; }
        public override string Submitted { get => ""; }

        protected override (int cost, int numberOFDistinctWays) Do((int numberOfBoxes, int[] boxSizes) input)
        {
            return default((int, int));
        }

        public override IEnumerable<ChallengeInputOutput<(int numberOfBoxes, int[] boxSizes), (int cost, int numberOFDistinctWays)>> Input()
        {
            var inputs = new ChallengeInputOutputCollection<(int, int[]), (int, int)>();

            inputs.Add((5, new int[] { 3, 2, 3, 5, 1 }), (31, 3));

            return inputs.AsEnumerable();
        }

        public struct InputType
        {
            public int First;
            public int[] Second;

            public InputType(int first, int[] second)
            {
                First = first;
                Second = second;
            }
        }

    }
}
