using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C0.Challenges.Codechef
{
    /// <summary>
    /// Challenge Template.
    /// Define the two Type-Parameter (Tin then Tout) below for the base <see cref="Challenge{Tin, Tout}"/> 
    ///  and change the types for the methods accordingly (see comments).
    /// PaR, 31. May 2017 @ C0 | VS2017
    /// </summary>
    class ExampleNewChallenge : Challenge<object, object>
    {
        public ExampleNewChallenge() : base(Console.WriteLine)
        { }

        public ExampleNewChallenge(Action<object> resultReceiver) : base(resultReceiver)
        { /* Change type-param of the Action<> above to Tout */ }

        public override string Name { get => "Name of the Challenge"; }
        public override string Code { get => "CODE_OF_CHALLENGE"; }
        public override string Url { get => "http://url.to.challenge"; }
        public override string Description { get => "Description text of challenge"; }
        public override string Notes { get => "Personal notes for the challenge implementation"; }
        public override string Rating { get => "Rating for the challange"; }
        public override string Submitted { get => "Date(+Time) of (last) submitting"; }

        protected override object Do(object input)
        {
            // Implement challange here.
            // Change type of param "input" to the Tin specified at the top.
            // Change type of return to the Tout specified at the top.
            return null;
        }

        public override IEnumerable<ChallengeInputOutput<object, object>> Input()
        {
            var inputs = new ChallengeInputOutputCollection<object, object>();

            // Here you can add Input-Output-Cases (Input and expected Output)
            // Change the 2 type-params of the ChallengeInputOutput<Tin,Tout> above to first Tin then Tout.
            // Also change the 2 type-params of the ChallengeInputOutputCollection<Tin,Tout> to the same.

            inputs.Add(null, null);

            return inputs.AsEnumerable();
        }

    }
}
