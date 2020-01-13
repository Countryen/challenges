using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C0.CodechefChallenges;

namespace C0.CodechefChallenges.ForeignChallenges
{
    class TestForeignEchoChallenge : Challenge<string, string>, IAutomaticChallenge
    {
        public TestForeignEchoChallenge() : base(Console.WriteLine)
        { }

        public override string Name { get => "TestEchoChallenge"; }
        public override string Code { get => "C0ECHOTEST"; }
        public override string Url { get => "http://this.test.has.no.url"; }
        public override string Description { get => "This is a test challenge simply to test the class \"Challenge\". It returns any input"; }
        public override string Notes { get => "Don't Run() this class directly!"; }
        public override string Rating { get => "5*"; }
        public override string Submitted { get => DateTime.Now.ToString(); }

        protected override string Do(string input)
        {
            return (input != null ? input.ToString() : "NULL");
        }

        public override IEnumerable<ChallengeInputOutput<string, string>> Input()
        {
            var inputs = new ChallengeInputOutputCollection<string, string>();

            inputs.Add("C#", "C#");
            inputs.Add("node", "node");
            inputs.Add("PHP", "PHP");
            inputs.Add("DF", "DF");

            return inputs.AsEnumerable();
        }
    }
}
