using System.Collections.Generic;

namespace LeetCode.Study.DataStructure.DataStructureOne;

/// <summary>
/// https://leetcode.com/problems/valid-parentheses
/// </summary>
public class ValidParentheses
{
    public bool IsValid(string s)
    {
        var stack = new Stack<char>();

        for (int i = 0; i < s.Length; i++)
        {
            var curr = s[i];
            if (curr is '(' or '{' or '[')
            {
                stack.Push(curr);
            }
            else
            {
                if (stack.Count == 0)
                {
                    return false;
                }

                if (Closes(stack.Pop(), curr))
                {
                    continue;
                };

                return false;
            }
        }

        return stack.Count == 0;

        static bool Closes(char opening, char closing) => (opening, closing) switch
        {
            ('(', ')') => true,
            ('[', ']') => true,
            ('{', '}') => true,
            _ => false
        };
    }
}