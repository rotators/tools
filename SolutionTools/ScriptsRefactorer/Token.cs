using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScriptsRefactorer
{
        public class Token
        {
            public enum Typeenum { Begin, Identifier, Symbol, TemplateClosure, TextOrCommentBlock, End };
            public Typeenum Type;
            public String Value;
            public String Whitespaces;
            public Token RealNextTok;
            public Token RealPrevTok;
            public int ScopeLevel;

            public Token()
            {
                Type = Token.Typeenum.Begin;
                Value = "";
                Whitespaces = "";
                RealNextTok = null;
                RealPrevTok = null;
                ScopeLevel = 0;
            }

            public Token Next() { Token tok = this.RealNext(); while (tok != null && tok.IsUnimportant()) tok = tok.RealNext(); return tok; }
            public Token Prev() { Token tok = this.RealPrev(); while (tok != null && tok.IsUnimportant()) tok = tok.RealPrev(); return tok; }
            public Token RealNext() { return RealNextTok; }
            public Token RealPrev() { return RealPrevTok; }
            public static Token operator ++(Token tok) { return tok.RealNext(); }
            public static Token operator --(Token tok) { return tok.RealPrev(); }
            public bool IsBegin() { return Type == Typeenum.Begin; }
            public bool IsEnd() { return Type == Typeenum.End; }
            public bool IsEdge() { return IsBegin() || IsEnd(); }
            public bool IsGlobal() { return ScopeLevel==0; }

            public bool MakesNewLine() { return Whitespaces.Contains('\n'); }

            public bool IsSymbol() { return Type == Typeenum.Symbol; }
            public bool IsIdentifier() { return Type == Typeenum.Identifier; }
            public void SetUnimportant() { Type = Typeenum.TextOrCommentBlock; }
            public bool IsUnimportant() { return Type == Typeenum.TextOrCommentBlock; }

            public bool IsLeft() { return IsSymbol() && Value == "["; }
            public bool IsRight() { return IsSymbol() && Value == "]"; }
            public bool IsLeftBracket() { return IsSymbol() && Value == "{"; }
            public bool IsRightBracket() { return IsSymbol() && Value == "}"; }
            public bool IsLeftPar() { return IsSymbol() && Value == "("; }
            public bool IsRightPar() { return IsSymbol() && Value == ")"; }
            public bool IsSemi() { return IsSymbol() && Value == ";"; }
            public bool IsAssign() { return IsSymbol() && Value == "="; }
            public bool IsControl() { return IsSymbol() && Value == "\\"; }
            public bool IsComma() { return IsSymbol() && Value == ","; }
            public bool IsHandle() { return IsSymbol() && Value == "@"; }
            public bool IsRef() { return IsSymbol() && Value == "&"; }
            public bool IsGarbage() { return IsSymbol() && Value == "+"; }
            public bool IsTypeMod() { return IsIdentifier() && (Value == "in" || Value == "out" || Value == "inout"); }
            public bool IsTypeSpec() { return IsHandle() || IsRef() || IsTypeMod() || IsGarbage(); }
            public bool IsImport() { return IsIdentifier() && Value == "import"; }
            public bool IsConst() { return IsIdentifier() && Value == "const"; }
            public bool IsTemplateBegin() { return IsSymbol() && Value == "<"; }
            public bool IsTemplateEnd() { return (IsSymbol() || Type==Typeenum.TemplateClosure) && Value == ">"; }
            public bool IsQuote() { return IsSymbol() && Value == "\""; }
            public bool IsCharDelimiter() { return IsSymbol() && Value == "'" && (RealPrev().IsBegin() || !RealPrev().IsControl()); }
            public bool IsNot() { return IsIdentifier() && Value == "not"; }

            public bool IsArrayStart()
            {
                if(!(IsLeft() && Next() != null && Next().IsRight())) return false;
                for(Token n = RealNext();!n.IsRight();n=n.RealNext())
                {
                    if (n.IsLineCommentStart()) continue;
                    if (n.IsBlockCommentStart()) continue;
                    if (n.IsBlockCommentEnd()) continue;
                    return false;
                }
                return true;
            }
            public bool IsArrayEnd() { return IsRight() && Prev() != null && Prev().IsArrayStart(); }
            public bool IsEnum() { return IsIdentifier() && Value == "enum"; }
            public bool IsClass() { return IsIdentifier() && Value == "class"; }
            public bool IsInterface() { return IsIdentifier() && Value == "interface"; }

            public bool IsLineCommentStart() { return IsSymbol() && Value == "/" && !MakesNewLine() && RealNext() != null && RealNext().IsSymbol() && RealNext().Value == "/"; }
            public bool IsBlockCommentStart() { return IsSymbol() && Value == "/" && !MakesNewLine()  && RealNext() != null && RealNext().IsSymbol() && RealNext().Value == "*"; }
            public bool IsBlockCommentEnd() { return IsSymbol() && Value == "*" && !MakesNewLine() && RealNext() != null && RealNext().IsSymbol() && RealNext().Value == "/"; }
            public bool IsDirectiveStart() { return IsSymbol() && Value == "#" && !MakesNewLine() && (RealPrev().IsBegin() || RealPrev().MakesNewLine()) && !RealNext().IsEnd() && RealNext().IsIdentifier(); }

            public Token PreCompleteClass()
            {
                Token tok = this.Next();
                while (!tok.IsEnd() && !tok.IsLeftBracket()) tok = tok.Next();
                if (tok.IsEnd())
                {
                    Program.Log("Bad class definition?\n");
                    return tok;
                }
                int depth = 1;
                while (depth > 0)
                {
                    tok = tok.Next();
                    if (tok.IsEnd())
                    {
                        Program.Log("Failure on class closure: " + this.Next().Value);
                        return tok;
                    }
                    if (tok.IsLeftBracket()) depth++;
                    else if (tok.IsRightBracket()) depth--;
                }
                return tok;
            }

            public Token RealCompleteText()
            {
                Token tok = this.RealNext();
                while(!tok.IsEnd() && !tok.IsQuote())
                {
                    if(tok.IsControl() && tok.Whitespaces.Length == 0)
                        tok++;
                    else if(tok.IsQuote() && tok.Whitespaces.Length == 0 && tok.RealNext().IsQuote())
                        tok++;
                    tok++;
                }

                if (tok.IsEnd())
                {
                    Program.Log("Text block not finished?");
                    return tok;
                }
                Tokenizer.SetUnimportantRange(this, tok);
                return tok.RealNext();
            }

            public Token RealCompleteChar()
            {
                //Console.WriteLine("starting at " + this.Value + "," + this.RealNext().Value);
                Token tok = this.RealNext();
                while (!tok.IsEnd() && !tok.IsCharDelimiter())
                {
                    //Console.Write(tok.Value + "|");
                    tok++;

                }
                if (tok.IsEnd())
                {
                    Program.Log("Char block not finished?");
                    Console.ReadKey();
                    return tok;
                }
                Tokenizer.SetUnimportantRange(this, tok);
                return tok.RealNext();
            }

            public Token RealCompleteBlockComment()
            {
                //Console.WriteLine("Starting block comment from "+this.Value);
                Token tok = this.RealNext();
                while (!tok.IsEnd() && !tok.IsBlockCommentEnd()) tok = tok.RealNext();
                if (tok.IsEnd())
                {
                    Console.Write("Block comment not finished?\n");
                    return tok;
                }
                Tokenizer.SetUnimportantRange(this, tok.RealNext());
                //Console.WriteLine("Ending block comment at " + tok.Value+tok.RealNext().Value+tok.RealNext().RealNext().Value);
                return tok.RealNext().RealNext();
            }

            public Token RealCompleteLineComment()
            {
                Token tok = this.RealNext();
                while (!tok.IsEnd() && !tok.MakesNewLine()) tok = tok.RealNext();
                if (tok.IsEnd())
                {
                    Program.Log("Line comment without newline.");
                    return tok;
                }
                Tokenizer.SetUnimportantRange(this, tok);
                return tok.RealNext();
            }

            public Token RealCompleteDirective()
            {
                Token tok = this;
                bool no_control = true;
                while (!tok.IsEnd() && !tok.MakesNewLine() && no_control)
                {
                    if (tok.IsQuote())
                    {
                        tok = tok.RealCompleteText();
                        if (tok.RealPrev().MakesNewLine())
                        {
                            tok = tok.RealPrev();
                            break;
                        }
                        continue;
                    }
                    if (tok.IsControl()) no_control = false;
                    if (tok.MakesNewLine()) no_control = true;
                    tok = tok.RealNext();
                }

                Tokenizer.SetUnimportantRange(this, tok);
                return tok.RealNext();
            }

            public Token CompleteType(Type type)
            {
                Token tok = this;
                bool save = type != null;
                if (tok.IsConst())
                {
                    if(save) type.IsConst = true;
                    tok = tok.Next();
                }
                if (save) type.Name = tok.Value;
                tok = tok.Next();
                if (tok.IsTemplateBegin())
                {
                    if (save)
                    {
                        type.IsTemplate = true;
                        type.SubType = new Type();
                    }
                    tok = tok.Next();
                    tok=tok.CompleteType(save ? type.SubType : null);
                    if(!tok.IsTemplateEnd())
                    {
                        Program.Log("Not closed template?");
                    }
                    tok = tok.Next();
                }
                //Console.WriteLine("endtok: " + tok.Value);
                while (tok.IsTypeSpec())
                {
                    //Console.WriteLine("mod: "+tok.Value);
                    if (save) type.ModName += tok.Value;
                    tok = tok.Next();
                }
                return tok;
            }

            public void InsertAfter(Token tok)
            {
                tok.RealNextTok = this.RealNext();
                if (!this.RealNext().IsEnd()) this.RealNext().RealPrevTok = tok;
                tok.RealPrevTok = this;
                this.RealNextTok = tok;
            }

            public void InsertBefore(Token tok)
            {
                tok.RealPrevTok = this.RealPrev();
                if (!this.RealPrev().IsBegin()) this.RealPrev().RealNextTok = tok;
                tok.RealNextTok = this;
                this.RealPrevTok = tok;
            }

            public void RemoveSelf()
            {
                if (this.IsEdge())
                {
                    ;// Console.WriteLine("Should not happen.");
                }
                this.RealPrev().RealNextTok = this.RealNext();
                this.RealNext().RealPrevTok = this.RealPrev();
            }

        }

}
