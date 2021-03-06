﻿namespace C0.Challenges.Codechef
{
    public struct ChallengeInputOutput<Tinput, Toutput>
    {
        public Tinput Input { get; private set; }
        public Toutput Output { get; private set; }

        public ChallengeInputOutput(Tinput input, Toutput output)
        {
            this.Input = input;
            this.Output = output;
        }
    }

}
