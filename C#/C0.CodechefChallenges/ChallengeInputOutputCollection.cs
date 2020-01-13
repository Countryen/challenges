using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C0.CodechefChallenges
{
    public class ChallengeInputOutputCollection<Tin, Tout>
    {
        private readonly List<ChallengeInputOutput<Tin, Tout>> InputOutputs = new List<ChallengeInputOutput<Tin, Tout>>();

        public void Add(Tin input, Tout output)
        {
            InputOutputs.Add(new ChallengeInputOutput<Tin, Tout>(input, output));
        }

        public void AddRange(IEnumerable<ChallengeInputOutput<Tin, Tout>> inputOutputs)
        {
            InputOutputs.AddRange(InputOutputs);
        }

        public ChallengeInputOutput<Tin, Tout> this[int index] { get => InputOutputs[index]; set => InputOutputs[index] = value; }

        public IEnumerable<ChallengeInputOutput<Tin, Tout>> AsEnumerable()
        {
            return InputOutputs.AsEnumerable();
        }
    }
}
