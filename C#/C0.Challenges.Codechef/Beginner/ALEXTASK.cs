using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C0.Challenges.Codechef.Beginner
{
    /// <summary>
    /// Challenge.
    /// PaR, 5. June 2017 @ C0 | VS2017
    /// </summary>
    class ALEXTASK : Challenge<ALEXTASK.InputType, int>
    {
        public ALEXTASK() : base(Console.WriteLine)
        { }

        public ALEXTASK(Action<int> resultReceiver) : base(resultReceiver)
        { }

        public override string Name { get => "Task for Alexey"; }
        public override string Code { get => "ALEXTASK"; }
        public override string Url { get => "https://www.codechef.com/problems/ALEXTASK"; }
        public override string Description { get => @"
            Alexey is trying to develop a program for a very simple microcontroller.
            It makes readings from various sensors over time, and these readings must happen at specific regular times.
            Unfortunately, if two of these readings occur at the same time, the microcontroller freezes and must be reset.
            There are N different sensors that read data on a regular basis. 
            For each i from 1 to N, the reading from sensor i will occur every Ai milliseconds with the first reading occurring 
                exactly Ai milliseconds after the microcontroller is powered up. Each reading takes precisely one millisecond on Alexey's microcontroller.
            Alexey wants to know when the microcontroller will freeze after he turns it on.
            
            Input:
            [The first line of the input contains an integer T denoting the number of test cases.] 
            The description of T test cases follows.
            - The first line contains single integer N denoting the number of sensors.
            - The second line contains N space-separated integers A1, A2, ..., AN denoting frequency of measurements. 
                Namely, sensor i will be read every Ai milliseconds with the first reading occurring Ai milliseconds after the microcontroller is first turned on.

            Output:
            For each test case, output a single line containing the number of milliseconds until the microcontroller freezes.
            
            Constrains:
            1 ≤ T ≤ 10
            2 ≤ N ≤ 500
            1 ≤ Ai ≤ 109
            
            Subtasks:
            Subtask #1 (10 points) 1 ≤ T ≤ 10, 2 ≤ N ≤ 9, 1 ≤ Ai ≤ 500
            Subtask #2 (20 points) 1 ≤ T ≤ 10, 2 ≤ N ≤ 500, 1 ≤ Ai ≤ 1000
            Subtask #3 (70 points) original constraints

            Explanation:
            Case 1: in 6 milliseconds, the third reading will be attempted from the 1st sensor and the second reading will be attempted from the 2nd sensor.
            Case 2: in 7 milliseconds the seventh reading will be attempted from the 1st sensor and the first reading will be attempted from the 3rd sensor.
            Case 3: in 4 milliseconds, the first readings from the first two sensors will be attempted.";
        }
        public override string Notes { get => "I changed the input into 1 InputType for both lines (instead of 2 separate lines/inputs)"; }
        public override string Rating { get => "15 min prep., 30min Do()"; }
        public override string Submitted { get => "5. June 2017"; }

        protected override int Do(ALEXTASK.InputType input)
        {
            int runningTime = 0;

            int sensorCount = input.First;
            int[] sensorReadingFrequencies = input.Second;

            int[] nowReadingSensors;

            bool isNotFreezed = true;

            // Start of the microcontroller, a loop, each iteration is equal to 1 millisecond.
            while(isNotFreezed && runningTime < 100)
            {
                // Time advances.
                runningTime += 1;

                // We check which sensors need to read now.
                nowReadingSensors = sensorReadingFrequencies.Where(freq => (runningTime % freq ) == 0).ToArray();
                if (nowReadingSensors.Length > 1)
                {
                    // more than 1 sensor activates -> freeze.
                    isNotFreezed = false;
                }
                else if (nowReadingSensors.Length == 1)
                {
                    // only 1 sensor activates -> costs 1 millisecond
                    runningTime += 1;
                }
            }

            return runningTime;
        }

        public override IEnumerable<ChallengeInputOutput<ALEXTASK.InputType, int>> Input()
        {
            var inputs = new ChallengeInputOutputCollection<ALEXTASK.InputType, int>();

            inputs.Add(new InputType(3, new int[] {2, 3, 5}), 6);
            inputs.Add(new InputType(4, new int[] {1, 8, 7, 11}), 7);
            inputs.Add(new InputType(4, new int[] { 4, 4, 6, 6 }), 4);


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
