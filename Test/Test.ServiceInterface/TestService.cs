using ServiceStack;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test.ServiceInterface.Interfaces;
using Test.ServiceModel;
using Test.ServiceModel.Enums;
using Test.ServiceModel.Models;

namespace Test.ServiceInterface
{
    public class TestService : Service
    {
        private readonly IRules _rules;

        public TestService(IRules rules)
        {
            _rules = rules;
        }

        public TestResponse Any(TestRequest request)
        {
            var builder = new StringBuilder();
            var summary = new Summary();

            foreach (var n in Enumerable.Range(request.Range[0], request.Range[1]))
            {
                if (_rules.IsAMultipleOf(n, 15))
                {
                    builder.Append($"{OutputValues.fizzbuzz} ");
                    summary.FizzBuzz++;
                }
                else if (_rules.IsAMultipleOf(n, 5))
                {
                    builder.Append($"{OutputValues.buzz} ");
                    summary.Buzz++;
                }
                else if (_rules.IsAMultipleOf(n, 3))
                {
                    builder.Append($"{OutputValues.fizz} ");
                    summary.Fizz++;
                }
                else
                {
                    builder.Append($"{n} ");
                    summary.Integer++;
                }
            }

            return new TestResponse { Result = builder.ToString().Trim(), Summary = summary };
        }
    }
}