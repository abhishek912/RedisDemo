﻿/* A Java program to evaluate a
given expression where tokens
are separated by space.
*/
import java.util.Stack;

public class EvaluateString
{
	public static int evaluate(String expression)
	{
		char[] tokens = expression.toCharArray();

		Stack<Integer> values = new Stack<Integer>();

		Stack<Character> ops = new Stack<Character>();

		for (int i = 0; i < tokens.length; i++)
		{
			if (tokens[i] == ' ')
				continue;

			if (tokens[i] >= '0' && tokens[i] <= '9')
			{
				StringBuffer sbuf = new StringBuffer();

				while (i < tokens.length && tokens[i] >= '0' && tokens[i] <= '9')
                {
					sbuf.append(tokens[i++]);
				}
				values.push(Integer.parseInt(sbuf.toString()));
				i--;
			} 
			else if (tokens[i] == '(')
				ops.push(tokens[i]); 
			else if (tokens[i] == ')')
			{
				while (ops.peek() != '(')
                {
					values.push(applyOp(ops.pop(), values.pop(), values.pop()));
				}
				ops.pop();
			}
			else if (tokens[i] == '+' || tokens[i] == '-' || tokens[i] == '*' || tokens[i] == '/')
			{ 
				while (!ops.empty() && hasPrecedence(tokens[i], ops.peek()))
                {
					values.push(applyOp(ops.pop(), values.pop(), values.pop()));
				}
				ops.push(tokens[i]);
			}
		} 

		while (!ops.empty())
        {
			values.push(applyOp(ops.pop(), values.pop(), values.pop()));
		}

		return values.pop();
	}
	public static boolean hasPrecedence(char op1, char op2)
	{
		if (op2 == '(' || op2 == ')')
			return false;
		if ((op1 == '*' || op1 == '/') &&
			(op2 == '+' || op2 == '-'))
			return false;
		else
			return true;
	} 
	public static int applyOp(char op, int b, int a)
	{
		switch (op)
		{
			case '+':
				return a + b;
			case '-':
				return a - b;
			case '*':
				return a * b;
			case '/':
				return a / b;
		}
		return 0;
	}

	// Driver method to test above methods
	public static void main(String[] args)
	{ 
		System.out.println(EvaluateString.evaluate("8+(7-9)*2"));
	}
}