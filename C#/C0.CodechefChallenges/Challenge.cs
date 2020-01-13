using System;
using System.Collections;
using System.Collections.Generic;

namespace C0.CodechefChallenges
{
    /// <summary>
    /// Part (Abstract baseclass) of the testbed for challenges from www.codechef.com.
    /// Each challange implements this class and
    ///   sets some meta-data (Name, Url, Description...),
    ///   sets the <see cref="ResultReceiver"/> in the constructor,
    ///   and implements <see cref="Input"/> and <see cref="Do(Tin)"/>.
    /// Run this challenge via <see cref="Run(Tin[])"/>.
    /// It will than <see cref="Do(Tin)"/> the challange, first the class-defined <see cref="Input"/> than the argument of <see cref="Run(Tin[])"/>.
    /// Result will than be forwarded to the <see cref="ResultReceiver"/> from <see cref="Output(bool, Tout)"/>.
    /// PaR, 1. June 2017 @ C0 | VS2017.
    /// </summary>
    /// <typeparam name="Tin">Type of input.</typeparam>
    /// <typeparam name="Tout">Type of output.</typeparam>
    abstract public class Challenge<Tin, Tout>
    {
        /// <summary>
        /// Meta-Data: Name of the Challenge.
        /// </summary>
        public abstract string Name { get; }
        /// <summary>
        /// Meta-Data: URL of the Challenge.
        /// </summary>
        public abstract string Url { get; }
        /// <summary>
        /// Meta-Data: Description text for the challenge with example result and stuff.
        /// </summary>
        public abstract string Description { get; }
        /// <summary>
        /// Meta-Data: Codename of the challenge.
        /// </summary>
        public abstract string Code { get; }
        /// <summary>
        /// Meta-Data: Own notes for the implementation.
        /// </summary>
        public abstract string Notes { get; }
        /// <summary>
        /// Meta-Data: When was the result (last) submitted.
        /// </summary>
        public abstract string Submitted { get; }
        /// <summary>
        /// Meta-Data: What personal rating do you give the challenge?.
        /// </summary>
        public abstract string Rating { get; }

        /// <summary>
        /// This is the Receiver (delegate) for any result of the <see cref="Do(Tin)"/>. 
        /// For example: Set to <see cref="Console.WriteLine"/> if <see cref="Tout"/> is string!
        /// </summary>
        public Action<Tout> ResultReceiver { get; set; }

        /// <summary>
        /// Only constructor.
        /// Forces subclasses to implement a constructor setting <see cref="ResultReceiver"/>.
        /// </summary>
        /// <param name="resultReceiver">The wanted receiver of any <see cref="Output(bool, Tout)"/>.</param>
        public Challenge(Action<Tout> resultReceiver)
        {
            this.ResultReceiver = resultReceiver;
        }

        /// <summary>
        /// This is the INPUT for the Challenge.
        /// Usually from the definition/description.
        /// Return type depends on <see cref="Tin"/> and <see cref="Tout"/>.
        /// Must be <see cref="IEnumerable"/> of Type <see cref="ChallengeInputOutput{Tinput, Toutput}"/>
        /// Each case will then be called by the subclasses <see cref="Do(Tin)"/>
        /// This method MUST be IMPLEMENTED in the subclass.
        /// </summary>
        /// <returns></returns>
        public abstract IEnumerable<ChallengeInputOutput<Tin,Tout>> Input();

        /// <summary>
        /// This is the MAIN method for the Challenge.
        /// Here each test should process the input (<see cref="Tin"/>).
        /// All result will be forwarded to <see cref="Output(bool, Tout)"/>.
        /// Input can come from extern (call) and from intern (class, <see cref="Input"/>)
        /// This method MUST be IMPLEMENTED in the subclass.
        /// </summary>
        /// <param name="input"><see cref="Object"/> or NULL; Can be IEnumerable.</param>
        /// <returns><see cref="Tout"/> result. Since <see cref="Do(Tin)"/> is called by <see cref="Run(Tin[])"/> will be forwarded to the <see cref="ResultReceiver"/></returns>
        protected abstract Tout Do(Tin input);

        protected virtual bool ValidateResult(Tout expected, Tout actual)
        {
            return expected.Equals(actual);
        }

        /// <summary>
        /// First gets the <see cref="Input"/> and processes it via <see cref="Do(Tin)"/> for each input.
        /// Then processes the given argument "inputs" via <see cref="Do(Tin)"/>.
        /// For the result see <see cref="ResultReceiver"/> and <see cref="Output(bool, Tout))"/>.
        /// </summary>
        /// <param name="inputs"></param>
        public virtual void Run(ChallengeInputOutputCollection<Tin,Tout> inputs)
        {
            // First: Class-Defined input via Input().
            Tout result;

            IEnumerable<ChallengeInputOutput<Tin, Tout>> classInputs = Input();
            if (classInputs != null)
            {
                foreach (ChallengeInputOutput<Tin, Tout> classInput in classInputs)
                {
                    result = Do(classInput.Input);
                    Output(ValidateResult(result, classInput.Output), result);
                }
            }

            // Next: Call-Defined input via Run(input...).
            if (inputs != null)
            {
                foreach (ChallengeInputOutput<Tin, Tout> input in inputs.AsEnumerable())
                {
                    result = Do(input.Input);
                    Output(ValidateResult(result, input.Output), result);
                }
            }
        }

        /// <summary>
        /// Used by the subclass to give result information.
        /// All given output will be sent to the <see cref="ResultReceiver"/>.
        /// </summary>
        /// <param name="outputs">What will be outputted? Will be sent to the <see cref="ResultReceiver"/>.</param>
        protected virtual void Output(bool validateResult, Tout result)
        {
                ResultReceiver.Invoke(result);
        }
    }
}
