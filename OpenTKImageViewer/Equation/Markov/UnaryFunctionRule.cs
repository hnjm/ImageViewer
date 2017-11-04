﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTKImageViewer.Equation.Token;

namespace OpenTKImageViewer.Equation.Markov
{
    // function with exactly one argument
    public class UnaryFunctionRule : MarkovRule
    {
        public UnaryFunctionRule()
        {
            Tokens = new List<Token.Token.Type>
            {
                Token.Token.Type.Function,
                Token.Token.Type.Value,
                Token.Token.Type.BracketClose
            };
        }

        public override List<Token.Token> Apply(List<Token.Token> match)
        {
            var function = (FunctionToken)match[0];
            var value = (ValueToken)match[1];
            return new List<Token.Token>{new UnaryFunctionToken(function.FuncName, value)};
        }
    }
}