﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Interpreter.Reserved;
namespace Interpreter {
    //registers all of the reserved functions and variables.
    class Registry {
        private SymbolTable root;
        public void Import(RDelegate dle, string name, params string[] parameters) {
            root.AddFunction(name, new SystemFunction(dle), parameters);
        }
        public Registry(SymbolTable root) { 
            //root is the root of the program symbol table tree
            this.root = root;
           
            //list of all the list to import automaticly.
            F[] toAdd = new F[]{
                new F(new RDelegate(Functions.Indexer_Fu) ,"~indexer","~index","~enums"),
                new F(new RDelegate(Functions.Be_Fu)      ,"Set","~value","~id"),
                new F(new RDelegate(Functions.Add_Fu)     ,"Add", "~a","~b"),
                new F(new RDelegate(Functions.Clear_Fu)   ,"Clear"),
                new F(new RDelegate(Functions.Pop_Fu)     ,"Pop"),
                new F(new RDelegate(Functions.Print_Fu)   ,"Print","~toPrint"),
                new F(new RDelegate(Functions.PrintL_Fu)  ,"PrintL","~toPrint"),
                new F(new RDelegate(Functions.ReadFile_Fu),"ReadFile","~path"),
                new F(new RDelegate(Functions.WriteFile_Fu),"WriteToFile","~path","~text"),
                new F(new RDelegate(Functions.ReadLine_Fu),"ReadL"),
                new F(new RDelegate(Functions.ListCast_Fu),"list","~items"),
                new F(new RDelegate(Functions.IntCast_Fu),"int","~toCast"),
                new F(new RDelegate(Functions.Def_Fu),"Def","~value","~id"),
                new F(new RDelegate(Functions.DefMethod_Fu),"function","~paramList","~id"),
            };
            foreach (F i in toAdd) {
                this.Import(i.dle, i.name, i.parameters);
            }
        }
        private struct F {
            public RDelegate dle;
            public string name;
            public string[] parameters;
            public F(RDelegate dele,string name, params string[] parameters){
                this.dle=dele;
                this.name=name;
                this.parameters=parameters;
            }
        }
    }
}