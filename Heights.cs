using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicrosoftResearch.Infer.Models;
using MicrosoftResearch.Infer;

namespace Heights
{
	public class Program
	{
		static void Main(string[] args)
		{
			// The probabilistic program
			// -------------------------		
			Variable<double> heightMan = Variable.GaussianFromMeanAndVariance(177,8*8);
			Variable<double> heightWoman = Variable.GaussianFromMeanAndVariance(164, 8*8);

            //Variable<bool> isTaller = Variable.Bernoulli(0.1253);
            Variable<bool> isTaller = heightWoman > heightMan;

            isTaller.ObservedValue = true;                              // uncomment this to infer P(isTaller)

            // The inference
            InferenceEngine engine = new InferenceEngine();
			engine.ShowProgress = false;
            Console.WriteLine("P(isTaller) = {0}", engine.Infer(isTaller));
            Console.WriteLine("P(man's height|isTaller) = {0}", engine.Infer(heightMan));
			Console.WriteLine("P(woman's height|isTaller) = {0}", engine.Infer(heightWoman));

			Console.WriteLine("Press any key...");
			Console.ReadKey();

			// Answer key
			// ----------
			// P(isTaller) = Bernoulli(0.1253)
			// P(man's height|isTaller) = Gaussian(167.7, 37.84)
			// P(woman's height|isTaller) = Gaussian(173.3, 37.84)
		}
	}
}
