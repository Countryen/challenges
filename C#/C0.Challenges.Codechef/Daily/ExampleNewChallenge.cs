using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C0.Challenges.Codechef
{
    /// <summary>
    /// Challenge
    /// JAN 2019
    /// </summary>
    class AddsUpChallenge : Challenge<int[], bool>, IAutomaticChallenge
    {
        public AddsUpChallenge() : base(Console.WriteLine)
        { }

        public AddsUpChallenge(Action<bool> resultReceiver) : base(resultReceiver)
        { /* Change type-param of the Action<> above to Tout */ }

        public override string Name { get => "Check if any 2 numbers in an array add up to any element in the same array"; }
        public override string Code { get => "NO_CODE"; }
        public override string Url { get => "http://url.to.challenge"; }
        public override string Description { get => "Check if any 2 numbers in an array add up to any element in the same array"; }
        public override string Notes { get => "Any() ends on first success, Where() not; Not most performant, but easy and readable."; }
        public override string Rating { get => "2*"; }
        public override string Submitted { get => "13. Januar 2019, 22:00 MET"; }

        protected override bool Do(int[] input)
        {
            // Implement challange here.
            // Change type of param "input" to the Tin specified at the top.
            // Change type of return to the Tout specified at the top.

            bool CheckAny(params int[] list)
            {
                return list.Any(k => list.Any(a => list.Any(b => k == a + b)));
            }

            // Alternative for checking the results:
            void CheckWhere(params int[] list)
            {
                var successfulAddUps = list.Where(k => list.Any(a => list.Any(b => k == a + b)));
                Console.WriteLine("k: {0}", string.Join(",", successfulAddUps));
            }

            return CheckAny(input);
        }



        public override IEnumerable<ChallengeInputOutput<int[], bool>> Input()
        {
            var inputs = new ChallengeInputOutputCollection<int[], bool>();

            // Here you can add Input-Output-Cases (Input and expected Output)
            // Change the 2 type-params of the ChallengeInputOutput<Tin,Tout> above to first Tin then Tout.
            // Also change the 2 type-params of the ChallengeInputOutputCollection<Tin,Tout> to the same.

            inputs.Add(Enumerable.Range(1, 10000).ToArray(), true);
            inputs.Add(Enumerable.Range(1, 1000).Select(x => x * x).ToArray(), true);
            inputs.Add(new int[]{ 10, 15, 3, 7 }, true);
            inputs.Add(new int[] { 10, 15, 3, 8 }, false);

            return inputs.AsEnumerable();
        }

    }
}
