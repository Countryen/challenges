using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C0.Challenges.Codechef.Beginner
{
    /// <summary>
    /// Challenge.
    /// PaR, 31. May 2017 @ C0 | VS2017
    /// </summary>
    class HS08TEST : Challenge<double[], double>
    {
        public HS08TEST() : base(Console.WriteLine)
        { }

        public HS08TEST(Action<double> resultReceiver) : base(resultReceiver)
        { }

        public override string Name { get => "ATM"; }
        public override string Code { get => "HS08TEST"; }
        public override string Url { get => "https://www.codechef.com/problems/HS08TEST"; }
        public override string Description { get => @"
            Pooja would like to withdraw X $US from an ATM.
            The cash machine will only accept the transaction if X is a multiple of 5,
            and Pooja's account balance has enough cash to perform the withdrawal transaction (including bank charges).
            For each successful withdrawal the bank charges 0.50 $US.
            Calculate Pooja's account balance after an attempted transaction.
            
            Input:
            Positive integer 0 < X <= 2000 - the amount of cash which Pooja wishes to withdraw.
            Nonnegative number 0<= Y <= 2000 with two digits of precision - Pooja's initial account balance.
            
            Output:
            Output the account balance after the attempted transaction, given as a number with two digits of precision.
            If there is not enough money in the account to complete the transaction, output the current bank balance.
            ";
        }
        public override string Notes { get => "-"; }
        public override string Rating { get => "15min"; }
        public override string Submitted { get => "31. May 2017"; }

        protected override double Do(double[] input)
        {
            double deposit = input[1];
            double withdraw = input[0];

            // Catch: Isn't the withdraw a multiple of 5? -> NOT ALLOWED
            if (withdraw % 5 != 0)
                return deposit;
            // Catch: Isn't enough cache in the deposit? -> NOT ALLOWED
            else if ((withdraw + 0.50) > deposit)
                return deposit;
            else
            // OK
                return (deposit - withdraw - 0.50);
        }

        public override IEnumerable<ChallengeInputOutput<double[], double>> Input()
        {
            var inputs = new ChallengeInputOutputCollection<double[], double>();

            inputs.Add(new double[] { 30D, 120.00D }, 89.50D);
            inputs.Add(new double[] { 42D, 120.00D }, 120.00D);
            inputs.Add(new double[] { 300D, 120.00D }, 120.00D);

            return inputs.AsEnumerable();
        }

    }
}
