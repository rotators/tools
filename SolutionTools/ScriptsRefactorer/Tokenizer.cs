using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ScriptsRefactorer
{
    class Tokenizer
    {
        private String filename;
        private String newline;
        private Token root;
        private LinkedList<String> lines;
        private enum enumstate { FoundBracket, Clear, FoundAssign, FoundIdentifier };

        public Tokenizer(String filename, String newline)
        {
            this.newline = newline;
            this.filename = filename;
        }

        public bool Parse()
        {
            root = new Token();
            lines = new LinkedList<String>();
            StreamReader file = new StreamReader(filename, System.Text.Encoding.Default);
            String line;
            while ((line = file.ReadLine()) != null) lines.AddLast(line);
            if(lines.Count == 0) return false;
            file.BaseStream.Seek(-1, SeekOrigin.End);
            int c = (char)file.BaseStream.ReadByte();
            if(c == '\n' || c == '\r') lines.AddLast("");
            file.Close();
            parseLines();
            process();
            return true;
        }

        private void parseLines()
        {
            parseWhitespaces(root);
            Token tok = root;
            while (lines.Count>0)
            {
                tok.RealNextTok = new Token();
                tok.RealNext().RealPrevTok = tok;
                tok = tok.RealNext();
                if (!parseSymbol(tok)) parseIdentifier(tok);
            }
            Token end = new Token();
            end.Type = Token.Typeenum.End;
            end.RealNextTok = end;
            tok.RealNextTok = end;
            end.RealPrevTok = tok;
        }

        private bool isIdentifierSymbol(Char ch)
        {
            return Char.IsLetterOrDigit(ch) || ch == '_';
        }

        private bool parseWhitespacesLine(Token tok)
        {
            int i = 0;
            while (i < lines.First.Value.Length)
            {
                if(!Char.IsWhiteSpace(lines.First.Value[i]))
                {
                    if (i == 0) return false;
                    tok.Whitespaces += lines.First.Value.Substring(0, i);
                    lines.First.Value = lines.First.Value.Remove(0, i);
                    return true;
                }
                i++;
            }
            tok.Whitespaces += lines.First.Value;
            if(lines.Count > 1)
                tok.Whitespaces += newline;
            lines.First.Value = lines.First.Value.Remove(0, i);
            return true;
        }

        private void parseWhitespaces(Token tok)
        {
            while (lines.Count > 0)
            {
                if (!parseWhitespacesLine(tok)) return;
                if(lines.First.Value.Length == 0) lines.RemoveFirst();
            }
        }

        private bool parseSymbol(Token tok)
        {
            if(!isIdentifierSymbol(lines.First.Value[0]))
            {
                tok.Type = Token.Typeenum.Symbol;
                tok.Value = lines.First.Value[0].ToString();
                lines.First.Value = lines.First.Value.Remove(0, 1);
                parseWhitespaces(tok);
                return true;
            }
            return false;
        }

        private bool parseIdentifier(Token tok)
        {
            int i = 0;
            while(i < lines.First.Value.Length)
            {
                if(!isIdentifierSymbol(lines.First.Value[i]))
                {
                    tok.Type = Token.Typeenum.Identifier;
                    tok.Value = lines.First.Value.Substring(0, i);
                    lines.First.Value = lines.First.Value.Remove(0, i);
                    parseWhitespaces(tok);
                    return true;
                }
                i++;
            }
            tok.Type = Token.Typeenum.Identifier;
            tok.Value = lines.First.Value;
            lines.First.Value = lines.First.Value.Remove(0, i);
            parseWhitespaces(tok);
            return true;
        }

        public static void SetUnimportantRange(Token from, Token to)
        {
            if(from.IsEdge()) return;
            for (Token tok = from; tok != to && !tok.IsEnd(); tok++)
                tok.SetUnimportant();

            if (!to.IsEdge()) to.SetUnimportant();
        }

        private void linkTokens(Token left, Token right)
        {
            left.RealNextTok = right;
            right.RealPrevTok = left;
        }

        private void process()
        {
            // real tokens, cut out comments and such
            Token prev = root;
            Token tok = root.RealNext();
            
            while (!tok.IsEnd())
            {
                if(tok.IsQuote()) tok = tok.RealCompleteText();
                else if (tok.IsCharDelimiter()) tok = tok.RealCompleteChar();
                else if (tok.IsBlockCommentStart()) tok = tok.RealCompleteBlockComment();
                else if (tok.IsLineCommentStart()) tok = tok.RealCompleteLineComment();
                else if (tok.IsDirectiveStart()) tok = tok.RealCompleteDirective();
                else
                {
                    prev = tok;
                    tok++;
                }
            }

            // add semicolons to class definitions
            tok = root;
            for (tok = root; !tok.IsEnd(); tok = tok.Next())
            {
                if (tok.IsEnum() || tok.IsClass() || tok.IsInterface())
                {
                    tok = tok.PreCompleteClass();
                    if (tok.IsEnd())
                    {
                        Token semi = new Token();
                        semi.Type = Token.Typeenum.Symbol;
                        semi.Value = ";";
                        Token tail = tok.Prev();
                        tail.InsertAfter(semi);
                        tail.Whitespaces = "";
                        tok = semi;
                    }
                    else if (tok.Next().IsEnd() || !tok.Next().IsSemi())
                    {
                        Token semi = new Token();
                        semi.Type = Token.Typeenum.Symbol;
                        semi.Value = ";";
                        semi.Whitespaces = tok.Whitespaces;
                        tok.Whitespaces = "";
                        tok.InsertAfter(semi);
                        tok = semi;
                    }
                }
            }

            // expand arrays
            enumstate state = enumstate.Clear;
            tok = root;
            while (!tok.Next().IsEnd()) tok = tok.Next();

            for (; !tok.IsBegin(); tok = tok.Prev())
            {
                if (tok.IsLeftBracket())
                {
                    state = enumstate.FoundBracket;
                    continue;
                }
                else if (tok.IsAssign())
                {
                    if (state == enumstate.FoundBracket) state = enumstate.FoundAssign;
                    else state = enumstate.Clear;
                    continue;
                }
                else if (tok.IsIdentifier())
                {
                    if (state == enumstate.FoundAssign) state = enumstate.FoundIdentifier;
                    else state = enumstate.Clear;
                    continue;
                }

                if (tok.IsArrayEnd())
                {
                    if (state == enumstate.FoundIdentifier)
                    {
                        // go back until identifier
                        while (!tok.IsEnd() && !tok.IsIdentifier()) tok = tok.Prev();
                        if (tok.IsEnd())
                        {
                            Program.Log("Assignment in beginning of the file?");
                            continue;
                        }
                        continue;
                    }
                    // make an array
                    tok.RemoveSelf();
                    tok = tok.Prev();
                    tok.RemoveSelf();
                    tok = tok.Prev();
                    Token ident = tok;
                    while (!ident.IsBegin() && !ident.IsIdentifier()) ident = ident.Prev();
                    if (ident.IsBegin())
                    {
                        Program.Log("Array without a type, am I parsing something inlined?");
                        continue;
                    }
                    replaceWithArray(ident, tok);
                    tok = tok.Next();
                }
                state = enumstate.Clear;
            }

            tok = root;
            for (tok = root; !tok.IsEnd(); tok = tok.Next())
            {
                if (tok.IsNot())
                {
                    tok.Value = "!";
                    tok.Whitespaces = "";
                }
            }

            if (Program.Fixargs) fixAllArguments();
        }

        private void fixAllArguments()
        {
            // find global scope identifiers
            Token tok;
            int scope_level = 0;
            for (tok = root.Next(); !tok.IsEnd(); tok = tok.Next())
            {
                if (tok.IsLeftBracket()) scope_level++;
                tok.ScopeLevel = scope_level;
                if (tok.IsRightBracket()) scope_level--;
            }

            // fix &
            tok = root.Next();
            String class_name = "";
            while (!tok.IsEnd())
            {
                if (tok.IsImport())
                {
                    while (!tok.IsEnd() && !tok.IsSemi()) tok = tok.Next();
                    if (!tok.IsEnd()) tok = tok.Next();
                    continue;
                }
                if (tok.IsGlobal())
                {
                    if (tok.IsClass())
                    {
                        tok = tok.Next();
                        if (tok.IsEnd()) continue;
                        class_name = tok.Value;
                        while (!tok.IsEnd() && !tok.IsLeftBracket()) tok = tok.Next();
                        continue;
                    }
                    class_name = "";
                    if (!tok.IsIdentifier())
                    {
                        tok = tok.Next();
                        continue;
                    }
                    //Type type = new Type();
                    tok = tok.CompleteType(null);
                    if (tok.IsEnd()) continue;
                    tok=tok.Next();
                    if (!tok.IsLeftPar())
                    {
                        while (!tok.IsEnd() && !tok.IsSemi()) tok = tok.Next();
                        if (!tok.IsEnd()) tok = tok.Next();
                        continue;
                    }
                    tok = tok.Next();
                    if (tok.IsEnd()) continue;
                    fixArguments(tok);
                }
                else
                {
                    if (tok.Value == class_name && tok.IsIdentifier() && tok.ScopeLevel == 1 && tok.Next().IsLeftPar())
                    {
                        fixArguments(tok.Next().Next());
                    }
                }
                tok = tok.Next();
            }
        }

        private void fixArguments(Token tok)
        {
            String funcname = tok.Prev().Prev().Value;
            List<String> nonbasic = new List<String>();
            while (!tok.IsRightPar())
            {
                Type type = new Type();
                tok = tok.CompleteType(type);
                String ident = tok.IsIdentifier() ? tok.Value : "anonymous";

                if (tok.IsIdentifier() && !type.IsBasic() && type.ModName=="") nonbasic.Add(ident);
                if (tok.IsIdentifier()) tok = tok.Next();
                if (tok.IsEnd() || tok.IsRightPar()) break;
               
                tok = tok.Next();
                if (tok.IsEnd()) break;
            }

            tok = tok.Next();
            if (!tok.IsLeftBracket())
                return;

            tok=tok.Next();
            if (tok.IsEnd()) return;
            int level = tok.ScopeLevel;
            Token next;
            Token prev;
            while (nonbasic.Count>0 && !tok.IsRightBracket() && !tok.IsEnd() && tok.ScopeLevel == level)
            {
                next=tok.Next();
                prev = tok.Prev();
                if (tok.IsIdentifier() && !next.IsEnd() && prev.IsSemi() && next.IsAssign() && nonbasic.Contains(tok.Value))
                {
                    nonbasic.Remove(tok.Value);
                }
                tok = tok.Next();
            }

            if (nonbasic.Count > 0)
            {
                Program.LogNoLine(funcname + ": nonbasic value-args without assignment found: ");
                bool comma = false;
                foreach (String s in nonbasic)
                {
                    if (comma) Program.LogNoLine(", ",false); else comma = true;
                    Program.LogNoLine(s,false);
                }
                Program.Log(".",false);
            }
        }

        private void replaceWithArray(Token from, Token to)
        {
            Token arr = new Token();
            arr.Type = Token.Typeenum.Identifier;
            arr.Value = "array";
            Token left = new Token();
            left.Type = Token.Typeenum.Symbol;
            left.Value = "<";
            Token right = new Token();
            right.Type = Token.Typeenum.TemplateClosure;
            right.Value = ">";
            linkTokens(arr, left);
            linkTokens(from.RealPrev(), arr);
            linkTokens(left,from);
            linkTokens(right,to.RealNext());
            if (right.Next().IsIdentifier() || right.Next().Type == Token.Typeenum.TemplateClosure) right.Whitespaces = " ";
            linkTokens(to, right);
        }

        public bool Save(String filename)
        {
            StreamWriter file = new StreamWriter(filename, false, System.Text.Encoding.Default);
            file.NewLine = newline;

            for (Token tok = root; !tok.IsEnd(); tok++)
            {
                file.Write(tok.Value);
                file.Write(tok.Whitespaces);
            }
            file.Close();
            return true;
        }
    }
}
