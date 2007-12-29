﻿using System.Collections.Generic;
using System.Linq;
using System.Text;

using ScriptCoreLib.JavaScript.DOM.HTML;
using ScriptCoreLib.JavaScript.Runtime;
using ScriptCoreLib.JavaScript;
using ScriptCoreLib.JavaScript.Extensions;
using ScriptCoreLib;
using ScriptCoreLib.Shared;

namespace TextRotator.js
{
    [Script, ScriptApplicationEntryPoint]
    public class TextRotator2
    {

        public TextRotator2()
        {
            TextRotator1.CreateStyle();
            TextRotator1.GenerateView(this.Text.Trim().Split('\n'));
        }

        // http://sysprog.net/quotlang.html

        public string Text = @"
It should be noted that no ethically-trained software engineer would ever consent to write a DestroyBaghdad procedure. Basic professional ethics would instead require him to write a DestroyCity procedure, to which Baghdad could be given as a parameter. (Nathaniel S Borenstein)
There are only two kinds of programming languages: those people always bitch about and those nobody uses. (Bjarne Stroustrup)
Should array indices start at 0 or 1? My compromise of 0.5 was rejected without, I thought, proper consideration. (Stan Kelly-Bootle)
Voodoo Programming: Things programmers do that they know shouldn't work but they try anyway, and which sometimes actually work, such as recompiling everything. (Karl Lehenbauer)
Please don't fall into the trap of believing that I am terribly dogmatical about [the goto statement]. I have the uncomfortable feeling that others are making a religion out of it, as if the conceptual problems of programming could be solved by a single trick, by a simple form of coding discipline! (Edsger Dijkstra)
Computer language design is just like a stroll in the park. Jurassic Park, that is. (Larry Wall)
XML is not a language in the sense of a programming language any more than sketches on a napkin are a language. (Charles Simonyi)
Using TSO is like kicking a dead whale down the beach. (Stephen C Johnson)
The object-oriented model makes it easy to build up programs by accretion. What this often means, in practice, is that it provides a structured way to write spaghetti code. (Paul Graham)
Reusing pieces of code is liked picking off sentences from other people's stories and trying to make a magazine article. (Bob Frankston)
[The BLINK tag in HTML] was a joke, okay? If we thought it would actually be used, we wouldn't have written it! (Mark Andreessen)
Software is like sex: It's better when it's free. (Linus Torvalds)
I had a running compiler and nobody would touch it. They told me computers could only do arithmetic. (Rear Admiral Grace Hopper)
If you don't think carefully, you might think that programming is just typing statements in a programming language. (Ward Cunningham)
A language that doesn't have everything is actually easier to program in than some that do. (Dennis M Ritchie)
Projects promoting programming in natural language are intrinsically doomed to fail. (Edsger Dijkstra)
Pointers are like jumps, leading wildly from one part of the data structure to another. Their introduction into high-level languages has been a step backwards from which we may never recover. (Charles Hoare)
The string is a stark data structure and everywhere it is passed there is duplication. It is a perfect vehicle for hiding information. (Alan J Perlis)
First learn computer science and all the theory. Next develop a programming style. Then forget all that and just hack. (George Carrette)
I fear the the new object-oriented systems may suffer the fate of LISP, in that they can do many things, but the complexity of the class hierarchies may cause them to collapse under their own weight. (Bill Joy)
If we wish to count lines of code, we should not regard them as lines produced but as lines spent. (Edsger Dijkstra)
You can either have software quality or you can have pointer arithmetic, but you cannot have both at the same time. (Bertrand Meyer)
Syntax, my lad. It has been restored to the highest place in the republic. (John Steinbeck)
Are you quite sure that all those bells and whistles, all those wonderful facilities of your so called powerful programming languages, belong to the solution set rather than the problem set? (Edsger Dijkstra)
Thou shalt not follow the NULL pointer, for chaos and madness await thee at its end. (Henry Spencer)
I think conventional languages are for the birds. They're just extensions of the von Neumann computer, and they keep our noses in the dirt of dealing with individual words and computing addresses, and doing all kinds of silly things like that, things that we've picked up from programming for computers; we've built them into programming languages; we've built them into Fortran; we've built them in PL/1; we've built them into almost every language. (John Backus)
Get and set methods are evil. (Allen Holub)
Writing code has a place in the human hierarchy worth somewhere above grave robbing and beneath managing. (Gerald Weinberg)
Part of the reason so many companies continue to develop software using variations of waterfall is the misconception that the analysis phase of waterfall completes the design and the rest of the process is just non-creative execution of programming skills. (Steven Gordon)
If the programmer can simulate a construct faster than a compiler can implement the construct itself, then the compiler writer has blown it badly. (Guy Steele)
Classes struggle, some classes triumph, others are eliminated. (Mao Zedong)
If buffer overflows are ever controlled, it won't be due to mere crashes, but due to their making systems vulnerable to hackers. Software crashes due to mere incompetence apparently don't raise any eyebrows, because no one wants to fault the incompetent programmer and his incompetent boss. (Henry Baker)
There is not a fiercer hell than the failure in a great object. (John Keats)
Objects can be classified scientifically into three major categories: those that don't work, those that break down and those that get lost. (Russell Baker)
Memory is like an orgasm. It's a lot better if you don't have to fake it. (Seymour Cray)
";



        static TextRotator2()
        {
            typeof(TextRotator2).Spawn();

        }
    }
}
