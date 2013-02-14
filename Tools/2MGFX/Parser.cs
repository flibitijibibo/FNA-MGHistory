// Generated by TinyPG v1.3 available at www.codeproject.com

using System;
using System.Collections.Generic;

namespace TwoMGFX
{
    #region Parser

    public partial class Parser 
    {
        private Scanner scanner;
        private ParseTree tree;
        
        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
        }

        public ParseTree Parse(string input, string fileName)
        {
            tree = new ParseTree();
            return Parse(input, fileName, tree);
        }

        public ParseTree Parse(string input, string fileName, ParseTree tree)
        {
            scanner.Init(input, fileName);

            this.tree = tree;
            ParseStart(tree);
            tree.Skipped = scanner.Skipped;

            return tree;
        }

        private void ParseStart(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Start), "Start");
            parent.Nodes.Add(node);


            
            tok = scanner.LookAhead(TokenType.Code, TokenType.Technique, TokenType.Sampler);
            while (tok.Type == TokenType.Code
                || tok.Type == TokenType.Technique
                || tok.Type == TokenType.Sampler)
            {
                tok = scanner.LookAhead(TokenType.Code, TokenType.Technique, TokenType.Sampler);
                switch (tok.Type)
                {
                    case TokenType.Code:
                        tok = scanner.Scan(TokenType.Code);
                        n = node.CreateNode(tok, tok.ToString() );
                        node.Token.UpdateRange(tok);
                        node.Nodes.Add(n);
                        if (tok.Type != TokenType.Code) {
                            tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Code.ToString(), 0x1001, tok));
                            return;
                        }
                        break;
                    case TokenType.Technique:
                        ParseTechnique_Declaration(node);
                        break;
                    case TokenType.Sampler:
                        ParseSampler_Declaration(node);
                        break;
                    default:
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, tok));
                        break;
                }
            tok = scanner.LookAhead(TokenType.Code, TokenType.Technique, TokenType.Sampler);
            }

            
            tok = scanner.Scan(TokenType.EndOfFile);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.EndOfFile) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.EndOfFile.ToString(), 0x1001, tok));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseTechnique_Declaration(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Technique_Declaration), "Technique_Declaration");
            parent.Nodes.Add(node);


            
            tok = scanner.Scan(TokenType.Technique);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Technique) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Technique.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.LookAhead(TokenType.Identifier);
            if (tok.Type == TokenType.Identifier)
            {
                tok = scanner.Scan(TokenType.Identifier);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.Identifier) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                    return;
                }
            }

            
            tok = scanner.Scan(TokenType.OpenBracket);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.OpenBracket) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenBracket.ToString(), 0x1001, tok));
                return;
            }

            
            do {
                ParsePass_Declaration(node);
                tok = scanner.LookAhead(TokenType.Pass);
            } while (tok.Type == TokenType.Pass);

            
            tok = scanner.Scan(TokenType.CloseBracket);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.CloseBracket) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseBracket.ToString(), 0x1001, tok));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseRender_State_Expression(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Render_State_Expression), "Render_State_Expression");
            parent.Nodes.Add(node);


            
            tok = scanner.Scan(TokenType.Identifier);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Identifier) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Equals);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Equals) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.LookAhead(TokenType.Identifier, TokenType.Number);
            switch (tok.Type)
            {
                case TokenType.Identifier:
                    tok = scanner.Scan(TokenType.Identifier);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.Identifier) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                        return;
                    }
                    break;
                case TokenType.Number:
                    tok = scanner.Scan(TokenType.Number);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.Number) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number.ToString(), 0x1001, tok));
                        return;
                    }
                    break;
                default:
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, tok));
                    break;
            }

            
            tok = scanner.Scan(TokenType.Semicolon);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Semicolon) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon.ToString(), 0x1001, tok));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParsePass_Declaration(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Pass_Declaration), "Pass_Declaration");
            parent.Nodes.Add(node);


            
            tok = scanner.Scan(TokenType.Pass);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Pass) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Pass.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.LookAhead(TokenType.Identifier);
            if (tok.Type == TokenType.Identifier)
            {
                tok = scanner.Scan(TokenType.Identifier);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.Identifier) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                    return;
                }
            }

            
            tok = scanner.Scan(TokenType.OpenBracket);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.OpenBracket) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenBracket.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.LookAhead(TokenType.VertexShader, TokenType.PixelShader, TokenType.Identifier);
            while (tok.Type == TokenType.VertexShader
                || tok.Type == TokenType.PixelShader
                || tok.Type == TokenType.Identifier)
            {
                tok = scanner.LookAhead(TokenType.VertexShader, TokenType.PixelShader, TokenType.Identifier);
                switch (tok.Type)
                {
                    case TokenType.VertexShader:
                        ParseVertexShader_Pass_Expression(node);
                        break;
                    case TokenType.PixelShader:
                        ParsePixelShader_Pass_Expression(node);
                        break;
                    case TokenType.Identifier:
                        ParseRender_State_Expression(node);
                        break;
                    default:
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, tok));
                        break;
                }
            tok = scanner.LookAhead(TokenType.VertexShader, TokenType.PixelShader, TokenType.Identifier);
            }

            
            tok = scanner.Scan(TokenType.CloseBracket);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.CloseBracket) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseBracket.ToString(), 0x1001, tok));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseVertexShader_Pass_Expression(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.VertexShader_Pass_Expression), "VertexShader_Pass_Expression");
            parent.Nodes.Add(node);


            
            tok = scanner.Scan(TokenType.VertexShader);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.VertexShader) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.VertexShader.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Equals);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Equals) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Compile);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Compile) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Compile.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.ShaderModel);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.ShaderModel) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ShaderModel.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Identifier);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Identifier) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.OpenParenthesis);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.OpenParenthesis) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenParenthesis.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.CloseParenthesis);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.CloseParenthesis) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseParenthesis.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Semicolon);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Semicolon) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon.ToString(), 0x1001, tok));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParsePixelShader_Pass_Expression(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.PixelShader_Pass_Expression), "PixelShader_Pass_Expression");
            parent.Nodes.Add(node);


            
            tok = scanner.Scan(TokenType.PixelShader);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.PixelShader) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.PixelShader.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Equals);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Equals) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Compile);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Compile) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Compile.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.ShaderModel);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.ShaderModel) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ShaderModel.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Identifier);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Identifier) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.OpenParenthesis);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.OpenParenthesis) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenParenthesis.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.CloseParenthesis);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.CloseParenthesis) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseParenthesis.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Semicolon);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Semicolon) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon.ToString(), 0x1001, tok));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseSampler_State_Expression(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_State_Expression), "Sampler_State_Expression");
            parent.Nodes.Add(node);


            
            tok = scanner.Scan(TokenType.Identifier);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Identifier) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Equals);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Equals) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.LookAhead(TokenType.LessThan, TokenType.Identifier, TokenType.Number);
            switch (tok.Type)
            {
                case TokenType.LessThan:

                    
                    tok = scanner.Scan(TokenType.LessThan);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.LessThan) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.LessThan.ToString(), 0x1001, tok));
                        return;
                    }

                    
                    tok = scanner.Scan(TokenType.Identifier);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.Identifier) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                        return;
                    }

                    
                    tok = scanner.Scan(TokenType.GreaterThan);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.GreaterThan) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.GreaterThan.ToString(), 0x1001, tok));
                        return;
                    }
                    break;
                case TokenType.Identifier:
                    tok = scanner.Scan(TokenType.Identifier);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.Identifier) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                        return;
                    }
                    break;
                case TokenType.Number:
                    tok = scanner.Scan(TokenType.Number);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.Number) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number.ToString(), 0x1001, tok));
                        return;
                    }
                    break;
                default:
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, tok));
                    break;
            }

            
            tok = scanner.Scan(TokenType.Semicolon);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Semicolon) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon.ToString(), 0x1001, tok));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseSampler_Register_Expression(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_Register_Expression), "Sampler_Register_Expression");
            parent.Nodes.Add(node);


            
            tok = scanner.Scan(TokenType.Colon);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Colon) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Colon.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Register);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Register) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Register.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.OpenParenthesis);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.OpenParenthesis) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenParenthesis.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Identifier);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Identifier) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.LookAhead(TokenType.Comma);
            if (tok.Type == TokenType.Comma)
            {

                
                tok = scanner.Scan(TokenType.Comma);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.Comma) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Comma.ToString(), 0x1001, tok));
                    return;
                }

                
                tok = scanner.Scan(TokenType.Identifier);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.Identifier) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                    return;
                }

                
                tok = scanner.LookAhead(TokenType.OpenSquareBracket);
                if (tok.Type == TokenType.OpenSquareBracket)
                {

                    
                    tok = scanner.Scan(TokenType.OpenSquareBracket);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.OpenSquareBracket) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenSquareBracket.ToString(), 0x1001, tok));
                        return;
                    }

                    
                    tok = scanner.Scan(TokenType.Number);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.Number) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Number.ToString(), 0x1001, tok));
                        return;
                    }

                    
                    tok = scanner.Scan(TokenType.CloseSquareBracket);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.CloseSquareBracket) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseSquareBracket.ToString(), 0x1001, tok));
                        return;
                    }
                }
            }

            
            tok = scanner.Scan(TokenType.CloseParenthesis);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.CloseParenthesis) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseParenthesis.ToString(), 0x1001, tok));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseSampler_Declaration(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Sampler_Declaration), "Sampler_Declaration");
            parent.Nodes.Add(node);


            
            tok = scanner.Scan(TokenType.Sampler);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Sampler) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Sampler.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.Scan(TokenType.Identifier);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.Identifier) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Identifier.ToString(), 0x1001, tok));
                return;
            }

            
            tok = scanner.LookAhead(TokenType.Colon);
            while (tok.Type == TokenType.Colon)
            {
                ParseSampler_Register_Expression(node);
            tok = scanner.LookAhead(TokenType.Colon);
            }

            
            tok = scanner.LookAhead(TokenType.Equals);
            if (tok.Type == TokenType.Equals)
            {

                
                tok = scanner.Scan(TokenType.Equals);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.Equals) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Equals.ToString(), 0x1001, tok));
                    return;
                }

                
                tok = scanner.Scan(TokenType.SamplerState);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.SamplerState) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.SamplerState.ToString(), 0x1001, tok));
                    return;
                }

                
                tok = scanner.Scan(TokenType.OpenBracket);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.OpenBracket) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.OpenBracket.ToString(), 0x1001, tok));
                    return;
                }

                
                tok = scanner.LookAhead(TokenType.Identifier);
                while (tok.Type == TokenType.Identifier)
                {
                    ParseSampler_State_Expression(node);
                tok = scanner.LookAhead(TokenType.Identifier);
                }

                
                tok = scanner.Scan(TokenType.CloseBracket);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.CloseBracket) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.CloseBracket.ToString(), 0x1001, tok));
                    return;
                }

                
                tok = scanner.Scan(TokenType.Semicolon);
                n = node.CreateNode(tok, tok.ToString() );
                node.Token.UpdateRange(tok);
                node.Nodes.Add(n);
                if (tok.Type != TokenType.Semicolon) {
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.Semicolon.ToString(), 0x1001, tok));
                    return;
                }
            }

            parent.Token.UpdateRange(node.Token);
        }


    }

    #endregion Parser
}
