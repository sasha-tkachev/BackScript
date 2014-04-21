﻿using System;
using System.Collections.Generic;


namespace Interaptor {
    static class Tokenizer {

        public static LinkedList<Token> Tokenize (string input,int start, int end){
            LinkedList<Token> output=new LinkedList<Token>();
            string carry="";
            //end of stream carry is an emty carry.
            Token.Type carryType= Token.Type.EOS;

            while (start < end) {
                switch(input[start]){
                    //escape charecter
                    case '\\':
                        if (carryType != Token.Type.String)
                            throw new Exception("escape char outside of a string defention");
                        start++;
                        if (start >= end)
                            throw new Exception("end of string toosoon");

                        switch(input[start]){
                            case 'n':
                                carry += '\n';
                                break;
                            case 't':
                                carryType += '\t';
                                break;
                            case '\"':
                                carryType += '\"';
                                break;
                            case '\'':
                                carryType += '\'';
                                break;
                            default:
                                throw new Exception("unrecognized charecter after escape charecter");
                                

                        }
                        break;
                    case '\n':
                        //igonre
                        break;
                    
                    case ' ':
                        
                        if (carryType != Token.Type.String) {
                            if (carryType == Token.Type.FunctionCall && ((carry == "") || (carry == "<"))) {
                                //ignore uneeded spaces
                            }
                            else {
                                if (carryType != Token.Type.EOS) {

                                    output.AddLast(new Token(carry, carryType));
                                    carry = "";
                                    carryType = Token.Type.EOS;
                                }
                            }
                        }
                        else
                            carry += ' ';
                        break;
                    case '\"':
                        if (carryType == Token.Type.String) {
                            output.AddLast(new Token(carry, carryType));
                            carryType = Token.Type.EOS;
                        }
                        else if (carryType == Token.Type.EOS) {
                            carryType = Token.Type.String;
                        }
                        else
                            throw new Exception("invalid operator placement");
                        break;
                    case '.':
                        switch(carryType){
                            case Token.Type.String:
                                carry += '.';
                                break;
                            case Token.Type.Integer:
                                carryType=Token.Type.Double;
                                carry+='.';
                                break;
                            case Token.Type.IdHEad:
                                output.AddLast(new Token(carry, carryType));
                                carryType=Token.Type.IdTail;
                                carry="";
                                break;
                            case Token.Type.IdTail:
                                output.AddLast(new Token(carry, carryType));
                                carryType=Token.Type.IdTail;
                                carry="";
                                break;
                            default:
                                throw new Exception("invalid dot position");
                        }
                        break;
                    case '0':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='0';
                        break;
                    case '1':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='1';
                        break;
                    case '2':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='2';
                        break;
                    case '3':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='3';
                        break;
                    case '4':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='4';
                        break;
                    case '5':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='5';
                        break;
                    case '6':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='6';
                        break;
                    case '7':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='7';
                        break;
                    case '8':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='8';
                        break;
                    case '9':
                        if(carryType==Token.Type.EOS)
                            carryType=Token.Type.Integer;
                        carry+='9';
                        break;
                    
                    
                    case '<':
                        if (carryType != Token.Type.EOS) {
                            if (carryType == Token.Type.String)
                                carry += "<";
                            else
                                throw new Exception("invalid placement");
                        }
                        else {
                            carryType = Token.Type.FunctionCall;
                            carry = "<";
                        }
                        break;
                    case '-':
                        if(carryType==Token.Type.EOS)
                            output.AddLast(new Token("-", Token.Type.Operator));
                        else if(carryType == Token.Type.FunctionCall){
                            if(carry!="<")
                                throw new Exception("invalid placement");
                            carry="";
                        }
                        else
                            throw new Exception("invalid placement");
                        break;
                    case '{':
                        if (carryType == Token.Type.EOS)
                            output.AddLast(new Token("{", Token.Type.Operator));
                        else
                            throw new Exception("invalid placement");
                        break;
                    case '}':
                        if (carryType == Token.Type.EOS)
                            output.AddLast(new Token("}", Token.Type.Operator));
                        else
                            throw new Exception("invalid placement");
                        break;
                    case '+':
                        if(carryType==Token.Type.EOS)
                            output.AddLast(new Token("+", Token.Type.Operator));
                        else
                            throw new Exception("invalid placement");
                        break;
                    case '*':
                        if(carryType==Token.Type.EOS)
                            output.AddLast(new Token("*", Token.Type.Operator));
                        else
                            throw new Exception("invalid placement");
                        break;
                    case ';':
                        
                        if (carryType == Token.Type.String)
                            carry += ';';
                        else if (carryType != Token.Type.EOS) {
                            output.AddLast(new Token(carry, carryType));
                            carry = "";
                            carryType = Token.Type.EOS;
                        }
                        output.AddLast(new Token(";", Token.Type.Operator));
                        break;
                    

                    default:
                        if (carryType == Token.Type.EOS)
                            carryType = Token.Type.IdHEad;
                        if (carryType == Token.Type.FunctionCall && carry == "<") {
                            carry = "";
                            
                        }
                        carry+=input[start];
                        break;
                }
                start++;
            }
            if (carry != "") {
                output.AddLast(new Token(carry, carryType));
            }
            output.AddLast(new Token("$", Token.Type.EOS));
            return output;
        }
    }
}
